using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoesOnContainers.Web.WebMvc.Services;
using ShoesOnContainers.Web.WebMvc.ViewModels;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class CatalogController : Controller
    {
        private ICatalogService _catalogSvc;

        public CatalogController(ICatalogService catalogSvc)
        {
            _catalogSvc = catalogSvc;
        }

        public async Task<IActionResult> Index(int? brandFilterApplied, int? typesFilterApplied, int? page)
        {
            int itemsPage = 10;
            var catalog = await _catalogSvc.GetCatalogItems(page ?? 0, itemsPage, brandFilterApplied, typesFilterApplied);
            var vm = new CatalogIndexViewModel()
            {
                CatalogItems = catalog.Data,
                Brands = await _catalogSvc.GetBrands(),
                Types = await _catalogSvc.GetTypes(),
                BrandFilterApplied = brandFilterApplied ?? 0,
                TypesFilterApplied = typesFilterApplied ?? 0,
                PaginationInfo = new PaginationInfo()
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = itemsPage, //catalog.Data.Count,
                    TotalItems = catalog.Count,
                    TotalPages = (int)Math.Ceiling(((decimal)catalog.Count / itemsPage))
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return View(vm);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

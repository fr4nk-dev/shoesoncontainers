FROM microsoft/mssql-server-linux:latest
RUN apt-get update && apt-get -y install apt-transport-https curl
ENV PATH="/opt/mssql-tools/bin:${PATH}"
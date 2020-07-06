#!/bin/bash

sudo docker stop db_design_patterns_sql_dev
sudo docker rm db_design_patterns_sql_dev
sudo docker pull mcr.microsoft.com/mssql/server:2017-latest
sudo docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=somethingStr0ng#' \
   -p 1433:1433 --name db_design_patterns_sql_dev \
   -v /:/host-fs \
   -d mcr.microsoft.com/mssql/server:2017-latest

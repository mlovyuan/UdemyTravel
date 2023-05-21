### The project includes .NET7, code first EF, RESTful within HATEOAS, Docker
---

* set up mssql with docker by using
```
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Password@123" -p 1433:1433 --name sql -d mcr.microsoft.com/mssql/server:2022-latest
```

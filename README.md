### The project includes .NET7, model first EF, RESTful within HATEOAS, Docker
---

#### Entity Framework
* set up mssql with docker by using
```
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Password@123" -p 1433:1433 -v sqlvolume:/var/opt/mssql --name sql -d mcr.microsoft.com/mssql/server:2022-latest
```

Within model first, we need to clarify business model and create models first.
After that, adding `DbContext` and repository, using DI to enroll the class.
  
Meanwhile, we have to install three packages: 
 1. Microsoft.EntityFrameworkCore
 2. Microsoft.EntityFrameworkCore.SqlServer
 3. Microsoft.EntityFrameworkCore.Tools

The command we do db migration and update(create) to local mssql:
 1. `dotnet tool install --global dotnet-ef`, install dotnet ef command.
 2. `dotnet ef migrations add initialMigration`, will create sql query map to our models.
 3. `dotnet ef database update`, run sql query and create tables.
 
When we'd like to insert mock data into our db from json file, we have to override `OnModelCreating` method in `AppDbContext` class,
getting Assembly from bin file to load the data.

Add DataSeeding:
 1. `dotnet ef migrations add DataSedding`
 2. `dotnet ef database update`

If we'd like to update schema to some tables in the db, just modify the model class and use command as `migrations` and `update`.

#### AutoMapper
Install package: AutoMapper.Extensions.Microsoft.DependencyInjection

Setup DI, scan and load all profile file into AppDomain, then add into AutoMapper.


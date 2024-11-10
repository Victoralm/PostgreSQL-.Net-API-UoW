# PostgreSQL-.Net-API-UoW

Example of a setup with PostgreSQL and .Net, using the UoW pattern

## Postgre

victor 4lms@12

### To easy access

#### Create a new database

[Ref](https://www.postgresql.org/docs/current/sql-createdatabase.html)

```SQL
CREATE DATABASE mydatabase;

# Example:
CREATE DATABASE tests;
```

#### Create a new schema

[Ref](https://www.sqliz.com/postgresql/schema/#:~:text=PostgreSQL%20Create%20Schema%20To%20create%20a%20new%20schema,create%20a%20new%20schema%20in%20the%20current%20database.)

```SQL
CREATE SCHEMA [IF NOT EXISTS] schema_name
[AUTHORIZATION role_name];

# Example:
CREATE SCHEMA IF NOT EXISTS dev;
```

#### Create a new table

[Ref](https://www.sqliz.com/postgresql/schema/#:~:text=PostgreSQL%20Create%20Schema%20To%20create%20a%20new%20schema,create%20a%20new%20schema%20in%20the%20current%20database.)

```SQL
CREATE TABLE schema_name.table_name
(...)

# Example:
CREATE TABLE IF NOT EXISTS dev.products
(
    id uuid NOT NULL,
    name "char"[] NOT NULL,
    description "char"[],
    img_url "char"[],
    category uuid,
    in_stock boolean DEFAULT true,
    creation_date timestamp with time zone NOT NULL,
    PRIMARY KEY (id)
);

ALTER TABLE IF EXISTS dev.products
    OWNER to victor;
```

#### Alter table

```SQL
ALTER TABLE IF EXISTS dev.users
    ADD COLUMN creation_date timestamp with time zone NOT NULL;
```

## .Net

### Nuget Packages

-   Microsoft.EntityFrameworkCore
-   Microsoft.EntityFrameworkCore.Tools
-   Microsoft.Extensions.DependencyInjection
-   Npgsql.EntityFrameworkCore.PostgreSQL
-   System.ComponentModel.Annotations

### Setting the migrations to work

#### Create the Context

PostgreContext.cs

```csharp
public class PostgreContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public PostgreContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
         //connect to postgres with connection string from app settings
        var ops = options.UseNpgsql(Configuration.GetConnectionString("PostgreSql"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configure default schema
        modelBuilder.HasDefaultSchema("dev");
    }

    // DbSets
    public DbSet<User> Users { get; set; }

}
```

#### Inject the contex to the Api

Program.cs

```csharp
...
builder.Services.AddSwaggerGen();

//
builder.Services.AddScoped<PostgreContext>();
...
```

#### Initializing the migrations

On the `Package Manager Console`

```powershell
Add-Migration "init"
Update-Database
```

If needs to be recreated from scratch:

```powershell
Remove-Migration
```

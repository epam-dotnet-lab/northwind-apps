# Northwind Applications

## Модуль 5. Использование Entity Framework Core

### Цель

* Изучить Database-First и Code-First подходы.
* Научиться генерировать код сущностей и контекста для существующей базы данных.
* Научиться создавать сущности 
* Научиться реализовывать связи между сущностями при помощи атрибутов.


### Задание 1. Основы Entity Framework

Изучите материалы:

1. [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core).
2. [Getting Started with EF Core](https://docs.microsoft.com/en-us/ef/core/get-started).
3. [Get the Entity Framework Core runtime](https://docs.microsoft.com/en-us/ef/core/get-started/install/#get-the-entity-framework-core-runtime).
4. [How Entity Framework Works?](https://www.entityframeworktutorial.net/basics/how-entity-framework-works.aspx)
5. [Context Class in Entity Framework](https://www.entityframeworktutorial.net/basics/context-class-in-entity-framework.aspx)
6. [What is an Entity in Entity Framework?](https://www.entityframeworktutorial.net/basics/entity-in-entityframework.aspx)


### Задание 2. Scaffolding

Изучите материалы:

* [Development Approaches with Entity Framework](https://www.entityframeworktutorial.net/choosing-development-approach-with-entity-framework.aspx)
* [Creating a Model for an Existing Database in Entity Framework Core](https://www.entityframeworktutorial.net/efcore/create-model-for-existing-database-in-ef-core.aspx)
* [Reverse Engineering](https://docs.microsoft.com/en-us/ef/core/managing-schemas/scaffolding)
* [Get the Entity Framework Core tools](https://docs.microsoft.com/en-us/ef/core/get-started/install/#get-the-entity-framework-core-tools)

#### dotnet-ef

```sh
$ dotnet tool install --global dotnet-ef
$ dotnet add Northwind.Services.EntityFrameworkCore\Northwind.Services.EntityFrameworkCore.csproj package Microsoft.EntityFrameworkCore.Design
$ dotnet-ef dbcontext scaffold "data source=(localdb)\MSSQLLocalDB;Integrated Security=True;Database=Northwind;" Microsoft.EntityFrameworkCore.SqlServer --context-dir Context --context NorthwindContext --output-dir Entities --data-annotations -p Northwind.Services.EntityFrameworkCore\Northwind.Services.EntityFrameworkCore.csproj
```

#### Scaffold-DbContext

1. Solution Explorer -> Northwind.Services.EntityFrameworkCore -> Set as Startup Project (menu).
2. Откройте _Package Manager Console_.
3. Default project: Northwind.Services.EntityFrameworkCore.
4. Запустите команды:

```sh
PM> Install-Package Microsoft.EntityFrameworkCore.Tools
PM> Install-Package Microsoft.EntityFrameworkCore.Design
PM> Scaffold-DbContext -Connection "data source=(localdb)\MSSQLLocalDB;Integrated Security=True;Database=Northwind;" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -ContextDir Context -Context NorthwindContext -DataAnnotations
```

После генерации кода, исправьте ошибки и соберите проект. Предупреждения компилятора о необходимости документации и другие предупреждения в сгенерированных файлах можно отключить.


### Задание 3. Refactoring

#### Выполнение

1. Удалите прежний контекст и модели.
2. Используйте сгенерированный контект и модели для реализации сервисов в проекте.
3. Удалите метод _NorthwindContext.OnConfiguring_. Нет необходимости хранить строку подключения в контексте, так как используется файл конфигурации. См. [Connection Strings](https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-strings#aspnet-core)
4. Изучите материалы:
	* [Creating and configuring a model](https://docs.microsoft.com/en-us/ef/core/modeling)
	* [Entity Types](https://docs.microsoft.com/en-us/ef/core/modeling/entity-types)
	* [Entity Properties](https://docs.microsoft.com/en-us/ef/core/modeling/entity-properties)
	* [Keys](https://docs.microsoft.com/en-us/ef/core/modeling/keys)
	* [Relationships](https://docs.microsoft.com/en-us/ef/core/modeling/relationships)
5. Найдите код сущностей _Products_ и _Categories_, изучите их реализацию.


### Проверочные вопросы:

* Зачем нужен метод _DbContext.OnModelCreating_?
* Что такое _Data Annotations_?

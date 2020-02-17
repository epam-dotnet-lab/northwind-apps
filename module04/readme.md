# Northwind Applications

## Модуль 4. Создание приложений WebAPI

### Цель

* Научиться проектировать и разрабатывать приложения WebAPI при помощи ASP.NET Core.
* Научиться использовать ADO.NET для работы с базой данных.


### Задание 1. ASP.NET Core Web API

Научитесь создавать простые _ASP.NET Core Web API_ приложения.

#### Выполнение

1. Пройдите интерактивное руководство [Create a web API with ASP.NET Core](https://docs.microsoft.com/en-us/learn/modules/build-web-api-net-core/).
2. Пройдите руководство [Tutorial: Create a web API with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api).


### Задание 2. Northwind Web API

Создайте приложение Web API с методами для управления товарами и категориями товаров.

#### Выполнение

1. Создайте решение _NorthwindWebApps_:

![NorthwindWebApps: Services](northwindwebapps-services.png)

2. В проект _Northwind.Services_ добавьте файлы:

* [Products\Product.cs](NorthwindWebApps/Northwind.Services/Products/Product.cs)
* [Products\ProductCategory.cs](NorthwindWebApps/Northwind.Services/Products/ProductCategory.cs)
* [Products\IProductManagementService.cs](NorthwindWebApps/Northwind.Services/Products/IProductManagementService.cs)
* [Products\ProductManagementService.cs](NorthwindWebApps/Northwind.Services/Products/ProductManagementService.cs)

3. Зарегистрируйте сервис _ProductManagementService_ как реализацию интерфейса _IProductManagementService_ в _Startup.ConfigureServices_ с transient lifetime. См. [App startup in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/startup) и [Dependency injection in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection).
4. Добавьте новый контроллер _ProductCategoriesController_. Используйте [Constructor Injection](http://sergeyteplyakov.blogspot.com/2012/12/di-constructor-injection.html) для того, чтобы внедрить в контроллер зависимость на сервис _IProductManagementService_.
5. Заполните пустые колонки в таблице API методов для категорий.

| Operation        | HTTP Verb | URI                  | Request body | Response body |
| ---------------- | --------- | -------------------- | ------------ |  ------------ |
| Create           |           | /api/categories      |              |               |
| Read (all items) |           | /api/categories      |              |               |
| Read (item)      |           | /api/categories/{id} |              |               |
| Update           |           | /api/categories/{id} |              |               |
| Delete           |           | /api/categories/{id} |              |               |

6. Реализуйте все методы для _ProductCategoriesController_, используя методы интерфейса _IProductManagementService_. См. [Controller action return types in ASP.NET Core web API](https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types).
7. Реализуйте в _ProductManagementService_ использованные методы интерфейса _IProductManagementService_. В качестве хранилища используйте in-memory database. Добавьте классы - контекст _NorthwindContext_ и необходимые сущности. Запустите приложение и проверьте работоспособность методов при помощи Postman.
8. Добавьте новый контроллер _ProductsController_, заполните пустые колонки в таблице API методов для товаров, реализуйте необходимые методы контроллера и сервиса.

| Operation        | HTTP Verb | URI                  | Request body | Response body |
| ---------------- | --------- | -------------------- | ------------ | ------------- |
| Create           |           |                      |              |               |
| Read (all items) |           |                      |              |               |
| Read (item)      |           |                      |              |               |
| Update           |           |                      |              |               |
| Delete           |           |                      |              |               |

9. Реализуйте в _ProductCategoriesController_ новые методы для управления картинкой (поток байтов) для категории. См. [Upload files in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads), [IFormFile](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iformfile) и [ControllerBase.File](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.file).

| Operation        | HTTP Verb | URI                                  | Request body    | Response body  |
| ---------------- | --------- | ------------------------------------ | --------------- | -------------- |
| Upload picture   | PUT       | /api/categories/{categoryId}/picture | Picture stream  | None           |
| Get picture      | GET       | /api/categories/{categoryId}/picture | None            | Picture stream |
| Delete picture   | DELETE    | /api/categories/{categoryId}/picture | None            | None           |

11. Проанализируйте зависимости:

![NorthwindWebApps: IProductManagementService](northwindapiapp-iproductmanagementservice.png)

12. Выделите из интерфейса _IProductManagementService_ интерфейсы для работы с категориями и картинками - _IProductCategoryManagementService_ и _IProductCategoryPicturesService_, перенесите в новые интерфейсы соответствующие методы, зарегистрируйте интерфейсы как сервисы и исправьте конструкторы соответствующих контроллеров. См. [I: Принцип разделения интерфейса](https://refactoring.guru/ru/didp/principles/solid-principles/isp).

13. Выделите методы класс сервиса _ProductManagementService_ в отдельные сервисы _ProductCategoryManagementService_ и _ProductCategoryPicturesService_. См. [S: Принцип единственной ответственности](https://refactoring.guru/ru/didp/principles/solid-principles/srp).

14. Добавьте библиотеку _Northwind.Services.EntityFrameworkCore_, исправьте зависимости на nuget-пакеты, перенесите в библиотеку код сервисов (_ProductManagementService_,  _ProductCategoryManagementService_, _ProductCategoryPictureService_) и все необходимые классы.

![NorthwindWebApps: EntityFramework Core](northwind-webapi-entityframeworkcore.png)

15. Проанализируйте зависимости:

![NorthwindWebApps: 3 interfaces](northwindapiapp-three-interfaces.png)

16. Добавьте документацию для методов Web API. См. [Use web API conventions](https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/conventions).

17. Найдите в приложении Composition Root. См. [Using a DI Container in a Composition Root](https://freecontent.manning.com/dependency-injection-in-net-2nd-edition-understanding-the-composition-root/).


### Задание 2. LocalDB

Научитесь использовать базу данных LocalDB для разработки приложений.

#### Выполнение

1. Запустите _Visual Studio Installer_ и установите LocalDB:

![Visual Studio Installer - SQL Server Express 2016 LocalDB](visual-studio-install-localdb.png)

2. Откройте вкладку _SQL Server Object Explorer_ и создайте новую базу данных _Northwind_.

![Add New Database in SQL Server Object Explorer](visual-studio-sql-server-object-explorer-add-new.png)

3. Выберите _New Query_ в меню БД:

![New Query in SQL Server Object Explorer](visual-studio-new-query.png)

4. Вставьте текст скрипта для создания БД ([instnwnd.sql](https://github.com/microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs)) в окно запроса, выберите БД _Northwind_ и запустите скрипт на выполнение (_Execute_). Начало скрипта нужно пропутить до первого выражения CREATE TABLE.

![Execute Query](visual-studio-execute-query.png)


### Задание 3. Приложение Web API

#### Выполнение

1. 

![NorthwindWebApi Solution](northwind-webapi-solution.png)

1. Заполните колонку HTTP Verb в таблице:

| CRUD             | HTTP Verb | URI                  | Request body  | Response body           |
| ---------------- | --------- | -------------------- | ------------- | ----------------------- |
| Create           |           | /api/categories      | Category item | Category item           |
| Read (all items) |           | /api/categories      | None          | Array of category items |
| Read (item)      |           | /api/categories/{id} | None          | Category item           |
| Update           |           | /api/categories/{id} | Category item | None                    |
| Delete           |           | /api/categories/{id} | None          | None                    |


2. 

| CRUD             | HTTP Verb | URI                                  | Request body  | Response body           |
| ---------------- | --------- | ------------------------------------ | ------------- | ----------------------- |
| Create           |           | /api/categories/{categoryId}/picture | Picture       | None                    |
| Read (item)      |           | /api/categories/{categoryId}/picture | None          | Picture                 |
| Update           |           | /api/categories/{categoryId}/picture | Pciture       | None                    |
| Delete           |           | /api/categories/{categoryId}/picture | None          | None                    |


If you think that can proceed with the standard control, please, let me know till the end of the week.

1. Создание web api с products, ef + memory

2. Создание web api product+category, dao

3. category picture
# Northwind Applications

## Модуль 4. Приложения Web API на ASP.NET Core

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


### Задание 2. Data Access Object

### Задание 3. Приложение Web API

#### Выполнение

1. 

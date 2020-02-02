# Northwind Applications

## Модуль 3. Querying the Data Services

[WCF](https://docs.microsoft.com/en-us/dotnet/framework/wcf/) - это фреймворк, который в прошлом широко использовался для [построения распределенных приложений](http://sergeyteplyakov.blogspot.com/2011/02/wcf.html) на базе платформы .NET. Существует много рабочих приложений, которые построены на его основе и успешно работают. На текущий момент [популярность фреймворка сильно упала](https://github.com/dotnet/wcf/issues/1784), однако поддержка работающих сервисов и работа с ними по-прежнему является актуальной задачей для крупных промышленных программных систем.

URL [Northwind (v3)](https://www.odata.org/odata-services/) OData-сервиса выглядит следующим образом:

```
https://services.odata.org/V3/Northwind/Northwind.svc/
```

Наличие [".svc" в пути сервиса](https://stackoverflow.com/questions/17363429/does-a-wcf-service-always-use-an-svc-file) намекает на реализацию сервиса с помощью WCF.

### Цель

* Научиться использовать LINQ для построения запросов данных.
* Научиться использовать Fiddler для инспекции HTTP/HTTPS-трафика.


### Задание 1. Создание каркаса приложения отчетов

#### Выполнение

1. Создайте консольное приложение _ReportingApp_ и библиотеку классов _Northwind.ReportingServices.OData_ в решении _ReportingApps_.

```sh
$ mkdir ReportingApps
$ cd ReportingApps
$ dotnet new sln --name ReportingApps
$ dotnet new classlib --name Northwind.ReportingServices.OData
$ dotnet sln ReportingApps.sln add Northwind.ReportingServices.OData\Northwind.ReportingServices.OData.csproj
$ dotnet new console --name ReportingApp
$ dotnet sln ReportingApps.sln add ReportingApp\ReportingApp.csproj
$ dotnet add ReportingApp\ReportingApp.csproj reference Northwind.ReportingServices.OData\Northwind.ReportingServices.OData.csproj
```

2. Добавьте в проекты статические анализаторы кода.

```sh
$ dotnet add Northwind.ReportingServices.OData\Northwind.ReportingServices.OData.csproj package Microsoft.CodeAnalysis.FxCopAnalyzers
$ dotnet add Northwind.ReportingServices.OData\Northwind.ReportingServices.OData.csproj package StyleCop.Analyzers
$ dotnet add ReportingApp\ReportingApp.csproj package Microsoft.CodeAnalysis.FxCopAnalyzers
$ dotnet add ReportingApp\ReportingApp.csproj package StyleCop.Analyzers
```

3. Создайте пустые файлы настроек IDE и статических анализаторов кода.

> Для linux-систем и Git Bash нужно использовать команду _touch_ вместо команды Windows _type_.

```sh
$ type nul > .\code-analysis.ruleset
$ type nul > .\stylecop.json
$ type nul > .\.editorconfig
$ type nul > Northwind.ReportingServices.OData\.editorconfig
$ type nul > ReportingApp\.editorconfig
```

Заполните содержимое - [code-analysis.ruleset](ReportingApps/code-analysis.ruleset), [stylecop.json](ReportingApps/stylecop.json), [.editorconfig решения](ReportingApps/.editorconfig) и [.editorconfig проектов](ReportingApps/ReportingApp/.editorconfig).

4. Добавьте в файлы проектов ссылки на файлы настроек и подключите статические анализаторы кода:

```xml
<PropertyGroup>
  <CodeAnalysisRuleSet>..\code-analysis.ruleset</CodeAnalysisRuleSet>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
  <NoWarn>$(NoWarn),1573,1591,1712</NoWarn>
</PropertyGroup>
<ItemGroup>
  <AdditionalFiles Include="..\code-analysis.ruleset" Link="Properties\code-analysis.ruleset" />
  <AdditionalFiles Include="..\stylecop.json" Link="Properties\stylecop.json" />
</ItemGroup>
```

5. Добавьте в _Northwind.ReportingServices.OData_ код OData-клиента для сервиса [Northwind (v3)](https://www.odata.org/odata-services/).

6. Создайте пустые файлы для сервиса отчетов _ProductReportService_ в каталоге _ProductReports_.

```sh
$ type nul > Northwind.ReportingServices.OData\ProductReports\ProductPrice.cs
$ type nul > Northwind.ReportingServices.OData\ProductReports\ProductReport.cs
$ type nul > Northwind.ReportingServices.OData\ProductReports\ProductReportService.cs
```
Заполните содержимое - [ProductPrice.cs](ReportingApps/Northwind.ReportingServices.OData/ProductReports/ProductPrice.cs), [ProductReport.cs](ReportingApps/Northwind.ReportingServices.OData/ProductReports/ProductReport.cs), [ProductReportService.cs](ReportingApps/Northwind.ReportingServices.OData/ProductReports/ProductReportService.cs). Замените _Program.cs_ на [Program.cs](ReportingApps/ReportingApp/Program.cs).

Проект должен выглядеть следующим образом - [ReportingApps](ReportingApps/).


### Задание 2. Работа с Fiddler

#### Выполнение

1. Скачайте и установите [Fiddler](https://www.telerik.com/fiddler).
2. Изучите:
	* [Скриптуемый отладочный прокси Fiddler](https://learn.javascript.ru/fiddler).
	* [Getting Started with Fiddler Web Debugging Proxy](https://www.youtube.com/watch?v=gujBKFGwjd4).
	* Дополнительно: [Fiddler - подробный разбор](https://www.youtube.com/watch?v=YPg18W7O8aU).


### Задание 3. Запрос данных из OData-сервиса.

#### Выполнение

1. Изучите статьи про асинхронную работу с сервисом:
	* [Asynchronous Operations](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/asynchronous-operations-wcf-data-services)
	* [How to: Execute Asynchronous Data Service Queries](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/how-to-execute-asynchronous-data-service-queries-wcf-data-services)

Получите данные по товарам - замените код _ProductReportService.GetCurrentProducts_:

```cs
public async Task<ProductReport<ProductPrice>> GetCurrentProducts()
{
    var query = (DataServiceQuery<NorthwindProduct>)this.entities.Products;

    var result = await Task<IEnumerable<NorthwindProduct>>.Factory.FromAsync(query.BeginExecute(null, null), (ar) =>
    {
        return query.EndExecute(ar);
    });

    var productPrices = new List<ProductPrice>();
    foreach (var product in result)
    {
        productPrices.Add(new ProductPrice
        {
            Name = product.ProductName,
            Price = product.UnitPrice ?? 0,
        });
    }

    return new ProductReport<ProductPrice>(productPrices);
}
```

Запустите приложение и найдите в Fiddler соответствующий запрос.

2. Изучите статьи про запросы данных из сервиса: 
	* [Querying the Data Service](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/querying-the-data-service-wcf-data-services).
	* [How to: Execute Data Service Queries](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/how-to-execute-data-service-queries-wcf-data-services).

Напишите LINQ-запрос для выборки всех текущих товаров (не discontinued) и сделайте нисходящую сортировку списка по полю _ProductName_:

```cs
var query = (DataServiceQuery<NorthwindProduct>)this.entities.Products.Where(p => !p.Discontinued).OrderBy(p => p.ProductName);
```

Исследуйте в Fiddler соответствующий запрос.

3. Изучите статью [LINQ Considerations](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/linq-considerations-wcf-data-services) и перепишите запрос с применением синтаксиса запроса:

```cs
var query = (DataServiceQuery<NorthwindProduct>)(
                from p in this.entities.Products
                where !p.Discontinued
                orderby p.ProductName
                select p);
```

Найдите в Fiddler ответ сервиса в XML-формате.

4. Изучите статью [Query Projections](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/query-projections-wcf-data-services).

Используйте проекцию для получения имени и цены единицы товара:

```cs
public async Task<ProductReport<ProductPrice>> GetCurrentProducts()
{
    var query = (DataServiceQuery<ProductPrice>)(
        from p in this.entities.Products
        where !p.Discontinued
        orderby p.ProductName
        select new ProductPrice
        {
            Name = p.ProductName,
            Price = p.UnitPrice ?? 0,
        });

    var result = await Task<IEnumerable<ProductPrice>>.Factory.FromAsync(query.BeginExecute(null, null), (ar) =>
    {
        return query.EndExecute(ar);
    });

    return new ProductReport<ProductPrice>(result);
}
```

Найдите в Fiddler ответ сервиса и сравните с предыдущим запросом.

5. Реализуйте метод _ProductReportService.GetMostExpensiveProductsReport_, используя проекцию:

```cs
var query = (DataServiceQuery<ProductPrice>)this.entities.Products.
    Where(p => p.UnitPrice != null).
    OrderByDescending(p => p.UnitPrice.Value).
    Take(count);
```

6. Метод _ProductReportService.GetCurrentProducts_ возвращает неполный список товаров. Количество элементов в списке ограничено количеством элементов, которое сервис возвращает за один запрос. См. статьи [Loading Deferred Content](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/loading-deferred-content-wcf-data-services) и [How to: Load Paged Results](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/how-to-load-paged-results-wcf-data-services). Используйте метод [GetContinuation](https://docs.microsoft.com/en-us/dotnet/api/system.data.services.client.queryoperationresponse-1.getcontinuation), чтобы получить полный список товаров из сервиса.

Найдите в Fiddler соответствующие запросы.


### Задание 4. Дополнительные отчеты

#### Выполнение

Query, product (id, name, unit price) where current (not discontinued) and product cost less than $20.

Query, product (id, name, unit price) where current (not discontinued) and product cost between $15 and $25

Query, product (id, name, unit price) of above average price.


Write a query to get Product list (name, units on order , units in stock) of stock is less than the quantity on order

https://www.geeksengine.com/database/problem-solving/northwind-queries-part-1.php

https://habr.com/ru/company/ruvds/blog/436884/
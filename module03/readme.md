# Northwind Applications

## Модуль 3. 


### Цель

* 
* Научиться использовать Fiddler для инспекции HTTP/HTTPS-трафика.

#### Сервисы WCF

[WCF](https://docs.microsoft.com/en-us/dotnet/framework/wcf/) - это фреймворк, который в прошлом широко использовался для [построения распределенных приложений](http://sergeyteplyakov.blogspot.com/2011/02/wcf.html) на базе платформы .NET. Существует много рабочих приложений, которые построены на его основе и успешно работают. На текущий момент [популярность фреймворка сильно упала](https://github.com/dotnet/wcf/issues/1784), однако поддержка работающих сервисов и работа с ними по-прежнему является актуальной задачей для крупных промышленных программных систем.

Url сервиса выглядит следующим образом:

```
https://services.odata.org/V3/Northwind/Northwind.svc/
```

Наличие [".svc" в пути сервиса](https://stackoverflow.com/questions/17363429/does-a-wcf-service-always-use-an-svc-file) намекает на реализацию сервиса с помощью WCF.


### Задание 1. Отчеты


#### Выполнение

1. Создайте консольное приложение _ReportingApp_ и библиотеку классов _Northwind.ReportingServices.OData_ в общем решении _ReportingApps_.

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

Заполните содержимое - [code-analysis.ruleset](ReportingApps/code-analysis.ruleset), [stylecop.json](ReportingApps\stylecop.json), [.editorconfig решения](ReportingApps/.editorconfig) и [.editorconfig проектов](ReportingApps/ReportingApp/.editorconfig).

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
Заполните содержимое - [ProductPrice.cs](ReportingApps/Northwind.ReportingServices.OData/ProductReports/ProductPrice.cs), [ProductReport.cs](ReportingApps/Northwind.ReportingServices.OData/ProductReports/ProductReport.cs), [ProductReportService.cs](ReportingApps/Northwind.ReportingServices/ProductReports/ProductReportService.cs).

Проект должен выглядеть следующим образом - [ReportingApps](ReportingApps/).

7. 


ReportingApp
()

Northwind.ReportingServices



Query, product (id, name, unit price) where current (not discontinued) and product cost less than $20.

Query, product (id, name, unit price) where current (not discontinued) and product cost between $15 and $25

Query, product (id, name, unit price) of above average price.

query, product (id, name, unit price) N most expensive products.

Write a query to get Product list (name, units on order , units in stock) of stock is less than the quantity on order

https://www.geeksengine.com/database/problem-solving/northwind-queries-part-1.php

https://habr.com/ru/company/ruvds/blog/436884/


[Querying the Data Service](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/querying-the-data-service-wcf-data-services)
[How to: Execute Data Service Queries](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/how-to-execute-data-service-queries-wcf-data-services)
[How to: Add Query Options to a Data Service Query](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/how-to-add-query-options-to-a-data-service-query-wcf-data-services)
[Query Projections](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/query-projections-wcf-data-services)
[LINQ Considerations](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/linq-considerations-wcf-data-services)

[Asynchronous Operations](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/asynchronous-operations-wcf-data-services)
[How to: Execute Asynchronous Data Service Queries](https://docs.microsoft.com/en-us/dotnet/framework/data/wcf/how-to-execute-asynchronous-data-service-queries-wcf-data-services)

[Скриптуемый отладочный прокси Fiddler](https://learn.javascript.ru/fiddler)
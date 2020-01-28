# Northwind Apps

## Модуль 2. Клиент для OData-сервиса

_Протокол OData имеет несколько версий (на текущий момент - четыре). Версии 1, 2 и 3 являются обратно совместимыми, однако версия 4 не является обратно совместимой. Работа с сервисами разных версий отличается с точки зрения реализации клиента. Также реализация клиента зависит от целевой платформы - .NET Framework или .NET Core. Поэтому подход к работе с сервисами OData на стороне клиента может значительно отличаться в зависимости от версии сервиса и платформы._

### Цель

* Научиться создавать консольные клиентские приложения для OData-сервисов версий 3 и 4.
* Научиться создавать клиентские приложения для OData-сервисов для .NET Framework и .NET Core.
* Научиться использовать расширения Visual Studio для генерации кода клиента OData-сервиса.
* Научиться вызывать OData-сервисы асинхронно.


### Задание 1. Генерация кода клиента с помощью расширения для Visual Studio для .NET Framework

Работать с сервисом OData можно с помощью прямых вызовов методов HTTP, однако часто для связи с сервисом используются прокси-классы, которые генерируются автоматически с помощью дополнительного инструментария. Кодогенерация значительно упрощает написание клиента, снижает количество кода, которое требуется для полноценной работы с сервисом, а также снижает количество ошибок, которые может допустить разработчик.

Научитесь генерировать код клиента для [сервиса _TripPin_](https://www.odata.org/odata-services/), который реализует 4 версию протокола OData.

#### Выполнение

1. Установите расширение [Unchase OData Connected Service](https://marketplace.visualstudio.com/items?itemName=Unchase.unchaseodataconnectedservice) для Visual Studio 2017 и 2019, которое поддерживает версии протокола 3 и 4. Существует другое расширение - [OData v4 Client Code Generator](https://marketplace.visualstudio.com/items?itemName=bingl.ODatav4ClientCodeGenerator), однако расширение поддерживает только версию 4 протокола и доступно только для Visual Studio 2015 и 2017.
2. Создайте новое консольное приложение **.NET Framework** - _TripPinUnchaseFrameworkClient_.
3. Сгенерируйте код клиента _TripPin_, используя руководство [How to generate C# or Visual Basic client code for OData protocol versions 1.0–4.0](https://medium.com/@unchase/how-to-generate-c-or-visual-basic-client-code-for-odata-protocol-versions-1-0-4-0-a3a4f9402ea1). Настройки генератора - [Metadata Endpoint](unchase-odata-generation-endpoint.png), [Advanced Settings](unchase-odata-generation-advanced-settings.png).
4. Добавьте код в метод _Program.Main_, который получает из сервиса список людей и выводит на экран имя и фамилию каждого человека:

```cs
const string serviceUri = "https://services.odata.org/TripPinRESTierService";
var container = new Microsoft.OData.Service.Sample.TrippinInMemory.Models.Container(new Uri(serviceUri));

Console.WriteLine("People in TripPin service:");
var people = container.People;

foreach (var person in people)
{
    Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
}
```


### Задание 2. Генерация кода клиента с помощью расширения для Visual Studio для .NET Core

1. Создайте новое консольное приложение **.NET Core**, сгенерируйте код клиента, добавьте код в _Program.Main_ и запустите приложение.

При выполнении приложения будет брошено исключение:

> System.NotSupportedException: 'This target framework does not enable you to directly enumerate over a data service query. This is because enumeration automatically sends a synchronous request to the data service. Because this framework only supports asynchronous operations, you must instead call the BeginExecute and EndExecute methods to obtain a query result that supports enumeration.'

См. причину ошибки в тикете [System.NotSupportedException when calling OData service from NetCoreApp2.1](https://github.com/OData/odata.net/issues/1303) - библиотека Microsoft.OData.Client поддерживает только [асинхронные вызовы](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/). (Используйте [ru-ru в URL](https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/concepts/async/), чтобы автоматически переводить текст статей документации на русский язык).

2. Примените [подход Asynchronous Programming Model](https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/asynchronous-programming-model-apm), чтобы сделать вызов к сервису асинхронным:

```cs
const string serviceUri = "https://services.odata.org/TripPinRESTierService";
var container = new Microsoft.OData.Service.Sample.TrippinInMemory.Models.Container(new Uri(serviceUri));

IAsyncResult asyncResult = container.People.BeginExecute((ar) =>
{
    Console.WriteLine("People in TripPin service:");
    var people = container.People.EndExecute(ar);

    foreach (var person in people)
    {
        Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
    }

}, null);

WaitHandle.WaitAny(new[] { asyncResult.AsyncWaitHandle });
```

3. Создайте новое консольное приложение **.NET Core** - _TripPinUnchaseCoreAsyncClient_ и сгенерируйте код клиента.
4. Исправьте сигнатуру метода _Program.Main_.

```cs
static async Task Main(string[] args)
```

Возможность помечать метод _Main_ модификатором async появилась в C# в версии 7.1, поэтому при необходимости [измените версию языка в Visual Studio](https://codez.deedx.cz/posts/csharp-async-main/) или [параметр LangVersion в csproj](https://stackoverflow.com/questions/47588531/error-message-cs5001-program-does-not-contain-a-static-main-method-suitable-f).

5. Добавьте код вызова сервиса с применением _await_ и запустите приложение.

```cs
Console.WriteLine("People in TripPin service:");
var people = await container.People.ExecuteAsync();

foreach (var person in people)
{
    Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
}
```

Такой подход называется [Task-based Asynchronous Pattern](https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap).


### Задание 3. Генерация кода клиента с помощью утилиты DataSvcUtil для .NET Framework

#### Выполнение

1. Используя Postman, получите метаданные [сервиса Northwind (read only)](https://www.odata.org/odata-services/) и сохраните их в файл _northwind-data-service.edmx_.
2. Установите пакет [WCF Data Services 5.6 Tools](http://download.microsoft.com/download/1/C/A/1CAA41C7-88B9-42D6-9E11-3C655656DAB1/WcfDataServices.exe) и найдите каталог, в котором находится DataSvcUtil.exe.

Для поиска файлов можно использовать [консоль PowerShell](https://devblogs.microsoft.com/scripting/use-windows-powershell-to-search-for-files/):

```powershell
(base) PS C:\> Get-Childitem -Path "C:\" -Include DataSvcUtil.exe -Recurse -Name
Program Files (x86)\Microsoft WCF Data Services\5.6\bin\tools\DataSvcUtil.exe
Windows\Microsoft.NET\Framework\v4.0.30319\DataSvcUtil.exe
Windows\Microsoft.NET\Framework64\v4.0.30319\DataSvcUtil.exe
Windows\WinSxS\amd64_netfx4-datasvcutil_b03f5f7f11d50a3a_4.0.15788.0_none_ba863bb33d17498f\DataSvcUtil.exe
Windows\WinSxS\msil_datasvcutil_b77a5c561934e089_4.0.15788.0_none_a0e47c56b58e609e\DataSvcUtil.exe
```

Каталог пакета _WCF Data Services 5.6 Tools_ - C:\Program Files (x86)\Microsoft WCF Data Services\5.6.

> Вместо указанного пакета можно установить другой пакет - [WCF Data Services 5.0 (OData v3)](http://download.microsoft.com/download/8/F/9/8F93DBBD-896B-4760-AC81-646F61363A6D/WcfDataServices.exe). У этого пакета будет другой установочный каталог. Также, если в системе установлен .NET Framework, то утилита DataSvcUtil находится в каталоге фреймворка в C:\Windows\Microsoft.NET. Однако эта версия утилиты не поддерживает версию 3 протокола OData.

Получите параметры коммандной строки:

```sh
$ "%ProgramFiles(x86)%\Microsoft WCF Data Services\5.6\bin\tools\DataSvcUtil.exe" /?
```

Нужные параметры - in, out, dataServiceCollection и version.

3. Сгенерируйте кода клиента при помощи DataSvcUtil:

```sh
D:\Work\northwind-apps>"%ProgramFiles(x86)%\Microsoft WCF Data Services\5.6\bin\tools\DataSvcUtil.exe" /in:northwind-data-service.edmx /out:NorthwindDataService.cs /version:3.0 /dataservicecollection
Microsoft (R) DataSvcUtil version 5.6.0.0
Copyright (C) 2008 Microsoft Corporation. All rights reserved.

Writing object layer file...

Generation Complete -- 0 errors, 0 warnings
```

> %ProgramFiles(x86)% - это [переменная окружения](https://stackoverflow.com/questions/9594066/how-to-get-program-files-x86-env-variable).

4. Создайте новое консольное приложение **.NET Framework** - _NorthwindServiceFrameworkClient_. Скопируйте файл _NorthwindDataService.cs_ в каталог проекта и добавьте его в проект.
5. Добавьте в проект nuget-пакет [Microsoft.Data.Services.Client](https://www.nuget.org/packages/Microsoft.Data.Services.Client) при помощи [Package Manager Console](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-powershell):

```powershell
Install-Package Microsoft.Data.Services.Client -Version 5.8.4
```

Для команды следует задать актуальную версию, которую можно найти на портале nuget.org.

5. Добавьте код в метод _Program.Main_, который получает из сервиса список сотрудников и выводит на экран имя и фамилию каждого человека:

```cs
const string serviceUri = "https://services.odata.org/V3/Northwind/Northwind.svc/";
var entities = new NorthwindModel.NorthwindEntities(new Uri(serviceUri));

var employees = entities.Employees;
Console.WriteLine("Employees in Northwind service:");

foreach (var person in employees)
{
    Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
}
```


### Задание 4. Генерация кода клиента с помощью утилиты DataSvcUtil для .NET Core

1. Создайте новое консольное приложение **.NET Core** - _NorthwindServiceCoreAsyncClient_. Скопируйте файл _NorthwindDataService.cs_ в каталог проекта.
2. Добавьте в проект nuget-пакет _Microsoft.Data.Services.Client_.
3. Скопируйте код _Program.Main_ из _NorthwindServiceFrameworkClient_ и запустите приложение. Какое исключение было брошено?
4. Перепишите код с использованием подхода APM (методы BeginExecute и EndExecute).
5. Измените сигнатуру метода _Program.Main_, чтобы сделать этот метод асинхронным.
6. В коде клиента отсутствует поддержка подхода TAP - у entities.Employees нет асинхронного метода ExecuteAsync. [APM можно преобразовать в TAP](https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/interop-with-other-asynchronous-patterns-and-types#from-apm-to-tap) при помощи метода FromAsync.

```cs
const string serviceUri = "https://services.odata.org/V3/Northwind/Northwind.svc/";
var entities = new NorthwindModel.NorthwindEntities(new Uri(serviceUri));

var employees = await Task<IEnumerable<NorthwindModel.Employee>>.Factory.FromAsync(entities.Employees.BeginExecute(null, null), (iar) =>
{
    return entities.Employees.EndExecute(iar);
});

Console.WriteLine("Employees in Northwind service:");
foreach (var person in employees)
{
    Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
}
```
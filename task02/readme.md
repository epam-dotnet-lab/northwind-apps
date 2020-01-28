# Northwind Apps

## Модуль 2. Клиент OData-сервиса

_Протокол OData имеет несколько версий (на текущий момент - четыре). Версии 1, 2 и 3 являются обратно совместимыми, однако версия 4 не является обратно совместимой. Работа с сервисами разных версий отличается с точки зрения реализации клиента. Также реализация клиента зависит от целевой платформы - .NET Framework или .NET Core. Поэтому подход к работе с сервисами OData на стороне клиента может значительно отличаться в зависимости от версии сервиса и платформы._

### Цели

* Научиться создавать консольные клиентские приложения для сервисов OData версий 3 и 4.
* Научиться использовать расширения Visual Studio для генерации кода клиента OData-сервиса.


### Задание 1. Кодогенерация с помощью расширения для Visual Studio

Работать с сервисом OData можно с помощью прямых вызовов методов HTTP, однако часто для связи с сервисом используются прокси-классы, которые генерируются автоматически с помощью дополнительного инструментария. Кодогенерация значительно упрощает написание клиента, снижает количество кода, которое требуется для полноценной работы с сервисом, а также снижает количество ошибок, которые может допустить разработчик.

#### Выполнение

Установите расширение [Unchase OData Connected Service](https://marketplace.visualstudio.com/items?itemName=Unchase.unchaseodataconnectedservice) для Visual Studio 2017 и 2019, которое поддерживает версии протокола 3 и 4. Существует другое расширение - [OData v4 Client Code Generator](https://marketplace.visualstudio.com/items?itemName=bingl.ODatav4ClientCodeGenerator), однако оно поддерживает только версию 4 и доступно только для Visual Studio 2015 и 2017.
2. Создайте новое консольное приложение *.NET Framework* - _TripPinUnchaseFrameworkClient_.
3. Сгенерируйте код клиента, используя руководство [How to generate C# or Visual Basic client code for OData protocol versions 1.0–4.0](https://medium.com/@unchase/how-to-generate-c-or-visual-basic-client-code-for-odata-protocol-versions-1-0-4-0-a3a4f9402ea1).

Настройки генератора:

![Metadata Endpoint](unchase-odata-generation-endpoint.png)

![Advanced Settings](unchase-odata-generation-advanced-settings.png)

4. Добавьте код в метод _Program.Main_, который получает из сервиса список людей и выводит на экран имя и фамилию каждого человека:

```cs
const string serviceUri = "https://services.odata.org/TripPinRESTierService";
var container = new Microsoft.OData.Service.Sample.TrippinInMemory.Models.Container(new Uri(serviceUri));

Console.WriteLine("People in TripPin service:");
var people = container.People.ToArray();

foreach (var person in people)
{
    Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
}
```

5. Создайте новое консольное приложение *.NET Core*, сгенерируйте код клиента, добавьте код в _Program.Main_ и запустите приложение. При выполнении должно появиться исключение:

> System.NotSupportedException: 'This target framework does not enable you to directly enumerate over a data service query. This is because enumeration automatically sends a synchronous request to the data service. Because this framework only supports asynchronous operations, you must instead call the BeginExecute and EndExecute methods to obtain a query result that supports enumeration.'

См. причину ошибки в тикете [System.NotSupportedException when calling OData service from NetCoreApp2.1](https://github.com/OData/odata.net/issues/1303) - библиотека Microsoft.OData.Client поддерживает только асинхронные вызовы.

6. Примените [APM-подход](https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/asynchronous-programming-model-apm), чтобы сделать вызов к сервису асинхронным:

```cs
const string serviceUri = "https://services.odata.org/TripPinRESTierService";
var container = new Microsoft.OData.Service.Sample.TrippinInMemory.Models.Container(new Uri(serviceUri));

IAsyncResult asyncResult = container.People.BeginExecute((ar) =>
{
    Console.WriteLine("People in TripPin service:");
    var people = container.People.EndExecute(ar).ToArray();

    foreach (var person in people)
    {
        Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
    }

}, null);

WaitHandle.WaitAny(new[] { asyncResult.AsyncWaitHandle });
```

7. Создайте новое консольное приложение *.NET Core* - _TripPinUnchaseCoreAsyncClient_ и сгенерируйте код клиента.
8. Исправьте сигнатуру метода _Program.Main_. (При необходимости [измените версию языка в Visual Studio](https://codez.deedx.cz/posts/csharp-async-main/) или [параметр LangVersion в csproj](https://stackoverflow.com/questions/47588531/error-message-cs5001-program-does-not-contain-a-static-main-method-suitable-f)).

```cs
static async Task Main(string[] args)
```

9. Добавьте код вызова сервиса из пункта (4) с применением _await_ и запустите приложение.

```cs
Console.WriteLine("People in TripPin service:");
var people = await container.People.ExecuteAsync();

foreach (var person in people)
{
    Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
}
```
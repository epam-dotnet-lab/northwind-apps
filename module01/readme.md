# Northwind Apps

## Модуль 1. Использование сервисов OData

### Цель

* Изучить основы протокола HTTP.
* Научиться использовать Postman для выполнения запросов к внешним API с использованием протокола HTTP.
* Научиться работать с OData-сервисом на примере демонстрационного сервиса TripPin.
* Изучить модель базы данных Northwind.
* Научиться работать с OData-сервисом Northwind.


### Задание 1. Основы протокола HTTP

Изучите основы протокола HTTP - структуру, основные методы (GET, POST, PUT, DELETE), коды состояния, заголовки.

#### Выполнение

1. Изучите статьи:
	* [Простым языком об HTTP](https://habr.com/ru/post/215117/).
	* [Обзор протокола HTTP](https://developer.mozilla.org/ru/docs/Web/HTTP/Overview) и [HTTP-сообщения](https://developer.mozilla.org/ru/docs/Web/HTTP/Messages) в разделе [MDN web docs: HTTP](https://developer.mozilla.org/ru/docs/Web/HTTP).
2. Используйте в качестве справочного материала:
	* [HTTP-заголовки](https://developer.mozilla.org/ru/docs/Web/HTTP/Headers).
	* [Методы HTTP запроса](https://developer.mozilla.org/ru/docs/Web/HTTP/Methods).
	* [Коды ответа HTTP](https://developer.mozilla.org/ru/docs/Web/HTTP/Status).
	* [Wiki: HTTP](https://ru.wikipedia.org/wiki/HTTP).
3. Проверьте свои знания [с помощью теста](http://www.quizful.net/test/http-basics).


### Задание 2. OData-сервис TripPin

Научитесь использовать демонстрационный OData-сервис TripPin с помощью Postman.

[OData](https://ru.wikipedia.org/wiki/Open_Data_Protocol) - это открытый стандартизированный протокол для запроса и обновления данных, который позволяет выполнять операции с ресурсами, используя команды ("методы") протокола HTTP.

#### Выполнение

1. Установите [Postman](https://www.getpostman.com/downloads/), инструмент для работы с HTTP-сервисами и REST API.
2. Освойте базовые сценарии работы с Postman:
	* Статья [Введение в Postman](https://habr.com/ru/company/kolesa/blog/351250/).
	* Руководство [Postman Tutorial for Beginners with API Testing Example](https://www.guru99.com/postman-tutorial.html).
	* Дополнительные материалы:
		* Серия видео [New to Postman](https://www.youtube.com/playlist?list=PLM-7VG-sgbtBsenu0CM-UF3NZj3hQFs7E).
		* Серия видео [Postman "How To"](https://www.youtube.com/playlist?list=PLM-7VG-sgbtCJYpjQfmLCcJZ6Yd74oytQ).
		* Видео [Postman: от простого API-теста до конечного сценария"](https://www.youtube.com/watch?v=hGmJMeE_ok0).
3. Изучите основы OData с использованием руководства [Understand OData in 6 steps](https://www.odata.org/getting-started/understand-odata-in-6-steps/).
4. Используйте статью [Learning OData on Postman](https://www.odata.org/getting-started/learning-odata-on-postman/), чтобы добавить в Postman коллекцию "Postman OData Tutorial" для работы с сервисом _TripPin_.
5. Добавьте в Postman коллекцию "Trippin Basic Usages" из раздела [Basic Usages](https://www.odata.org/odata-services/).
5. Изучите [модель данных сервиса TripPin](https://www.odata.org/blog/trippin-new-odata-v4-sample-service/).
6. Пройдите [Basic Tutorial](https://www.odata.org/getting-started/basic-tutorial).


### Задание 3. Модель БД Northwind

Изучите модель базы данных Northwind, которая используется в этом руководстве в качестве основной.


#### Выполнение

1. Изучите схему [Northwind Sample Database](https://www.zentut.com/wp-content/uploads/downloads/2013/06/Northwind-Sample-Database-Diagram.pdf). Связи между таблицами в диаграмме обозначены с помощью графической нотации "воронья лапка", объяснение которой дано в статье ["Data Modelling using ERD with Crow Foot Notation"](https://www.codeproject.com/Articles/878359/Data-Modelling-using-ERD-with-Crow-Foot-Notation).
2. Используя диаграмму, выясните, какие данные хранят в себе сущности модели.
3. Выясните кардинальность для связей между таблицами PK Table (таблица, которая содержит первичный ключ сущности) и FK Table (таблица, содержащая внешний ключ сущности). Заполните колонки Cardinality в таблице:

| PK Table      | Cardinality PK Table | FK Table             | Cardinality FK Table | Relationship |
| ------------- | -------------------- | -------------------- | -------------------- | ------------ |
| shippers      | Zero-or-One          | orders               |  One-or-Many         | One-to-Many  |
| employees     |                      | orders               |                      |              |
| employees     |                      | employees            |                      |              |
| employees     | -                    | territories          | -                    |              |
| customers     |                      | orders               |                      |              |
| customers     | -                    | customerdemographics | -                    |              |
| orders        |                      | orderdetails         |                      |              |
| products      |                      | orderdetails         |                      |              |
| suppliers     |                      | products             |                      |              |
| categories    |                      | products             |                      |              |
| region        |                      | territories          |                      |              |

4. Выясните тип отношений между таблицами базы данных. Заполните колонку Relationship в таблице выше. Используйте статью [Many-to-Many Relationship in the Northwind database](http://blog.codeontime.com/2012/04/many-to-many-relationship-in-northwind.html), чтобы найти связи типа "многие-ко-многим".


### Задание 4. Сервис Northwind.

Научитесь использовать демонстрационный OData-сервис Northwind с помощью Postman.

#### Выполнение

1. Создайте в Postman новую коллекцию с именем Northwind, в этой коллекции создайте такие запросы к [Northwind OData Service](https://services.odata.org/V3/Northwind/Northwind.svc/), которые будут удовлетворять описанию из таблицы ниже. После проверки запроса, занесите необходимые параметры в таблицу:

| Query Description                                                 | HTTP Verb | Url                        |
| ----------------------------------------------------------------- | --------- | -------------------------- |
| Get service metadata.                                             | GET       | /$metadata                 |
| Get all customers.                                                |           |                            |
| Get a customer with "ALFKI" id.                                   |           |                            |
| Get all orders.                                                   |           |                            |
| Get an order with "10248" id.                                     |           |                            |
| Get all orders for a customer with "ANATR" id.                    |           |                            |
| Get a customer for an order with "10248" id.                      |           |                            |
| Get a customer for an order with "10248" id.                      |           |                            |
| Get all customers from Germany.                                   |           |                            |
| Get all orders shipped to France in 1997.                         |           |                            |
| Get all products with units in stock less than 20.                |           |                            |
| Get all orders shipped by company "Speedy Express".               |           |                            |
| Get all orders shipped to UK with employees.                      |           |                            |

Создайте самостоятельно еще минимум 5 сложных запросов и запишите их в таблицу.

#### Дополнительные материалы

* OData REST API — мелкие хитрости ([часть 1](https://habr.com/ru/company/databoom/blog/262937/), [часть 2](https://habr.com/ru/company/databoom/blog/263167/), [часть 3](https://habr.com/ru/company/databoom/blog/263435/)).

# Northwind Applications

## Работа с базой данных

### MS SQL LocalDB

1. Запустите _Visual Studio Installer_ и установите LocalDB:

![Visual Studio Installer - SQL Server Express 2016 LocalDB](visual-studio-install-localdb.png)

2. Откройте вкладку _SQL Server Object Explorer_ и создайте новую базу данных _Northwind_.

![Add New Database in SQL Server Object Explorer](visual-studio-sql-server-object-explorer-add-new.png)

3. Выберите _New Query_ в меню БД:

![New Query in SQL Server Object Explorer](visual-studio-new-query.png)

4. Вставьте текст скрипта для создания БД ([instnwnd.sql](https://github.com/microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs)) в окно запроса, выберите БД _Northwind_ и запустите скрипт на выполнение (_Execute_). Начало скрипта нужно пропутить до первого выражения CREATE TABLE.

![Execute Query](visual-studio-execute-query.png)

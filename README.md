# AddressWebApi

>Веб-сервис получает от пользователя адрес, запросом к сервису DaData (https://dadata.ru/api/clean/address/) выполняет его стандартизацию, затем возвращает полученный ответ пользователю в виде модели с определенным набором полей. 
Стандартизация адресов в этом сервисе разбивает адрес по КЛАДР / ФИАС (ГАР), затем определяет индекс, координаты, район города и другую полезную информацию. 

При разработке веб-сервиса применялись: 
`ASP .Net Core Web Api`, `AutoMapper`, `Swagger`, `Serilog`.

Ключевыми являются классы:
1. `Program.cs` - конфигурируются сервисы и формируется конвейер обработки запросов.
По сути вся работа веб-сервиса заключена в get-методе, в котором отправляется строка адреса на `AddressDetailsService`.

2. `AddressDetailsService.cs` - сервис для пробрасывания адреса в метод класса `DaDataClient.cs` и меппинга ответа - объекта типа `Address` (который имеет множество свойств) в объект `AddressResponse` (с несколькими свойствами).
Для меппинга установлен nuget-пакет `AutoMapper`, и в классе `AppMappingProfile` настраивается правила меппинга при несовпадении свойств source-класса (`Address`) и destination-класса (`AddressResponse`).

3. `DaDataClient.cs` - по сути клиент для сервиса DaData для получения стандартного адреса `GetStandartAddress`-методом.
В нем формируется объект `HttpRequestMessage`, добавляются `Headers`: Token и X-Secret.
Подготавливается Content, а т.к. сервер принимает список строк, то сериализация в список адресов.
Из `httpClientFactory` создается объект `httpClient` и `SendAsync` запрос.
Полученный ответ - json строка со списком адресов типа `Address`.

Сама структура класса `Address` была получена с помощью конвертера https://json2csharp.com/, 
т.е. предварительно была получена строка с DaData-сервиса 
`return await response.Content.ReadAsStringAsync()`.


В веб-сервере реализовано:
1. Регистрация сервисов в `ConfigureServices()`-методе `Program.cs`, и в `DaDataClient` и `AddressDetailsService` применение DI, в конструкторах классов.

2. Токен, секретный ключ и url DaData-api содержатся в подключенном файле конфигурации AddressApiConfig.json.
Параметры конфигурации биндятся на свойства класса `AddressApiConfig.cs`, который регистрируем в сервисах.
А в `DaDataClient` эти параметры используются через DI через объект типа `IOptions<AddressApiConfig>`.

3. Настройки логгирования `Serilog` в `appsettings.json`, в Console и в txt-файле Logs-папки проекта.
Подключаем соответствующие nuget-пакеты Serilog. 
В `AddressDetailsService` через DI используется logger типа `ILogger<AddressDetailsService>` и логгируются:
"!!! Request of details for address has been sent",
"!!! Response was received".

4. Обработка кросс-доменных запросов CORS политика - принимаются запросы с любого адреса
`app.UseCors(builder => builder.AllowAnyOrigin())`.

5. Обработка ошибок с помощью middleware `UseExceptionHandler`.

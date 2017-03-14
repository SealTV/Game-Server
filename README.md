# README #

Сервер для тестового задания от Nival.
Сервер запускается по адресу 127.0.0.1:3990 и ждёт подключения клиентов.

Сервер пишет логи в консоль и в файл с логами.

В работе использовался TCP протокол для обмена сообщениями.
Реализовал отдельный проект (библиотеку)  к кодом пакетов и их бинарной сериализации. 
Эта библиотека используется и на сервере и на клиенте. 
Все расчёты позиций юнитов происходят на клиенте. 

Сервер поддерживает подключение нескольких клиентов. Для каждого нового подключения создаётся новый асинхронный инстанс клиента. Так же для каждого отдельного подключения в отдельном потоке запускается процесс отвечающий за просчёт игрового мира.
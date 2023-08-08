﻿#region WebApi
// Copyright (c) 2021 by Serhiy Lakas (https://github.com/sabatex) 
// version 1.0.1

function GetConnectionConfig()
	config = new structure;
	config.Insert("host",Константы.SabatexAPIHost.Получить());
	config.Insert("port",Константы.SabatexAPIPort.Получить());
	config.Insert("SecureConnection",?(Константы.SabatexAPISecureConnection.Получить()=Истина,Новый ЗащищенноеСоединениеOpenSSL( неопределено, неопределено ),Неопределено));
	config.Insert("Password","11111111");
	config.Insert("NodeId","158bcbb1-1c6d-457c-b683-5bc25e754aec");
	Return config;
endfunction	

Функция ФункцияПреобразования(Свойство, Значение, ДополнительныеПараметры, Отказ) Экспорт
 Если ТипЗнч(Значение) = Тип("УникальныйИдентификатор") Тогда
 	Возврат Строка(Значение);
 КонецЕсли;
 Отказ = Истина;
КонецФункции

function POSTJSON(destination,objectId,objectName,stringJSON) export
	config = GetConnectionConfig();
	connection = new HTTPConnection(
		config.host, // сервер (хост)
        config.port, // порт, по умолчанию для http используется 80, для https 443
        , // пользователь для доступа к серверу (если он есть)
        , // пароль для доступа к серверу (если он есть)
        , // здесь указывается прокси, если он есть
        , // таймаут в секундах, 0 или пусто - не устанавливать
        config.SecureConnection // защищенное соединение, если используется https
	);
	request = new HTTPRequest("/api/exchange");
	request.Заголовки.Вставить("Content-Type", "application/json; charset=utf-8");
	request.Заголовки.Вставить("accept","*/*");
endfunction	

function POSTObject(destinations,object) export
	json = new ЗаписьJSON;
	jsonParams = new JSONWriterSettings(ПереносСтрокJSON.Нет,,,,,,true);
	json.УстановитьСтроку(jsonParams);
	СериализаторXDTO.ЗаписатьJSON(json,object,НазначениеТипаXML.Неявное);
	result = json.Закрыть();
	
	return POSTJSON(destinations,""+object.ref.УникальныйИдентификатор(),object.Метаданные().Name,result);
endfunction	

function Login(url,port,nodeName,password)
	config = GetConnectionConfig();
	connection = new HTTPConnection(
		config.host, // сервер (хост)
        config.port, // порт, по умолчанию для http используется 80, для https 443
        , // пользователь для доступа к серверу (если он есть)
        , // пароль для доступа к серверу (если он есть)
        , // здесь указывается прокси, если он есть
        , // таймаут в секундах, 0 или пусто - не устанавливать
        config.SecureConnection // защищенное соединение, если используется https
	);
	request = new HTTPRequest("/api/v0/login");
	request.Заголовки.Вставить("Content-Type", "multipart/formdata; charset=utf-8");
	form = "nodeName="+nodeName+Символы.ПС+"password="+password;
	
	form = new Соответствие;
	form.Вставить("nodeName",nodeName);
	form.Insert("password",password);
	
	
	//request.Заголовки.Вставить("Content-Type", "text; charset=utf-8");

	request.Заголовки.Вставить("accept","*/*");
	
	//r = new structure;
	//r.Insert("senderNode",config.NodeId);
	//r.Insert("password",config.Password);
	//r.Insert("destinationNode",destination);
	//r.Insert("objectId",objectId);
	//r.Insert("objectName",objectName);
	//r.Insert("objectJSON",stringJSON);
	//json = sabatex_json.Write_JSON(r);

	//request.УстановитьТелоИзСтроки(json,"UTF-8",ИспользованиеByteOrderMark.НеИспользовать);
	//response = connection.ОтправитьДляОбработки(request);
	//return response;
	
endfunction	

function HTTPSGet(host,port=443,url,queryParams=null,headers = null) export
	if queryparams <> null then
		url  = url +"?";
		last = false;
		for each item in queryParams do
			if last then
				url = url + "&";
			endif;
			last=true;
			url = url + item.Ключ +"="+item.Значение;
		enddo;
	endif;

	connection = new HTTPConnection(host,port,
        , // пользователь для доступа к серверу (если он есть)
        , // пароль для доступа к серверу (если он есть)
        , // здесь указывается прокси, если он есть
        , // таймаут в секундах, 0 или пусто - не устанавливать
        new ЗащищенноеСоединениеOpenSSL( неопределено, неопределено )
	);
	request = new HTTPRequest(url);
	
	if headers <> null then
		for each header in headers do
			request.Заголовки.Вставить(header.Ключ, header.Значение);
		enddo;
	endif;
	
	response = connection.Получить(request);
	return response;
	
endfunction

function HTTPSPostForm(host,port=443,url,headers=null,formParams=null) export
	connection = new HTTPConnection(host,port,
        , // пользователь для доступа к серверу (если он есть)
        , // пароль для доступа к серверу (если он есть)
        , // здесь указывается прокси, если он есть
        , // таймаут в секундах, 0 или пусто - не устанавливать
        new ЗащищенноеСоединениеOpenSSL( неопределено, неопределено )
	);
	request = new HTTPRequest(url);
	if headers <> null then
		for each header in headers do
			request.Заголовки.Вставить(header.Ключ, header.Значение);
		enddo;
	endif;	
		
	if formParams <> null then
		form = "";
		last = false;
		for each item in formParams do
			if last then
				form = form + "&";
			endif;
			last = true;
			form = form + item.Ключ +"="+item.Значение;
		enddo;	
	endif;
	
	request.УстановитьТелоИзСтроки(form,"UTF-8",ИспользованиеByteOrderMark.НеИспользовать);
	response = connection.ОтправитьДляОбработки(request);
	return response;
endfunction

#endregion
/*!
* Infragistics.Web.MobileUI ListView localization resources 12.2.20122.1021
*
* Copyright (c) 2011-2012 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

$.ig = $.ig || {};

if (!$.ig.mobileListView) {
	$.ig.mobileListView = {};
	$.ig.mobileListViewFiltering = {};
	$.ig.mobileListViewLoadOnDemand = {};
	$.ig.mobileListViewSorting = {};

	$.ig.mobileListView.locale = {
		noSuchWidget: "Компонент не найден: ",
		optionChangeNotSupported: "Изменение этой опции не поддерживается после инициализации igListView:",
		emptyListText: "Список пуст!",
		goBackLabel: "Назад",
		detailsLabel: "Подробнее",
		searchTrayExpandLabel: "Сортировка/Фильтр",
		searchTrayCollapseLabel: "Свернуть"
	};
	$.ig.mobileListViewFiltering.locale = {
		keywordSearchLabel: "",
		keywordAllStateText: "Везде",
		filterPresetsLabel: "Фильтры:",
		searchBarPlaceHolder: "Фильтровать по...",
		filterAllStateText: "Все",
		showLabel: "Показать ",
		cancelButtonLabel: "Отмена",
		clearSearchButtonText: "Очистить"
	};
	$.ig.mobileListViewLoadOnDemand.locale = {
		loadMoreItemsLabel: "Загрузить еще",
		noMoreItemsLabel: "Загружено полностью"
	};
	$.ig.mobileListViewSorting.locale = {
		sortPresetsLabel: "Сортировки:",
		sortDefaultStateText: "По умолчанию",
		sortByLabel: "Сортировать по "
	};

}
/*!
* Infragistics.Web.ClientUI data source localization resources 12.2.20122.1021
*
* Copyright (c) 2011-2012 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

$.ig = $.ig || {};

if (!$.ig.DataSourceLocale) {
	$.ig.DataSourceLocale = {};

	$.extend($.ig.DataSourceLocale, {

		locale: {
			invalidDataSource: "Источник данных неизвестен. Скалярные данные не поддерживаются.",
			unknownDataSource: "Невозможно определить тип источника данных. Установите тип данных: JSON или XML.",
			errorParsingArrays: "Произошла ошибка при обработке массива данных. Схема не соответствует: ",
			errorParsingJson: "Произошла ошибка при обработке данных формата JSON. Схема не соответствует: ",
			errorParsingXml: "Произошла ошибка при обработке данных формата XML. Схема не соответствует: ",
			errorParsingHtmlTable: "Произошла ошибка при извлечении данных из HTML таблицы. Схема не соответствует: ",
			errorExpectedTbodyParameter: "Ожидаемый параметр: tbody или table элемент.",
			errorTableWithIdNotFound: "Не найдена HTML таблица соответствующая идентификатору: ",
			errorParsingHtmlTableNoSchema: "Произошла ошибка при извлечении данных из DOM таблицы: ",
			errorParsingJsonNoSchema: "Произошла ошибка при извлечении данных из JSON текста: ",
			errorParsingXmlNoSchema: "Произошла ошибка при извлечении данных из XML текста: ",
			errorXmlSourceWithoutSchema: "Источник данных является XML документом, но схема для документа не определена ($.IgDataSchema) ",
			errorUnrecognizedFilterCondition: " Предоставленное условие фильтра нераспознаваемо: ",
			errorRemoteRequest: "Удаленный запрос на извлечение данных не удался: ",
			errorSchemaMismatch: "Входные данные не соответствуют схеме, это поле не может быть сопоставлено: ",
			errorSchemaFieldCountMismatch: "Входные данные не соответствуют схеме по числу полей. ",
			errorUnrecognizedResponseType: "Тип ответа указан некорректно или невозможно определить автоматически. Пожалуйста установите settings.responseDataType и/или settings.responseContentType.",
			hierarchicalTablesNotSupported: "Таблицы в HierarchicalSchema не поддерживаются.",
			cannotBuildTemplate: "Шаблон jQuery не может быть сконструирован. В источнике данных отсутствуют записи и колонки неопределены.",
			unrecognizedCondition: "Нераспозноваемое условие фильтровки в следующем выражении: ",
			fieldMismatch: "Следующее выражение содержит ошибочное поле или условие фильтровки: ",
			noSortingFields: "Поля не установлены. Необходимо установить хотя бы одно поле для сортировки перед использованием функции sort().",
			filteringNoSchema: "Схема/поля не установлены. Необходимо установить схему с определением полей и типов перед фильтровкой источника данных."
		}
	});

}

/*!
* Infragistics.Web.ClientUI common DV widget localization resources 12.2.20122.1021
*
* Copyright (c) 2011-2012 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

$.ig = $.ig || {};

if (!$.ig.Chart) {
	$.ig.Chart = {};

	$.extend($.ig.Chart, {

		locale: {
			unsupportedBrowser: "Ваш браузер не поддерживает HTML5 canvas элемент. <br/>Попробуйте воспользоваться следующими версиями браузеров:",
			currentBrowser: "Ваш браузер: {0}",
			ie9: "Microsoft Internet Explorer 9+",
			chrome8: "Google Chrome 8+",
			firefox36: "Mozilla Firefox 3.6+",
			safari5: "Apple Safari 5+",
			opera11: "Opera 11+",
			ieDownload: "http://www.microsoft.com/windows/internet-explorer/default.aspx",
			operaDownload: "http://www.opera.com/download/",
			chromeDownload: "http://www.google.com/chrome",
			firefoxDownload: "http://www.mozilla.com/",
			safariDownload: "http://www.apple.com/safari/download/",
			seriesName: "необходимо установить опцию name при определении серий.",
			axisName: "необходимо установить опцию name при определении осей.",
			close: "Закрыть",
			overview: "Обзор",
			zoomOut: "Уменьшить",
			zoomIn: "Увеличить",
			resetZoom: "Сброс увеличения"
		}
	});

}

/*!
* Infragistics.Web.ClientUI templating localization resources 12.2.20122.1021
*
* Copyright (c) 2011-2012 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

$.ig = $.ig || {};

if (!$.ig.Templating) {
	$.ig.Templating = {};

	$.extend($.ig.Templating, {
		locale: {
			undefinedArgument: 'Произошла ошибка при извлечении значения поля из источника данных: '
		}
	});
}



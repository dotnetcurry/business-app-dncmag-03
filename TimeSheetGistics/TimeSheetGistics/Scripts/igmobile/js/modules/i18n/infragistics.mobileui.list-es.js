﻿/*!
* Infragistics.Web.ClientUI ListView localization resources 12.2.20122.1021
*
* Copyright (c) 2011-2012 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/

/*global jQuery */
$.ig = $.ig || {};

if (!$.ig.mobileListView) {
	$.ig.mobileListView = {};
	$.ig.mobileListViewFiltering = {};
	$.ig.mobileListViewLoadOnDemand = {};
	$.ig.mobileListViewSorting = {};

	$.ig.mobileListView.locale = {
		noSuchWidget: "No se ha cargado ese widget: ",
		optionChangeNotSupported: "No se admite el cambio de la siguiente opción después de crear igListView.",
		emptyListText: "¡No hay elementos de lista!",
		goBackLabel: "Volver",
		detailsLabel: "Detalles",
		searchTrayExpandLabel: "Ordenar/Filtrar",
		searchTrayCollapseLabel: "Contraer"
	};
	$.ig.mobileListViewFiltering.locale = {
		keywordSearchLabel: "",
		keywordAllStateText: "Todos los campos",
		filterPresetsLabel: "Filtrar preestablecidos:",
		searchBarPlaceHolder: "Filtrar elementos...",
		filterAllStateText: "Todos",
		showLabel: "Mostrar ",
		cancelButtonLabel: "Cancelar",
		clearSearchButtonText: "Borrar texto"
	};
	$.ig.mobileListViewLoadOnDemand.locale = {
		loadMoreItemsLabel: "Cargar más elementos",
		noMoreItemsLabel: "No quedan elementos para cargar"
	};
	$.ig.mobileListViewSorting.locale = {
		sortPresetsLabel: "Ordenar preestablecidos:",
		sortDefaultStateText: "Predeterminado",
		sortByLabel: "Ordenar por "
	};

}

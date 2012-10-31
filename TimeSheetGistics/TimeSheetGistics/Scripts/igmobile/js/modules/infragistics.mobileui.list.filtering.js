﻿/*
 * Infragistics.Web.MobileUI ListView Filtering 12.2.20122.1021
 *
 * Copyright (c) 2011-2012 Infragistics Inc.
 *
 * http://www.infragistics.com/
 *
 * Depends on:
 *  jquery-1.6.4.js
 *  jquery.mobile-1.0.1.js
 *  infragistics.mobileui.list.js
 *	infragistics.dataSource.js
 *  infragistics.ui.shared.js
 *	infragistics.util.js
 */
(function($){$.widget("mobile.igListViewFiltering",$.mobile.widget,{css:{filterKeywordArea:"ui-iglist-filter-key-word-area",filterPresets:"ui-iglist-filter-presets",filterScopeOptions:"ui-iglist-keyword-scope-options",preset:"ui-btn-inline ui-iglist-preset",presetSelected:"ui-iglist-preset-selected",presetHidden:"ui-iglist-preset-hidden",keywordFooterText:"ui-iglist-tray-footer-item ig-tray-keyword",presetFooterText:"ui-iglist-tray-footer-item ig-tray-filter-preset",separatorFooterText:"ui-iglist-tray-footer-sep",keywordSearchScopeHidden:"ui-iglist-search-scope-hidden"},options:{type:null,caseSensitive:false,filterExprUrlKey:null,searchBarEnabled:true,searchBarFields:[{text:"",fieldName:"",condition:""}],searchBarFieldName:"",searchBarPlaceHolder:null,searchBarCondition:"contains",filteredFields:[{fieldName:"",searchValue:"",condition:"",logic:"AND"}],filterPresetsLabel:null,filterState:"default",presets:[{text:"",filteredFields:[{fieldName:"",searchValue:"",condition:"",logic:"AND"}]}]},events:{presetChanging:"presetChanging",presetChanged:"presetChanged",keywordChanging:"keywordChanging",keywordChanged:"keywordChanged",scopeChanging:"scopeChanging",scopeChanged:"scopeChanged"},_createWidget:function(){this.options.filteredFields=[];this.options.presets=[];this.options.searchBarFields=[];$.Widget.prototype._createWidget.apply(this,arguments)},_create:function(){var self=this;this._searchScopeTapHandler=function(evnt){var target=self.list._itemFromTarget(evnt.target,"li","idx"),$li,liLeft,liWidth,$div,divWidth,divScrollLeft,newLeft,noCancel,newName,index,fields,field,condition,x,args;if(target&&!target.hasAttribute("scoping")){$li=$(target);index=$li.attr("idx");if(index!=="all"){newName=self.options.searchBarFields[index].fieldName;condition=self.options.searchBarFields[index].condition;if(!condition){fields=self.list.dataSource.schema().fields();if(!fields||fields.length===0){fields=self.list.options.schema.fields}condition="contains";for(x=0;x<fields.length;++x){field=fields[x];if(field.name===newName){if(field.type==="number"||field.type==="date"){condition="equals"}break}}}}else{newName="";condition="contains"}args={scopeField:newName,condition:condition,owner:self};noCancel=self._trigger(self.events.scopeChanging,null,args);if(noCancel){liLeft=$li.offset().left;liWidth=$li.outerWidth();$div=$li.parent().parent();divWidth=$div.outerWidth();divScrollLeft=$div.scrollLeft();$li.attr("scoping",1).addClass(self.css.presetSelected).siblings().attr("scoping",null).removeClass(self.css.presetSelected);self.options.searchBarFieldName=args.scopeField;self.options.searchBarCondition=args.condition;if(liLeft+liWidth>divWidth){newLeft=divScrollLeft+liWidth;$div.animate({scrollLeft:newLeft},function(){self._refreshPresets($div)})}else{if(liLeft<0){newLeft=divScrollLeft-liWidth;$div.animate({scrollLeft:newLeft},function(){self._refreshPresets($div)})}}if(self._filterInput.val()!==""){$.mobile.showPageLoadingMsg();self._shouldFireScopeChanged=true;self._refilter()}else{self._trigger(self.events.scopeChanged,null,{scopeField:newName,owner:self})}}}};this._presetTapHandler=function(evnt){var target=self.list._itemFromTarget(evnt.target,"li","idx"),$li,state,noCancel;if(target&&!target.hasAttribute("filtered")){$li=$(target);state=$li.attr("idx");noCancel=self._trigger(self.events.presetChanging,null,{state:state,owner:self});if(noCancel){self._adjustFilterState(state,$li);$.mobile.showPageLoadingMsg();self._shouldFirePresetChanged=true;self._refilter()}}};this.css.preset=this.css.preset.replace("{0}","c")},_setOption:function(key,value){$.Widget.prototype._setOption.apply(this,arguments);if(key==="type"){throw new Error(this.list._locale("optionChangeNotSupported")+" "+key)}else{if(key==="caseSensitive"){this.list.dataSource.settings.filtering.caseSensitive=this.options.caseSensitive}else{if(key==="filterState"){this._adjustFilterState(value);this._refilter()}}}},_locale:function(key){var val=this.options[key];if(val===undefined||val===null){val=$.ig.mobileListViewFiltering&&$.ig.mobileListViewFiltering.locale?$.ig.mobileListViewFiltering.locale[key]:null}return val||""},_adjustFilterState:function(filterState,li){var self=this,liLeft,liWidth,$div,divWidth,divScrollLeft,newLeft;if(li===null||li===undefined){li=this.list.container().find("."+this.css.filterPresets).find('[idx="'+filterState+'"]')}this.options.filterState=filterState;if(li&&li.length>0){liLeft=li.offset().left;liWidth=li.outerWidth();$div=li.parent().parent();divWidth=$div.outerWidth();divScrollLeft=$div.scrollLeft();li.attr("filtered",1).addClass(this.css.presetSelected).siblings().attr("filtered",null).removeClass(this.css.presetSelected);if(liLeft+liWidth>divWidth){newLeft=divScrollLeft+liWidth;$div.animate({scrollLeft:newLeft},function(){self._refreshPresets($div)})}else{if(liLeft<0){newLeft=divScrollLeft-liWidth;$div.animate({scrollLeft:newLeft},function(){self._refreshPresets($div)})}}}this._adjustTrayText(this.options.presets[this.options.filterState])},_adjustTrayText:function(preset){var showing;if(this._presetSpan){if(preset){showing=this._presetSpan.text()==="";this._presetSpan.text(preset.text).prev().show();if(showing){if(this._presetSpan.prevAll('[data-trayRole="keyword"]').text()){this._presetSpan.prevAll('[data-trayRole="bullet"]').show()}if(this._presetSpan.nextAll('[data-trayRole="keyword"]').text()){this._presetSpan.nextAll('[data-trayRole="bullet"]').show()}this._presetSpan.nextAll('[data-trayRole="empty"]').hide().next().hide()}}else{this._presetSpan.text("").prev().hide().siblings('[data-trayRole="bullet"]').hide();if(this._presetSpan.prevAll('[data-trayRole="keyword"]').text()&&this._presetSpan.nextAll('[data-trayRole="keyword"]').text()){this._presetSpan.prevAll('[data-trayRole="bullet"]').show()}else{this._presetSpan.nextAll('[data-trayRole="empty"]').show().next().show()}}}},_adjustKeywordTrayText:function(val){var showing,prevShown;if(this._keywordSpan){if(val){showing=this._keywordSpan.text()==="";prevShown=showing;this._keywordSpan.text('"'+val+'"');if(showing){this._keywordSpan.nextAll('[data-trayRole="keyword"]').each(function(i){if($(this).text()&&prevShown){$(this).prev().prev().show()}if(!prevShown){prevShown=$(this).text().length>0}});if(prevShown){this._keywordSpan.prev().prev().show()}this._keywordSpan.nextAll('[data-trayRole="empty"]').hide().next().hide()}}else{this._keywordSpan.text("").next().hide();prevShown=false;this._keywordSpan.nextAll('[data-trayRole="keyword"]').each(function(i){if(i>0&&!$(this).text()&&prevShown){$(this).prev().prev().hide();prevShown=false}if(!prevShown){prevShown=$(this).text().length>0}});if(!this._keywordSpan.nextAll('[data-trayRole="keyword"]').text()){this._keywordSpan.nextAll('[data-trayRole="empty"]').show().next().show()}}}},filter:function(fieldExpressions,trayText){this.options.filterState="custom";if(this.options.presets&&this.options.presets.length>0){this.list.container().find("."+this.css.filterPresets).find("."+this.css.presetSelected).removeClass(this.css.presetSelected).attr("filtered",null);this._adjustTrayText(trayText?{text:trayText}:null)}this._refilter(true,null,fieldExpressions)},_refilter:function(renderData,sortedFields,filteredFields){var x,filtering=this.options.filterState,filters=[],index=0,searchBarFields,val,allFilters=[],schema,fields;if(renderData===undefined){renderData=true}for(x=0;x<this.options.filteredFields.length;++x){filters[index++]={fieldName:this.options.filteredFields[x].fieldName,expr:this.options.filteredFields[x].searchValue,cond:this.options.filteredFields[x].condition,logic:this.options.filteredFields[x].logic}}if(filtering!==null&&filtering!==""&&filtering!=="default"&&filtering!=="custom"){filtering=this.options.presets[parseInt(filtering,10)].filteredFields}else{if(filteredFields&&filtering==="custom"){filtering=filteredFields}else{filtering=[]}}for(x=0;x<filtering.length;++x){filters[index++]={fieldName:filtering[x].fieldName,expr:filtering[x].searchValue,cond:filtering[x].condition,logic:filtering[x].logic}}if(this.options.searchBarEnabled){val=this._filterInput?this._filterInput.val():null;if(this.options.searchBarFieldName===null||this.options.searchBarFieldName.length===0){schema=this.list.dataSource.schema();fields=schema?schema.fields():null;if(!fields||fields.length===0){fields=this.list.options.schema}if(!(this.options.type==="remote"&&this.options.filterExprUrlKey)){for(x=0;x<fields.length&&val;++x){if(!(schema&&schema.schema&&schema.schema.childDataProperty&&schema.schema.childDataProperty.name===fields[x].name)&&fields[x].name!==this.list.options.bindings.isDividerKey&&!(this.list.options.childLayout&&this.list.options.childLayout.key===fields[x].name)){allFilters[allFilters.length]={fieldName:fields[x].name,expr:val,cond:this.options.searchBarCondition,logic:"or"}}}}else{if(val){this._needsAll=true}else{delete this._needsAll}}}else{searchBarFields=this.options.searchBarFieldName.split(",");for(x=0;x<searchBarFields.length&&val;++x){filters[index++]={fieldName:searchBarFields[x],expr:val,cond:this.options.searchBarCondition,logic:"or"}}delete this._needsAll}}delete this._dividers;if(this.list.dataSource.settings.sorting.enabled&&((sortedFields&&sortedFields.length>0)||(!sortedFields&&this.list.dataSource.settings.sorting.expressions.length>0))&&this._filterDividers){if(this.options.type==="local"||!this.options.filterExprUrlKey||(filters.length===0&&allFilters.length===0)){filters=this._filterDividers.concat(filters)}else{this._dividers="out"}}else{if(filters.length>0&&this._keepDividers){if(this.options.type==="local"||!this.options.filterExprUrlKey){filters=this._keepDividers.concat(filters)}else{this._dividers="keep"}}if(allFilters.length>0&&this._keepDividersAll&&(this.options.type==="local"||filters.length===0)){if(this.options.type==="local"||!this.options.filterExprUrlKey){allFilters=this._keepDividersAll.concat(allFilters)}else{this._dividers="keep"}}}this.list.dataSource.settings.filtering.expressions=filters.concat(allFilters);this.list.dataSource.settings.paging.appendPage=false;this.list.dataSource.settings.paging.pageIndex=0;this.list._requireRecordsClear=true;if(this.options.type==="local"){if(filters.length>0||allFilters.length>0){this.list.dataSource.filter(filters,"AND",true,allFilters)}else{this.list.dataSource.clearLocalFilter()}if(this.list.dataSource.settings.sorting.type==="local"&&this.list.dataSource.settings.sorting.expressions.length>0&&!sortedFields){this.list.dataSource.sort(this.list.dataSource.settings.sorting.expressions,"asc")}if(renderData){this.list._renderData()}this.list.dataSource.settings.paging.appendPage=true}else{if(renderData){this.list.dataSource.dataBind()}}},_renderingTray:function(e,ui){var self=this,wrapper,x,index,searchScope,insertedSearch,search,$this,val,noCancel;if(e.target.id!==this.list.element[0].id){return}if(this.options.searchBarEnabled){if(!ui.needTray){ui.needTray=1}ui.css=(ui.css+"-keyword").replace("-one-keyword","-keyword-one");wrapper=$("<div />").addClass(this.css.filterKeywordArea).append("<span>"+this._locale("keywordSearchLabel")+"</span>");search=this._filterInput=$("<input>",{placeholder:this._locale("searchBarPlaceHolder")}).jqmData("lastval","").attr("data-"+$.mobile.ns+"type","search").bind("keyup change",function(){$this=$(this);val=this.value;if(val!==$this.jqmData("lastVal")){noCancel=self._trigger(self.events.keywordChanging,null,{value:val,owner:self});if(noCancel){$.mobile.showPageLoadingMsg();self._shouldFireKeywordChanged=true;self._adjustKeywordTrayText(val);if(self._expr&&self.options.type==="remote"){self._filterNew=val}else{self._expr=val;self._refilter()}}if(searchScope){if(val.length>0){noCancel=searchScope.hasClass(self.css.keywordSearchScopeHidden);searchScope.removeClass(self.css.keywordSearchScopeHidden);searchScope.parent().parent().parent()[0].className=searchScope.parent().parent().parent()[0].className.replace("-keyword","-keyword-scope");if(noCancel){self._refreshPresets(searchScope)}}else{if(val.length===0){searchScope.addClass(self.css.keywordSearchScopeHidden);searchScope.parent().parent().parent()[0].className=searchScope.parent().parent().parent()[0].className.replace("-scope","")}}if(!this._setScopeWidths){searchScope.find("li").each(function(){var $this=$(this);$this.width($this.outerWidth())});this._setScopeWidths=true}}}$this.jqmData("lastVal",val)}).appendTo(wrapper).textinput({clearSearchButtonText:this._locale("clearSearchButtonText")});if(this.options.searchBarFields.length>0){wrapper.append("<div class='"+this.css.filterScopeOptions+"'><ul class='"+this.css.keywordSearchScopeHidden+"'></ul></div>").find("ul").bind("tap",this._searchScopeTapHandler).append("<li class='"+this.css.preset+" "+(!this.options.searchBarFieldName?this.css.presetSelected:"")+"' idx='all'>"+this._locale("keywordAllStateText")+"</li>");searchScope=wrapper=wrapper.find("ul");for(x=0;x<this.options.searchBarFields.length;++x){wrapper.append($("<li class='"+this.css.preset+"' idx='"+x+"'>"+this.options.searchBarFields[x].text+"</li>"));if(this.options.searchBarFields[x].fieldName===this.options.searchBarFieldName&&(this.options.searchBarFields[x].condition===this.options.searchBarCondition||(this.options.searchBarCondition==="contains"&&!this.options.searchBarFields[x].condition))){wrapper.children().removeClass(this.css.presetSelected).last().addClass(this.css.presetSelected)}}wrapper=wrapper.parent().bind({scroll:function(evnt){self._refreshPresets()}});if(wrapper.scrollview){wrapper.scrollview()}else{if(wrapper.igScroll){wrapper.igScroll({cancelStart:true})}}wrapper=wrapper.parent()}if(!ui.tray){ui.tray=$("<div />");ui.tray.append(wrapper)}else{wrapper.prependTo(ui.tray)}insertedSearch=true}if(this.options.presets&&this.options.presets.length>0){if(ui.needTray!==2){ui.needTray=2}if(ui.css.indexOf("one")>-1){ui.css=ui.css.replace("one","two")}else{ui.css=ui.css+"-one"}wrapper=$("<div />").addClass(this.css.filterPresets).append("<span>"+this._locale("filterPresetsLabel")+"</span>").append("<div><ul></ul></div>").find("ul").bind("click",this._presetTapHandler);wrapper.append("<li class='"+this.css.preset+"' idx='default'>"+this._locale("filterAllStateText")+"</li>");for(x=0;x<this.options.presets.length;++x){wrapper.append($("<li class='"+this.css.preset+"' idx='"+x+"'>"+this.options.presets[x].text+"</li>"))}index=parseInt(this.options.filterState,10)+1;if(isNaN(index)){index=0}wrapper.children().eq(index).addClass(this.css.presetSelected);wrapper=wrapper.parent().bind({scroll:function(evnt){self._refreshPresets()}});if(wrapper.scrollview){wrapper.scrollview()}else{if(wrapper.igScroll){wrapper.igScroll({cancelStart:true})}}wrapper=wrapper.parent();if(!ui.tray){ui.tray=$("<div />");wrapper.appendTo(ui.tray)}else{if(insertedSearch){wrapper.insertAfter(ui.tray.children().eq(0))}else{wrapper.prependTo(ui.tray)}}}},_renderedTray:function(e,ui){var self=this,$win=$(window),noCancel,needRendering=false;if((this.options.presets&&this.options.presets.length>0)||(this.options.searchBarEnabled&&this.options.searchBarFields&&this.options.searchBarFields.length>0)){this._refreshPresets();$win.bind("resize",function(){self._refreshPresets()});$win.bind("orientationchange",function(){self._refreshPresets()})}if(this.options.searchBarEnabled&&ui.needTray===2){$("<div>").text(this._locale("cancelButtonLabel")).buttonMarkup({inline:true}).insertAfter(this._filterInput.parent().css("display","inline-block")).bind("click",function(evnt){var needToBind;if(self._filterInput.val()!==""){noCancel=self._trigger(self.events.keywordChanging,null,{value:"",owner:self});if(noCancel){self._filterInput.val("");self._filterInput.jqmData("lastVal","");self._adjustKeywordTrayText("");self._shouldFireKeywordChanged=true;needRendering=true}}if(self.options.filterState!=="default"){noCancel=self._trigger(self.events.presetChanging,null,{state:"default",owner:self});if(noCancel){self._adjustFilterState("default");self._shouldFirePresetChanged=true;needRendering=true}}if(needRendering){$.mobile.showPageLoadingMsg();self._refilter(false)}needToBind=self._trigger("_cancelButtonClicked");self.list.toggleSearchArea();if(needToBind&&needRendering){if(self.options.type==="remote"){self.list.dataSource.dataBind()}else{self.list._renderData()}}})}},_refreshPresets:function(parent){var self=this,activePage=$.mobile.activePage,listPage=this.list.element.closest(".ui-page");if(activePage&&activePage[0]===listPage[0]){if(!parent){parent=this.list.container().find("."+this.css.filterPresets).parent()}$(parent).find("li").removeClass(this.css.presetHidden).each(function(i,val){var $div=$(val.parentNode.parentNode),$li=$(val),liLeft=$li.offset().left,liWidth=$li.width(),divWidth=$div.outerWidth(),divLeft=Math.min(0,$div.offset().left);if(liLeft+liWidth>divWidth+divLeft||liLeft<divLeft){$li.addClass(self.css.presetHidden)}})}},_renderingTrayFooterBar:function(e,ui){var wrapper=$("<div>");if(this.options.searchBarEnabled){wrapper.append(this._keywordSpan=$("<span data-trayRole='keyword' class='"+this.css.keywordFooterText+"'></span>").text(this._filterInput.val())).append("<span style='display:none;' class='"+this.css.separatorFooterText+"' data-trayRole='bullet'> &bull; </span>")}if(this.options.presets&&this.options.presets.length>0){wrapper.append("<span class='"+this.css.separatorFooterText+"' style='display:none;'> "+this._locale("showLabel")+" </span>").append(this._presetSpan=$("<span data-trayRole='keyword' class='"+this.css.presetFooterText+"'></span>"));if(this.options.filterState!=="default"){this._presetSpan.text(this.options.presets[this.options.filterState].text).prev().show();if(this._keywordSpan&&this._keywordSpan.text()){this._keywordSpan.next().show()}}}if(ui.trayFooter.children().length>0){wrapper.children().prependTo(ui.trayFooter)}else{ui.trayFooter.append(wrapper.children())}if(this.options.filterState!=="default"&&this._presetSpan.nextAll('[data-trayRole="keyword"]').text()){this._presetSpan.next().show()}},_dataRendering:function(e,ui){if(this.list._dataBinding&&this.options.type==="local"){this._refilter(false)}if(this._filterNew&&this._expr!==this._filterNew){this._expr=this._filterNew;this._refilter();return false}},_itemsRendering:function(e,ui){delete this._expr;delete this._filterNew},_itemsRendered:function(e,ui){if(this._shouldFirePresetChanged){this._trigger(this.events.presetChanged,null,{state:this.options.filterState,owner:this});this._shouldFirePresetChanged=false}if(this._shouldFireKeywordChanged){this._trigger(this.events.keywordChanged,null,{value:this._filterInput.val(),owner:this});this._shouldFireKeywordChanged=false}if(this._shouldFireScopeChanged){this._trigger(this.events.scopeChanged,null,{scopeField:this.options.searchBarFieldName,owner:this});this._shouldFireScopeChanged=false}$.mobile.hidePageLoadingMsg()},_sortChanging:function(e,ui){if(this._keepDividers&&this._filterDividers){this._refilter(false,ui.sortedFields);ui.handled=true}},_encodeExtraParams:function(dataSource,params){if(this._dividers){params.filteringParams.dividers=this._dividers}if(this._needsAll){params.filteringParams.allCondition=this.options.searchBarCondition;params.filteringParams.allValue=this._filterInput.val()}},_createHandlers:function(){this._renderingTrayHandler=$.proxy(this._renderingTray,this);this._renderedTrayHandler=$.proxy(this._renderedTray,this);this._renderingTrayFooterBarHandler=$.proxy(this._renderingTrayFooterBar,this);this._dataRenderingHandler=$.proxy(this._dataRendering,this);this._itemsRenderingHandler=$.proxy(this._itemsRendering,this);this._itemsRenderedHandler=$.proxy(this._itemsRendered,this);this._sortChangingHandler=$.proxy(this._sortChanging,this)},_registerEvents:function(){this.list.element.bind("iglistrenderingtray",this._renderingTrayHandler);this.list.element.bind("iglistrenderedtray",this._renderedTrayHandler);this.list.element.bind("iglistrenderingtrayfooterbar",this._renderingTrayFooterBarHandler);this.list.element.bind("iglistdatarendering",this._dataRenderingHandler);this.list.element.bind("iglistitemsrendering",this._itemsRenderingHandler);this.list.element.bind("iglistitemsrendered",this._itemsRenderedHandler);this.list.element.bind("iglistviewsorting_sortchanging",this._sortChangingHandler)},_unregisterEvents:function(){this.list.element.unbind("iglistrenderingtray",this._renderingTrayHandler);this.list.element.unbind("iglistrenderedtray",this._renderedTrayHandler);this.list.element.unbind("iglistrenderingtrayfooterbar",this._renderingTrayFooterBarHandler);this.list.element.unbind("iglistdatarendering",this._dataRenderingHandler);this.list.element.unbind("iglistitemsrendering",this._itemsRenderingHandler);this.list.element.unbind("iglistitemsrendered",this._itemsRenderedHandler);this.list.element.unbind("iglistviewsorting_sortchanging",this._sortChangingHandler);delete this._renderingTrayHandler;delete this._renderedTrayHandler;delete this._renderingTrayFooterBarHandler;delete this._dataRenderingHandler;delete this._itemsRenderedHandler;delete this._sortChangingHandler},_injectList:function(listInstance,isRebind){var x,presets,filteredFields=[];this.list=listInstance;if(this.options.type===null){this.options.type=this.list._inferOpType()}if(this.options.type){this.list.dataSource.settings.filtering.type=this.options.type}else{this.list.dataSource.settings.filtering.type="remote"}if(this.options.filterExprUrlKey){this.list.dataSource.settings.filtering.filterExprUrlKey=this.options.filterExprUrlKey;this._encodeExtraParamsHandler=$.proxy(this._encodeExtraParams,this);this.list.dataSource.settings.urlParamsEncoded=this._encodeExtraParamsHandler}this.list.dataSource.settings.filtering.caseSensitive=this.options.caseSensitive;presets=this.options.filteredFields;if(this.list.options.bindings&&this.list.options.bindings.isDividerKey){this._keepDividers=[];this._keepDividers[0]={fieldName:this.list.options.bindings.isDividerKey,cond:"true",logic:"Or"};this._keepDividersAll=[];this._keepDividersAll[0]={fieldName:this.list.options.bindings.isDividerKey,cond:"equals",expr:"true",logic:"Or"};this._filterDividers=[];this._filterDividers[0]={fieldName:this.list.options.bindings.isDividerKey,cond:"false",logic:"And"}}if(presets&&presets.length>0){for(x=0;x<presets.length;++x){filteredFields[filteredFields.length]={fieldName:presets[x].fieldName,cond:presets[x].condition,expr:presets[x].searchValue}}}if(this.options.filterState!=="default"&&this.options.presets){presets=this.options.presets[this.options.filterState].filteredFields}if(presets&&presets.length>0){for(x=0;x<presets.length;++x){filteredFields[filteredFields.length]={fieldName:presets[x].fieldName,cond:presets[x].condition,expr:presets[x].searchValue}}}if(this.list.dataSource.settings.sorting.enabled&&this.list.dataSource.settings.sorting.defaultFields.length>0&&this._filterDividers){filteredFields=this._filterDividers.concat(filteredFields)}else{if(filteredFields.length>0&&this._keepDividers){filteredFields=this._keepDividers.concat(filteredFields)}}if(filteredFields.length>0){this.list.dataSource.settings.filtering.expressions=filteredFields}this._createHandlers();this._registerEvents()},destroy:function(){if(this._presetSpan){this._presetSpan.prev().remove();this._presetSpan.prev().remove();this._presetSpan.remove();delete this._presetSpan}if(this._keywordSpan){this._keywordSpan.remove();delete this._keywordSpan}if(this._filterInput){if(this._filterInput.data("textinput")){this._filterInput.data("textinput").destroy()}this._filterInput.parent().parent().remove();delete this._filterInput}this.list.container().find("."+this.css.filterPresets).remove();this._unregisterEvents();$.Widget.prototype.destroy.call(this);return this}});function _addDash(c){return"-"+c.toLowerCase()}function _fixSearchValue(searchValue){var temp=searchValue;try{searchValue=eval(searchValue)}catch(ex){searchValue=temp}if(searchValue===undefined&&temp){searchValue=temp}return searchValue}$(document).bind("_igListViewHtmlOptions",function(evnt,args){var elem=args.element,filtering,option,value,x,i;if(elem.jqmData("filtering")===true){if(!args.options.features){args.options.features=[]}filtering=args.options.features[args.options.features.length]={};filtering.name="Filtering";for(option in $.mobile.igListViewFiltering.prototype.options){if($.mobile.igListViewFiltering.prototype.options.hasOwnProperty(option)){value=elem.jqmData("filtering-"+option.replace(/[A-Z]/g,_addDash));if(value!==undefined){if(option==="presets"){for(x=0;x<value.length;++x){if(value[x]&&value[x].filteredFields){for(i=0;i<value[x].filteredFields.length;++i){value[x].filteredFields[i].searchValue=_fixSearchValue(value[x].filteredFields[i].searchValue)}}}}else{if(option==="filteredFields"){for(x=0;x<value.length;++x){if(value[x]){value[x].searchValue=_fixSearchValue(value[x].searchValue)}}}}filtering[option]=value}}}}});if(typeof define==="function"&&define.amd&&define.amd.jQuery){define("ig.mobileui.list.filtering",["ig.mobileui.list"],function(){return $.mobile.igListViewFiltering})}$.extend($.mobile.igListViewFiltering,{version:"12.2.20122.1021"})}(jQuery));(function(a){a(document).ready(function(){var b=a("#__ig_wm__").length>0?a("#__ig_wm__"):a('<div id="__ig_wm__"></div>').appendTo(document.body);b.css({position:"fixed",bottom:0,right:0,zIndex:1000}).addClass("ui-igtrialwatermark")})}(jQuery));
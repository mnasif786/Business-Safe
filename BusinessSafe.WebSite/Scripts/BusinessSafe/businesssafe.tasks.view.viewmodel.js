var taskViewViewModelLoader = function () {

var getUrl = function (data) {
        var url = '';
        if (data.taskType === 'gra') {
            url = window.globalajaxurls.getGeneralRiskAssessmentViewFurtherControlMeasureTaskUrl;
        }

        if (data.taskType === 'hsra') {
            url = window.globalajaxurls.getHazardousSubstanceRiskAssessmentViewFurtherControlMeasureTaskUrl;
        }

        if (data.taskType === 'pra') {
            url = window.globalajaxurls.getPersonalRiskAssessmentViewFurtherControlMeasureTaskUrl;
        }

        if (data.taskType === 'fra') {
            url = window.globalajaxurls.getFireRiskAssessmentViewFurtherControlMeasureTaskUrl;
        }

        if (data.taskType === 'action') {
            url = window.globalajaxurls.getActionsCompletedTaskViewUrl;
        }

        if (data.taskType === 'responsibility') {
            url = window.globalajaxurls.getResponsibilityViewTaskUrl;
        }

        return url;
    };

    var getTitle = function (data) {

        var title = '';
        if (data.taskType === 'gra') {
            title = 'Further Control Measure Task';
        }

        if (data.taskType === 'hsra') {
            title = 'Further Control Measure Task';
        }

        if (data.taskType === 'pra') {
            title = 'Further Control Measure Task';
        }

        if (data.taskType === 'fra') {
            title = 'Further Control Measure Task';
        }

        if (data.taskType === 'responsibility') {
            title = 'Responsibility Task';
        }

        if (data.taskType === 'action') {
            title = 'Action Task';
        }
        return title;
    };
    
    function load(data) {

        var url = getUrl(data);
        var title = getTitle(data);
        
        var callback = function (callbackData) {
            
            new TaskViewViewModel().initialize(callbackData, title);
        };
        
        AjaxCall.execute(url, callback, data, "GET");
    }

    return {
        load: load
    };

}();

function TaskViewViewModel(){
    
    var selectors = {
        dialog : "#dialogFurtherControlMeasureTask, #TaskDialog:first"
    };

    var showDialog = function(callbackData, title){

        $(selectors.dialog).empty().dialog({
            title: title,
            autoOpen: false,
            width: 820,
            modal: true,
            resizable: false,
            draggable: false
        }).append(callbackData).dialog('open');

    };

    this.initialize = function(callbackData, title){

        showDialog(callbackData, title);
        initialiseCalendar();       
        
    };

}
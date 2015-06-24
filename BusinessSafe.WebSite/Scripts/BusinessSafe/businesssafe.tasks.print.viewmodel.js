var taskPrintViewmodelLoader = function () {

    var getUrl = function (data) {

        var url = '';
        if (data.taskType === 'gra') {
            url = window.globalajaxurls.getGeneralRiskAssessmentPrintFurtherControlMeasureTaskUrl;
        }

        if (data.taskType === 'hsra') {
            url = window.globalajaxurls.getHazardousSubstanceRiskAssessmentPrintFurtherControlMeasureTaskUrl;
        }

        if (data.taskType === 'pra') {
            url = window.globalajaxurls.getPersonalRiskAssessmentPrintFurtherControlMeasureTaskUrl;
        }

        if (data.taskType === 'fra') {
            url = window.globalajaxurls.getFireRiskAssessmentPrintFurtherControlMeasureTaskUrl;
        }

        return url;
    };

    var load = function (data) {
        var url = getUrl(data);

        var callback = function (callbackData) {
            new TaskPrintViewModel().initialize(callbackData);
        };

        AjaxCall.execute(url, callback, data, "GET");

    };

    return {
        load: load
    };
} ();

function TaskPrintViewModel() {
    var selectors = {
        dialog: "#TaskDialog",
        printButton: "#FurtherActionTaskPrintButton",
        cancelButton: "#FurtherActionTaskCancelButton"
    };

    var hookEvents = function() {
        $(selectors.cancelButton).click(function(event) {
            event.preventDefault();
            $(selectors.dialog).dialog('close');
        });

        $(selectors.printButton).click(function(event) {
            event.preventDefault();
            window.print();
        });
    };

    var showDialog = function (callbackData) {

        $(selectors.dialog).empty().dialog({
            autoOpen: false,
            width: 820,
            modal: true,
            resizable: false,
            draggable: false
        }).append(callbackData).dialog('open');
    };

    this.initialize = function(callbackData) {
        showDialog(callbackData);
        hookEvents();
    };
}

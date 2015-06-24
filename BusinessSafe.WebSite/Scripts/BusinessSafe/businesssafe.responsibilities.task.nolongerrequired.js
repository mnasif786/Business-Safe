
var TaskNoLongerRequiredModule = function(callBack) {

    var self = this;

    var selectors = {
        dialog: "#dialogNoLongerRequired",
        TaskId: "#TaskId",
        companyId: "#CompanyId"
    };

    var showDialog = function() {

        $(selectors.dialog).dialog({
            buttons: {
                "Confirm": function() {
                    markTaskAsNoLongerRequired();
                },
                "Cancel": function() {
                    $(this).dialog("close");
                }
            },
            resizable: false
        });

    };

    var markTaskAsNoLongerRequired = function() {

        var data = {
            TaskId: $(selectors.TaskId).val(),
            companyId: $(selectors.companyId).val(),
            status: "No Longer Required"
        };

        var successfulCallBack = function() {
            $(selectors.dialog).dialog("close");
            callBack();
        };

        var url = window.globalajaxurls.markResponsibilityTaskAsNoLongerRequiredUrl;
        AjaxCall.execute(url, successfulCallBack, data, "POST");

    };

    this.initialize = function(){        
        showDialog();        
    };
}
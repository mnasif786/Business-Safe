
function TaskNoLongerRequiredViewModel(callBack){

    var self = this;
    
    var selectors = {
        dialog: "#dialogNoLongerRequired",
        furtherControlMeasureTaskId : "#FurtherControlMeasureTaskId",
        companyId : "#CompanyId"
    };    

    var showDialog = function(){

        $(selectors.dialog).dialog({
            buttons: {
                "Confirm": function () {
                    markTaskAsNoLongerRequired();
                },
                "Cancel": function () {
                    $(this).dialog("close");
                }
            },
            resizable: false
        });

    };

    var markTaskAsNoLongerRequired = function(){

        var data = {
            furtherControlMeasureTaskId: $(selectors.furtherControlMeasureTaskId).val(),
            companyId: $(selectors.companyId).val(),
            status: "No Longer Required"
        };

        var successfulCallBack = function () {
            $(selectors.dialog).dialog("close");
            callBack();
        };

        var url = window.globalajaxurls.markFurtherActionTaskAsNoLongerRequiredUrl;
        AjaxCall.execute(url, successfulCallBack, data , "POST");            
        
    };   

    this.initialize = function(){        
        showDialog();        
    };
}
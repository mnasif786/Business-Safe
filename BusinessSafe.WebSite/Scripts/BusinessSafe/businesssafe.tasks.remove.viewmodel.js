function TaskRemoveViewModel(taskDataViewModel){

    var self = this;
    
    var selectors = {
        dialogDeleteFurtherControlMeasureTask: "#dialogDeleteFurtherControlMeasureTask",
        dialogDeleteReoccurringFurtherControlMeasureTask: "#dialogDeleteReoccurringFurtherControlMeasureTask",
        dialogDeleteResponsibilityTask: "#dialogDeleteResponsibilityTask"
    };    

    var showDialog = function(){

        var dialog;

        var deleteData = taskDataViewModel.furtherControlMeasureData();
        
        var selector = selectors.dialogDeleteFurtherControlMeasureTask;
        
        if(deleteData.taskType=="responsibility") {
            selector = selectors.dialogDeleteResponsibilityTask;
        }

        if (deleteData.IsReoccurring == 1) {
            dialog = $(selectors.dialogDeleteReoccurringFurtherControlMeasureTask);
        } else {
            dialog = $(selector);
        }

        $(dialog).dialog({
            buttons: {
                "Confirm": function () {
                    deleteTask(deleteData, this);
                },
                "Cancel": function () {
                    $(this).dialog("close");
                }
            },
            resizable: false
        });

    };

    var deleteTask = function (deleteData, dialog) {

        var successfulCallBack = function (data) {
            $(dialog).dialog("close");

            if (data.Success === true) {
                removeTaskTableRow();

            } else {
                showCanNotDeleteReturnedValidationMessage(data.Message);
            }
        };
        var url = "";
        if (deleteData.taskType == "responsibility") {
            url = window.globalajaxurls.markResponsibilityTaskAsDeletedUrl;
        }
        else {
            url = window.globalajaxurls.markFurtherActionTaskAsDeletedUrl;
        }
        
        AjaxCall.execute(url, successfulCallBack, deleteData, "POST");
    };   

    var showCanNotDeleteReturnedValidationMessage = function(message){

        $(dialogDeleteFurtherControlMeasureTaskResponse).html("<p>" + message + "</p>");
        $(dialogDeleteFurtherControlMeasureTaskResponse).dialog({
            buttons: {
                "OK": function () {
                    $(this).dialog("close");
                }
            }
        });
    }; 

    var removeTaskTableRow = function(){
        taskDataViewModel.tableRow().remove();
    };   

    this.initialize = function(){        
        showDialog();        
    };
}
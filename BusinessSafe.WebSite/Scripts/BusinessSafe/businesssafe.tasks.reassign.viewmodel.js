var taskReassignViewModelLoader = function () {

    var _data = "";
    var getUrl = function (data) {
        var url = '';
        if (data.taskType === 'gra') {
            url = window.globalajaxurls.getGeneralRiskAssessmentReassignFurtherControlMeasureTaskUrl;
        }

        if (data.taskType === 'hsra') {
            url = window.globalajaxurls.getHazardousSubstanceRiskAssessmentReassignFurtherControlMeasureTaskUrl;
        }

        if (data.taskType === 'pra') {
            url = window.globalajaxurls.getPersonalRiskAssessmentReassignFurtherControlMeasureTaskUrl;
        }

        if (data.taskType === 'fra') {
            url = window.globalajaxurls.getFireRiskAssessmentReassignFurtherControlMeasureTaskUrl;
        }
        if (data.taskType === 'responsibility') {
            url = window.globalajaxurls.getRespobsibilityReassignResponsibilityTaskUrl;
        }
        return url;
    };

    function load(data) {
        _data = data;
        var url = getUrl(data);

        var callback = function (callbackData) {
            new TaskReassignViewModel().initialize(callbackData, _data);
        };

        AjaxCall.execute(url, callback, data, "GET");
    }

    return {
        load: load
    };

} ();

function TaskReassignViewModel(){

    var self = this;

    var selectors = {
        dialog : "#dialogFurtherControlMeasureTask, #TaskDialog",
        reassignTaskToId: "#ReassignTaskToId",
        reassignTaskTo: "#ReassignTaskTo",
        cancelButton: "#FurtherActionTaskCancelButton",
        saveButton: "#FurtherActionTaskSaveButton",
        companyId: "#CompanyId",
        form : "#FurtherActionTask",
        validationSummary : ".validation-summary-errors"  
    };

    var showDialog = function (callbackData, data) {
        var title = "";

        if (data.taskType === 'responsibility') {
            title = "Responsibility Task";
        } else {
            title = "Further Control Measure Task";
        }

        $(selectors.dialog).empty().dialog({
            title: title,
            autoOpen: false,
            width: 820,
            modal: true,
            resizable: false,
            draggable: false
        }).append(callbackData).dialog('open');

    };
    
    var loadReassignDropDown = function(){

        var successfulCallBack = function (result) {
            
            result.splice(0, 0, { label: '--Select Option--', value: '' });
            
            $(selectors.reassignTaskTo).combobox({
                selectedId: $(selectors.taskAssignedToId).val(),
                initialValues: result,
                url: window.globalajaxurls.getEmployees,
                data: {
                    companyId: $(selectors.companyId).val(),
                    pageLimit: 500
                },
                afterSelect: function (event, ui) {
                    var taskAssignedToId = $(selectors.reassignTaskToId).val();
                    new ShowIsSelectedAssignedToEmployeeABusinessSafeUserIndicator(taskAssignedToId, $(selectors.form)).execute();

                    return false;
                }
            });
        };

        var requestData = {
            filter: '',
            companyId: $(selectors.companyId).val(),
            pageLimit: 500
        };

        AjaxCall.execute(window.globalajaxurls.getEmployees, successfulCallBack, requestData);

    };

    var hookCommandButtons = function(){

        $(selectors.cancelButton).live('click', function (event) {
            event.preventDefault();
            $(selectors.dialog).dialog('close');
        });

        $(selectors.saveButton).die();
        $(selectors.saveButton).live('click', function (event) {

            event.preventDefault();

            var saveButton = $(this).attr('disabled', 'disabled').addClass('disabled');

            saveReassign({
                success : function(){
                    closeDialogAndReload();
                },
                error: function(result){
                    saveButton.removeAttr('disabled').removeClass('disabled');
                    showValidationErrors(selectors.validationSummary, result.Errors);
                }
            });

            return;
        });   

    };

    var validateReassign = function () {
        var errors = [];

        $(".input-validation-error").removeClass("input-validation-error");

        var reassignTaskTo = $(selectors.reassignTaskToId);

        if (reassignTaskTo.val() === "") {
            reassignTaskTo.addClass("input-validation-error");
            errors.push("Reassign To is required");
        }

        showValidationErrors(selectors.validationSummary, errors);

        return errors.length === 0;
    };

    var closeDialogAndReload = function(){
        $(selectors.dialog).dialog('close');
        location.reload();
    };    

    var saveReassign = function(callbacks){

        if (validateReassign() === false) {
            return;
        }

        var f = $(selectors.form);

        $.post(f[0].action, f.serialize(), function (result) {
            if (result.Success === true) {               
                callbacks.success();                
            }
            else if (result.Errors !== undefined) {
                callbacks.error(result);
            }

        }).error(function (jqXhr, textStatus, errorThrown) {
            if (jqXhr.status !== 0) {
                if (window.debugErrorHandler === undefined) {
                    window.location.replace(window.globalajaxurls.errorPage);
                } else {
                    alert("DEBUG: AjaxCall.execute encountered a problem.");
                }
            } else {
                window.location.reload();
            }
        });
    };

    this.initialize = function(callbackData, data){
        
        showDialog(callbackData, data);
        loadReassignDropDown();
        hookCommandButtons();
    };

}
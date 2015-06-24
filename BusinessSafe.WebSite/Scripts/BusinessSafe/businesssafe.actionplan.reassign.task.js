var actionTaskReassignViewModelLoader = function () {

    var _data = "";
    var getUrl = function (data) {
        var url = window.globalajaxurls.reAssignActionTaskWithTaskId;
        return url;
    };

    function load(data) {
        _data = data;
        var url = getUrl(data);

        var callback = function (callbackData) {
            new ActionTaskReassignViewModel().initialize(callbackData, _data);
        };

        AjaxCall.execute(url, callback, data, "GET");
    }

    return {
        load: load
    };

} ();

function ActionTaskReassignViewModel() {

    var self = this;

    var selectors = {
        dialog: "#dialogFurtherControlMeasureTask, #TaskDialog",
        assignTaskToId: "#ActionTaskAssignedToId",
        assignTaskTo: "#ActionTaskAssignedTo",
        cancelButton: "#cancelButton",
        saveButton: "#saveButton",
        companyId: "#CompanyId",
        form: "#ActionPlanTasks",
        validationSummary: ".validation-summary-errors",
        taskAssignedToId: "#TaskAssignedToId"
    };

    var actionTaskShowDialog = function (callbackData, data) {
        var title = "Reassign Action Task";                     

        $(selectors.dialog).empty().dialog({
            title: title,
            autoOpen: false,
            width: 820,
            modal: true,
            resizable: false,
            draggable: false
        }).append(callbackData).dialog('open');

    };

    var actionTaskLoadReassignDropDown = function (data) {

        var successfulCallBack = function (result) {

            result.splice(0, 0, { label: '--Select Option--', value: '' });

            $(selectors.assignTaskTo).combobox({
                selectedId: $(selectors.assignTaskToId).val(),
                initialValues: result,
                url: window.globalajaxurls.getEmployees,
                data: {
                    companyId: $(selectors.companyId).val(),
                    pageLimit: 100
                },
                afterSelect: function (event, ui) {
                    var taskAssignedToId = $(selectors.assignTaskToId).val();
                    new ShowIsSelectedAssignedToEmployeeABusinessSafeUserIndicator(taskAssignedToId, $(selectors.form)).execute();

                    return false;
                }
            });
        };

        var requestData = {
            filter: '',
            companyId: $(selectors.companyId).val(),
            pageLimit: 100
        };

        AjaxCall.execute(window.globalajaxurls.getEmployees, successfulCallBack, requestData);

    };

    var actionTaskHookCommandButtons = function () {

        $(selectors.cancelButton).live('click', function (event) {
            event.preventDefault();
            $(selectors.dialog).dialog('close');
        });

        $(selectors.saveButton).die();
        $(selectors.saveButton).live('click', function (event) {

            event.preventDefault();

            var saveButton = $(this).attr('disabled', 'disabled').addClass('disabled');

            actionTaskSaveReassign({
                success: function () {
                    actionTaskCloseDialogAndReload();
                },
                error: function (result) {
                    saveButton.removeAttr('disabled').removeClass('disabled');
                    showValidationErrors(selectors.validationSummary, result.Errors);
                }
            });

            return;
        });

    };

    var actionTaskvalidateReassign = function () {
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

    var actionTaskCloseDialogAndReload = function () {
        $(selectors.dialog).dialog('close');
        location.reload();
    };

    var actionTaskSaveReassign = function (callbacks) {

        if (actionTaskvalidateReassign() === false) {
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

        }).error(function () {
            window.location.replace(window.globalajaxurls.errorPage);
        });
    };

    this.initialize = function (callbackData, data) {

        actionTaskShowDialog(callbackData, data);
        actionTaskLoadReassignDropDown(data);
        actionTaskHookCommandButtons();
    };

}
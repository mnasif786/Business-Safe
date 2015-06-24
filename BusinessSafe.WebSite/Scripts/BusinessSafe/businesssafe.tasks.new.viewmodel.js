var taskNewViewModelLoader = function () {

    function load(triggerButton, tasksOptions) {

        var data = tasksOptions.getDataForNewFurtherControlMeasure($(triggerButton));
        var url = tasksOptions.newFurtherControlMeasureTaskUrl;

        var callback = function (callbackData) {
            new TaskNewViewModel().initialize(callbackData);        
        };

        AjaxCall.execute(url, callback, data, "GET");
    }       

    return {
        load: load            
    };

}();


function TaskNewViewModel(){

    var self = this;
    
    var selectors = {
        dialog : "#dialogFurtherControlMeasureTask",
        companyId : "#CompanyId",
        taskAssignedTo : "#TaskAssignedTo",
        taskAssignedToId: "#TaskAssignedToId",
        taskReoccurringType: "#TaskReoccurringType",
        taskReoccurringTypeId: "#TaskReoccurringTypeId",
        form : "#FurtherControlMeasureTaskForm",
        saveButton : "#FurtherActionTaskSaveButton",
        cancelButton : "#FurtherActionTaskCancelButton",
        isReoccurring: "#IsRecurring",
        reoccurringDiv: "#reoccurringDiv",
        nonReoccurringDiv: "#nonReoccurringDiv",        
        validationSummary : ".validation-summary-errors" 
    };

    var showDialog = function(callbackData){

        $(selectors.dialog).empty().dialog({
            autoOpen: false,
            width: 820,
            modal: true,
            resizable: false,
            draggable: false
        }).append(callbackData).dialog('open');

    };

    var loadDropDowns = function(){
        loadTaskAssignedDropDown();
        loadTaskReoccuringDropDown();
    };

    var loadTaskAssignedDropDown = function(){

        var successfulCallBack = function (result) {
            
            result.splice(0, 0, { label: '--Select Option--', value: '' });
            
            $(selectors.taskAssignedTo).combobox({
                selectedId: $(selectors.taskAssignedToId).val(),
                initialValues: result,
                url: window.globalajaxurls.getEmployees,
                data: {
                    companyId: $(selectors.companyId).val(),
                    pageLimit: 500
                },
                afterSelect: function (event, ui) {
                    var taskAssignedToId = $(selectors.taskAssignedToId).val();
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

    
    var loadTaskReoccuringDropDown = function () {
        
        var successfulCallBack = function (result) {
            
            $(selectors.taskReoccurringType).combobox({
                selectedId: $(selectors.taskReoccurringTypeId).val(),
                initialValues: result,
                url: window.globalajaxurls.getTaskReoccuringTypes,
                data: {
                    companyId: $(selectors.companyId).val(),
                    pageLimit: 100
                }
            });
        };

        var requestData = {
            filter: '',
            companyId: $(selectors.companyId).val(),
            pageLimit: 100
        };

        AjaxCall.execute(window.globalajaxurls.getTaskReoccuringTypes, successfulCallBack, requestData);
    };


    var hookCommandButtons = function(){

        $(selectors.saveButton).die();
        $(selectors.saveButton).live('click', function (event) {
            event.preventDefault();
            
            var saveButton = $(this).attr('disabled', 'disabled').addClass('disabled');

            saveFurtherControlMeasureTask({

                success : successfullyAddedFurtherControlMeasureTaskCallBack,

                error : function(validationSummary, errors){
                    saveButton.removeAttr('disabled').removeClass('disabled');
                    showValidationErrors(validationSummary, errors); 
                } 

            });

        });

        $(selectors.cancelButton).live('click', function (event) {
            event.preventDefault();

            $(selectors.dialog).dialog('close');
        });

    };

    var successfullyAddedFurtherControlMeasureTaskCallBack = function(){
        location.reload(true);
    };

    var saveFurtherControlMeasureTask = function (callbacks) {

        var form = $(selectors.form);

        // ??? WHAT IS THIS
        if (form[0] === undefined) {
            form = $("#FurtherActionTask");
        }

        $.post(form[0].action, form.serialize(), function (result) {

            if (result.Success === true) {
                callbacks.success();
            }
            else if (result.Errors !== undefined) {
                callbacks.error(selectors.validationSummary, result.Errors);
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

        return;
    };

    var setReoccurring = function(reoccurringCheckbox){
    
        var reoccurringDiv = $(selectors.reoccurringDiv);
        var nonReoccurringDiv = $(selectors.nonReoccurringDiv);
        var checked = $(reoccurringCheckbox).attr('checked');

        if (checked === "checked") {
            reoccurringDiv.removeClass("hide");
            nonReoccurringDiv.addClass("hide");
        }
        else {
            reoccurringDiv.addClass("hide");
            nonReoccurringDiv.removeClass("hide");
        }

    };

    var hookEvents = function(){
        $(selectors.isReoccurring).click(function () {
            setReoccurring(this);
        });
    };

    this.initialize = function(callbackData){

        showDialog(callbackData);
        loadDropDowns();
        hookCommandButtons();
        hookEvents();
        initialiseCalendar({
            minDate : 1
        });       
        
    };
}


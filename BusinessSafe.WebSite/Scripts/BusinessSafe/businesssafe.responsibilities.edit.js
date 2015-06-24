
var responsibilities = function () {

    function initialise(sites, employees, frequencyOptions, categories, reasons) {

        var selectors = {
            createResponsibility: "#createResponsibility",
            isCreatorResponsibilityOwner: "#IsCreatorResponsibilityOwner",
            responsibilityOwnerContainer: "#responsibilityOwnerContainer",
            reasponsibilityOwnerTrue: "input:radio#ReasponsibilityOwnerTrue",
            reasponsibilityOwnerFalse: "input:radio#ReasponsibilityOwnerFalse",
            companyId: "#CompanyId",
            responsibilityId: "#ResponsibilityId",
            site: '#Site',
            siteId: '#SiteId',
            owner: "#Owner",
            ownerId: "#OwnerId",
            frequency: "#Frequency",
            frequencyId: "#FrequencyId",
            category: "#Category",
            categoryId: "#CategoryId",
            reason: "#Reason",
            reasonId: "#ReasonId",
            actionButtons: "#ResponsibilitiesTaskGrid div.rsp-status",
            addResponsibilityTaskButton: "#add-responsibility-task",
            editResponsibilityTaskButton: ".responsibilityTaskEditButton",
            deleteResponsibilityTaskButton: ".responsibilityTaskDeleteButton",
            dialogDeleteResponsibilityTask: "#dialogDeleteResponsibilityTask",
            dialogDeleteRecurringResponsibilityTask: "#dialogDeleteRecurringResponsibilityTask",
            responsibilityTaskAssignButton: ".responsibilityTaskAssignButton",
            responsibilityTaskViewButton: ".responsibilityTaskViewButton",
            isReadOnly: '#IsReadOnly',
            showCreateResponsibilityTaskDialogOnLoad: "#ShowCreateResponsibilityTaskDialogOnLoad"
        };

        $(selectors.createResponsibility).click(function (e) {
            if ($(this).closest('form').valid()) {
                $(this).addClass('disabled');
            }
        });

        showEmployee($(null), null);


        $(selectors.reasponsibilityOwnerTrue).change(function (e) {
            showEmployee($(this), e);
        });

        $(selectors.reasponsibilityOwnerFalse).change(function (e) {
            showEmployee($(this), e);
        });

        $(selectors.site).combobox({
            selectedId: $(selectors.siteId).val(),
            initialValues: sites
        });

        $(selectors.owner).combobox({
            selectedId: $(selectors.ownerId).val(),
            initialValues: employees
        });

        $(selectors.frequency).combobox({
            selectedId: $(selectors.frequencyId).val(),
            initialValues: frequencyOptions
        });

        $(selectors.category).combobox({
            selectedId: $(selectors.categoryId).val(),
            initialValues: categories
        });

        $(selectors.reason).combobox({
            selectedId: $(selectors.reasonId).val(),
            initialValues: reasons
        });

        $(selectors.addResponsibilityTaskButton).on('click', function (event) {
            event.preventDefault();
            responsibilityTasksModule.load();
        });

        $(selectors.editResponsibilityTaskButton).on('click', function (event) {
            event.preventDefault();

            var responsibilityTaskId = $(this).attr("data-id");

            responsibilityTasksModule.load(responsibilityTaskId);
        });

        $(selectors.deleteResponsibilityTaskButton).on('click', function (event) {

            var data = getTaskData(this);
            var row = $(this).parent().parent().parent();

            var dialog = $(selectors.dialogDeleteResponsibilityTask);
            if (data.IsReccurring == 'True') {
                dialog = $(selectors.dialogDeleteRecurringResponsibilityTask);
            }

            $(dialog).dialog({
                buttons: {
                    "Confirm": function () {
                        deleteTask(data, this, row);
                        $(this).dialog("close");
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                },
                resizable: false
            });

        });

        $(selectors.responsibilityTaskAssignButton).on('click', function (event) {
            var data = getTaskData(this);
            taskReassignViewModelLoader.load(data);
        });

        $(selectors.responsibilityTaskViewButton).on('click', function (event) {
            var data = getTaskData(this);
            taskViewViewModelLoader.load(data);
        });

        var getTaskData = function (link) {
            var taskData = $(link).data();

            var data = {
                companyId: $(selectors.companyId).val(),
                taskId: taskData.id,
                IsReccurring: taskData.ir,
                taskType: 'responsibility'
            };
            return data;
        };

        if ($(selectors.showCreateResponsibilityTaskDialogOnLoad).val() === 'True') {
            responsibilityTasksModule.load();
            $(selectors.showCreateResponsibilityTaskDialogOnLoad).val('False');
        }
    }

    var deleteTask = function (deleteData, dialog, row) {

        var successfulCallBack = function (data) {
            $(dialog).dialog("close");

            if (data.Success === true) {
                $(row).remove();

            } else {
                showCanNotDeleteReturnedValidationMessage(data.Message);
            }
        };

        var url = window.globalajaxurls.markResponsibilityTaskAsDeletedUrl;
        AjaxCall.execute(url, successfulCallBack, deleteData, "POST");
    };

    var showCanNotDeleteReturnedValidationMessage = function (message) {
        $('#dialogDeleteResponsibilityTaskResponse').html("<p>" + message + "</p>");
        $('#dialogDeleteResponsibilityTaskResponse').dialog({
            buttons: {
                "OK": function () {
                    $(this).dialog("close");
                }
            }
        });
    };

    function showEmployee(sender, e) {

        var showOwner = ($(sender).attr('id') == 'ReasponsibilityOwnerFalse' && $(sender).attr('checked') == 'checked') || $('#ResponsibilityId').val() != 0;

        if (showOwner) {
            $('#responsibilityOwnerContainer').fadeIn('fast');
        }
        else {
            $('#responsibilityOwnerContainer').fadeOut('fast');
        }

        $('#IsCreatorResponsibilityOwner').val(!showOwner);
    }
    return { initialise: initialise };

} ();

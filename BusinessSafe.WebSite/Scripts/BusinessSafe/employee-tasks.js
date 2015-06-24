    function BulkReassignTasks(data, successfulCallBack) {
    this.execute = function () {

        var url = window.globalajaxurls.bulkReassignTasks;
        var newData = JSON.stringify(data);
        AjaxCall.execute(url, successfulCallBack, newData, "POST", "application/json; charset=utf-8");
    };
}

var employeeTasksManager = function () {
    var selectors = {
        companyId: "#CompanyId",
        showDeletedCheckBox: "#ShowDeleted",
        showDeletedLink: "#showDeletedLink",
        showCompletedCheckBox: "#ShowCompleted",
        showCompletedLink: "#showCompletedLink",
        bulkReassignButton: "#btnBulkAssign",
        bulkReassignSaveButton: "#BulkReassignSaveButton",
        bulkReassignTo: "#BulkReassignToId",
        siteGroup: "#SiteGroup",
        siteGroupId: "#SiteGroupId",
        siteId: "#SiteId",
        taskCategoryId: "#TaskCategoryId",
        employeeId: "#EmployeeId",
        resetButton: "#ResetButton",
        isBulkReassign: "#IsBulkReassign"
    };

    function initialize(employees, taskCategories, sites, siteGroups) {

        $(selectors.resetButton).live('click', function () {
            colouredBalls.resetTotals();
        });

        $(selectors.showDeletedLink).click(function (e) {
            e.preventDefault();

            $(selectors.isBulkReassign).val('');
            $(selectors.showDeletedCheckBox).prop("checked", true);
            $('#formEmployeeTasks').submit();
        });

        $(selectors.showCompletedLink).click(function (e) {
            e.preventDefault();

            $(selectors.isBulkReassign).val('');
            $(selectors.showCompletedCheckBox).prop("checked", true);
            $('#formEmployeeTasks').submit();
        });

        var getRiskAssessmentFurtherActionTaskData = function (link) {
            var parent = $(link).parent();
            var taskId = $(link).data("id");
            var riskAssessmentId = parent.data("ra-id");
            var riskAssessmentHazardId = parent.data("rah-id");
            var companyId = $("#CompanyId").val();
            var isReoccurring = parent.attr("data-is-reoccurring");
            var taskType = $(link).data("task-type");
            var data = {
                companyId: companyId,
                riskAssessmentId: riskAssessmentId,
                riskAssessmentHazardId: riskAssessmentHazardId,
                furtherControlMeasureTaskId: taskId,
                isReoccurring: isReoccurring,
                taskType: taskType,
                taskId: taskId
            };

            return data;
        };

        var getResponsibilityTaskData = function (link) {
            var companyId = $("#CompanyId").val();
            var responsibilityTaskId = $(link).data("id");
            var taskType = $(link).data("task-type");
            var isRecurring = $(link).data("is-reoccurring");
            var data = {
                companyId: companyId,
                taskId: responsibilityTaskId,
                taskType: taskType,
                IsReoccurring: isRecurring == "True" ? 1 : 0
            };

            return data;
        };


        var getActionsTaskData = function (link) {
            var companyId = $("#CompanyId").val();
            var actionTaskId = $(link).data("id");
            var taskType = $(link).data("task-type");

            var data = {
                companyId: companyId,
                taskId: actionTaskId,
                taskType: taskType
            };

            return data;
        };

        $(".complete-task-popup-link").click(function (event) {
            event.preventDefault();

            var data = "";
            switch ($(this).data("task-type")) {
                case "responsibility":
                    data = getResponsibilityTaskData(this);
                    break;

                case "action":
                    data = getActionsTaskData(this);
                    break;

                default:
                    data = getRiskAssessmentFurtherActionTaskData(this);
                    break;

            };

            tasksCompleteViewModelLoader.load(data);
        });

        // hook up the print link
        $('.icon-print').click(function (event) {
            event.preventDefault();

            var data = getRiskAssessmentFurtherActionTaskData(this);

            taskPrintViewmodelLoader.load(data);
        });

        // hook up the reassign link
        $('.icon-share').click(function (event) {

            event.preventDefault();

            var data = "";
            switch ($(this).data("task-type"))
            {
                case "responsibility":
                    data = getResponsibilityTaskData(this);
                    taskReassignViewModelLoader.load(data);
                    break;
                case "action":
                    data = getActionsTaskData(this);
                    //taskReassignViewModelLoader.load(data);
                    actionTaskReassignViewModelLoader.load(data);
                    break;
                default:
                    data = getRiskAssessmentFurtherActionTaskData(this);
                    taskReassignViewModelLoader.load(data);
                    break;
            };
        });

        $(".icon-remove").click(function (event) {
            event.preventDefault();

            var that = this;

            var taskDataViewModel = {
                furtherControlMeasureData: function () {
                    if ($(that).data("task-type") == "responsibility") {
                        return getResponsibilityTaskData.call(undefined, that);
                    } else {
                        return getRiskAssessmentFurtherActionTaskData.call(undefined, that);
                    }

                },
                tableRow: function () { return $(that).parent().parent(); }
            };

            new TaskRemoveViewModel(taskDataViewModel).initialize();


        });


        $('.icon-search').click(function (event) {

            var data = "";
            if ($(this).data("task-type") == "responsibility") {
                data = getResponsibilityTaskData(this);

            } else {
                data = getRiskAssessmentFurtherActionTaskData(this);
            }

            if ($(this).data().isReviewTask == 0) {
                event.preventDefault();
                taskViewViewModelLoader.load(data);
            }
        });

        $(selectors.bulkReassignButton).click(function (event) {
            $("#IsBulkReassign").val(true);
            $('form')[0].submit();
        });

        var getBulkReassignData = function () {
            var result = [];
            var selectedCheckBoxes = $(".bulk-reassign-checkbox:checked");

            var companyId = $(selectors.companyId).val();
            var reassignTaskToId = $(selectors.bulkReassignTo).val();

            $.each(selectedCheckBoxes, function (index, value) {

                var checkBox = $(value);

                var data = {
                    CompanyId: companyId,
                    RiskAssessmentId: checkBox.attr("data-ra-id"),
                    RiskAssessmentHazardId: checkBox.attr("data-rah-id"),
                    FurtherControlMeasureTaskId: checkBox.attr("data-id"),
                    ReassignTaskToId: reassignTaskToId,
                    TaskGuid: checkBox.attr("data-task-guid")
                };

                result.push(data);
            });

            return result;
        };

        $(selectors.bulkReassignSaveButton).click(function (event) {

            event.preventDefault();

            $(this).attr('disabled', 'disabled').addClass('disabled');

            var data = getBulkReassignData();
            var successfullyCallBack = function () {
                $("#IsBulkReassign").val(false);
                $('form')[0].submit();
            };

            new BulkReassignTasks(data, successfullyCallBack).execute();
        });

        $("#Employee").combobox({
            selectedId: $("#EmployeeId").val(),
            initialValues: employees,
            url: window.globalajaxurls.getEmployees,
            afterSelect: function () {
                tasksSummaryDetailsLoader.initialise();
            },
            data: {
                companyId: $("#CompanyId").val(),
                pageLimit: 500
            }
        });

        $("#TaskCategory").combobox({
            selectedId: $("#TaskCategoryId").val(),
            initialValues: taskCategories,
            url: "",
            afterSelect: function () {
                tasksSummaryDetailsLoader.initialise();
            },
            data: {
                companyId: $("#CompanyId").val(),
                pageLimit: 100
            }
        });

        $("#Site").combobox({
            selectedId: $("#SiteId").val(),
            initialValues: sites,
            url: window.globalajaxurls.getSites,
            afterSelect: function () {
                tasksSummaryDetailsLoader.initialise();
            },
            data: {
                companyId: $("#CompanyId").val(),
                pageLimit: 100
            }
        });

        $("#SiteGroup").combobox({
            selectedId: $("#SiteGroupId").val(),
            initialValues: siteGroups,
            url: '',
            afterSelect: function () {
                tasksSummaryDetailsLoader.initialise();
            }
        });

        $("#BulkReassignTo").combobox({
            selectedId: $("#BulkReassignToId").val(),
            initialValues: employees,
            url: window.globalajaxurls.getEmployees,
            data: {
                companyId: $("#CompanyId").val(),
                pageLimit: 500
            }
        });

        tasksSummaryDetailsLoader.initialise();
    }

    return { initialize: initialize };
} ();
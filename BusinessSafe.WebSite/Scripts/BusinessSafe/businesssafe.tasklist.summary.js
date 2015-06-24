var tasksSummaryDetailsLoader = function () {
    var selectors = {
        siteGroupId: "#SiteGroupId",
        siteId: "#SiteId",
        taskCategoryId: "#TaskCategoryId",
        employeeId: "#EmployeeId"
    };

    var url = window.globalajaxurls.getTaskListSummary;

    var getRequestData = function () {
        return {
            siteGroupId: $(selectors.siteGroupId).val(),
            siteId: $(selectors.siteId).val(),
            taskCategoryId: $(selectors.taskCategoryId).val(),
            employeeId: $(selectors.employeeId).val()
        };
    };

    var updateBalls = function (response) {
        colouredBalls.setTotals({
            TotalOverdueTasks: response.TotalOverdueTasks,
            TotalPendingTasks: response.TotalPendingTasks,
            TotalTasks: response.TotalTasks
        });
    };

    var initialise = function () {
        // TASK SUMMARY

        var data = getRequestData();
        AjaxCall.execute(url, updateBalls, data, "GET");


    };

    return {
        initialise: initialise
    };
    
} ();

var colouredBalls = function () {

    var selectors = {
        redBall: "#taskSummaryContainer .statusWrapperRed",
        greenBall: "#taskSummaryContainer .statusWrapperGreen",
        greyBall: "#taskSummaryContainer .statusWrapperGray"
    };

    var setRedBall = function (value) {
        $(selectors.redBall).html(value);
    };

    var setGreenBall = function (value) {
        $(selectors.greenBall).html(value);
    };

    var setGreyBall = function (value) {
        $(selectors.greyBall).html(value);
    };

    return {
        resetTotals: function () {
            setRedBall(0);
            setGreenBall(0);
            setGreyBall(0);
        },

        setTotals: function (totals) {
            setRedBall(totals.TotalOverdueTasks);
            setGreenBall(totals.TotalPendingTasks);
            setGreyBall(totals.TotalTasks);
        }
    };
} ();

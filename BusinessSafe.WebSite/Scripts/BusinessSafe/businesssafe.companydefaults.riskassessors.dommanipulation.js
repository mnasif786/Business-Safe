BusinessSafe.CompanyDefaults.RiskAssessors.DomManipulation = new function () {
    var riskAssessorGrid = $('#riskAssessorsDiv');

    var _setCheckBoxCheckedValue = function(checkbox, value) {
        if (value === true) {
            checkbox.attr("checked", "checked");
        } else {
            checkbox.removeAttr("checked");
        }
    };

    var _buildClonedRow = function (riskAssessorDetails) {
        var rowTemplate = $("#newRiskAssessorRowTemplate").html();
        var clonedRow = $(rowTemplate).clone().removeClass('hide');
        var tableCells = $("td", clonedRow);

        var forenameCell = $(tableCells[0]);
        forenameCell.text(riskAssessorDetails.forname);

        var surnameCell = $(tableCells[1]);
        surnameCell.text(riskAssessorDetails.surname);

        var siteCell = $(tableCells[2]);
        siteCell.text(riskAssessorDetails.siteName);

        var doNotSendTaskOverDueNotificationsCheckBox = $("input:checkbox", tableCells[3]);
        _setCheckBoxCheckedValue(
            doNotSendTaskOverDueNotificationsCheckBox,
            riskAssessorDetails.doNotSendTaskOverdueNotifications);

        var doNotSendTaskCompletedNotificationsCheckBox = $("input:checkbox", tableCells[4]);
        _setCheckBoxCheckedValue(
            doNotSendTaskCompletedNotificationsCheckBox,
            riskAssessorDetails.doNotSendTaskCompletedNotifications);

        var doNotSendReviewDueNotificationCheckBox = $("input:checkbox", tableCells[5]);
        _setCheckBoxCheckedValue(
            doNotSendReviewDueNotificationCheckBox,
            riskAssessorDetails.doNotSendReviewDueNotification);
            
        var lastCell = $("td:last", clonedRow);
        var editLink = $("a:first", lastCell);
        editLink.removeAttr("data-id");
        editLink.attr("data-id", riskAssessorDetails.riskAssessorId);

        var deleteLink = $("a:last", lastCell);
        deleteLink.removeAttr("data-id");
        deleteLink.attr("data-id", riskAssessorDetails.riskAssessorId);

        return clonedRow;
    };

    var _addNewRow = function (riskAssessorDetails) {
        var clonedRow = _buildClonedRow(riskAssessorDetails);

        if ($("table tr", riskAssessorGrid).length > 2) {
            $("table tr:last", riskAssessorGrid).after(clonedRow);
        } else {
            $("table tbody", riskAssessorGrid).append(clonedRow);
        }

        var newTable = $("table", riskAssessorGrid).clone();
        $("table", riskAssessorGrid).replaceWith(newTable);
        newTable.tablesorter();
    };

    var _updateRow = function (riskAssessorDetails) {
        var clonedRow = _buildClonedRow(riskAssessorDetails);

        $('a.edit-risk-assessor[data-id=' + riskAssessorDetails.riskAssessorId + ']').
            closest('tr').
            replaceWith(clonedRow);

        var newTable = $("table", riskAssessorGrid).clone();
        $("table", riskAssessorGrid).replaceWith(newTable);
        newTable.tablesorter();
    };

    return {
        addNewRow: _addNewRow,
        updateRow: _updateRow
    };
} ();
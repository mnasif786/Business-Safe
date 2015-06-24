// ******************************** GENERAL RISK ASSESSMENT CONTROL MEASURES ********************************************************//
BusinessSafe.GeneralRiskAssessment.ControlMeasures = function () {

    var selectors = {
        riskAssessmentId: "#RiskAssessmentId",
        companyId: "#CompanyId"
    };

    var urls = {
        removeControlMeasureUrl: window.globalajaxurls.removeControlMeasureFromGenerlRiskAssessmentHazardUrl,
        updateControlMeasureUrl: window.globalajaxurls.updateControlMeasureForGeneralRiskAssessmentHazardUrl,
        addControlMeasureUrl: window.globalajaxurls.addControlMeasureToGeneralRiskAssessmentHazardUrl
    };

    var _removeControlMeasure = function (removeButton) {

        var row = $(removeButton).parent().parent().parent();
        var row = $(removeButton).parent().parent().parent();
        var riskAssessmentId = $(selectors.riskAssessmentId).val();
        var companyId = $(selectors.companyId).val();
        var riskAssessmentHazardId = $("td:first", row).attr("data-rah-id");
        var controlMeasureId = $("td:first", row).attr("data-id");

        var successCallBack = function (data) {
            if (data.Success !== "false") {
                row.replaceWith('');

                // This renumbers the control measures table
                var selector = '#control-measure-table-' + riskAssessmentHazardId + ' .controlCount';

                $(selector).each(function (i) {
                    $(this).html(i + 1 + '. ');
                });
            }
        };

        var data = {
            riskAssessmentId: riskAssessmentId,
            CompanyId: companyId,
            riskAssessmentHazardId: riskAssessmentHazardId,
            controlMeasureId: controlMeasureId
        };

        AjaxCall.execute(urls.removeControlMeasureUrl, successCallBack, data, "POST");

    };

    var _updateControlMeasure = function (updateButton) {

        var row = $(updateButton).parent().parent();
        var riskAssessmentId = $(selectors.riskAssessmentId).val();
        var companyId = $(selectors.companyId).val();


        var riskAssessmentHazardId = $("td:first", row).attr("data-rah-id");
        var controlMeasureId = $("td:first", row).attr("data-id");
        var controlMeasure = $("#updateControlMeasureText").val();

        var successCallBack = function (data) {
            if (data.Success === "false") {
                $("#controlMeasureErrorContainer").html("<span class='field-validation-error create-control-measure-error-display'><span>" + data.Errors + "</span></span>");
            } else {
                var rowIndex = $(row[0].rowIndex);
                var newRow = $("<tr><td data-rah-id='" + riskAssessmentHazardId + "' data-id='" + controlMeasureId + "' data-text='" + controlMeasure + "'><div class='controlMeasureText'><span class='controlCount'>" + rowIndex[0] + ". </span>" + controlMeasure + "</div></td></tr>");

                row.replaceWith(newRow);

                $('td', newRow).attr('data-text', controlMeasure);
            }
        };

        var data = {
            RiskAssessmentId: riskAssessmentId,
            CompanyId: companyId,
            RiskAssessmentHazardId: riskAssessmentHazardId,
            ControlMeasureId: controlMeasureId,
            ControlMeasure: controlMeasure
        };

        AjaxCall.execute(urls.updateControlMeasureUrl, successCallBack, data, "POST");

    };

    var _addControlMeasure = function (updateButton) {

        $(updateButton).attr('disabled', 'disabled');
        var riskAssessmentId = $(selectors.riskAssessmentId).val();
        var companyId = $(selectors.companyId).val();
        var riskAssessmentHazardId = hazardControls.hazardId;
        var controlMeasure = $("#newControlMeasureText").val();

        var successCallBack = function (data) {
            if (data.Success === "false") {
                $("#controlMeasureErrorContainer").html("<span class='field-validation-error create-control-measure-error-display'><span>" + data.Errors + "</span></span>");
                $(updateButton).removeAttr("disabled");
            } else {
                var form = $("#" + hazardControls.hazardId);
                var table = $('table.control-measure-table', form);

                $("tr:last", table).remove();

                var newControlMeasureId = data.Id;
                var rowCount = $("tr", table).length;
                var newRow = $("<tr><td data-rah-id='" + riskAssessmentHazardId + "' data-id='" + newControlMeasureId + "' data-text='" + controlMeasure + "'><div class='controlMeasureText'><span class='controlCount'>" + rowCount + ". </span>" + controlMeasure + "</div></td></tr>");

                $("tr:last", table).after(newRow);
                $('td', newRow).attr('data-text', controlMeasure);
            }
        };

        var data = {
            RiskAssessmentId: riskAssessmentId,
            CompanyId: companyId,
            RiskAssessmentHazardId: riskAssessmentHazardId,
            ControlMeasure: controlMeasure
        };

        AjaxCall.execute(urls.addControlMeasureUrl, successCallBack, data, "POST");

    };

    return {
        removeControlMeasure: _removeControlMeasure,
        updateControlMeasure: _updateControlMeasure,
        addControlMeasure: _addControlMeasure
    };

} ();

// ******************************** PERSONAL RISK ASSESSMENT CONTROL MEASURES ********************************************************//
BusinessSafe.PersonalRiskAssessment.ControlMeasures = function () {

    var selectors = {
        riskAssessmentId: "#RiskAssessmentId",
        companyId: "#CompanyId"
    };

    var urls = {
        removeControlMeasureUrl: window.globalajaxurls.removeControlMeasureFromPersonalRiskAssessmentHazardUrl,
        updateControlMeasureUrl: window.globalajaxurls.updateControlMeasureForPersonalRiskAssessmentHazardUrl,
        addControlMeasureUrl: window.globalajaxurls.addControlMeasureToPersonalRiskAssessmentHazardUrl
    };

    var _removeControlMeasure = function (removeButton) {

        var row = $(removeButton).parent().parent().parent();
        var riskAssessmentId = $(selectors.riskAssessmentId).val();
        var companyId = $(selectors.companyId).val();
        var riskAssessmentHazardId = $("td:first", row).attr("data-rah-id");
        var controlMeasureId = $("td:first", row).attr("data-id");

        var successCallBack = function (data) {
            if (data.Success !== "false") {
                row.replaceWith('');

                // This renumbers the control measures table
                var selector = '#control-measure-table-' + riskAssessmentHazardId + ' .controlCount';

                $(selector).each(function (i) {
                    $(this).html(i + 1 + '. ');
                });
            }
        };

        var data = {
            riskAssessmentId: riskAssessmentId,
            CompanyId: companyId,
            riskAssessmentHazardId: riskAssessmentHazardId,
            controlMeasureId: controlMeasureId
        };

        AjaxCall.execute(urls.removeControlMeasureUrl, successCallBack, data, "POST");

    };

    var _updateControlMeasure = function (updateButton) {

        var row = $(updateButton).parent().parent();
        var riskAssessmentId = $(selectors.riskAssessmentId).val();
        var companyId = $(selectors.companyId).val();
        var riskAssessmentHazardId = $("td:first", row).attr("data-rah-id");
        var controlMeasureId = $("td:first", row).attr("data-id");
        var controlMeasure = $("#updateControlMeasureText").val();

        var successCallBack = function (data) {
            if (data.Success === "false") {
                $("#controlMeasureErrorContainer").html("<span class='field-validation-error create-control-measure-error-display'><span>" + data.Errors + "</span></span>");
            } else {
                var rowIndex = $(row[0].rowIndex);
                var newRow = $("<tr><td data-rah-id='" + riskAssessmentHazardId + "' data-id='" + controlMeasureId + "' data-text='" + controlMeasure + "'><div class='controlMeasureText'><span class='controlCount'>" + rowIndex[0] + ". </span>" + controlMeasure + "</div></td></tr>");

                row.replaceWith(newRow);

                $('td', newRow).attr('data-text', controlMeasure);
            }
        };

        var data = {
            RiskAssessmentId: riskAssessmentId,
            CompanyId: companyId,
            RiskAssessmentHazardId: riskAssessmentHazardId,
            ControlMeasureId: controlMeasureId,
            ControlMeasure: controlMeasure
        };

        AjaxCall.execute(urls.updateControlMeasureUrl, successCallBack, data, "POST");

    };

    var _addControlMeasure = function (updateButton) {

        $(updateButton).attr('disabled', 'disabled');
        var riskAssessmentId = $(selectors.riskAssessmentId).val();
        var companyId = $(selectors.companyId).val();
        var riskAssessmentHazardId = hazardControls.hazardId;
        var controlMeasure = $("#newControlMeasureText").val();

        var successCallBack = function (data) {
            if (data.Success === "false") {
                $("#controlMeasureErrorContainer").html("<span class='field-validation-error create-control-measure-error-display'><span>" + data.Errors + "</span></span>");
                $(updateButton).removeAttr("disabled");
            } else {
                var form = $("#" + hazardControls.hazardId);
                var table = $('table.control-measure-table', form);

                $("tr:last", table).remove();

                var newControlMeasureId = data.Id;
                var rowCount = $("tr", table).length;
                var newRow = $("<tr><td data-rah-id='" + riskAssessmentHazardId + "' data-id='" + newControlMeasureId + "' data-text='" + controlMeasure + "'><div class='controlMeasureText'><span class='controlCount'>" + rowCount + ". </span>" + controlMeasure + "</div></td></tr>");

                $("tr:last", table).after(newRow);

                $('td', newRow).attr('data-text', controlMeasure);
            }
        };

        var data = {
            RiskAssessmentId: riskAssessmentId,
            CompanyId: companyId,
            RiskAssessmentHazardId: riskAssessmentHazardId,
            ControlMeasure: controlMeasure
        };

        AjaxCall.execute(urls.addControlMeasureUrl, successCallBack, data, "POST");

    };

    return {
        removeControlMeasure: _removeControlMeasure,
        updateControlMeasure: _updateControlMeasure,
        addControlMeasure: _addControlMeasure
    };

} ();

// ******************************** HAZARDOUS SUBSTANCE RISK ASSESSMENTS CONTROL MEASURES ********************************************************//
BusinessSafe.HazardousSubstanceRiskAssessment.ControlMeasures = function () {

    var selectors = {
        riskAssessmentId: "#RiskAssessmentId",
        companyId: "#CompanyId"
    };

    var urls = {
        removeControlMeasureUrl: window.globalajaxurls.removeControlMeasureFromHazardousSubstanceRiskAssessmentUrl,
        updateControlMeasureUrl: window.globalajaxurls.updateControlMeasureOnHazardousSubstanceRiskAssessmentUrl,
        addControlMeasureUrl: window.globalajaxurls.addControlMeasureToHazardousSubstanceRiskAssessmentUrl
    };

    var _removeControlMeasure = function (removeButton) {

        var row = $(removeButton).parent().parent().parent();
        var riskAssessmentId = $(selectors.riskAssessmentId).val();
        var companyId = $(selectors.companyId).val();
        var controlMeasureId = $("td:first", row).attr("data-id");

        var successCallBack = function (data) {
            if (data.Success !== "false") {
                row.replaceWith('');

                // This renumbers the control measures table
                var selector = '.controlCount';

                $(selector).each(function (i) {
                    $(this).html(i + 1 + '. ');
                });
            }
        };

        var data = {
            RiskAssessmentId: riskAssessmentId,
            CompanyId: companyId,
            ControlMeasureId: controlMeasureId,
            RiskAssessmentHazardId: 0
        };

        AjaxCall.execute(urls.removeControlMeasureUrl, successCallBack, data, "POST");

    };

    var _updateControlMeasure = function (updateButton) {

        var row = $(updateButton).parent().parent();
        var riskAssessmentId = $(selectors.riskAssessmentId).val();
        var companyId = $(selectors.companyId).val();
        var controlMeasureId = $("td:first", row).attr("data-id");
        var controlMeasure = $("#updateControlMeasureText").val();

        var successCallBack = function (data) {
            if (data.Success === "false") {
                $("#controlMeasureErrorContainer").html("<span class='field-validation-error create-control-measure-error-display'><span>" + data.Errors + "</span></span>");
            } else {
                var rowIndex = $(row[0].rowIndex);
                var newRow = $("<tr><td data-id='" + controlMeasureId + "' data-text='" + controlMeasure + "'><div class='controlMeasureText'><span class='controlCount'>" + rowIndex[0] + ". </span>" + controlMeasure + "</div></td></tr>");

                row.replaceWith(newRow);

                $('td', newRow).attr('data-text', controlMeasure);
            }
        };

        var data = {
            RiskAssessmentId: riskAssessmentId,
            CompanyId: companyId,
            ControlMeasureId: controlMeasureId,
            ControlMeasure: controlMeasure,
            RiskAssessmentHazardId: 0
        };

        AjaxCall.execute(urls.updateControlMeasureUrl, successCallBack, data, "POST");

    };

    var _addControlMeasure = function (updateButton) {

        $(updateButton).attr('disabled', 'disabled');
        var riskAssessmentId = $(selectors.riskAssessmentId).val();
        var companyId = $(selectors.companyId).val();
        var controlMeasure = $("#newControlMeasureText").val();

        var successCallBack = function (data) {
            if (data.Success === "false") {
                $("#controlMeasureErrorContainer").html("<span class='field-validation-error create-control-measure-error-display'><span>" + data.Errors + "</span></span>");
                $(updateButton).removeAttr("disabled");
            } else {
                var form = $("#" + hazardControls.hazardId);
                var table = $('table.control-measure-table', form);

                $("tr:last", table).remove();

                var newControlMeasureId = data.Id;
                var rowCount = $("tr", table).length;
                var newRow = $("<tr><td data-id='" + newControlMeasureId + "' data-text='" + controlMeasure + "'><div class='controlMeasureText'><span class='controlCount'>" + rowCount + ". </span>" + controlMeasure + "</div></td></tr>");

                $("tr:last", table).after(newRow);

                $('td', newRow).attr('data-text', controlMeasure);
            }
        };

        var data = {
            RiskAssessmentId: riskAssessmentId,
            CompanyId: companyId,
            RiskAssessmentHazardId: 0,
            ControlMeasure: controlMeasure
        };

        AjaxCall.execute(urls.addControlMeasureUrl, successCallBack, data, "POST");

    };

    return {
        removeControlMeasure: _removeControlMeasure,
        updateControlMeasure: _updateControlMeasure,
        addControlMeasure: _addControlMeasure
    };

} ();


BusinessSafe.RiskAssessment.ControlMeasures.ViewModel = function (controlMeasures) {

    var isAddingOrEditing = function (form) {
        if ($('#updateControlMeasureText').size()) {
            return true;
        }
        if ($('#newControlMeasureText, #updateControlMeasureText', form).length > 0) {
            return true;
        }

        return false;
    };

    var isReadOnly = function () {
        return $("#IsReadOnly").length > 0;
    };

    // Initialise the add remove control measure row
    $(".control-measure-table tr").live('click', function (event) {
        event.preventDefault();
        if (isReadOnly()) {
            return;
        }

        var form = $("#" + hazardControls.hazardId);

        if (isAddingOrEditing($(form))) {
            return;
        }

        $(".btn-view-further-action-task").remove();
        $(".btn-edit-further-action-task").remove();
        $(".btn-reassign-further-action-task").remove();
        $(".btn-remove-further-action-task").remove();
        $(".btn-remove-control-measure").remove();

        var removeButton = $('<button id="removeControlMeasure" class="btn btn-danger btn-remove-control-measure pull-right" title="Remove"><i class="icon-remove"></i></button>');

        $(".btn-edit-control-measure").remove();

        var editButtonButton = $('<button id="editControlMeasure" class="btn btn-edit-control-measure pull-right" title="Edit"><i class="icon-edit"></i></button>');

        $("td:last div", $(this)).append(removeButton);
        $("td:last div", $(this)).append(editButtonButton);
    });


    // When showing the add remove control measure row
    $("button.btn-remove-control-measure").live('click', function (event) {
        event.preventDefault();
        controlMeasures.removeControlMeasure(this);
    });

    $("button.btn-edit-control-measure").live('click', function (event) {
        event.preventDefault();

        var newControlMeasureRows = $(".new-control-measure-row");
        var editControlMeasureRows = $(".edit-control-measure-row");

        if (newControlMeasureRows.length > 0 || editControlMeasureRows.length > 0) {
            $("#dialogControlMeasuresAlreadyOpen").dialog({
                buttons: {
                    "Ok": function () {
                        $(this).dialog("close");
                    }
                },
                resizable: false
            });
            return;
        }
        
        // Get the row we are on
        var row = $(this).parent().parent().parent();
        var cell = $("td:first", row);
        var riskAssessmentHazardId = cell.data("rah-id");
        var controlMeasureId = cell.data("id");
        var controlMeasure = cell.data("text");

        // Create replacement row for the row
        var newRow = $("<tr class='edit-control-measure-row'><td data-rah-id='" + riskAssessmentHazardId + "' data-id='" + controlMeasureId + "' data-text='" + controlMeasure + "'><span id='controlMeasureErrorContainer'></span><textarea rows='4' class='span9' id='updateControlMeasureText' name='updateControlMeasureText' /><button class='btn btn-link cancel-control-measure-button pull-right'>Cancel</button><button id='saveNewControlMeasure' class='btn btn-primary edit-control-measure-button pull-right' data-id='" + controlMeasureId + "' data-rah-id='" + riskAssessmentHazardId + "'>Update</button></td></tr>");
        row.replaceWith(newRow);

        $("#updateControlMeasureText", newRow).attr('value', controlMeasure);

        $(newRow).find('input').focus();
    });

    // When showing the edit existing control measure row
    $("button.edit-control-measure-button").live('click', function (event) {
        event.preventDefault();
        controlMeasures.updateControlMeasure(this);
    });

    $("button.cancel-control-measure-button").live('click', function (event) {
        event.preventDefault();
        var row = $(this).parent().parent();
        var riskAssessmentHazardId = $("td:first", row).attr("data-rah-id");
        var controlMeasureId = $("td:first", row).attr("data-id");
        var controlMeasure = $("td:first", row).attr("data-text");
        var rowIndex = $(row[0].rowIndex);
        var newRow = $("<tr><td data-rah-id='" + riskAssessmentHazardId + "' data-id='" + controlMeasureId + "' data-text='" + controlMeasure + "'><div class='controlMeasureText'><span class='controlCount'>" + rowIndex[0] + ". </span>" + controlMeasure + "</div></td></tr>");

        row.replaceWith(newRow);
    });

    // When showing the add new control measure row
    $("button.cancel-new-control-measure-button").live('click', function (event) {
        event.preventDefault();

        var form = $("#" + hazardControls.hazardId);
        var table = $('table.control-measure-table', form);

        $("tr:last", table).remove();
    });

    $("button.save-control-measure-button").live('click', function (event) {
        event.preventDefault();
        controlMeasures.addControlMeasure(this);
    });

    $(".add-control-measure").click(function (event) {
        event.preventDefault();

        var newControlMeasureRows = $(".new-control-measure-row");
        var editControlMeasureRows = $(".edit-control-measure-row");

        if (newControlMeasureRows.length > 0 || editControlMeasureRows.length > 0) {
            $("#dialogControlMeasuresAlreadyOpen").dialog({
                buttons: {
                    "Ok": function () {
                        $(this).dialog("close");
                    }
                },
                resizable: false
            });
            return;
        }

        $(".btn-view-further-action-task").remove();
        $(".btn-edit-control-measure").remove();
        $(".btn-remove-control-measure").remove();

        var hazardId = $(this).attr("data-id");

        var form = $("#" + hazardId);

        if (isAddingOrEditing(form)) {
            return;
        }

        var table = $('table.control-measure-table', form);
        var newRow = $("<tr class='new-control-measure-row'><td><button class='btn btn-link cancel-new-control-measure-button pull-right'>Cancel</button><button id='saveNewControlMeasure' class='btn btn-primary save-control-measure-button pull-right'>Save</button><span id='controlMeasureErrorContainer'></span><textarea rows='4' class='span9' id='newControlMeasureText' name='newControlMeasureText' /></td></tr>").hide();

        $("tr:last", table).after(newRow);
        newRow.fadeIn();
        $("#newControlMeasureText").focus();

        hazardControls.hazardId = hazardId;
        $(hazardControls).trigger('ControlMeasureInAddNewState');
    });

};





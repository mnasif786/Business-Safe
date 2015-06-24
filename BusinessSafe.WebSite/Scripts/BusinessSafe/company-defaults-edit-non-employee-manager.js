﻿function LoadNonEmployeeCommand(nonEmployeeId, companyId, successCallBack) {

    this.companyId = companyId;
    this.nonEmployeeId = nonEmployeeId;

    this.execute = function () {

        var url = window.globalajaxurls.editNonEmployee;
        var data = {
            companyId: this.companyId,
            nonEmployeeId: this.nonEmployeeId
        };

        $.ajax({
            type: "GET",
            url: url,
            data: data,
            error: function () {
                window.location.replace(window.globalajaxurls.errorPage);
            },
            cache: false
        }).done(function (data) {
            editNonEmployeeManager.initialize(data, successCallBack);
        });
    };
}

function UpdateNonEmployee(runMatchCheck, nonEmployeeId, name, position, companyName, companyIdLink, formattedName, successCallBack, failureCallBack) {
    this.name = name;
    this.nonEmployeeId = nonEmployeeId;
    this.position = position;
    this.companyName = companyName;
    this.successCallBack = successCallBack;
    this.formattedName = formattedName;
    this.companyIdLink = companyIdLink;

    var getUrl = function () {
        return window.globalajaxurls.updateNewNonEmployee;
    };
    this.execute = function () {

        $.ajax({
            url: getUrl(),
            type: "POST",
            dataType: "json",
            data: {
                nonEmployeeId: this.nonEmployeeId,
                name: this.name,
                position: this.position,
                companyName: this.companyName,
                companyIdLink: this.companyIdLink,
                runMatchCheck: runMatchCheck
            },
            error: function () {
                window.location.replace(window.globalajaxurls.errorPage);
            },
            success: function (data) {

                if (data.Success === true) {
                    successCallBack(data.NonEmployeeId, formattedName, nonEmployeeId);
                } else if (data.Success === false) {
                    failureCallBack(data);
                }
            }
        });
    };
}

var editNonEmployeeManager = function () {

    var selectors = {
        nonEmployeeId: "#NonEmployeeId",
        name: "#Name",
        position: "#Position",
        company: "#Company",
        linkToCompanyId: "#CompanyId",
        validationDisplay: "#validationDisplay",
        dialog: '#dialogAddNonEmployee',
        dialogResult: '#editNewNonEmployeeForm',
        matchingNamesDisplay: ".matchingNamesDisplay",
        matchingNamesMessage: "div.matchingNameMessage",
        updateBtn: "#updateNonEmployeeBtn",
        cancelBtn: "#cancelBtn"
    };

    var showValidationMessage = function (message) {
        var validationMessage = $(selectors.validationDisplay);
        validationMessage.val(message);
        validationMessage.removeClass("hide");

        var errorList = $("ul", validationMessage);
        errorList.append('<li>' + message + '</li>');

    };

    var getNonEmployeeId = function () {
        return $(selectors.nonEmployeeId).val();
    };

    var getName = function () {
        return $(selectors.name).val();
    };

    var getPosition = function () {
        return $(selectors.position).val();
    };

    var getNonEmployeeCompanyName = function () {
        return $(selectors.company).val();
    };

    var getNonEmployeeLinkToCompanyId = function () {
        return $(selectors.linkToCompanyId).val();
    };

    var getRunMatchCheckOnCreate = function () {
        return $(selectors.updateBtn).val() === '';
    };

    var getFormattedName = function () {
        var name = getName();
        var position = getPosition();
        var company = getNonEmployeeCompanyName();
        return name + ", " + company + ", " + position;
    };

    var validate = function () {

        if ($(selectors.name).val() === "") {
            return { Success: false, Message: "Name is required" };
        }

        return { Success: true };

    };

    var resetForm = function () {
        $(selectors.nonEmployeeId).val();
        $(selectors.name).val('');
        $(selectors.position).val('');
        $(selectors.company).val('');
        var updateBtn = $(selectors.updateBtn);
        updateBtn.val('');
        updateBtn.text('Update');
        updateBtn.addClass('btn-primary');
        updateBtn.removeClass('btn-danger');

        resetValidatioDisplay();
        resetMatchingNamesDisplay();
    };

    var resetValidatioDisplay = function () {
        var validationDisplay = $(selectors.validationDisplay);
        validationDisplay.empty();
        $("<ul></ul>").appendTo(validationDisplay);
    };

    var resetMatchingNamesDisplay = function () {
        var matchingNamesDisplay = $(selectors.matchingNamesDisplay, $(selectors.dialogResult));
        matchingNamesDisplay.addClass('hide');

        var ul = $("ul", matchingNamesDisplay.children());
        ul.empty();
    };

    function close() {
        $(selectors.dialog).dialog("close");
    }

    function showMatchingNames(matchingNames) {
        var updateBtn = $(selectors.updateBtn);
        updateBtn.removeClass('btn-primary');
        updateBtn.addClass('btn-danger');
        updateBtn.text("Update Non-Employee");
        updateBtn.val('nomatch');

        var form = $(selectors.dialog);
        var matchingNamesDisplay = $(selectors.matchingNamesDisplay, form);
        matchingNamesDisplay.removeClass('hide');

        var ul = $("ul", matchingNamesDisplay.children());

        for (var i = matchingNames.length - 1; i >= 0; i--) {
            var matchingName = matchingNames[i];
            $("<li/>").appendTo(ul).html(matchingName);
        }

        $(selectors.matchingNamesMessage, form).empty().text('If you wish to still update Non-Employee select Update Non-Employee.');
    }

    function initialize(html, successCallBack) {

        resetForm();


        $(selectors.dialog).dialog({
            autoOpen: false,
            width: 820,
            modal: true,
            resizable: false,
            draggable: false
        });

        $(selectors.dialog).html(html);
        $(selectors.dialog).dialog("open");


        $(selectors.cancelBtn).click(function (event) {
            event.preventDefault();
            close();
        });

        $(selectors.updateBtn).unbind('click');
        $(selectors.updateBtn).click(function (event) {

            event.preventDefault();

            resetValidatioDisplay();

            var validateResult = validate();
            if (validateResult.Success === false) {
                showValidationMessage(validateResult.Message);
                return false;
            }

            var unSuccessfullyCreatedNonEmployeeCallBack = function (data) {
                if (data.Matches !== undefined) {
                    showMatchingNames(data.Matches);
                }
                else {
                    showValidationMessage(data.Message);
                }
            };


            var command = new UpdateNonEmployee(getRunMatchCheckOnCreate(), getNonEmployeeId(), getName(), getPosition(), getNonEmployeeCompanyName(), getNonEmployeeLinkToCompanyId(), getFormattedName(), successCallBack, unSuccessfullyCreatedNonEmployeeCallBack);

            command.execute();

            return false;
        });
    }


    return {
        initialize: initialize,
        close: close
    };

} ();


$('.editNonEmployees').live('click', function (event) {
    event.preventDefault();

    var anchor = $(this);
    var successCallBack = function (nonEmployeeId, formattedName) {

        editNonEmployeeManager.close();
        var dataRow = anchor.parent().parent();
        var dataCell = $('td:first', dataRow);
        dataCell.text(formattedName);
    };

    var nonEmployeeId = anchor.attr("data-id");
    var linkToCompanyId = $("#CompanyId").val();

    new LoadNonEmployeeCommand(nonEmployeeId, linkToCompanyId, successCallBack).execute();
});
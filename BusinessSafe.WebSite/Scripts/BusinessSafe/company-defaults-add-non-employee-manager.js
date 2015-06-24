function CreateNewNonEmployee(runMatchCheck, name, position, companyName, companyIdLink, formattedName, successCallBack, failureCallBack) {

    this.name = name;
    this.position = position;
    this.companyName = companyName;
    this.successCallBack = successCallBack;
    this.formattedName = formattedName;
    this.companyIdLink = companyIdLink;

    var getUrl = function () {
        if (window.generalriskassessment === undefined) {
            return window.globalajaxurls.createNewNonEmployee;
        }

        return window.globalajaxurls.createNewNonEmployee;
    };
    this.execute = function () {

        $.ajax({
            url: getUrl(),
            type: "POST",
            dataType: "json",
            data: {
                nonEmployeeId: 0,
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
                    successCallBack(data.NonEmployeeId, formattedName, companyIdLink);
                } else if (data.Success === false) {
                    failureCallBack(data);
                }
            }
        });
    };
}

var addNonEmployeeManager = function () {

    var selectors = {
        nonEmployeeId: "#NonEmployeeId",
        name: "#Name",
        position: "#Position",
        company: "#Company",
        linkToCompanyId: "#CompanyId",
        validationDisplay: "#validationDisplay",
        matchingNamesDisplay: ".matchingNamesDisplay",
        matchingNamesMessage: "div.matchingNameMessage",
        dialog: '#dialogAddNonEmployee',
        createBtn: "#createNonEmployeeBtn",
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
        return $(selectors.createBtn).val() === '';
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

    var resetForm = function (nameToAdd) {
        var updateBtn = $(selectors.updateBtn);
        if (updateBtn.length > 0) {

            updateBtn.unbind('click');
            updateBtn.attr("id", "createNonEmployeeBtn");
            updateBtn.attr("name", "createNonEmployeeBtn");
        }

        $(selectors.nonEmployeeId).val();
        $(selectors.name).val('');
        $(selectors.name).val(nameToAdd);
        $(selectors.position).val('');
        $(selectors.company).val('');
        var createBtn = $(selectors.createBtn);
        createBtn.val('');
        createBtn.text('Create');
        createBtn.addClass('btn-primary');
        createBtn.removeClass('btn-danger');

        resetValidatioDisplay();
        resetMatchingNamesDisplay();
    };

    var resetValidatioDisplay = function () {
        var validationDisplay = $(selectors.validationDisplay);
        validationDisplay.empty();
        $("<ul></ul>").appendTo(validationDisplay);
    };

    var resetMatchingNamesDisplay = function () {
        var matchingNamesDisplay = $(selectors.matchingNamesDisplay, $(selectors.dialog));
        matchingNamesDisplay.addClass('hide');

        var ul = $("ul", matchingNamesDisplay.children());
        ul.empty();

    };

    function close() {
        $(selectors.dialog).dialog("close");
    }

    function showMatchingNames(matchingNames) {
        var createBtn = $(selectors.createBtn);
        createBtn.removeClass('btn-primary');
        createBtn.addClass('btn-danger');
        createBtn.text("Create New Non-Employee");
        createBtn.val('nomatch');

        var form = $(selectors.dialog);
        var matchingNamesDisplay = $(selectors.matchingNamesDisplay, form);
        matchingNamesDisplay.removeClass('hide');

        var ul = $("ul", matchingNamesDisplay.children());

        for (var i = matchingNames.length - 1; i >= 0; i--) {
            var matchingName = matchingNames[i];
            $("<li/>").appendTo(ul).html(matchingName);
        }

        $(selectors.matchingNamesMessage, form).empty().text('If you wish to still create Non-Employee select Create New Non-Employee.');
    }

    function initialize(riskAssessmentId, nameToAdd, successCallBack) {


        resetForm(nameToAdd);

        $(selectors.dialog).dialog({
            autoOpen: true,
            width: 820,
            modal: true,
            resizable: false,
            draggable: false
        });

        $(selectors.dialog).dialog("open");

        $(selectors.cancelBtn).click(function (event) {
            event.preventDefault();
            close();
        });

        $(selectors.createBtn).unbind('click');
        $(selectors.createBtn).click(function (event) {

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

            var command = new CreateNewNonEmployee(getRunMatchCheckOnCreate(), getName(), getPosition(), getNonEmployeeCompanyName(), getNonEmployeeLinkToCompanyId(), getFormattedName(), successCallBack, unSuccessfullyCreatedNonEmployeeCallBack);

            command.execute();

            return false;
        });
    }


    return {
        initialize: initialize,
        close: close
    };

} ();
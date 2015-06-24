function SetFormToReadOnly(context) {
    this.execute = function () {
        var readOnlySelectors = ['input', 'select', 'textarea'];
        var disabledSelectors = ['button', '.add-edit-link', '.btn-popup-trigger', 'input[type="submit"]', '.add-to-selected', '.remove-from-selected', 'input[type="checkbox"]', 'input[type="radio"]'];
        var hiddenSelectors = ['.btn-primary', '.add-edit-link', '.btn-cancel', '#AddReview', '#sendButton', '.resend-checklist-link'];
        var ignoredSelectors = ['.btn-link', '.override-readonly'];

        var constructedReadOnlySelectionString = _constructSelectionString(context, readOnlySelectors);
        var constructedDisabledSelectionString = _constructSelectionString(context, disabledSelectors);
        var constructedHiddenSelectionString = _constructSelectionString(context, hiddenSelectors);
        var constructedIgnoredSelectionString = _constructSelectionString(context, ignoredSelectors);

        $(constructedReadOnlySelectionString).
            not(constructedIgnoredSelectionString).
            attr('readonly', 'readonly');

        $(constructedDisabledSelectionString).
            not(constructedIgnoredSelectionString).
            attr('disabled', 'disabled');

        $(constructedHiddenSelectionString).
            not(constructedIgnoredSelectionString).
            addClass('hide');
    };

    function _constructSelectionString(reqContext, selectors){
        var constructedString = '';

        for (var i = 0; i < selectors.length; i++) {
            var contextualisedSelector = (reqContext === undefined) ? selectors[i] : reqContext + ' ' + selectors[i];
            constructedString += (i === 0) ? contextualisedSelector : ', ' + contextualisedSelector;
        }

        return constructedString;
    }
}

$(function () {
    if (isReadOnly()) {
        new SetFormToReadOnly().execute();
    }
});

var isReadOnly = function () {
    return $("#IsReadOnly").length > 0;
};

var AjaxCall = {
    execute: function (url, successfulCallBack, data, type, contentType) {
        
        var ajaxType = (type !== undefined) ? type : 'GET';
        var ajaxContentType = (contentType !== undefined) ? contentType : 'application/x-www-form-urlencoded; charset=UTF-8';

        $.ajax({
            type: ajaxType,
            url: url,
            cache: false,
            data: data,
            contentType: ajaxContentType,
            error: function(jqXHR, textStatus, errorThrown){
                // Status Code 0 indicates that the ajax request has been cancelled because moved away from the requested page
                if(jqXHR.status !== 0){
                    if (window.debugErrorHandler === undefined) {
                        window.location.replace(window.globalajaxurls.errorPage);
                    } else {
                        alert("DEBUG: AjaxCall.execute encountered a problem.");                      
                    }    
                }

            }
        }).done(function (doneData) {
            successfulCallBack(doneData);
        });
    }
};

function showValidationErrors(container, errors) {
    if (errors.length === 0) {
        return;
    }
    $('.alert-success').hide();

    var validationSummary = $(container);

    validationSummary.removeClass("hide");

    var errorList = validationSummary.find("ul");
    if (errorList.size() === 0) {
        validationSummary.append('<ul></ul>');
        errorList = validationSummary.find("ul");
    }

    errorList.empty();
    jQuery.each(errors, function (index, value) {
        $("<li>" + value + "</li>").appendTo(errorList);
    });

    var validationSummaryPosition = validationSummary.position();

    window.scrollTo(validationSummaryPosition.left, validationSummaryPosition.top);
}

function showValidationErrorsWithHighlightedFields(container, errors) {
    if (errors.length === 0) {
        return;
    }
    $('.alert-success').hide();

    var validationSummary = $(container);
    validationSummary.removeClass("hide");

    var errorList = validationSummary.find("ul");
    if (errorList.size() === 0) {
        validationSummary.append('<ul></ul>');
        errorList = validationSummary.find("ul");
    }

    $('.input-validation-error').removeClass('input-validation-error');
    errorList.empty();
    jQuery.each(errors, function (index, value) {
        $("<li>" + value.Errors[0].ErrorMessage + "</li>").appendTo(errorList);
        $('#' + value.PropertyName).addClass('input-validation-error');
    });

    $('.input-validation-error').first().focus();

    var validationSummaryPosition = validationSummary.position();

    window.scrollTo(validationSummaryPosition.left, validationSummaryPosition.top);
}

function initialiseCalendar(additionalAttributes){

    var defaultParameters = {
        dateFormat: 'dd/mm/yy',
        showOn: "button",
        buttonImage: "/Content/images/Icons/glyphicon-calendar.png",
        buttonImageOnly: false        
    };

    if(additionalAttributes !== undefined){
        
        var keyes = [];
        
        for(var key in additionalAttributes) {
           if(additionalAttributes.hasOwnProperty(key)) {
              keyes.push(key);
           }
        }
        
        for (var i = 0; i < keyes.length; i++) {
            key = keyes[i];
            defaultParameters[key] = additionalAttributes[key]; 
        }
    }

    $('.calendar').datepicker(defaultParameters);
}

function ShowIsSelectedAssignedToEmployeeABusinessSafeUserIndicator(assignedToId, boundingHtmlElement) {
    var selectors = {
        taskAssignedToId: "#TaskAssignedToId",
        companyId: "#CompanyId",
        employeeNotAUser: ".employee-not-user-alert-message"
    };

    this.execute = function () {
        if (assignedToId === "00000000-0000-0000-0000-000000000000" || assignedToId === "") {
            return;
        }

        var companyId = $(selectors.companyId).val();

        var url = window.globalajaxurls.getIsEmployeeUser;

        $.ajax({
            type: "GET",
            url: url,
            cache: false,
            data: {
                employeeId: assignedToId,
                companyId: companyId
            },
            error: function () {
                window.location.replace(window.globalajaxurls.errorPage);
            }
        }).done(function (data) {
            $(selectors.employeeNotAUser).addClass("hide");
            if (data.IsBusinessSafeUser === false) {
                if (boundingHtmlElement === undefined) {
                    $(selectors.employeeNotAUser + ":first").removeClass("hide");
                } else {
                    $(boundingHtmlElement).find(selectors.employeeNotAUser + ":first").removeClass("hide");
                }
            }
        });
    };
}

//columnIndex starts at 1
var selectAllCheckboxesInTableColumn = function (table, columnIndex) {
    //for each row, select the first column, select all checkboxes
    table.find("tr td:nth-child(" + columnIndex + ") input[type=checkbox]").prop("checked", true);
};

var deSelectAllCheckboxesInTableColumn = function (table, columnIndex) {
    table.find("tr td:nth-child(" + columnIndex + ") input[type=checkbox]").prop("checked", false);
};


function showLogOutWarning() 
{
    alert("Please note for security reasons, and to keep your data secure, you will be logged out within 5 minutes. To prevent this, either save your work or move to a different tab to prevent any loss of information added.");
};

var FIFTEEN_MINUTES_IN_MS = 15 * 60 * 1000;

var logoutWarningTimerID;
var logoutWarningTimerReset = function () 
{
    clearTimeout(logoutWarningTimerID);
    logoutWarningTimerID = setTimeout(showLogOutWarning, FIFTEEN_MINUTES_IN_MS);
};


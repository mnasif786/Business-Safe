var riskAssessmentReviewsManager = function () {
    var selectors = {
        companyId: "#CompanyId",
        riskAssessmentId: "#RiskAssessmentId",
        addReview: "#AddReview",
        addRiskAssessmentReviewDialog: "#AddRiskAssessmentReviewDialog",
        completeTaskDialog: "#completeTaskDialog",
        reviewDate: "#ReviewDate",
        completeReview: ".completeReview",
        editRiskAssessmentReview: ".editRiskAssessmentReview",
        reviewPopupCancelButton: "#ReviewCancelButton",
        reviewPopupSaveButton: "#ReviewSaveButton",
        taskStatus: "#IsComplete",
        validationSummary: ".validation-summary-errors"
    };

    function initialize(saveReviewUrl) {

        /**********************************************************************************
        add review
        **********************************************************************************/
        var loadAddRiskAssessmentReviewPopupCallback = function (data) {
            var addRiskAssessmentReviewDialog = $(selectors.addRiskAssessmentReviewDialog);

            addRiskAssessmentReviewDialog.dialog({
                autoOpen: false,
                modal: true,
                resizable: false,
                draggable: false,
                width: 820
            });

            addRiskAssessmentReviewDialog.empty();
            addRiskAssessmentReviewDialog.append(data).dialog('open');
            addEditRiskAssessmentReviewManager.initialize(saveReviewUrl);
        };

        $(selectors.addReview).live('click', function (event) {
            event.preventDefault();

            var data = {
                companyId: $(selectors.companyId).val(),
                riskAssessmentId: $(selectors.riskAssessmentId).val()
            };

            AjaxCall.execute(window.globalajaxurls.getAddRiskAssessmentReviewUrl, loadAddRiskAssessmentReviewPopupCallback, data, "GET");
        });

        /**********************************************************************************
        complete review
        **********************************************************************************/
        var loadCompleteReviewCallback = function (data) {
            $('#completeTaskDialog').empty().dialog({
                autoOpen: false,
                width: 820,
                modal: true,
                resizable: false,
                draggable: false
            });
            $('#completeTaskDialog').append(data).dialog('open');
            completeRiskAssessmentReviewManager.initialize();
        };

        function setCompleteEnabled(completeCheckbox) {
            var completeButton = $(selectors.reviewPopupSaveButton);
            var checked = $(completeCheckbox).attr('checked');
            if (checked === "checked") {
                completeButton.removeAttr("disabled").removeClass("disabled");
            }
            else {
                completeButton.attr("disabled", "disabled").addClass("disabled");
            }
        }

        function getUrlForCompleteForm(completeButton) {
            var raType = $(completeButton).attr('data-risk-assessment-type');
            var emptyUrl = '';

            switch (raType) {
                case 'GRA':
                    return window.globalajaxurls.getCompleteGeneralRiskAssessmentReviewUrl;
                case 'HSRA':
                    return window.globalajaxurls.getCompleteHazardousSubstanceRiskAssessmentReviewUrl;
                case 'PRA':
                    return window.globalajaxurls.getCompletePersonalRiskAssessmentReviewUrl;
                case 'FRA':
                    return window.globalajaxurls.getCompleteFireRiskAssessmentReviewUrl;
                default:
                    return emptyUrl;
            }
        }

        $(selectors.taskStatus).live('click', function () {
            setCompleteEnabled(this);
        });

        $(selectors.completeReview).live('click', function (event) {
            event.preventDefault();
            AjaxCall.execute(getUrlForCompleteForm(this), loadCompleteReviewCallback, getReviewData(this), "GET");
        });

        /**********************************************************************************
        edit review
        **********************************************************************************/
        var loadEditReviewCallback = function (data) {

            $(selectors.addRiskAssessmentReviewDialog).empty().dialog({
                autoOpen: false,
                modal: true,
                resizable: false,
                draggable: false,
                width: 820
            });

            $(selectors.addRiskAssessmentReviewDialog).append(data).dialog('open');
            addEditRiskAssessmentReviewManager.initialize(saveReviewUrl);
        };

        $(selectors.editRiskAssessmentReview).live('click', function (event) {
            event.preventDefault();
            AjaxCall.execute(window.globalajaxurls.getEditRiskAssessmentReviewUrl, loadEditReviewCallback, getReviewData(this), "GET");
        });

        /**********************************************************************************
        general
        **********************************************************************************/

        var getReviewData = function (requestinglink) {
            var data = {
                companyId: $('#CompanyId').val(),
                riskAssessmentId: $('#RiskAssessmentId').val(),
                riskAssessmentReviewId: $(requestinglink).attr('data-id')
            };
            return data;
        };

        $(selectors.reviewPopupCancelButton).live('click', function (event) {
            event.preventDefault();
            $(selectors.completeTaskDialog).dialog('close');
        });

        initialiseCalendar();

    }

    return {
        initialize: initialize
    };
} ();

var addEditRiskAssessmentReviewManager = function () {
    var selectors = {
        addRiskAssessmentReviewDialog: "#AddRiskAssessmentReviewDialog",
        form: "#CreateRiskAssessmentReviewForm",
        reviewDate: "#ReviewDate",
        reviewingEmployeeId: "#ReviewingEmployeeId",
        saveButton: "#SaveButton",
        cancelButton: "#CancelButton",
        validationSummary: ".validation-summary-errors"
    };

    function initialize(saveReviewUrl) {
        initialiseCalendar();

        $(selectors.saveButton).click(function (event) {

            event.preventDefault();

            var saveButton = $(this).attr('disabled', 'disabled').addClass('disabled');

            var f = $(selectors.form);

            var url = f[0].action;

            if (saveReviewUrl != null && saveReviewUrl != undefined) {
                url = saveReviewUrl;
            }
            
            var data = {
                CompanyId: f.find('#CompanyId').val(),
                RiskAssessmentId: f.find('#RiskAssessmentId').val(),
                RiskAssessmentReviewId: f.find('#RiskAssessmentReviewId').val(),
                ReviewDate: f.find('#ReviewDate').val(),
                ReviewingEmployeeId: f.find('#ReviewingEmployeeId').val()
            };

            var successfulCallBack = function (result) {
                if (result.Success === true) {
                    $(selectors.dialog).dialog('close');
                    
                    if (result.userCantAccessPersonalRiskAssessment === true) {
                        $('#AddRiskAssessmentReviewDialog').dialog().remove();
                        $('#sensitiveTaskDialog').dialog({
                            width: 820,
                            modal: true,
                            resizable: false,
                            draggable: false,
                            buttons: {
                                "Ok": function () {
                                    window.location.replace(window.globalajaxurls.getPersonalRiskAssessmentIndexUrl + '?companyId=' + data.CompanyId);
                                }
                            }
                        }).parent().find(".ui-dialog-titlebar-close.ui-corner-all").remove();
                    } else {
                        location.reload();
                    } 
                }
                else if (result.Errors !== undefined) {
                    saveButton.removeAttr('disabled').removeClass('disabled');
                    showValidationErrors(selectors.validationSummary, result.Errors);
                } else {
                    window.location.replace(window.globalajaxurls.errorPage);
                }
            };

            AjaxCall.execute(url, successfulCallBack, data, "POST");

            return;
        });

        $(selectors.cancelButton).live('click', function (event) {
            event.preventDefault();

            $(selectors.addRiskAssessmentReviewDialog).dialog('close');

            return;
        });

        var reviewingEmployeeId = $(selectors.reviewingEmployeeId).val();
        new ShowIsSelectedAssignedToEmployeeABusinessSafeUserIndicator(reviewingEmployeeId).execute();
    }

    return { initialize: initialize };
} ();

var completeRiskAssessmentReviewManager = function () {
    var selectors = {
        form: '#CompleteReviewForm',
        hasUncompletedTasks: "#HasUncompletedTasks",
        nextReviewDate: "#NextReviewDate",
        archiveCheckbox: "#Archive",
        outstandingFCMT: ".outstanding-further-control-measure-tasks-message",
        saveButton: "#ReviewSaveButton",
        validationSummary: ".validation-summary-errors"
    };

    function initialize() {
        $(selectors.saveButton).attr('disabled', 'disabled').addClass('disabled');

        initialiseCalendar();

        $(selectors.saveButton).live('click', function (event) {
            event.preventDefault();
           
            var saveButton = $(this).attr('disabled', 'disabled').addClass('disabled');

            var f = $(selectors.form);

            $.post(f[0].action, f.serialize(), function (result) {
                if (result.Success === true) {
                    $(selectors.completeTaskDialog).dialog('close');
                    location.reload();
                }
                else if (result.Errors !== undefined) {
                    saveButton.removeAttr('disabled').removeClass('disabled');
                    showValidationErrors(selectors.validationSummary, result.Errors);
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
        });

        $(selectors.archiveCheckbox).click(function () {
            if ($(selectors.hasUncompletedTasks).val() === "True") {
                var checked = $(selectors.archiveCheckbox).attr('checked');

                if (checked === "checked") {
                    $(selectors.outstandingFCMT).removeClass("hide");
                }
                else {
                    $(selectors.outstandingFCMT).addClass("hide");
                }
            }
        });
    }

    return { initialize: initialize };
} ();

var IsEmployeeAbleToCompleteReviewTask = function () {
    var _initialise = function (employeeId, companyId) {
        var selectors = {
            notAUserAlert: '.employee-not-user-alert-message',
            hasPermissionsAlert: '#employee-cannot-complete-review'
        };
        var url = window.globalajaxurls.isEmployeeAbleToCompleteReviewTask;
        var data = {
            employeeId: employeeId,
            companyId: companyId
        };

        var callback = function (callbackData) {
            if (callbackData.IsUser === false) {
                $(selectors.notAUserAlert).show();
            }else if (callbackData.CanCompleteReviewTask === false) {
                $(selectors.hasPermissionsAlert).show();
            }
        };

        $(selectors.notAUserAlert).hide();
        $(selectors.hasPermissionsAlert).hide();
        AjaxCall.execute(url, callback, data, "GET");
    };

    return {
        initialise: _initialise
    };
} ();
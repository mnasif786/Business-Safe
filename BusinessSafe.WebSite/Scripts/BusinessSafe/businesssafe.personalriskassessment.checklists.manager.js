BusinessSafe.PersonalRiskAssessment.Checklists.Manager = function () {
    var _urls = {
        resendChecklist: window.globalajaxurls.resendPersonalRiskAssessmentChecklist,
        viewEmployeeChecklistEmail: window.globalajaxurls.viewPersonalRiskAssessmentEmployeeChecklistEmail,
        toggleFurtherActionRequired: window.globalajaxurls.toggleEmployeeChecklistFurtherActionRequired
    };

    var _selectors = {
        employeeChecklistId: '#Id',
        resendChecklistLinks: '.resend-checklist-link',
        dialogContainer: '#employee-checklist-email-dialog',
        viewChecklistLink: '.icon-search',
        dialogResendChecklistsConfirmation: "#dialog-resend-checklists-confirmation",
        checklistUrl: "#ChecklistUrl",
        saveButton: '#save',
        saveAndGenerateButton: '#save-and-generate',
        cancelButton: 'a#cancel-button',
        isFurtherActionRequired: 'input[name="IsFurtherActionRequired"]',
        selectedIsFurtherActionRequired: 'input[name="IsFurtherActionRequired"]:checked'
    };

    var _toggleSaveButtonDisplay = function () {
        var selectedIsFurtherActionRequired = $(_selectors.selectedIsFurtherActionRequired).val();

        if (selectedIsFurtherActionRequired === undefined) {
            $(_selectors.saveAndGenerateButton).hide();
            $(_selectors.saveButton).hide();
        }
        else if (selectedIsFurtherActionRequired == "true") {
            $(_selectors.saveAndGenerateButton).show();
            $(_selectors.saveButton).show();
        }
        else {
            $(_selectors.saveAndGenerateButton).hide();
            $(_selectors.saveButton).show();
        }

    };

    var _setupSaveButton = function () {
        $(_selectors.saveButton).live('click', function () {
            // todo: validate
            var isFurtherActionRequired = $(_selectors.selectedIsFurtherActionRequired).val();

            var data = {
                id: $(_selectors.employeeChecklistId).val(),
                isRequired: isFurtherActionRequired
            };
            var setOnCallBack = function () {
                $(_selectors.dialogContainer).dialog('close');
                window.location.reload();
            };
            AjaxCall.execute(_urls.toggleFurtherActionRequired, setOnCallBack, data, "POST");
        });
    };

    var _setupCancelButton = function () {
        $(_selectors.cancelButton).live('click', function () {
            $(_selectors.dialogContainer).dialog('close');
        });
    };

    var _setupIsFurtherActionRequired = function () {
        $(_selectors.isFurtherActionRequired).live('click', function () {
            _toggleSaveButtonDisplay();
        });
    }


    function _displayResendChecklistToSpecifiedEmailConfirmation(email, employeeChecklistId) {
        var url = _urls.resendChecklist;

        var riskAssessmentId = $("#RiskAssessmentId").val();

        var data = {
            employeeChecklistId: employeeChecklistId,
            riskAssessmentId: riskAssessmentId
        };

        $(_selectors.dialogResendChecklistsConfirmation).
                empty().
                append('<p>The checklist will be resent to ' + email + '. Are you sure you want to resend?</p>').
                dialog({
                    height: 'auto',
                    resizable: false,
                    modal: true,
                    buttons: {
                        "Confirm": function () {
                            var successfulCallBack = function () {
                                $(_selectors.dialogResendChecklistsConfirmation).dialog("close");
                            };

                            AjaxCall.execute(url, successfulCallBack, data, "POST");
                        },
                        "Cancel": function () {
                            $(this).dialog("close");
                        }
                    },
                    width: '300px'
                });

        $(_selectors.dialogResendChecklistsConfirmation).dialog("open");
    }

    function _initialize() {
        $(_selectors.resendChecklistLinks).click(function (event) {
            if (isReadOnly()) {
                return;
            }

            event.preventDefault();

            var selectedEmployeeId = $(this).attr('data-employee-id');
            var employeeChecklistId = $(this).attr('data-id');

            var getEmailCallback = function (getEmailCallbackSuccessData) {
                _displayResendChecklistToSpecifiedEmailConfirmation(getEmailCallbackSuccessData.email, employeeChecklistId);
            };

            var getEmailRequestData = { employeeId: selectedEmployeeId };
            AjaxCall.execute(window.globalajaxurls.getEmployeeEmail, getEmailCallback, getEmailRequestData);
        });

        $(_selectors.viewChecklistLink).click(function (event) {

            var postbackData = {
                employeeChecklistId: $(this).attr('data-id')
            };

            var successfulCallBack = function (returnedViewData) {
                $(_selectors.dialogContainer).empty().dialog({
                    autoOpen: false,
                    width: 820,
                    modal: true,
                    resizable: false,
                    draggable: false
                }).append(returnedViewData).dialog('open');

                $(_selectors.checklistUrl).blur();
                BusinessSafe.PersonalRiskAssessment.Checklists.Summary.initialize();
                _toggleSaveButtonDisplay();
            };

            // Exscute ajax call
            AjaxCall.execute(_urls.viewEmployeeChecklistEmail, successfulCallBack, postbackData);

        });

        _setupSaveButton();
        _setupIsFurtherActionRequired();
        _setupCancelButton();
    }

    return {
        initialize: _initialize
    };
} ();


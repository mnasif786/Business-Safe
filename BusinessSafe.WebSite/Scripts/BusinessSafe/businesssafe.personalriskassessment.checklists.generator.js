
BusinessSafe.PersonalRiskAssessment.Checklists.Generator = function () {

    var _handleSingleMultipleToggle = function () {
        var selectors = {
            singleOrMultipleSelect: 'input[name="IsForMultipleEmployees"]',
            singleEmployeeSelect: '#single-employee-select',
            multipleFieldset: '#multiple-employee-select',
            multipleEmployeesSectionVisible: '#MultipleEmployeesSectionVisible',
            singleEmployeesSectionVisible: '#SingleEmployeesSectionVisible',
            newEmployeeEmailVisible: '#NewEmployeeEmailVisible'
        };

        var singleOrMultipleSelect = $(selectors.singleOrMultipleSelect);
        var singleFieldset = $(selectors.singleEmployeeSelect);
        var multipleFieldset = $(selectors.multipleFieldset);

        $(singleOrMultipleSelect).click(function () {
            var radioButtonValue = $(this).val();

            if (radioButtonValue === "single") {
                singleFieldset.removeClass("hide");
                multipleFieldset.addClass("hide");
                $(selectors.singleEmployeesSectionVisible).val(true);
                $(selectors.multipleEmployeesSectionVisible).val(false);
                $(selectors.newEmployeeEmailVisible).val(true);
            } else if (radioButtonValue === "multiple") {
                singleFieldset.addClass("hide");
                multipleFieldset.removeClass("hide");
                $(selectors.singleEmployeesSectionVisible).val(false);
                $(selectors.multipleEmployeesSectionVisible).val(true);
                $(selectors.newEmployeeEmailVisible).val(false);
            }
        });
    };

    var setupSitesDropDown = function (sites) {
        var selectors = {
            site: '#Site',
            siteId: '#SiteId',
            companyId: "#CompanyId",
            employeeMultiSelect: "#EmployeeMultiSelect"
        };

        $(selectors.site).combobox({
            selectedId: $(selectors.siteId).val(),
            initialValues: sites,
            url: '',
            data: {
                companyId: $(selectors.companyId).val(),
                pageLimit: 100
            },
            afterSelect: function (event, ui) {

                var getEmployeeMultiSelectCallback = function (data) {
                    $(selectors.employeeMultiSelect).html(data);
                    setupEmployeesMultiSelect();
                };

                var getEmployeeMultiSelectData = {
                    companyId: $(selectors.companyId).val(),
                    siteId: $(selectors.siteId).val()
                };

                AjaxCall.execute(window.globalajaxurls.getEmployeeMultiSelect, getEmployeeMultiSelectCallback, getEmployeeMultiSelectData);
            }
        });
    };

    var _setupSingleEmployeeDropDown = function (employees) {
        var selectors = {
            employeeDropDown: '#Employee',
            selectedEmployeeId: '#EmployeeId',
            existingEmployeeEmailVisible: '#ExistingEmployeeEmailVisible',
            existingEmployeeEmail: '#ExistingEmployeeEmail',
            newEmployeeEmailVisible: '#NewEmployeeEmailVisible',
            newEmployeeEmail: '#NewEmployeeEmail'
        };

        $(selectors.employeeDropDown).combobox({
            selectedId: $("#EmployeeId").val(),
            initialValues: employees,
            url: '',
            afterSelect: function (event, ui) {
                var selectedEmployeeId = $(selectors.selectedEmployeeId).val();

                if (selectedEmployeeId === '' || selectedEmployeeId === null || selectedEmployeeId === 'undefined') {
                    $(selectors.existingEmployeeEmail).html('');
                    $(selectors.existingEmployeeEmail).hide();
                    $(selectors.newEmployeeEmail).hide();
                    $(selectors.newEmployeeEmailVisible).val(false);
                    return;
                }

                var getEmailCallback = function (data) {
                    if (data.email === '' || data.email === null || data.email === 'undefined') {
                        $(selectors.existingEmployeeEmail).hide();
                        $(selectors.existingEmployeeEmailVisible).val(true);
                        $(selectors.newEmployeeEmail).show();
                        $(selectors.newEmployeeEmailVisible).val(true);
                    } else {
                        $(selectors.existingEmployeeEmail).show();
                        $(selectors.existingEmployeeEmailVisible).val(true);
                        $(selectors.newEmployeeEmail).hide();
                        $(selectors.newEmployeeEmailVisible).val(false);
                    }

                    $(selectors.existingEmployeeEmail).html(data.email);
                };

                var getEmailRequestData = { employeeId: selectedEmployeeId };
                AjaxCall.execute(window.globalajaxurls.getEmployeeEmail, getEmailCallback, getEmailRequestData);
            }
        });
    };

    var setupEmployeesMultiSelect = function () {
        var selectors = {
            multiSelectController: '#EmployeeMultiSelect',
            employeeMultiSelectCheckbox: '.employeeMultiSelectCheckbox',
            selectAllEmployees: '#SelectAllEmployees',
            addMultipleEmployees: "#AddMultipleEmployees",
            riskAssessmentId: "#RiskAssessmentId",
            employeesSelected: "#EmployeesSelected"
           
        };

        var selectAll = false;

        $(selectors.selectAllEmployees).click(function () {
            selectAll = !selectAll;

            if (selectAll) {
                $(this).val('Deselect All');
                $(selectors.employeeMultiSelectCheckbox).attr('checked', 'checked');
            } else {
                $(this).val('Select All');
                $(selectors.employeeMultiSelectCheckbox).removeAttr('checked');
            }
        });

        $(selectors.addMultipleEmployees).click(function (event) {
            event.preventDefault();
            var employeeIds = [];

            $(selectors.employeeMultiSelectCheckbox).each(function () {
                if (this.checked) {
                    $(this).parent().addClass('greyed-out');
                    employeeIds.push(this.value);
                }
            });

            if (employeeIds.length > 0) {

                var successfulCallBack = function (data) {
                    $(selectors.employeesSelected).html(data);
                    setupRemoveEmployeeLink();
                };

                if (employeeIds.length > 0) {
                    var data = JSON.stringify({ riskAssessmentId: $(selectors.riskAssessmentId).val(), employeeIds: employeeIds });
                    AjaxCall.execute(window.globalajaxurls.addSelectedEmployees, successfulCallBack, data, "POST", "application/json");
                }
            }
        });
    };

    var _urls = {
        save: '/PersonalRiskAssessments/ChecklistGenerator/Save',
        saveAndNext: '/PersonalRiskAssessments/ChecklistGenerator/SaveAndNext',
        validate: '/PersonalRiskAssessments/ChecklistGenerator/ValidateGenerate',
        send: '/PersonalRiskAssessments/ChecklistGenerator/Generate'
    };

    var selectors = {
        saveAndNextButtons: '#nextBtn, a.riskassessment-tab-links',
        sendButton: '#sendButton',
        generating: "#Generating",
        dialogSendChecklistsConfirmation: "#dialogSendChecklistsConfirmation",
        validationSummary: ".validation-summary-errors",
        sendCompletedChecklistNotificationEmailYes: "#SendCompletedChecklistNotificationEmailYes",
        sendCompletedChecklistNotificationEmailNo: "#SendCompletedChecklistNotificationEmailNo",
        successMessageAnchor: '#success-message-anchor',
        removeEmployee: 'a.icon-remove',
        employeeMultiSelectInput: '#EmployeeMultiSelect input',
        riskAssessmentId: '#RiskAssessmentId',
        selectAllEmployees: '#SelectAllEmployees'
    };

    var _validateForSend = function () {

        var errors = [];

        $(".input-validation-error").removeClass("input-validation-error");

        var message = $("#Message");
        if (message.val() === "") {
            message.addClass("input-validation-error");
            errors.push("Message is required");
        }

        var singleEmployee = $("#singleEmployee:checked");
        var multipleEmployee = $("#multipleEmployees:checked");

        if (singleEmployee.length === 0 && multipleEmployee.length === 0) {
            errors.push("Sending to single or multiple employees is required");
        } else if (singleEmployee.length > 0) {
            if ($('#NewEmployeeEmailVisible').val() == 'true') {
                if ($('#NewEmployeeEmail').val().length === 0) {
                    errors.push("Email is required");
                }
            }
        }

        var checklists = $("#IncludeChecklist_1:checked, #IncludeChecklist_2:checked, #IncludeChecklist_3:checked, #IncludeChecklist_4:checked");
        if (checklists.length === 0) {
            errors.push("At least one checklist is required");
        }

        showValidationErrors($(selectors.validationSummary), errors);
        return errors.length === 0;
    };

    var _displayConfirmSendDialog = function () {
        $(selectors.dialogSendChecklistsConfirmation).dialog({
            height: 'auto',
            resizable: false,
            modal: true,
            buttons: {
                "Confirm": function () {

                    $(selectors.generating).val(true);

                    var f = $('form:first');
                    f.attr('action', _urls.send);
                    f.submit();

                },
                "Cancel": function () {
                    $(this).dialog("close");
                }
            },
            width: '300px'
        });

        $(selectors.dialogSendChecklistsConfirmation).dialog("open");

    };

    var removeEmployee = function (sender) 
    {
        var data = $(sender).data();

        var successfulCallBack = function (result) {
            if (result.Success === true) {
                $(sender).parent().parent().remove();

                var selectedEmployeeCheckbox = $(selectors.employeeMultiSelectInput + "[value='" + data.id + "']");
                var selectedEmployeeRow = $(selectedEmployeeCheckbox).parent();

                $(selectedEmployeeCheckbox).removeAttr('checked');
                $(selectedEmployeeRow).removeClass('greyed-out');

                if ($(selectors.employeeMultiSelectInput + ':checked').length == 0) {
                    $(selectors.selectAllEmployees).val('Select All');
                }
            }
        };

        var postData = JSON.stringify({ riskAssessmentId: $(selectors.riskAssessmentId).val(), employeeId: data.id });
        AjaxCall.execute(window.globalajaxurls.removeEmployeeFromChecklistGenerator, successfulCallBack, postData, "POST", "application/json");
    };

    var setupRemoveEmployeeLink = function () {
        $(selectors.removeEmployee).on('click', function (event) {
            event.preventDefault();
            removeEmployee(this);
        });
    };


    function _initialize(employees, sites) {
        _handleSingleMultipleToggle();
        _setupSingleEmployeeDropDown(employees);
        setupSitesDropDown(sites);
        setupEmployeesMultiSelect();
        setupRemoveEmployeeLink();

        $(selectors.saveAndNextButtons).click(function (event) {
            $(selectors.generating).val(false);
            var isReadOnly = $(selectors.isReadOnly);

            if (isReadOnly.length > 0) {
                return;
            }

            event.preventDefault();

            //            if (_validateForSend() === false) {
            //                return;
            //            };

            var destUrl = $(this).attr("href");
            var f = $('form:first');

            f.attr('action', _urls.saveAndNext);
            $.post(f[0].action, f.serialize(), function (result) {

                if (result.Success === true) {
                    window.location = destUrl;
                }
                else {
                    return;
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
        });

        $(selectors.sendButton).click(function (event) {
            event.preventDefault();

            $(selectors.generating).val(true);
            var isReadOnly = $(selectors.isReadOnly);

            if (isReadOnly.length > 0) {
                return;
            }

            if (_validateForSend() === false) {
                return;
            }

            var $form = $(this).parents('form');
            var validateData = $form.serialize();

            var callback = function (data) {
                if (data.success === true) {
                    _displayConfirmSendDialog();
                } else {
                    showValidationErrors($(selectors.validationSummary), data.errors);
                }
            };

            AjaxCall.execute(_urls.validate, callback, $form.serialize(), 'POST');
        });

        $(selectors.sendCompletedChecklistNotificationEmailYes).click(function (event) {
            $("#SendNotificationEmailAddress").removeClass('hide');
        });

        $(selectors.sendCompletedChecklistNotificationEmailNo).click(function (event) {
            $("#SendNotificationEmailAddress").addClass('hide');
        });



    }

    return {
        initialize: _initialize
    };
} ();

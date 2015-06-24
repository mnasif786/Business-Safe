
var accidentRecordDistribution = function () {

    var selectors = {
        companyId: "#CompanyId",
        siteId: "#ARDistributionSiteId",
        multiSelectController: '#EmployeeMultiSelect',
        employeesToSelectDiv: "#EmployeesToSelect",
        selectedEmployeesDiv: "#EmployeesSelected",
        employeeSelectionSection: "#EmployeeSelectionSection"
    };

    var initialise = function (sites) {
        $("#ARDistributionSite").combobox({
            selectedId: $("#ARDistributionSiteId").val(),
            initialValues: sites,
            url: window.globalajaxurls.getSites,
            afterSelect: function (event, ui) {                

                if ($(selectors.siteId).val() > 0) {
                    var getEmployeeMultiSelectCallback = function (data) {
                        $(selectors.employeeSelectionSection).html(data);
                        setupEmployeesMultiSelect();

                        //show sections
                        displayEmployees();
                    };

                    var getEmployeeMultiSelectData = {
                        companyId: $(selectors.companyId).val(),
                        siteId: $(selectors.siteId).val()
                    };

                    AjaxCall.execute(window.globalajaxurls.getEmployeeNotInAccidentRecordDistributionList, getEmployeeMultiSelectCallback, getEmployeeMultiSelectData);
                }
                else {
                    //hide sections
                    hideEmployees();
                }

            },
            data: {
                companyId: $("#CompanyId").val(),
                pageLimit: 100
            }
        });
    };

    var displayEmployees = function () {
        $(selectors.employeeSelectionSection).removeClass("hide");
    };

    var hideEmployees = function () {
        $(selectors.employeeSelectionSection).addClass("hide");
    };

    var setupEmployeesMultiSelect = function () {
        var selectors =
        {
            multiSelectController: '#EmployeeMultiSelect',
            employeeMultiSelectCheckbox: '.employeeMultiSelectCheckbox',
            selectAllEmployees: '#SelectAllEmployees',
            addMultipleEmployees: "#AddMultipleEmployees",
            addNonEmployee: "#addNonEmployeeToDistrbutionListButton",
            dlgAddNonEmployee: "#dialogAddNonEmployeeToDistributionList",
            riskAssessmentId: "#RiskAssessmentId",
            employeesSelected: "#EmployeesSelected",
            siteId: "#ARDistributionSiteId",
            employeeSelectionSection: "#EmployeeSelectionSection",
            removeEmployee: 'a.icon-remove',
            employeeMultiSelectInput: '#EmployeeSelectionSection input',

            /* Add NonEmployee form selectors */
            updateEmployeeEmailsButton: "#updateEmployeeEmailsButton",
            cancelBtn: "#addNonEmployeeToDistributionListCancelBtn",
            addBtn: "#addNonEmployeeToDistributionListAddBtn",
            name: "#nonEmployeeName",
            email: "#nonEmployeeEmail",
            validationDisplay: "#addNonEmployeeValidationDisplay"
        };

        var selectAll = false;

        //this configures the event handlers. Should be run each time the DOM is rebuilt
        var initialise = function () {
            $(selectors.selectAllEmployees).click(function () {
                selectAllEmployees();
            });

            $(selectors.addMultipleEmployees).click(function (event) {
                event.preventDefault();
                addEmployees();
            });

            $(selectors.addNonEmployee).click(function (event) {
                event.preventDefault();
                addNonEmployee();
            });

            $(selectors.removeEmployee).on('click', function (event) {
                event.preventDefault();
                removeEmployee(this);
            });

            $(selectors.updateEmployeeEmailsButton).on('click', function (event) {
                updateEmployeeEmails();
            });


            initialiseNonEmployeeForm();
        };

        var initialiseNonEmployeeForm = function() {
            resetValidationDisplay();

            $(selectors.email).val("");
            $(selectors.name).val("");
            
            $("#accidentRecordDistributionSuccessMessage").html("");
            $("#accidentRecordDistributionSuccessMessage").addClass("hide");
        };

        var showValidationMessage = function (message) {
            var validationDisplay = $(selectors.validationDisplay);
            validationDisplay.val(message);
            validationDisplay.removeClass("hide");

            var errorList = $("ul", validationDisplay);
            errorList.append('<li>' + message + '</li>');
        };

        var resetValidationDisplay = function () {
            var validationDisplay = $(selectors.validationDisplay);
            validationDisplay.addClass("hide");

            validationDisplay.empty();
            $("<ul></ul>").appendTo(validationDisplay);
        };

        var validateNonEmployeeDetails = function (name, email) {
            var result = true;

            resetValidationDisplay();

            if (name.length == 0 || name == "") {
                showValidationMessage("Name is required");
                result = false;
            }

            if (email.length == 0 || email == "") {
                showValidationMessage("Email address is required");
                result = false;
            }
            else {
                var validEmailRegex = new RegExp("^[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$");
                if (!validEmailRegex.test(email)) {
                    showValidationMessage("Email address is invalid");
                    result = false;
                }
            }

            return result;
        };

        var selectAllEmployees = function () {
            selectAll = !selectAll;

            if (selectAll) {
                $(this).val('Deselect All');
                $(selectors.employeeMultiSelectCheckbox).attr('checked', 'checked');
            } else {
                $(this).val('Select All');
                $(selectors.employeeMultiSelectCheckbox).removeAttr('checked');
            }
        };

        var addEmployees = function () {
            var employeeIds = [];

            $(selectors.employeeMultiSelectCheckbox).each(function () {
                if (this.checked) {
                    $(this).parent().addClass('greyed-out');
                    employeeIds.push(this.value);
                }
            });

            if (employeeIds.length > 0) {

                var successfulCallBack = function (data) {
                    $(selectors.employeeSelectionSection).html(data);
                    initialise();
                };

                if (employeeIds.length > 0) {
                    var data = JSON.stringify({ siteId: $(selectors.siteId).val(), employeeIds: employeeIds });
                    AjaxCall.execute(window.globalajaxurls.addSelectedEmployeesToAccidentRecordDistribution, successfulCallBack, data, "POST", "application/json");
                }
            }
        };



        var addNonEmployee = function () {
         
            initialiseNonEmployeeForm();

            $(selectors.dlgAddNonEmployee).dialog({
                autoOpen: false,
                width: 820,
                modal: true,
                resizable: false,
                draggable: false
            });

            $(selectors.dlgAddNonEmployee).dialog("open");

            $(selectors.cancelBtn).click(function (event) {
                event.preventDefault();
                $(selectors.dlgAddNonEmployee).dialog("close");
            });

            $(selectors.addBtn).unbind('click');
            $(selectors.addBtn).click(function (event) {
                event.preventDefault();

                var successfulCallBack = function (data) {
                    $(selectors.employeeSelectionSection).html(data);
                    initialise();
                };

                var newEmail = $(selectors.email).val();
                var newName = $(selectors.name).val();
                if (validateNonEmployeeDetails(newName, newEmail)) {
                    var postData = JSON.stringify({ siteId: $(selectors.siteId).val(), nonEmployeeName: newName, nonEmployeeEmail: newEmail });
                    AjaxCall.execute(window.globalajaxurls.addNonEmployeeToAccidentRecordDistribution, successfulCallBack, postData, "POST", "application/json");

                    $(selectors.dlgAddNonEmployee).dialog("close");
                }

                return false;
            });
        };

        var removeEmployee = function (sender) {
            var data = $(sender).data();

            var successfulCallBack = function (returnedData) {
                $(selectors.employeeSelectionSection).html(returnedData);
                initialise();
            };

            var postData = JSON.stringify({ siteId: $(selectors.siteId).val(), employeeId: data.id, nonEmployeeEmail: data.email });
            AjaxCall.execute(window.globalajaxurls.removeSelectedEmployeeFromAccidentRecordDistribution, successfulCallBack, postData, "POST", "application/json");
        };

        var updateEmployeeEmails = function () {

            var data = [];

            $("[id^=MultiSelectedEmployeeEmail_").each(function (index) {
                var employeeId = $(this)[0].id.replace("MultiSelectedEmployeeEmail_", "");
                var email = $(this).val();

                data.push({ "EmployeeId": employeeId, "Email": email });                
            });

            var successfulCallBack = function (dataFromCallback) {
                if (dataFromCallback.Success === false) {
                    $("#accidentRecordDistributionValidationSummary").removeClass("hide");
                    $("#accidentRecordDistributionValidationSummary").html("<ul><li>" + dataFromCallback.Error + "</li></ul>");

                    $("#accidentRecordDistributionSuccessMessage").html("");
                    $("#accidentRecordDistributionSuccessMessage").addClass("hide");
                }
                else {
                    $("#accidentRecordDistributionValidationSummary").html("");
                    $("#accidentRecordDistributionValidationSummary").addClass("hide");

                    $("#accidentRecordDistributionSuccessMessage").removeClass("hide");
                    $("#accidentRecordDistributionSuccessMessage").html(dataFromCallback.Message);
                }
            };


            AjaxCall.execute(window.globalajaxurls.updateEmployeeEmails, successfulCallBack, JSON.stringify(data), "POST", "application/json");
        };

        initialise();
    };


    return {
        initialise: initialise
    };
} ();

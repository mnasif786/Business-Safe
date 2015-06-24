var saveDefaultCommand = function () {
        
    function execute(data, successCallBack, existingMatchesCallBack, validationFailCallback) {
        var url = "";
        if(data.companyDefaultId === 0){
            url = window.globalajaxurls.createCompanyDefaults;    
        }
        else{
            url = window.globalajaxurls.updateCompanyDefaults;    
        }
        
        var callBack = function(result){
            if (result.Success === false) {

                if(result.Matches !== undefined && result.Matches !== null){
                    var isNew = data.companyDefaultId === undefined || data.companyDefaultId === 0;
                    existingMatchesCallBack(result.Matches, isNew);
                }
                else if(result.Message.length > 0) {
                    validationFailCallback(result.Message);
                }
                else{
                    window.location.replace(window.globalajaxurls.errorPage);
                }                 
                return;
            }            
            successCallBack(result);
        };

        var newData = JSON.stringify(data);
        var contentType = 'application/json; charset=utf-8';
        AjaxCall.execute(url, callBack, newData , "POST", contentType); 

    }

    return {
        execute: execute
    };

} ();

function DefaultsAddEditDeleteControl(defaultForm) {

    var existingMatchesOptions = defaultForm.existingMatchesOptions;

    var selectors = {
        form: defaultForm.formName,
        inputText: "input[type=text]",
        addButton: "button.add",
        rowTemplate: "table tr.template",
        firstCell: "td:first",
        lastRow: "table tr:last",
        lastCell: "td:last",
        validationSummary : ".validation-summary-errors",
        defaultType: "#DefaultType",
        matchingNamesDisplay: ".matchingNamesDisplay",
        matchingNamesMessage: "div.matchingNameMessage",
        linkToCompanyId: "#CompanyId",
        graApplicableCheckBox : "#GRAHazard",
        praApplicableCheckBox: "#PRAHazard",
        fraApplicableCheckBox: "#FRAHazard",
        companyId: "#CompanyId"
    };

    this.initialise = function () {

        var form = $(selectors.form);

        function resetAfterAddEdit(inputText) {
            inputText.val('');
            inputText.removeAttr("data-id");
            inputText.removeClass("input-validation-error");

            $(selectors.validationSummary, form).addClass("hide");

            var button = $(selectors.addButton, form);
            button.html('Add');
            button.val('');
            button.removeClass('btn-danger');
            resetMatchingNamesDisplay();

            $(selectors.graApplicableCheckBox).removeAttr("checked");
            $(selectors.praApplicableCheckBox).removeAttr("checked");
            $(selectors.fraApplicableCheckBox).removeAttr("checked");
        }

        var resetMatchingNamesDisplay = function () {
            var matchingNamesDisplay = $(selectors.matchingNamesDisplay, $(selectors.form));
            matchingNamesDisplay.addClass('hide');

            var ul = $("ul", matchingNamesDisplay.children());
            ul.empty();

            $("#resetFormLink").remove();
        };

        function getDefaultId(inputText) {
            var id = inputText.attr("data-id");
            if (id === undefined) {
                return 0;
            }
            return id;
        }

        function getDefaultType() {
            return $(selectors.defaultType, form).val();
        }

        function getRunMatchCheck() {
            return $(selectors.addButton, form).val() === '';
        }

        var getLinkToCompanyId = function () {
            return $(selectors.linkToCompanyId).val();
        };

        var getRiskAssessmentTypesApplicable = function () {
            var result = [];

            var graRiskAssessmentApplicable = $(selectors.graApplicableCheckBox + ":checked");
            if (graRiskAssessmentApplicable.length > 0) {
                result.push(graRiskAssessmentApplicable.val());
            }

            var praRiskAssessmentApplicable = $(selectors.praApplicableCheckBox + ":checked");
            if (praRiskAssessmentApplicable.length > 0) {
                result.push(praRiskAssessmentApplicable.val());
            }

            var fraRiskAssessmentApplicable = $(selectors.fraApplicableCheckBox + ":checked");
            if (fraRiskAssessmentApplicable.length > 0) {
                result.push(fraRiskAssessmentApplicable.val());
            }

            return result;
        };

        function getData(inputText) {
            var id = getDefaultId(inputText);

            return {
                isNew: id === 0,
                companyDefaultId: id,
                companyDefaultValue: inputText.val(),
                companyDefaultType: getDefaultType(),
                runMatchCheck: getRunMatchCheck(),
                companyId: getLinkToCompanyId(),
                riskAssessmentTypeApplicable: getRiskAssessmentTypesApplicable()
            };
        }

        function validate(form) {

            var inputText = $(selectors.inputText, form);
            if ($(inputText).val().length === 0) {
                $(selectors.validationSummary, form).removeClass("hide");
                inputText.addClass("input-validation-error");
                return false;
            }

            var checkboxOptions = $("[type=checkbox]", form);
            if (checkboxOptions.length > 0) {
                var selectedCheckboxOptions = $("[type=checkbox]:checked", form);
                if (selectedCheckboxOptions.length === 0) {
                    var validationSummary = $(selectors.validationSummary, form);
                    var item = $("li", validationSummary);
                    item.text("Please select applicable risk assessment types");
                    validationSummary.removeClass("hide");
                    selectedCheckboxOptions.addClass("input-validation-error");
                    return false;
                }
            }

            return true;
        }

        function addNewRow(companyDefaultValue, companyDefaultId) {

            var templateRow = $(selectors.rowTemplate, form);
            var newRow = templateRow.clone().removeClass('template').removeClass('hide');

            var firstCell = $(selectors.firstCell, newRow);
            firstCell.text(companyDefaultValue);

            var lastCell = $(selectors.lastCell, newRow);
            var editLink = $("a:first", lastCell);
            editLink.removeAttr("data-id");
            editLink.attr("data-id", companyDefaultId);
            editLink.click(editLinkClickFunction);

            var deleteLink = $("a:last", lastCell);
            deleteLink.removeAttr("data-id");
            deleteLink.attr("data-id", companyDefaultId);
            deleteLink.click(deleteLinkClickFunction);

            $(selectors.lastRow, form).after(newRow);

            var inputText = $(selectors.inputText, form);
            resetAfterAddEdit(inputText);
        }

        var addButton = $("button.add", form);
        addButton.click(function () {

            var inputText = $(selectors.inputText, form);

            if (validate(form) !== true) {
                return;
            }

            var data = getData(inputText);

            var successCallBackAdd = function (result) {
                addNewRow(data.companyDefaultValue, result.Id);
            };

            var successCallBackEdit = function () {

                var editLink = $('a[data-id="' + data.companyDefaultId + '"]');
                var row = editLink.parent().parent();
                var firstCell = $(selectors.firstCell, row);
                firstCell.text(data.companyDefaultValue);

                resetAfterAddEdit(inputText);
            };

            var existingMatchesCallBack = function (matchingNames, isNew) {

                var form = $(selectors.form);

                var addButton = $(selectors.addButton, form);
                addButton.addClass('btn-danger');
                addButton.val('nomatch');
                addButton.text(existingMatchesOptions.ButtonText);

                var matchingNamesDisplay = $(selectors.matchingNamesDisplay, form);
                matchingNamesDisplay.removeClass('hide');

                var ul = $("ul", matchingNamesDisplay.children());

                for (var i = matchingNames.length - 1; i >= 0; i--) {
                    var matchingName = matchingNames[i];
                    $("<li/>").appendTo(ul).html(matchingName);
                }


                var matchingNameMessage = $(selectors.matchingNamesMessage, form);
                if (isNew) {
                    matchingNameMessage.empty().text(existingMatchesOptions.MessageNew);
                }
                else {
                    matchingNameMessage.empty().text(existingMatchesOptions.MessageEdit);
                }

                matchingNameMessage.append('<br/> To cancel click <a style="cursor:pointer;" id="resetFormLink">here</a>.');

                $("#resetFormLink").click(function () {
                    var inputText = $(selectors.inputText, form);
                    resetAfterAddEdit(inputText);
                });
            };

            var validationMessageFailed = function (message) {
                var validationSummary = $(selectors.validationSummary, form);
                var item = $("li", validationSummary);
                item.text(message);
                validationSummary.removeClass("hide");
                inputText.addClass("input-validation-error");
            };


            var callBack;
            if (data.isNew) {
                callBack = successCallBackAdd;
            } else {
                callBack = successCallBackEdit;
            }

            saveDefaultCommand.execute(data, callBack, existingMatchesCallBack, validationMessageFailed);

        });

        var addNonEmployeeButton = $("button.addNonEmployee", form);
        addNonEmployeeButton.click(function () {

            var successfullyCreatedNonEmployeeCallback = function (nonEmployeeId, formattedName, linkToCompanyId) {
                addNonEmployeeManager.close();
                addNewRow(formattedName, nonEmployeeId);
            };

            addNonEmployeeManager.initialize(0, '', successfullyCreatedNonEmployeeCallback);
        });

        var editLinkClickFunction = function (event) {
            event.preventDefault();
            var id = $(this).attr("data-id");
            var row = $(this).parent().parent();
            var td = $(selectors.firstCell, row);
            var text = $.trim(td.text());

            var inputText = $(selectors.inputText, form);
            inputText.val(text);
            inputText.attr("data-id", id);
            inputText.focus();
            var editButton = $(selectors.addButton, form);
            editButton.html('Edit');

            if (defaultForm.loadEdit !== undefined) {
                defaultForm.loadEdit(id);
            }
        };

        var editLinks = $("a.edit", form);
        editLinks.click(editLinkClickFunction);

        var deleteCompanyDefault = function (data) {

            $("#dialog").dialog({
                buttons: {
                    "Confirm": function () {

                        var callBack = function (result) {
                            var editLink = $('a[data-id="' + result.Id + '"]');
                            editLink.parent().parent().remove();
                            $("#dialog").dialog("close");
                        };

                        var url = window.globalajaxurls.deleteCompanyDefaults;
                        if (defaultForm.deleteUrl !== undefined) {
                            url = defaultForm.deleteUrl;
                        }

                        var newData = JSON.stringify(data);
                        var contentType = 'application/json; charset=utf-8';
                        AjaxCall.execute(url, callBack, newData, "POST", contentType);

                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                },
                resizable: false
            });

            $(".ui-dialog-buttonset button").addClass("btn");
            $("#dialog").dialog("open");
        };

        var deleteLinkClickFunction = function (event) {
            event.preventDefault();

            var link = this;
            var id = $(link).attr("data-id");
            var data = {
                companyDefaultId: id,
                companyDefaultType: getDefaultType(),
                companyId: getLinkToCompanyId()
            };

            if (defaultForm.preDeleteCheckUrl !== undefined) {

                var preDeleteCheckCallBack = function (result) {
                    if (result.CanDelete === true) {
                        deleteCompanyDefault(data);
                    }
                    else {
                        $(defaultForm.canDeleteDialog).dialog({
                            buttons: {
                                "Ok": function () {
                                    $(this).dialog("close");
                                }
                            },
                            resizable: false
                        });
                        $(".ui-dialog-buttonset button").addClass("btn");
                        $(defaultForm.canDeleteDialog).dialog("open");

                    }
                };

                AjaxCall.execute(defaultForm.preDeleteCheckUrl, preDeleteCheckCallBack, data);
            }
            else {
                deleteCompanyDefault(data);
            }

        };

        var deleteLinks = $("a.delete", form);
        deleteLinks.click(deleteLinkClickFunction);
    };
}


$(function () {

    var specialistSuppliersFormDetails = {
        formName: "#specialist-suppliers",
        existingMatchesOptions: {
                                    MessageNew: "If you wish to still create Supplier select Save Supplier." ,
                                    MessageEdit: "If you wish to still update Supplier select Save Supplier." ,
                                    ButtonText : "Save Supplier"   
                                },
        preDeleteCheckUrl : window.globalajaxurls.canDeleteSupplier,
        canDeleteDialog : "#dialogCanNotDeleteSupplier"
    };

    var specialistSuppliersDefaults = new DefaultsAddEditDeleteControl(specialistSuppliersFormDetails);
    specialistSuppliersDefaults.initialise();
    
    var organisationalUnitClassificationFormDetails = {
        formName: "#organisational-unit-classification"
    };

    var organistationalUnitClassificationDefaults = new DefaultsAddEditDeleteControl(organisationalUnitClassificationFormDetails);
    organistationalUnitClassificationDefaults.initialise();

    var employeeGroupsFormDetails = {
        formName: "#employee-groups"
    };

    var employeeGroupsDefaults = new DefaultsAddEditDeleteControl(employeeGroupsFormDetails);
    employeeGroupsDefaults.initialise();
  
    var assessorsGridDetails = {
        name: "#riskAssessors"
    };

    var assessorsDefaults = new RiskAssessorsAddEditDeleteControl(assessorsGridDetails); // company-defaults-add-risk-assessor.js
    assessorsDefaults.initialise();
    
    var nonEmployeesFormDetails = {
        formName: "#non-employees",
        deleteUrl: window.globalajaxurls.markNonEmployeeAsDeleted,
        editLinkClassName: "a.editNonEmployees"
    };

    var nonEmployeesDefaults = new DefaultsAddEditDeleteControl(nonEmployeesFormDetails);
    nonEmployeesDefaults.initialise();
    
    var peopleAtRiskFormDetails = {
        formName: "#people-at-risk",
        existingMatchesOptions: {
                                    MessageNew: "If you wish to still create Person select Save Person." ,
                                    MessageEdit: "If you wish to still update Person select Save Person." ,
                                    ButtonText : "Save Person"   
                                }
    };

    var peopleAtRiskDefaults = new DefaultsAddEditDeleteControl(peopleAtRiskFormDetails);
    peopleAtRiskDefaults.initialise();
    
    var hazardsFormDetails = {
        formName: "#hazards",
        existingMatchesOptions: {
                                    MessageNew: "If you wish to still create Hazard select Save Hazard." ,
                                    MessageEdit: "If you wish to still update Hazard select Save Hazard." ,
                                    ButtonText : "Save Hazard"   
                                },
        loadEdit : function(id){

            var url = window.globalajaxurls.getCompanyDefaultRiskAssessmentTypeApplicable;
            var data = {
                companyId: $("#CompanyId").val(),
                companyDefaultId: id
            };

            var successfulCallBack = function (result) {

                var selectors = {
                    graApplicableCheckBox : "#GRAHazard",
                    praApplicableCheckBox: "#PRAHazard",
                    fraApplicableCheckBox : "#FRAHazard"
                };

                $(selectors.graApplicableCheckBox).removeAttr("checked");
                $(selectors.praApplicableCheckBox).removeAttr("checked");
                $(selectors.fraApplicableCheckBox).removeAttr("checked");
                
                if(result.IsGra === true){
                    $(selectors.graApplicableCheckBox).attr("checked","checked");
                }

                if(result.IsPra === true){
                    $(selectors.praApplicableCheckBox).attr("checked","checked");
                }

                if (result.IsFra === true) {
                    $(selectors.fraApplicableCheckBox).attr("checked", "checked");
                }   

            };

        AjaxCall.execute(url, successfulCallBack, data);

        },
        preDeleteCheckUrl : window.globalajaxurls.canDeleteHazard,
        canDeleteDialog : "#dialogCanNotDeleteHazard"
    };

    var hazardsDefaults = new DefaultsAddEditDeleteControl(hazardsFormDetails);
    hazardsDefaults.initialise();
});

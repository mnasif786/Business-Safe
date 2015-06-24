function RiskAssessorsAddEditDeleteControl(defaultGrid) {
    var riskAssessorGrid = $(defaultGrid.name);

    var selectors = {
        dialogReinstateRiskAssessor: "#dialogReinstateRiskAssessor"
    };

    var deleteLinkClickFunction = function (event) {

        var deleteLink = this;

        event.preventDefault();

        var riskAssessorId = $(deleteLink).attr('data-id');
        var companyId = $("#CompanyId").val();
        var data = {
            riskAssessorId: riskAssessorId,
            companyId: companyId
        };

        var canDeleteRiskAssessorCallBack = function (result) {

            if (result.CanDeleteRiskAssessor === false) {
                $("#dialogCanNotRiskAssessor").dialog({
                    buttons: {
                        "Ok": function () {
                            $(this).dialog("close");
                        }
                    },
                    resizable: false
                });
                return;
            }

            if (result.CanDeleteRiskAssessor === true) {

                $("#dialog").dialog({
                    buttons: {
                        "Confirm": function () {

                            var successCallBack = function () {
                                $("#dialog").dialog("close");

                                $(deleteLink).closest('tr').remove();
                                var newTable = $("table", riskAssessorGrid).clone();
                                $("table", riskAssessorGrid).replaceWith(newTable);
                                newTable.tablesorter();

                            };

                            AjaxCall.execute(window.globalajaxurls.deleteRiskAssessor, successCallBack, data, "POST");

                        },
                        "Cancel": function () {
                            $(this).dialog("close");
                        }
                    },
                    resizable: false
                });
            }
        };


        AjaxCall.execute(window.globalajaxurls.canDeleteRiskAssessor, canDeleteRiskAssessorCallBack, data);

    };

    var reinstateLinkClickFunction = function (event) {
        event.preventDefault();

        var reinstateLink = $(this);

        var riskAssessorId = $(reinstateLink).attr('data-id');
        var companyId = $("#CompanyId").val();
        var data = {
            riskAssessorId: riskAssessorId,
            companyId: companyId
        };

        $(selectors.dialogReinstateRiskAssessor).dialog({
            buttons: {
                "Confirm": function () {
                    var successCallBack = function () {
                        $(selectors.dialogReinstateRiskAssessor).dialog("close");

                        $(reinstateLink).closest('tr').remove();
                    };

                    AjaxCall.execute(window.globalajaxurls.reinstateRiskAssessor, successCallBack, data, "POST");
                },
                "Cancel": function () {
                    $(this).dialog("close");
                }
            },
            resizable: false
        });
    };

    var editLinkClickFunction = function (event) {
        event.preventDefault();
        var companyId = $("#CompanyId").val();
        var riskAssessorId = $(this).attr('data-id');

        var successfullyEditedRiskAssessorCallback = function (riskAssessorId, riskAssessorDetails) {
            addEditRiskAssessor.closeEdit();
            BusinessSafe.CompanyDefaults.RiskAssessors.DomManipulation.updateRow(riskAssessorDetails);
        };

        addEditRiskAssessor.intialiseEditForm(riskAssessorId, companyId, successfullyEditedRiskAssessorCallback);
    };

    this.initialise = function () {

        var selectors = {
            allSitesButton: '#HasAccessToAllSites',
            showDeletedRiskAssessorsButton: "button.showDeletedRiskAssessors",
            showActiveRiskAssessorsButton: "button.showActiveRiskAssessors",

            reinstateRiskAssessorLink: "a.reinstate-risk-assessor",
            editRiskAssessorLink : "a.edit-risk-assessor",
            deleteRiskAssessorsLink : "a.delete"
        };

        var addRiskAssessorButton = $("button.addRiskAssessor", riskAssessorGrid);
        var companyId = $("#CompanyId").val();

        $(selectors.allSitesButton).live('click', function () {
        });

        addRiskAssessorButton.live('click', function () {

            var successfullyCreatedRiskAssessorCallback = function (riskAssessorId, riskAssessorDetails) {
                addEditRiskAssessor.close();
                BusinessSafe.CompanyDefaults.RiskAssessors.DomManipulation.addNewRow(riskAssessorDetails);
            };

            addEditRiskAssessor.intialiseAddForm(0, companyId, successfullyCreatedRiskAssessorCallback);
        });

        $(selectors.showDeletedRiskAssessorsButton).live('click', function () {

            var showDeletedRiskAssessorsCallback = function (showDeletedRiskAssessorsCallbackData) {
                $('#riskAssessorsDiv').html(showDeletedRiskAssessorsCallbackData);
                $("table#riskAssessorsTable").tablesorter();
            };

            var showDeletedRiskAssessorsData = {
                companyId: companyId,
                showDeleted: true
            };

            AjaxCall.execute(window.globalajaxurls.indexRiskAssessors, showDeletedRiskAssessorsCallback, showDeletedRiskAssessorsData);
        });

        $(selectors.showActiveRiskAssessorsButton).live('click', function () {
            var showActiveRiskAssessorsCallback = function (showActiveRiskAssessorsCallbackData) {
                $('#riskAssessorsDiv').html(showActiveRiskAssessorsCallbackData);
                $("table#riskAssessorsTable").tablesorter();
            };

            var showActiveRiskAssessorsData = {
                companyId: companyId,
                showDeleted: false
            };

            AjaxCall.execute(window.globalajaxurls.indexRiskAssessors, showActiveRiskAssessorsCallback, showActiveRiskAssessorsData);
        });

        $(selectors.editRiskAssessorLink, riskAssessorGrid).live("click", editLinkClickFunction);
        $(selectors.deleteRiskAssessorsLink, riskAssessorGrid).live("click", deleteLinkClickFunction);
        $(selectors.reinstateRiskAssessorLink).live("click", reinstateLinkClickFunction);

        $("table", riskAssessorGrid).tablesorter();
    };
}


var addEditRiskAssessor = function () {

    var selectors = {
        form: "#riskAssessorForm",
        errorContainer: "#risk-assessor-validation-summary",
        companyId: "#CompanyId",
        dialogAddRiskAssessor: '#dialogAddRiskAssessor',
        dialogEditRiskAssessor: '#dialogEditRiskAssessor',
        employee: "#Employee",
        employeeId: "#EmployeeId",
        hasAccessToAllSites: "#HasAccessToAllSites",
        siteDropdownContainer: "#site-dropdown-container",
        site: "#Site",
        siteId: "#SiteId",
        cancelBtn: "#cancelAddRiskAssessorBtn",
        saveBtn: "#saveBtn",
        employeeJobTitle: "#EmployeeJobTitle",
        employeeSite: "#EmployeeSite",
        employeeNotAUserWarning: "#EmployeeNotAUserWarning"
    };

    function close() {
        $(selectors.dialogAddRiskAssessor).empty().dialog("close");
    }

    function closeEdit() {
        $(selectors.dialogEditRiskAssessor).empty().dialog("close");
    }

    var updateJobAndSite = function (event, ui) {

        var updateJobAndSiteCallback = function (updateJobAndSiteCallbackData) {
            $(selectors.employeeJobTitle).text(updateJobAndSiteCallbackData.JobTitle);
            $(selectors.employeeSite).text(updateJobAndSiteCallbackData.Site);

            if (updateJobAndSiteCallbackData.EmployeeNotAUser === true) {
                $(selectors.employeeNotAUserWarning).removeClass('hide');
            } else {
                $(selectors.employeeNotAUserWarning).addClass('hide');
            }
        };

        var updateJobAndSiteData = {
            companyId: $(selectors.companyId).val(),
            employeeId: $(selectors.employeeId).val()
        };

        AjaxCall.execute(window.globalajaxurls.getEmployeeJobAndSite, updateJobAndSiteCallback, updateJobAndSiteData);
    };

    function populateEmployeesAndSiteDropdowns() {
        var getEmployeesCallback = function(getEmployeesCallbackData) {

            getEmployeesCallbackData.splice(0, 0, { label: '--Select Option--', value: '' });

            $(selectors.employee).combobox({
                selectedId: $(selectors.employeeId).val(),
                initialValues: getEmployeesCallbackData,
                url: window.globalajaxurls.getEmployeesWithSite,
                data: {
                    companyId: $(selectors.companyId).val(),
                    pageLimit: 500
                },
                afterSelect: function(event, ui) {
                    updateJobAndSite(event, ui);
                }
            });
        };

        var getEmployeesData = {
            companyId: $(selectors.companyId).val(),
            pageLimit: 500
        };

        var populateSiteDropDownList = function(siteListData) {
            siteListData.splice(0, 0, { label: '--Select Option--', value: '' });

            $(selectors.site).combobox({
                selectedId: $(selectors.siteId).val(),
                initialValues: siteListData,
                url: window.globalajaxurls.getSites,
                data: {
                    companyId: $(selectors.companyId).val(),
                    pageLimit: 100
                },

                afterSelect: function(event, ui) {
                }
            });
        };


        var getSiteAndSiteGroupCallback = function (siteGroupData) {

            var items = [];

            //convert to autocomplete model
            $(siteGroupData).each(function() {
                var item = { "label": $(this)[0].Name, "value": $(this)[0].Id, "filterName": null, "filterValue": null };
                item.label += ($(this)[0].IsSiteGroup) ? " (Site group)" : "";
                items.push(item);
            });

            populateSiteDropDownList(items);
        };

        var getSitesData = {
            companyId: $(selectors.companyId).val(),
            pageLimit: 100
        };

        AjaxCall.execute(window.globalajaxurls.getEmployeesWithSite, getEmployeesCallback, getEmployeesData);
        AjaxCall.execute(window.globalajaxurls.getSiteAndSiteGroups, getSiteAndSiteGroupCallback, getSitesData);
    }

    var openDialog = function (container, data) {
        container
            .empty()
            .dialog({
                autoOpen: false,
                width: 820,
                modal: true,
                resizable: false,
                draggable: false
            })
            .append(data)
            .dialog('open');

        var setupSiteDropdownVisibility = function () {
            if ($(selectors.hasAccessToAllSites).is(':checked')) {
                $(selectors.site).val('--Select Option--');
                $(selectors.siteId).val('');
                $(selectors.siteDropdownContainer).hide();
            } else {
                $(selectors.siteDropdownContainer).show();
            }
        };

        setupSiteDropdownVisibility();
        populateEmployeesAndSiteDropdowns();

        $(selectors.hasAccessToAllSites).on('click', function (event) {
            setupSiteDropdownVisibility();
        });
    };

    var postFormForSaving = function (form, successfulCallback) {
        $.post(form[0].action, form.serialize(), function (result) {
            if (result.Success === true) {
                var riskAssessorDetails = {
                    riskAssessorId: result.RiskAssessorId,
                    forname: result.Forename,
                    surname: result.Surname,
                    siteName: result.SiteName,
                    doNotSendReviewDueNotification: result.DoNotSendReviewDueNotification,
                    doNotSendTaskCompletedNotifications: result.DoNotSendTaskCompletedNotifications,
                    doNotSendTaskOverdueNotifications: result.DoNotSendTaskOverdueNotifications
                };

                successfulCallback(result.RiskAssessorId, riskAssessorDetails);
            }
            else if (result.Errors !== undefined) {
                showValidationErrorsWithHighlightedFields(selectors.errorContainer, result.Errors);
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
    };

    function intialiseAddForm(riskAssessorId, companyId, successfullyCreatedRiskAssessorCallback) {

        var addRiskAssessorData = {
            companyId: companyId
        };

        var addRiskAssessorCallback = function (callBackData) {
            openDialog($(selectors.dialogAddRiskAssessor), callBackData);

            $(selectors.saveBtn).die('click');
            $(selectors.saveBtn).live('click', function (event) {
                event.preventDefault();
                postFormForSaving($(selectors.form), successfullyCreatedRiskAssessorCallback);
                return false;
            });

            $(selectors.cancelBtn).die('click');
            $(selectors.cancelBtn).live('click', function (event) {
                event.preventDefault();
                close();
            });
        };

        AjaxCall.execute(window.globalajaxurls.addRiskAssessor, addRiskAssessorCallback, addRiskAssessorData, "GET");
    }

    function intialiseEditForm(riskAssessorId, companyId, successfullyUpdatedRiskAssessorCallback) {

        var editRiskAssessorData = {
            companyId: companyId,
            riskAssessorId: riskAssessorId
        };

        var editRiskAssessorCallback = function (callBackData) {
            openDialog($(selectors.dialogEditRiskAssessor), callBackData);

            $(selectors.saveBtn).on('click', function (event) {
                event.preventDefault();
                postFormForSaving($(selectors.form), successfullyUpdatedRiskAssessorCallback);
                return false;
            });

            $(selectors.cancelBtn).on('click', function (event) {
                event.preventDefault();
                closeEdit();
            });
        };

        AjaxCall.execute(window.globalajaxurls.editRiskAssessor, editRiskAssessorCallback, editRiskAssessorData, "GET");
    }

    return {
        intialiseAddForm: intialiseAddForm,
        intialiseEditForm: intialiseEditForm,
        close: close,
        closeEdit: closeEdit,
        updateJobAndSite: updateJobAndSite
    };
} ();
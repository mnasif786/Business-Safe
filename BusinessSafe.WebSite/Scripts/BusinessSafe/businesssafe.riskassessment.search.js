var generalRiskAssessmentsViewModel = function () {

    return {
        copyRiskAssessmentUrl: window.globalajaxurls.copyRiskAssessmentUrl,
        copyRiskAssessmentForMultipleSitesUrl: window.globalajaxurls.copyRiskAssessmentForMultipleSitesUrl,
        markRiskAssessmentAsDeletedOrUnDeletedUrl: window.globalajaxurls.markRiskAssessmentAsDeletedOrUnDeletedUrl,
        checkRiskAssessmentCanBeDeletedUrl: window.globalajaxurls.checkRiskAssessmentCanBeDeletedUrl
    };
} ();

var hazardousSubstanceRiskAssessmentsViewModel = function () {

    return {
        copyRiskAssessmentUrl: window.globalajaxurls.copyHazardousSubstanceRiskAssessmentUrl,
        copyRiskAssessmentForMultipleSitesUrl: window.globalajaxurls.copyHazardousSubstanceRiskAssessmentForMultipleSitesUrl,
        markRiskAssessmentAsDeletedOrUnDeletedUrl: window.globalajaxurls.markRiskAssessmentAsDeletedOrUnDeletedUrl,
        checkRiskAssessmentCanBeDeletedUrl: window.globalajaxurls.checkRiskAssessmentCanBeDeletedUrl
    };
} ();

var personalRiskAssessmentsViewModel = function () {

    return {
        markRiskAssessmentAsDeletedOrUnDeletedUrl: window.globalajaxurls.markRiskAssessmentAsDeletedOrUnDeletedUrl,
        checkRiskAssessmentCanBeDeletedUrl: window.globalajaxurls.checkRiskAssessmentCanBeDeletedUrl
    };
} ();

var fireRiskAssessmentsViewModel = function () {

    return {
        copyRiskAssessmentUrl: window.globalajaxurls.copyFireRiskAssessmentUrl,
        copyRiskAssessmentForMultipleSitesUrl: window.globalajaxurls.copyFireRiskAssessmentForMultipleSitesUrl,
        markRiskAssessmentAsDeletedOrUnDeletedUrl: window.globalajaxurls.markRiskAssessmentAsDeletedOrUnDeletedUrl,
        checkRiskAssessmentCanBeDeletedUrl: window.globalajaxurls.checkRiskAssessmentCanBeDeletedUrl
    };
} ();

var riskAssessmentsSearch = function () {
    var selectors = {
        companyId: "#CompanyId",
        showDeletedLink: "#showDeletedLink",
        showDeletedInput: "#ShowDeleted",
        showArchivedLink: "#ShowArchivedLink",
        showArchivedInput: "#ShowArchived",
        copyRiskAssessmentLink: "#CopyGeneralRiskAssessmentLink",
        isRiskAssessmentTemplating: "#IsGeneralRiskAssessmentTemplating",
        deleteRiskAssessmentLink: "a.deleteIcon",
        undeleteRiskAssessmentLink: "a.undeleteIcon",
        dialogDeleteRiskAssessmentWithAssociatedTasks: "#dialogDeleteRiskAssessmentWithAssociatedTasks",
        dialogDeleteRiskAssessment: "#dialogDeleteRiskAssessment",
        dialogUnDeleteRiskAssessment: "#dialogUnDeleteRiskAssessment",
        copyRiskAssessmentButton: ".copy-risk-assessment-link",
        copySingleSiteRiskAssessmentLink: "#copySingleSiteRiskAssessmentLink",
        copyMultipleSitesRiskAssessmentLink: "#copyMultipleSitesRiskAssessmentLink",
        formCopyMultipleSitesRiskAssessment: "#formCopyMultipleSitesRiskAssessment",
        riskAssessmentIdToCopy: "#RiskAssessmentIdToCopy",
        selectAllSites: "#SelectAllSites",
        siteMultiSelectCheckbox: ".siteMultiSelectCheckbox"
    };

    function initialise(riskAssessmentViewModel, siteGroups, sites) {

        var copyRiskAssessment = function (companyId, riskAssessmentId, reference, title, successCallBack) {

            this.execute = function () {
                $.ajax({
                    url: riskAssessmentViewModel.copyRiskAssessmentUrl,
                    error: function () {
                        window.location.replace(window.globalajaxurls.errorPage);
                    },
                    type: "POST",
                    dataType: "json",
                    data: {
                        companyId: companyId,
                        riskAssessmentId: riskAssessmentId,
                        reference: reference,
                        title: title
                    },
                    success: function (result) {
                        if (result.Success === true) {
                            successCallBack(result.Id);
                        }
                        else if (result.Errors !== undefined) {
                            showValidationErrorsWithHighlightedFields(".alert-error", result.Errors);
                        }
                    }
                });
            };
        };


        var markRiskAssessmentAsDeletedOrUnDeleted = function (companyId, riskAssessmentId, deleted, successCallBack) {

            this.execute = function () {
                $.ajax({
                    url: riskAssessmentViewModel.markRiskAssessmentAsDeletedOrUnDeletedUrl,
                    error: function () {
                        window.location.replace(window.globalajaxurls.errorPage);
                    },
                    type: "POST",
                    dataType: "json",
                    data: {
                        companyId: companyId,
                        riskAssessmentId: riskAssessmentId,
                        deleted: deleted
                    },
                    success: function (data) {
                        successCallBack();
                    }
                });
            };
        };

        var validateCopyForm = function () {
            var errors = [];
            errors.push("a dummy error");

            alert(errors.length);

            return errors;
        };

        $(selectors.copyRiskAssessmentButton).click(function (event) {
            event.preventDefault();

            var copyButton = $(this);
            var riskAssessmentId = copyButton.attr("data-id");
            var url = copyButton.attr("data-url");
            $(selectors.copySingleSiteRiskAssessmentLink).attr("data-id", riskAssessmentId);
            $(selectors.copySingleSiteRiskAssessmentLink).attr("data-url", url);
            $(selectors.copyMultipleSitesRiskAssessmentLink).attr("data-id", riskAssessmentId);
            $(selectors.copyMultipleSitesRiskAssessmentLink).attr("data-url", url);

            $("#dialogCopyRiskAssessmentMenu").dialog({
                height: 'auto',
                resizable: false,
                modal: true,
                buttons: {
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                },
                width: '600px'
            });
        });

        $(selectors.copySingleSiteRiskAssessmentLink).click(function (event) {
            event.preventDefault();
            $("#dialogCopyRiskAssessmentMenu").dialog('close');

            var copyButton = $(this);
            var riskAssessmentId = copyButton.attr("data-id");
            var url = copyButton.attr("data-url");
            var companyId = $(selectors.companyId).val();
            var titleTextBox = $("#dialogCopyRiskAssessment").find("#Title");
            var referenceTextBox = $("#dialogCopyRiskAssessment").find("#Reference");
            titleTextBox.val("");
            referenceTextBox.val("");
            $("#dialogCopyRiskAssessment .alert-error").addClass("hide");

            $("#dialogCopyRiskAssessment").dialog({
                height: 'auto',
                resizable: false,
                modal: true,
                buttons: {
                    "Confirm": function () {
                        var successCallBack = function (newRiskAssessmentId) {
                            $(selectors.dialogDeleteRiskAssessment).dialog("close");
                            url = url.replace(/riskAssessmentId=(?:\d*)/, 'riskAssessmentId=' + newRiskAssessmentId);
                            window.location = url;
                        };

                        var title = titleTextBox.val();
                        var reference = referenceTextBox.val();
                        new copyRiskAssessment(companyId, riskAssessmentId, reference, title, successCallBack).execute();
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                },
                width: '600px'
            });

            $("#dialogCopyRiskAssessment").dialog("open");


        });

        $(selectors.copyMultipleSitesRiskAssessmentLink).click(function (event) {
            event.preventDefault();
            $("#dialogCopyRiskAssessmentMenu").dialog('close');
            $("#dialogCopyMultipleSitesRiskAssessment .alert-error").addClass("hide");
            var copyButton = $(this);
            var riskAssessmentId = copyButton.attr("data-id");

            $("#dialogCopyMultipleSitesRiskAssessment").dialog({
                height: 'auto',
                resizable: false,
                modal: true,
                buttons: {
                    "Confirm": function () {
                        $.ajax({
                            url: riskAssessmentViewModel.copyRiskAssessmentForMultipleSitesUrl,
                            type: "POST",
                            data: $(selectors.formCopyMultipleSitesRiskAssessment).serialize() + "&RiskAssessmentId=" + riskAssessmentId,
                            success: function (result) {
                                if (result.Success === true) {
                                    location.reload();
                                } else if (result.Errors !== undefined) {
                                    showValidationErrorsWithHighlightedFields("#formCopyMultipleSitesRiskAssessment .alert-error", result.Errors);
                                }
                            }
                        });
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                },
                width: '600px'
            });
        });

        $(selectors.copyRiskAssessmentLink).click(function (event) {
            event.preventDefault();
            $(selectors.isRiskAssessmentTemplating).val('True');
            $('form').submit();
        });

        $(selectors.selectAllSites).click(function (event) {
            if ($(this).val() == 'Select All') {
                $(selectors.siteMultiSelectCheckbox).prop('checked', true);
                $(this).val("Deselect All");
            }
            else if ($(this).val() == 'Deselect All') {
                $(selectors.siteMultiSelectCheckbox).prop('checked', false);
                $(this).val("Select All");
            }
        });

        $(selectors.deleteRiskAssessmentLink).click(function (event) {
            event.preventDefault();

            var riskAssessmentId = $(this).attr('data-id');
            var companyId = $(selectors.companyId).val();
            var deleted = true;

            $.ajax({
                url: riskAssessmentViewModel.checkRiskAssessmentCanBeDeletedUrl,
                type: "GET",
                dataType: "json",
                data: {
                    companyId: companyId,
                    riskAssessmentId: riskAssessmentId
                },
                success: function (data) {
                    if (data.hasUndeletedTasks === true) {
                        $(selectors.dialogDeleteRiskAssessment).dialog({
                            buttons: {
                                "Confirm": function () {
                                    var successCallBack = function () {
                                        $(selectors.dialogDeleteRiskAssessment).dialog("close");
                                        window.location.reload();
                                    };

                                    new markRiskAssessmentAsDeletedOrUnDeleted(companyId, riskAssessmentId, deleted, successCallBack).execute();
                                },
                                "Cancel": function () {
                                    $(this).dialog("close");
                                }
                            },
                            width: '392px',
                            resizable: false
                        });
                    }
                    else if (data.hasUndeletedTasks === false) {
                        $(selectors.dialogDeleteRiskAssessmentWithAssociatedTasks).dialog({
                            height: 'auto',
                            resizable: false,
                            modal: true,
                            buttons: {
                                "Confirm": function () {
                                    var successCallBack = function () {
                                        $(selectors.dialogDeleteRiskAssessment).dialog("close");
                                        window.location.reload();
                                    };

                                    new markRiskAssessmentAsDeletedOrUnDeleted(companyId, riskAssessmentId, deleted, successCallBack).execute();
                                },
                                "Cancel": function () {
                                    $(this).dialog("close");
                                }
                            },
                            width: '392px'
                        });
                    }
                },
                error: function () {
                    window.location.replace(window.globalajaxurls.errorPage);
                }
            });
        });

        $(selectors.undeleteRiskAssessmentLink).click(function (event) {
            event.preventDefault();

            var riskAssessmentId = $(this).attr('data-id');
            var companyId = $(selectors.companyId).val();
            var deleted = false;

            $(selectors.dialogUnDeleteRiskAssessment).dialog({
                buttons: {
                    "Confirm": function () {
                        var successCallBack = function () {
                            $(selectors.dialogUnDeleteRiskAssessment).dialog("close");
                            window.location.reload();
                        };

                        new markRiskAssessmentAsDeletedOrUnDeleted(companyId, riskAssessmentId, deleted, successCallBack).execute();
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                },
                height: 'auto',
                resizable: false,
                width: '392px'
            });
        });

        $(selectors.showDeletedLink).click(function (event) {
            $(selectors.showArchivedInput).val("false");
            $(selectors.showDeletedInput).val("true");
            $('#Search').click();
        });
        $(selectors.showArchivedLink).click(function (event) {
            $(selectors.showArchivedInput).val("true");
            $(selectors.showDeletedInput).val("false");
            $('#Search').click();
        });

        $("#SiteGroup").combobox({
            selectedId: $("#SiteGroupId").val(),
            initialValues: siteGroups,
            url: ''
        });

        $("#Site").combobox({
            selectedId: $("#SiteId").val(),
            initialValues: sites,
            data: {
                companyId: $("#CompanyId").val(),
                pageLimit: 100
            }
        });
    }

    return { initialise: initialise };
} ();

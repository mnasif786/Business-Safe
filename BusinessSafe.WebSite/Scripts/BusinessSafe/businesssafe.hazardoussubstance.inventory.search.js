BusinessSafe.HazardousSubstance.Search = function (suppliers) {
    var self = this;

    var selectors = {
        companyId: '#CompanyId',
        deleteHazardousSubstance: "a.deleteIcon",
        editHazardousSubstance: "a.editIcon",
        reinstateHazardousSubstance: "a.reinstateIcon",
        dialogCannotRemoveHazardousSubstance: "#dialogCannotRemoveHazardousSubstance",
        dialogDeleteHazardousSubstance: "#dialogDeleteHazardousSubstance",
        dialogReinstateHazardousSubstance: "#dialogReinstateHazardousSubstance",
        dialogHazardousSubstanceHasRiskAssessments: "#dialogHazardousSubstanceHasRiskAssessments",
        showDeletedLink: "#showDeletedLink",
        showDeletedInput: "#ShowDeleted"       
    };

    var urls = {
        canDeleteHazardousSubstance: window.globalajaxurls.canDeleteHazardousSubstance,
        markHazardousSubstanceAsDeleted: window.globalajaxurls.markHazardousSubstanceAsDeleted,
        markHazardousSubstanceAsNotDeleted: window.globalajaxurls.markHazardousSubstanceAsNotDeleted,
        viewHazardousSubstanceRiskAssessment: window.globalajaxurls.viewHazardousSubstanceRiskAssessment
    };

    $("#Supplier").combobox({
        selectedId: $("#SupplierId").val(),
        initialValues: suppliers,
        url: ''
    });

    $(selectors.showDeletedLink).click(function (event) {
        if ($(selectors.showDeletedInput).val().toLowerCase() == "false") {
            $(selectors.showDeletedInput).val("true");
            $('#Search').click();
        } else {
            $(selectors.showDeletedInput).val("false");
            $('#Search').click();
        }
    });

    $(selectors.editHazardousSubstance).click(function (event) {
        event.preventDefault();

        var url = $(this).attr('href');
        var hazardousSubstanceId = $(this).attr("data-id");
        var companyId = $(selectors.companyId).val();

        var data = {
            HazardousSubstanceId: hazardousSubstanceId,
            CompanyId: companyId
        };

        var canEditHazardousSubstanceCallBack = function (result) {
            if (result.CanDeleteHazardousSubstance === false) {
                $(selectors.dialogHazardousSubstanceHasRiskAssessments).dialog({
                    buttons: {
                        "Ok": function () {
                            $(this).dialog("close");
                            window.location.href = url;
                        },
                        "Cancel": function () {
                            $(this).dialog("close");
                        }
                    },
                    resizable: false,
                    width: "400px"
                });
            } else {
                window.location.href = url;
            }
        };

        AjaxCall.execute(urls.canDeleteHazardousSubstance, canEditHazardousSubstanceCallBack, data);
    });

    $(selectors.deleteHazardousSubstance).click(function (event) {
        event.preventDefault();

        var hazardousSubstanceId = $(this).attr("data-id");
        var companyId = $(selectors.companyId).val();

        var canDeleteEmployeeCallBack = function (result) {
            if (result.CanDeleteHazardousSubstance === false) {
                $(selectors.dialogCannotRemoveHazardousSubstance).dialog({
                    buttons: {
                        "Ok": function () {
                            $(this).dialog("close");
                        }
                    },
                    resizable: false
                });
                return;
            }

            if (result.CanDeleteHazardousSubstance === true) {
                $(selectors.dialogDeleteHazardousSubstance).dialog({
                    buttons: {
                        "Confirm": function () {
                            var successCallBack = function () {
                                $(selectors.dialogDeleteHazardousSubstance).dialog("close");
                                $('form').submit();
                            };
                            var data = {
                                hazardousSubstanceId: hazardousSubstanceId,
                                companyId: companyId
                            };

                            AjaxCall.execute(urls.markHazardousSubstanceAsDeleted, successCallBack, data, "POST");
                        },
                        "Cancel": function () {
                            $(this).dialog("close");
                        }
                    },
                    resizable: false
                });
            }
        };

        var data = {
            HazardousSubstanceId: hazardousSubstanceId,
            CompanyId: companyId
        };

        AjaxCall.execute(urls.canDeleteHazardousSubstance, canDeleteEmployeeCallBack, data);
    });
    
    $(selectors.reinstateHazardousSubstance).click(function(event) {
        event.preventDefault();

        var hazardousSubstanceId = $(this).attr("data-id");
        var companyId = $(selectors.companyId).val();

        $(selectors.dialogReinstateHazardousSubstance).dialog({
            buttons: {
                "Confirm": function() {
                    var successCallBack = function() {
                        $(selectors.dialogReinstateHazardousSubstance).dialog("close");
                        $('form').submit();
                    };
                    var data = {
                        hazardousSubstanceId: hazardousSubstanceId,
                        companyId: companyId
                    };

                    AjaxCall.execute(urls.markHazardousSubstanceAsNotDeleted, successCallBack, data, "POST");
                },
                "Cancel": function() {
                    $(this).dialog("close");
                }
            },
            resizable: false
        });
    });

    return self;
};





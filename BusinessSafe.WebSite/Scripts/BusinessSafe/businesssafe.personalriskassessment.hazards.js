var hazardsManager = function() {

    function initialize() {
        var _self = this;
        _self.selectors = {
            saveHazardsButton: "#SaveButton",
            dialogCannotRemoveHazard: "#dialogCannotRemoveRiskAssessmentHazard",
            errorSaving: "#errorSaving"
        };

        var selectDeselectAvailableHazard = function(e) {
            SortableListHelper.selectDeselectHazard($(this));
        };

        var appendSelectedHazardsToForm = function() {
            var hazardsIds = SortableListHelper.getItemIds($("ul#selectedHazards"), "data-value");

            $(hazardsIds).each(function(index) {
                var hazardId = hazardsIds[index];
                $("form").append("<input name=\"HazardIds\" type=\"hidden\" value=\"" + hazardId + "\" />");
            });
        };

        var canPersonalRiskAssessmentHazardBeRemoved = function(companyId, riskAssessmentId, riskAssessmentHazardId, callback) {
            var url = window.globalajaxurls.checkPersonalRiskAssessmentHazardCanBeRemovedUrl;
            var data = {
                companyId: companyId,
                riskAssessmentId: riskAssessmentId,
                riskAssessmentHazardId: riskAssessmentHazardId
            };

            AjaxCall.execute(url, callback, data);
        };

        var displayCantRemoveHazardDialog = function() {
            $("#dialogCannotRemoveRiskAssessmentHazard").dialog({
                height: 'auto',
                resizable: false,
                modal: true,
                buttons: {
                    "Ok": function() {
                        $(this).dialog("close");
                    }
                },
                width: '392px'
            });
        };

        var addNewHazardItem = function(hazardToAdd) {
            var newHazardName = $(hazardToAdd).val();
            if (newHazardName.length === 0) return;
            var data = {
                isNew: true,
                companyDefaultId: 0,
                companyDefaultValue: newHazardName,
                companyDefaultType: "Hazards",
                runMatchCheck: false,
                companyId: $("#CompanyId").val(),
                riskAssessmentId: $("#RiskAssessmentId").val(),
                riskAssessmentTypeApplicable: []
            };

            data.riskAssessmentTypeApplicable.push(2);

            var successfulCallBack = function(data) {

                if (data.Success === true) {
                    $("#selectedHazards").append(
                        $("<li></li>")
                        .attr("class", "ui-state-default")
                        .attr("data-value", data.Id)
                        .text(newHazardName));
                } else {
                    window.location.replace(window.globalajaxurls.errorPage);
                }
            };

            var existingMatchesCallBack = function() {
                /// Should not be called because the run match check is set to false
            };

            var url = window.globalajaxurls.createRiskAssessmentDefaults;
            var newData = JSON.stringify(data);
            var contentType = 'application/json; charset=utf-8';
            AjaxCall.execute(url, successfulCallBack, newData, "POST", contentType);

        };

        //Configure multi selectable/sortable list boxes
        $('ul#selectedHazards').multisortable();

        //when user clicks on on list item then highlight the item
        $('ul#availableHazards').find("li").on('click', selectDeselectAvailableHazard);

        $(this.selectors.saveHazardsButton).click(function(event) {
            event.preventDefault();

            appendSelectedHazardsToForm();

            $("form").submit();
        });

        $("#nextBtn, a.riskassessment-tab-links").click(function(event) {
            var isReadOnly = $("#IsReadOnly");
            if (isReadOnly.length > 0) {
                return;
            }

            event.preventDefault();

            var destUrl = $(this).attr("href");

            var f = $("form");
            f.attr('action', window.globalajaxurls.saveAndNextPersonalRiskAssessmentHazards);

            appendSelectedHazardsToForm();

            $.post(f[0].action, f.serialize(), function(result) {
                if (result.Success === true) {
                    window.location = destUrl;
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

        $("#AddNewHazard").on("click", function(event) {
            event.preventDefault();
            var newHazard = $(this).prev('input');
            addNewHazardItem(newHazard);
        });

        //moves highlighted available hazards to the selected hazards
        $("#addSelectedHazards").on("click", function() {
            var selectedElements = SortableListHelper.getSelectedElements("#availableHazards");

            $(selectedElements).each(function(index, item) {
                $("#selectedHazards").append(item);
                $(item).removeClass("selected");
                $(item).removeClass("multiselectable-previous");
                $(item).off('click', selectDeselectAvailableHazard);
            });
        });

        //moves highlighted selected hazards to the available hazards
        $("#removeSelectedHazards").on("click", function() {
            var selectedElements = SortableListHelper.getSelectedElements("#selectedHazards");

            $(selectedElements).each(function(index, item) {
                var companyId = $("#CompanyId").val();
                var riskAssessmentId = $("#RiskAssessmentId").val();
                var riskAssessmentHazardId = $(item).attr("data-value");

                canPersonalRiskAssessmentHazardBeRemoved(companyId, riskAssessmentId, riskAssessmentHazardId, function(data) {
                    if (data.CanBeRemoved) {
                        $("#availableHazards").append(item);
                        $(item).removeClass("selected");
                        $(item).removeClass("multiselectable-previous");
                        $(item).on("click", selectDeselectAvailableHazard);
                    } else {
                        $(item).removeClass("selected");
                        $(item).removeClass("multiselectable-previous");
                        $(item).removeClass("multiselectable-shift");

                        displayCantRemoveHazardDialog();
                    }
                });
            });

        });
    }

    return {
        initialize: initialize
    };
}();
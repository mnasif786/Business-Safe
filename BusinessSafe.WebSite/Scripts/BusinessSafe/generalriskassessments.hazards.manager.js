


var hazardsManager = function () {
    function initialize(passedMessages) {
        var _self = this;
        _self.messages = passedMessages;
        _self.selectors = {
            saveHazardsButton: "#SaveButton",
            dialogCannotRemoveHazard: "#dialogCannotRemoveRiskAssessmentHazard",
            errorSaving: "#errorSaving"
        };

        var selectDeselectAvailableHazard = function (e) {
            SortableListHelper.selectDeselectHazard($(this));
        };

        var appendSelectedHazardsToForm = function () {
            var hazardsIds = SortableListHelper.getItemIds($("ul#selectedHazards"), "data-value");

            $(hazardsIds).each(function (index) {
                var hazardId = hazardsIds[index];
                $("form").append("<input name=\"HazardIds\" type=\"hidden\" value=\"" + hazardId + "\" />");
            });
        };

        var canGeneralRiskAssessmentHazardBeRemoved = function (companyId, riskAssessmentId, riskAssessmentHazardId, callback) {
            var url = window.globalajaxurls.checkGeneralRiskAssessmentHazardCanBeRemovedUrl;
            var data = {
                companyId: companyId,
                riskAssessmentId: riskAssessmentId,
                riskAssessmentHazardId: riskAssessmentHazardId
            };

            AjaxCall.execute(url, callback, data);
        };

        var displayCantRemoveHazardDialog = function () {
            $("#dialogCannotRemoveRiskAssessmentHazard").dialog({
                height: 'auto',
                resizable: false,
                modal: true,
                buttons: {
                    "Ok": function () {
                        $(this).dialog("close");
                    }
                },
                width: '392px'
            });
        };

        var addNewHazardItem = function (hazardToAdd) {
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

            data.riskAssessmentTypeApplicable.push(1);

            var successfulCallBack = function (data) {

                if (data.Success === true) {
                    $("#selectedHazards").append(
                        $("<li></li>")
                        .attr("class", "ui-state-default")
                        .attr("data-value", data.Id)
                        .text(newHazardName));
                }
                else {
                    window.location.replace(window.globalajaxurls.errorPage);
                }
            };

            var existingMatchesCallBack = function () {
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

        $("#AddNewHazard").on("click", function (event) {
            event.preventDefault();
            var newHazard = $(this).prev('input');
            addNewHazardItem(newHazard);
        });

        //moves highlighted available hazards to the selected hazards
        $("#addSelectedHazards").on("click", function () {
            var selectedElements = SortableListHelper.getSelectedElements("#availableHazards");

            $(selectedElements).each(function (index, item) {
                $("#selectedHazards").append(item);
                $(item).removeClass("selected");
                $(item).removeClass("multiselectable-previous");
                $(item).off('click', selectDeselectAvailableHazard);
            });
        });

        //moves highlighted selected hazards to the available hazards
        $("#removeSelectedHazards").on("click", function () {
            var selectedElements = SortableListHelper.getSelectedElements("#selectedHazards");

            $(selectedElements).each(function (index, item) {
                var companyId = $("#CompanyId").val();
                var riskAssessmentId = $("#RiskAssessmentId").val();
                var riskAssessmentHazardId = $(item).attr("data-value");

                canGeneralRiskAssessmentHazardBeRemoved(companyId, riskAssessmentId, riskAssessmentHazardId, function(data) {
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

        $(this.selectors.saveHazardsButton).click(function (event) {
            event.preventDefault();

            appendSelectedHazardsToForm();
            $("#PeopleAtRiskIds option").prop('selected', true);

            $("form").submit();
        });

        $("#nextBtn, a.riskassessment-tab-links").click(function (event) {

            var isReadOnly = $("#IsReadOnly");
            if (isReadOnly.length > 0) {
                return;
            }

            event.preventDefault();

            var destUrl = $(this).attr("href");

            var f = $("form");
            f.attr('action', window.globalajaxurls.saveAndNextGeneralRiskAssessmentHazards);

            appendSelectedHazardsToForm();
            $("#PeopleAtRiskIds option").prop('selected', true);

            $.post(f[0].action, f.serialize(), function (result) {
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



        //ADD PERSON AT RISK JAVASCRIPT SECTION

        var addNewPersonAtRiskItem = function (personToRiskToAdd) {
            var newPersonAtRisk = $(personToRiskToAdd).val();
            if (newPersonAtRisk.length === 0) return;
            var data = {
                isNew: true,
                companyDefaultId: 0,
                companyDefaultValue: newPersonAtRisk,
                companyDefaultType: "PeopleAtRisk",
                runMatchCheck: false,
                companyId: $("#CompanyId").val(),
                riskAssessmentId: $("#RiskAssessmentId").val()
            };

            var successfulCallBack = function (data) {

                if (data.Success === true) {
                    $('#PeopleAtRiskIds').
                        append($("<option></option>").
                        attr("value", data.Id).
                        text(newPersonAtRisk));
                    $(personToRiskToAdd).val('');
                }
                else {
                    window.location.replace(window.globalajaxurls.errorPage);
                }
            };

            var existingMatchesCallBack = function () {
                /// Should not be called because the run match check is set to false
            };

            var url = window.globalajaxurls.createRiskAssessmentDefaults;
            AjaxCall.execute(url, successfulCallBack, data, "POST");

        };

        $("#AddNewPersonAtRisk").on("click", function (event) {
            event.preventDefault();
            var newPersonAtRisk = $(this).prev('input');
            addNewPersonAtRiskItem(newPersonAtRisk);
        });

        var eventBindingObject = {};

        var peopleAtRiskOptions = { id: "people-at-risk-multi-select" };

        PBS.Plugins.MultiSelectAndSearch(eventBindingObject, peopleAtRiskOptions);

        $(eventBindingObject).bind('added-item', function (event, addedItem) {

            var parentMultiSelect = $(addedItem).closest('.search-and-multi-select');
            if (parentMultiSelect.attr("id") === "people-at-risk-multi-select") {

                var id = parseInt($(addedItem).attr('data-value'), 10);
                var message = _self.messages.DisplayMessages[0];
                if ($.inArray(id, message.IdsApplicable) >= 0 && $('#people-at-risk-alert').size() === 0) {
                    parentMultiSelect.parent().after('<div id="people-at-risk-alert"><div class="span12"><div class="alert alert-error">' + message.MessageToShow + '</div></div>');
                }
            }
        });

        $(eventBindingObject).bind('removed-item', function (event, removedItem) {
            var alertMessage = $('#people-at-risk-alert');
            if ($('#people-at-risk-alert').size() === 0)
                return;

            var remainingSelections = $('#PeopleAtRiskIds').children();
            var messageStillApplicable = false;
            remainingSelections.each(function () {
                var message = _self.messages.DisplayMessages[0];
                var id = parseInt($(this).val(), 10);
                if ($.inArray(id, message.IdsApplicable) >= 0) {
                    messageStillApplicable = true;
                }
            });

            if (!messageStillApplicable)
                alertMessage.remove();
        });

        $("#PeopleAtRiskIds option").each(function(index, item){
            var parentMultiSelect = $(item).closest('.search-and-multi-select');
            var id = parseInt($(item).val(),10);
            var message = _self.messages.DisplayMessages[0];
            if ($.inArray(id, message.IdsApplicable) >= 0 && $('#people-at-risk-alert').size() === 0) {
                parentMultiSelect.parent().after('<div id="people-at-risk-alert"><div class="span12"><div class="alert alert-error">' + message.MessageToShow + '</div></div>');
            }
        });
     
    }

    return {
        initialize: initialize
    };
} ();
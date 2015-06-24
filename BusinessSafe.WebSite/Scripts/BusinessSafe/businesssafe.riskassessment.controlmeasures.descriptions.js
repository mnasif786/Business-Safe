
$(function () {

    var selectors = {
        riskAssessmentId: "#RiskAssessmentId",
        companyId: "#CompanyId",
        dialogFurtherActionTask: "#dialogDeleteFurtherActionTask",
        dialogDeleteReoccurringFurtherControlMeasureTask: "#dialogDeleteReoccurringFurtherControlMeasureTask"
    };

    // Initialise the edit hazard description row
    $("a.edit-hazard-title").live('click', function (event) {
        event.preventDefault();

        var editLink = $(this);
        var hazardId = parseInt(editLink.attr("data-id"), 10);
        var titleDisplay = editLink.prev();
        var currentTitle = editLink.parent().attr("data-text");
        var editDescriptionControls = $("<span><input id='newHazardTitle' name='newHazardTitle' type='text'  class='input-large' value='" + currentTitle + "' ></input><button id='saveHazardTitle' class='btn btn-primary save-hazard-title-button' data-id='" + hazardId + "'>Save</button><button class='btn btn-link cancel-hazard-title'>Cancel</button><span id='error-container-" + hazardId + "'></span></span>");

        titleDisplay.addClass('hide');
        editLink.addClass('hide');
        $(editDescriptionControls).insertBefore(editLink);
        $("#newHazardTitle").focus();
        $(hazardControls).trigger('HazardDescriptionInEditState');
    });

    $("a.edit-hazard-description").live('click', function (event) {
        event.preventDefault();

        var editLink = $(this);
        var hazardId = parseInt(editLink.attr("data-id"), 10);
        var description = editLink.parent('p');
        var currentDescription = description.attr("data-text");
        var editDescriptionControls = $("<div><br/><textarea id='newHazardDescription' rows='4' name='newHazardDescription' class='span9' >" + currentDescription + "</textarea><button id='saveHazardDescription' class='btn btn-primary save-hazard-description-button' data-id='" + hazardId + "'>Save</button><button class='btn btn-link cancel-hazard-description'>Cancel</button></div>");

        description.addClass('hide');
        editLink.addClass('hide');
        $(editDescriptionControls).insertAfter(description);
        $("#newHazardDescription").focus();
        $(hazardControls).trigger('HazardDescriptionInEditState');
    });


    $("button.save-hazard-title-button").live('click', function (event) {
        event.preventDefault();

        var saveButton = $(this);
        var newHazardTitleValue = $("#newHazardTitle").val();
        var riskAssessmentHazardId = parseInt(saveButton.attr("data-id"), 10);
        var riskAssessmentId = $(selectors.riskAssessmentId).val();
        var editHazardTitleContainer = $(this).parent();
        var titleDisplay = editHazardTitleContainer.prev('h3');

        var data = {
            RiskAssessmentId: riskAssessmentId,
            RiskAssessmentHazardId: riskAssessmentHazardId,
            Title: newHazardTitleValue
        };

        var errorContainer = "#error-container-" + riskAssessmentHazardId;

        $.ajax({
            data: data,
            type: "POST",
            url: window.globalajaxurls.saveRiskAssessmentHazardTitleUrl,
            error: function () {
                window.location.replace(window.globalajaxurls.errorPage);
            }
        }).done(function (data) {
            if (data.Success === "false") {
                $(errorContainer).html("<span class='alert alert-error edit-hazard-title-error-display' style='margin-left: 10px;'>" + data.Errors + "</span>");
            } else {
                editHazardTitleContainer.parent().attr("data-text", newHazardTitleValue);
                //description.find('span').empty();
                titleDisplay.text(newHazardTitleValue); ;

                editHazardTitleContainer.remove();
                titleDisplay.removeClass('hide');
                $(".edit-hazard-title").removeClass('hide');
                $(errorContainer).html("");
            }
        });
    });

    $("button.cancel-hazard-title").live('click', function (event) {
        event.preventDefault();
        var editHazardTitleContainer = $(this).parent();
        var titleDisplay = editHazardTitleContainer.prev('h3');
        editHazardTitleContainer.remove();
        titleDisplay.removeClass('hide');
        $(".edit-hazard-title").removeClass('hide');
    });

    // When showing the edit hazard description row
    $("button.save-hazard-description-button").live('click', function (event) {
        event.preventDefault();

        var saveButton = $(this);
        var newHazardValue = $("#newHazardDescription").val();
        var riskAssessmentHazardId = parseInt(saveButton.attr("data-id"), 10);
        var riskAssessmentId = $(selectors.riskAssessmentId).val();
        var editHazardDescriptionContainer = $(this).parent();
        var description = editHazardDescriptionContainer.siblings('p');
        var model = { RiskAssessmentId: riskAssessmentId, RiskAssessmentHazardId: riskAssessmentHazardId, Description: newHazardValue };
        var errorContainer = "#error-container-" + riskAssessmentHazardId;

        $.ajax({
            data: model,
            type: "POST",
            url: window.globalajaxurls.saveRiskAssessmentHazardDescriptionUrl,
            error: function () {
                window.location.replace(window.globalajaxurls.errorPage);
            }
        }).done(function (data) {
            if (data.Success === "false") {
                $(errorContainer).html("<span class='alert alert-error edit-hazard-description-error-display' style='margin-left: 10px;'>" + data.Errors + "</span>");
            } else {
                description.removeClass('hide');
                description.attr("data-text", newHazardValue);
                description.find('span').empty();
                description.find('span').text(newHazardValue);
                editHazardDescriptionContainer.remove();
                $(".edit-hazard-description").removeClass('hide');
                $(errorContainer).html("");
            }
        });
    });

    $("button.cancel-hazard-description").live('click', function (event) {
        event.preventDefault();

        var editHazardDescriptionContainer = $(this).parent();
        var description = editHazardDescriptionContainer.prev('p');

        editHazardDescriptionContainer.remove();
        description.removeClass('hide');

        $(".edit-hazard-description").removeClass('hide');
        $(".hazard-error-container").html('');
    });
});


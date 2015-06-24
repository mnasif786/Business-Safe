var setReoccurring = function (reoccurringCheckbox) {
    var selectors = {
        reoccurringDiv: "#reoccurringDiv",
        nonReoccurringDiv: "#nonReoccurringDiv",        
        reoccurringDiv: "#reoccurringDiv",
        nonReoccurringDiv: "#nonReoccurringDiv",
        reoccurringFormActionUrl: "#ReoccurringFormActionUrl",
        nonReoccurringFormActionUrl: "#NonReoccurringFormActionUrl"        
    };


    var form = $("#FurtherControlMeasureTaskForm");

    if (form.length === 0)
        form = $("#FurtherActionTask");

    var reoccurringDiv = $(selectors.reoccurringDiv);
    var nonReoccurringDiv = $(selectors.nonReoccurringDiv);
    var checked = $(reoccurringCheckbox).attr('checked');

    if (checked === "checked") {
        form.attr('action', $(selectors.reoccurringFormActionUrl).val());
        reoccurringDiv.removeClass("hide");
        nonReoccurringDiv.addClass("hide");
    }
    else {
        form.attr('action', $(selectors.nonReoccurringFormActionUrl).val());
        reoccurringDiv.addClass("hide");
        nonReoccurringDiv.removeClass("hide");
    }

};
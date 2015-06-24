function MarkRiskAssessmentDraftStatus(markRiskAssessmentDraftStatusUrl, companyId, riskAssessmentId, isDraft, successCallBack) {

    this.execute = function () {
        $.ajax({
            url: markRiskAssessmentDraftStatusUrl,
            error: function () {
                window.location.replace(window.globalajaxurls.errorPage);
            },
            type: "POST",
            dataType: "json",
            data: {
                companyId: companyId,
                riskAssessmentId: riskAssessmentId,
                isDraft: isDraft
            },
            success: function (data) {
                successCallBack(data);
            }
        });
    };
}

var generalRiskAssessmentSummary = function () {
    var selectors = {
        isDraft: "#IsDraft",
        companyId: "#CompanyId",
        riskAssessmentId: "#RiskAssessmentId, #HazardousSubstanceRiskAssessmentId",
        draftLabel: ".draft-label",
        liveLabel: ".live-label",
        markRiskAssessmentDraftStatusUrl: "#MarkRiskAssessmentDraftStatusUrl",
        dialogMarkAsLive: "#DialogMarkAsLive"
    };

    function initialise() {
        $(selectors.isDraft).change(function (event) {

            var markRiskAssessmentDraftStatusUrl = $(selectors.markRiskAssessmentDraftStatusUrl).val();
            var companyId = $(selectors.companyId).val();
            var riskAssessmentId = $(selectors.riskAssessmentId).val();
            var isDraft = this.checked;

            new MarkRiskAssessmentDraftStatus(markRiskAssessmentDraftStatusUrl, companyId, riskAssessmentId, isDraft, function (data) {
                if (data.Success) {
                    // Remove the draft flag if it exists
                    $(selectors.draftLabel).addClass("hide");
                    $(selectors.liveLabel).removeClass("hide");

                    if (isDraft) {
                        $(selectors.draftLabel).removeClass("hide");
                        $(selectors.liveLabel).addClass("hide");
                    }
                } else {
                    //Restore the UI back to original value in a case of data.Success fails.
                    $(selectors.isDraft).prop('checked', !isDraft);
                    showDialoge(data.Message);
                }
            }).execute();
        });
    }

    function showDialoge(textToDisplay) {
        $(selectors.dialogMarkAsLive).dialog({
            modal: true,
            width: '262px',
            height: 'auto',
            resizable: false,
            buttons: {
                "Ok": function () {
                    $(this).dialog("close");
                }
            }
        });
        $(selectors.dialogMarkAsLive).text(textToDisplay);
        $(selectors.dialogMarkAsLive).dialog("open");
    };

    return { initialise: initialise };
} ();


$(function () {
    generalRiskAssessmentSummary.initialise();
});
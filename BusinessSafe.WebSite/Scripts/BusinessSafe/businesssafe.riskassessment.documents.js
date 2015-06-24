var riskAssessmentDocumentsManager = function () {
    var selectors = {
        nextTab: "#nextBtn, a.riskassessment-tab-links"
    };

    function initialise(url) {
        $(selectors.nextTab).click(function (event) {
            var isReadOnly = $("#IsReadOnly");

            if (isReadOnly.length > 0) {
                return;
            }

            event.preventDefault();

            var destUrl = $(this).attr("href");
            var f = $('form:first');

            f.attr('action', url);

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
    }

    return { initialise: initialise };
} ();
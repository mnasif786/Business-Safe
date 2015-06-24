BusinessSafe.AccidentRecord.Overview = function () {
    var _formInputs = {
        isReadOnly: '#IsReadOnly',
        saveAndNextButtons: '#nextBtn, a.riskassessment-tab-links',
        validationSummary: '.validation-summary-errors'
    };

    var setupSaveAndNextButtons = function () {
        $(_formInputs.saveAndNextButtons).click(function (event) {
            var isReadOnly = $(_formInputs.isReadOnly);

            if (isReadOnly.length > 0) {
                return;
            }

            event.preventDefault();

            var destUrl = $(this).attr("href");
            var f = $('form:first');

            f.attr('action', '/AccidentReports/Overview/SaveAndNext');
            $.post(f[0].action, f.serialize(), function (result) {
                if (result.Success === true) {
                    window.location = destUrl;
                }
                else {
                    if ($(_formInputs.validationSummary).size() === 0) {
                        f.prepend('<div><div class="validation-summary-errors alert alert-error"></div></div>');
                    }
                    showValidationErrors(_formInputs.validationSummary, result.Errors);
                }
            }).error(function (error) {
                window.location.replace(window.globalajaxurls.errorPage);
            });
        });
    };

    var _initialise = function () {
        setupSaveAndNextButtons();
    };

    return {
        initialise: _initialise
    };
} ();
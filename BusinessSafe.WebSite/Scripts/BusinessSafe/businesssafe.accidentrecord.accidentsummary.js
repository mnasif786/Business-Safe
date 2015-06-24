function MarkAccidentRecordStatus(markAccidentRecordStatusUrl, accidentRecordId, isClosed, successCallBack) {

    this.execute = function () {
        $.ajax({
            url: markAccidentRecordStatusUrl,
            error: function () {
                window.location.replace(window.globalajaxurls.errorPage);
            },
            type: "POST",
            dataType: "json",
            data: {
                accidentRecordId: accidentRecordId,
                isClosed: isClosed
            },
            success: function (data) {
                successCallBack(data);
            }
        });
    };
}

BusinessSafe.AccidentRecord.AccidentSummary = function () {
    var _formInputs = {
        isReadOnly: '#IsReadOnly',
        saveAndNextButtons: '#nextBtn, a.riskassessment-tab-links',
        validationSummary: '.validation-summary-errors',
        isClosed: "#IsClosed",
        closeLabel: '.closed-label',
        openLabel: '.open-label',
        companyId: '#CompanyId',
        accidentRecordId: '#AccidentRecordId',
        markAccidentRecordStatusUrl: "#MarkAccidentRecordStatusUrl"
    };

    var setupStatusLabels = function() {
        $(_formInputs.isClosed).change(function (event) {
        
            var markAccidentRecordStatusUrl = $(_formInputs.markAccidentRecordStatusUrl).val();
            var accidentRecordId = $(_formInputs.accidentRecordId).val();
            var isClosed = this.checked;

            new MarkAccidentRecordStatus(markAccidentRecordStatusUrl, accidentRecordId, isClosed, function (data) {
                if (data.success === true) {
                    // Remove the closed flag if it exists
                    $(_formInputs.closeLabel).addClass("hide");
                    $(_formInputs.openLabel).removeClass("hide");

                    if (isClosed) {
                        $(_formInputs.closeLabel).removeClass("hide");
                        $(_formInputs.openLabel).addClass("hide");
                    }
                }
            }).execute();
        });
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

            $.post('/AccidentReports/Summary/SaveAndNext', f.serialize(), function (result) {
                if (result.Success === true) {
                    window.location = destUrl;
                }
                else {
                    if ($(_formInputs.validationSummary).size() === 0) {
                        f.prepend('<div><div class="validation-summary-errors alert alert-error"></div></div>');
                    }
                    showValidationErrors(_formInputs.validationSummary, result.Errors);
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
    };
    
    var _initialise = function (employees, sites, types, causes) {
        setupSaveAndNextButtons();
        setupStatusLabels();
    };

    return {
        initialise: _initialise
    };
} ();
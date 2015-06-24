
BusinessSafe.Checklists = function () {

    var _setupTextExpand = function () {
        var hiddenDiv = $(document.createElement('div')),
                        content = null;

        hiddenDiv.addClass('cloned-textarea');
        $('body').append($(hiddenDiv));

        $('textarea').keyup(function () {

            content = $(this).val();

            content = content.replace(/\n/g, '<br />');
            hiddenDiv.html(content + '<br />').css('width', $(this).css('width'));

            $(this).css('height', hiddenDiv.height());

        });
    };

    var _validate = function () {
        var submitButton = $(this);
        var errorClass = 'field-validation-error';
        var errorMessage = 'Please select Yes or No';

        // remove any error messages
        $('.' + errorClass).each(function () {
            $(this).remove();
        });

        var hasErrors = false;

        // add any error messages for radio buttons not selected
        $('div.question').each(function () {
            var questionId = $(this).attr('data-question-id');
            var radioButtonName = "YesNo_" + questionId;
            if ($('input[name="' + radioButtonName + '"]').length > 0 &&
                            $('input[name="' + radioButtonName + '"]:checked').attr('value') === undefined) {
                $('input[name="' + radioButtonName + '"]').siblings('label').last().after('<span class="' + errorClass + '">' + errorMessage + '</span>');
                hasErrors = true;
            }
        });

        // scroll to first error
        var firstError = $('.' + errorClass).first();
        if (firstError.length > 0) {
            var newPosition = $(firstError).offset();
            window.scrollTo(newPosition.left, newPosition.top);
        }

        return hasErrors;
    };

    var _initialise = function (saveForLaterURL, completeChecklistURL) {
        _setupTextExpand();

        $('#SaveForLaterButton').click(function (event) {
            event.preventDefault();

            var f = $("form");
            f.attr('action', saveForLaterURL);
            f.submit();
        });

        $('input[type="submit"]').click(function (event) {
            event.preventDefault();

            // submit form
            if (!_validate()) {
                $('#completeChecklistDialog').dialog({
                    buttons: {
                        "Yes": function () {
                            var f = $("form");
                            f.attr('action', completeChecklistURL);
                            f.submit();
                        },
                        "No": function () {
                            $(this).dialog("close");
                        }
                    },
                    resizable: false
                });
            }
        });
    };

    return { initialise: _initialise };
} ();
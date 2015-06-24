
BusinessSafe.FireRiskAssessment.Checklists = function () {

    var _selectors = {
        clonedTextAreaClass: 'cloned-textarea',
        areaErrorClass: 'in-error',
        inlineErrorClass: 'field-validation-error',
        guidanceNotesIcon: 'a.icon-question-sign',
        checklistTabs: "#checklistTabs",
        togglableButtonsSelector: '.tab-dependent-visibility-button',
        previousButton: '#previousButton',
        nextButton: '#nextButton',
        saveAndContinue: '#saveAndContinue',
        nextBtn: '#nextBtn',
        save: '#save',
        submit: '#saveButton',
        successMessage: '#successMessage'
    };

    var _setupTextExpand = function () {
        var hiddenDiv = $(document.createElement('div')),
                        content = null;

        hiddenDiv.addClass(_selectors.clonedTextAreaClass);
        $('body').append($(hiddenDiv));

        $('textarea').keyup(function () {
            content = $(this).val();

            content = content.replace(/\n/g, '<br />');
            hiddenDiv.html(content + '<br />').css('width', $(this).css('width'));

            $(this).css('height', hiddenDiv.height());
        });
    };

    var _setupGuidanceNotesPopover = function () {
        $(_selectors.guidanceNotesIcon).on('click', function (event) {
            event.preventDefault();

            var label = $(this).parent().siblings('label').clone(true);

            $("span", label).remove();


            $(this).
                attr('title', label.html()).
                popover({
                    title: "Guidance Notes",
                    placement: 'left'
                });

            $(this).popover("show");
        });
    };

    var _setMax500Characters = function () {
        $('.max500chars').on('keypress', function (event) {
            if ($(this).val().length >= 500) {
                event.preventDefault();
                alert('Comment box has a 500 character limit.');
            }
        });
    };

    var _unsetErrorStyles = function () {
        $('.' + _selectors.inlineErrorClass).each(function () {
            $(this).remove();
        });
        $('label.' + _selectors.areaErrorClass).removeClass(_selectors.areaErrorClass);
        $('ol.section li').removeClass(_selectors.areaErrorClass);
        $('ul.nav-tabs li a').removeClass(_selectors.areaErrorClass);
        $('textarea.input-validation-error').removeClass('input-validation-error');
        $('#checklists-error').remove();
    };

    var _setErrorStyles = function (radioButtonName, errorMessageToDisplay, additionalInfoName) {
        var radioButton = $('input[name="' + radioButtonName + '"]');
        $(radioButton).
            siblings('label').last().
            after('<span class="' + _selectors.inlineErrorClass + '">' + errorMessageToDisplay + '</span>').
            parent().siblings('label').addClass(_selectors.areaErrorClass); // add error style to question

        if (additionalInfoName !== null) {
            $('textarea[name="' + additionalInfoName + '"]').addClass('input-validation-error');
        }


        var parentFieldSetId = $(radioButton).closest('fieldset').attr('id');
        $('a[href="#' + parentFieldSetId + '"]').addClass(_selectors.areaErrorClass);
    };

    var _yesesAreValid = function (requiresAnswers) {
        var unselectedErrorMessage = 'Please select an answer';
        var yesCommentErrorMessage = 'Please enter a comment';

        // remove any error messages
        _unsetErrorStyles();

        var isValid = true;

        // add any error messages for radio buttons not selected
        $('div.question').each(function () {
            var questionId = $(this).attr('data-question-id');
            var radioButtonName = "YesNoNotApplicable_" + questionId;
            var additionalInfoName = "AdditionalInfo_" + questionId;

            if ($('input[name="' + radioButtonName + '"]').length > 0 &&
                requiresAnswers === true &&
                $('input[name="' + radioButtonName + '"]:checked').attr('value') === undefined) {

                _setErrorStyles(radioButtonName, unselectedErrorMessage);
                isValid = false;
            }
            else if ($('input[name="' + radioButtonName + '"]:checked').val() === 'Yes' &&
                $('textarea[name="' + additionalInfoName + '"]').val() === "") {
                _setErrorStyles(radioButtonName, yesCommentErrorMessage, additionalInfoName);
                isValid = false;
            }
        });
        return isValid;
    };

    var _validateAllQuestions = function() {
        var url = window.globalajaxurls.getFireRiskAssessmentValidateChecklistUrl;
        var noCommentErrorMessage = 'Please add a Further Control Measure Task';
        var allNoAnswerQuestionIds = [];

        // add any error messages for radio buttons not selected
        $('div.question').each(function() {
            var questionId = $(this).attr('data-question-id');
            var radioButtonName = "YesNoNotApplicable_" + questionId;
            if ($('input[name="' + radioButtonName + '"]:checked').val() === 'No') {
                allNoAnswerQuestionIds.push(questionId);
            }
        });

        var isValid = _yesesAreValid(true);

        var successfulCallBack = function(successData) {
            for (var i = 0; i < successData.Errors.length; i++) {
                var questionId = successData.Errors[i].PropertyName;
                var radioButtonName = "YesNoNotApplicable_" + questionId;

                _setErrorStyles(radioButtonName, noCommentErrorMessage);
            }

            if (isValid) {
                isValid = successData.Errors.length === 0;
            }

            if (isValid) {
                // submit form
                $('form').attr('action', window.globalajaxurls.getFireRiskAssessmentCompleteChecklistUrl);
                $('form').attr('method', 'POST');
                $('form').submit();
            } else {
                _displayValidationMessages();
            }

        };

        var data = {
            ChecklistId: $('#FireRiskAssessmentChecklistId').val(),
            AllNoAnswerQuestionIds: allNoAnswerQuestionIds
        };
        jQuery.ajaxSettings.traditional = true;
        AjaxCall.execute(url, successfulCallBack, data, 'GET', 'text/html; charset=utf-8');
    };

    var _hideSuccessfullyNotices = function () {
        $(".alert-success").addClass('hide');
    };

    var _displayValidationMessages = function () {
        $('.checklists').prepend('<div id="checklists-error" class="alert alert-error">There were errors in your submission, please correct them before continuing.</div>');

        // move to first tab with errors
        var firstErrorTabFound = false;
        $('.nav-tabs li').each(function () {
            if (!firstErrorTabFound) {
                var anchorTag = $(this).children('a');
                if (anchorTag.hasClass('in-error')) {
                    firstErrorTabFound = true;
                    anchorTag.click();
                }
            }
        });
    };

    var _saveCurrentChecklist = function () {

        var isReadOnly = $("#IsReadOnly");

        if (isReadOnly.length > 0) {
            return;
        }


        var data = $('form').serialize();
        $.ajax({
            url: window.globalajaxurls.getFireRiskAssessmentSaveChecklistOnlyForAuditingUrl,
            type: 'POST',
            data: data,
            async: false,
            cache: false,
            timeout: 30000,
            error: function (jqXhr, textStatus, errorThrown) {
                if (jqXhr.status !== 0) {
                    if (window.debugErrorHandler === undefined) {
                        window.location.replace(window.globalajaxurls.errorPage);
                    } else {
                        alert("DEBUG: AjaxCall.execute encountered a problem.");
                    }
                }
            },
            success: function (msg) {
                return;
            }
        });
    };

    var _saveChecklistOnTabChange = function () {
        $('a[data-toggle="tab"]').on('click', function (e) {
            _saveCurrentChecklist();
        });
    };

    var _setPreviousNextVisibilityOnTabChange = function () {
        $('a[data-toggle="tab"]').on('shown', function (e) {
            _setVisibilityOfPreviousNextButtons();
        });
    };

    var _decideWhetherToShowSaveAndNextButtons = function (tabs) {
        $(_selectors.togglableButtonsSelector).hide();
        var tabIndex = $(tabs).parent().index();
        var totalTabs = $('.nav-tabs li').size() - 1;
        if (tabIndex == totalTabs) {
            $(_selectors.togglableButtonsSelector).css('visibility', 'visible').fadeIn();
        }
        else {
            $(_selectors.togglableButtonsSelector).hide();
        }
    };

    var _setupSaveButtonDisplayToggling = function () {
        $('.nav-tabs a').on('shown', function () {
            _decideWhetherToShowSaveAndNextButtons(this);
        });
    };

    var _setVisibilityOfPreviousNextButtons = function () {
        var currentIndex = $('#checklistTabs li.active').index();

        if (currentIndex === 0) {
            $(_selectors.previousButton).hide();
            $(_selectors.nextBtn).hide();
            $(_selectors.submit).hide();

            $(_selectors.nextButton).show();
            $(_selectors.saveAndContinue).show();
            $(_selectors.save).show();
        }
        else if (currentIndex === 7) {
            $(_selectors.previousButton).show();
            $(_selectors.nextBtn).show();
            $(_selectors.submit).show();

            $(_selectors.save).hide();
            $(_selectors.nextButton).hide();
            $(_selectors.saveAndContinue).hide();
        } else {
            $(_selectors.nextBtn).hide();
            $(_selectors.submit).hide();

            $(_selectors.previousButton).show();
            $(_selectors.nextButton).show();
            $(_selectors.saveAndContinue).show();
            $(_selectors.save).show();
        }
    };

    var _setFunctionalityOfPreviousNextButtons = function () {

        $(_selectors.previousButton).click(function (event) {
            _saveCurrentChecklist();
            var previousIndex = $('#checklistTabs li.active').index() - 1;
            $('#checklistTabs li:nth-child(' + (previousIndex + 1) + ') a').tab('show');
            _setVisibilityOfPreviousNextButtons();
        });

        $(_selectors.nextButton).click(function (event) {
            _saveCurrentChecklist();
            var nextIndex = $('#checklistTabs li.active').index() + 1;
            $('#checklistTabs li:nth-child(' + (nextIndex + 1) + ') a').tab('show');
            _setVisibilityOfPreviousNextButtons();
        });

        $(_selectors.saveAndContinue).click(function (event) {
            _saveCurrentChecklist();
            var nextIndex = $('#checklistTabs li.active').index() + 1;
            $('#checklistTabs li:nth-child(' + (nextIndex + 1) + ') a').tab('show');
            _setVisibilityOfPreviousNextButtons();
        });

        $(_selectors.save).click(function (event) {
            _saveCurrentChecklist();
            var currentIndex = $('#checklistTabs li.active').index() + 1;
            $('#checklistTabs li:nth-child(' + (currentIndex) + ') a').tab('show');
            $(_selectors.successMessage).show();
            $(_selectors.successMessage).fadeOut(1600, 'linear', null);
            _setVisibilityOfPreviousNextButtons();
        });
    };

    var _initialise = function () {
        _setupTextExpand();
        _setupGuidanceNotesPopover();
        _setMax500Characters();
        _saveChecklistOnTabChange();
        _setupSaveButtonDisplayToggling();
        _setFunctionalityOfPreviousNextButtons();
        _setVisibilityOfPreviousNextButtons();
        _setPreviousNextVisibilityOnTabChange();

        $(_selectors.successMessage).hide();
        
        $('#saveButton').click(function (event) {
            event.preventDefault();

            _hideSuccessfullyNotices();

            if (!_yesesAreValid(false)) {
                _displayValidationMessages();
                return;
            }

            var f = $("form");
            f.attr('action', window.globalajaxurls.getFireRiskAssessmentSaveChecklistUrl);
            $('form').attr('method', 'POST');
            f.submit();
        });

        $('#nextBtn').click(function (event) {
            event.preventDefault();

            if (!_yesesAreValid(false)) {
                _displayValidationMessages();
                return;
            }

            var destUrl = $(this).attr("href");

            var data = $('form:first').serialize();
            $.ajax({
                url: window.globalajaxurls.getFireRiskAssessmentSavenAndNextChecklistUrl,
                type: 'POST',
                data: data,
                async: false,
                cache: false,
                timeout: 30000,
                error: function (jqXhr, textStatus, errorThrown) {
                    if (jqXhr.status !== 0) {
                        if (window.debugErrorHandler === undefined) {
                            window.location.replace(window.globalajaxurls.errorPage);
                        } else {
                            alert("DEBUG: AjaxCall.execute encountered a problem.");
                        }
                    } else {
                        window.location.reload();
                    }
                },
                success: function (msg) {
                    window.location = destUrl;
                    return;
                }
            });
        });

        $('input#complete-checklists').on('click', function (event) {
            event.preventDefault();

            _hideSuccessfullyNotices();

            _validateAllQuestions();
        });

        $("a.riskassessment-tab-links").click(function (event) {
            var isReadOnly = $("#IsReadOnly");

            if (isReadOnly.length > 0) {
                return;
            }

            event.preventDefault();
            // prevent multiple submissions 
            disableTabs();

            var destUrl = $(this).attr("href");
           
            var data = $('form:first').serialize();
            $.ajax({
                url: window.globalajaxurls.getFireRiskAssessmentSavenAndNextChecklistUrl,
                type: 'POST',
                data: data,
                async: false,
                cache: false,
                timeout: 30000,
                error: function (jqXhr, textStatus, errorThrown) {
                    if (jqXhr.status !== 0) {
                        if (window.debugErrorHandler === undefined) {
                            window.location.replace(window.globalajaxurls.errorPage);
                        } else {
                            alert("DEBUG: AjaxCall.execute encountered a problem.");
                        }
                    } else {
                        window.location.reload();
                    }
                },
                success: function (msg) {
                    window.location = destUrl;
                    return;
                }
            });
        });

        var disableTabs = function () {

            $("a.riskassessment-tab-links").off();
            $("a.riskassessment-tab-links").on('click', function (event) {
                event.preventDefault();
            });
        };

        checkFireAnswersCanBeChanged.initialize();

    };

    return { initialise: _initialise };

} ();
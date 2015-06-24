BusinessSafe.AccidentRecord.InjuryDetails = function () {
    var self = this;
    var _bodyPartSelector;
    var _injurylistSelector;
    var unknownBodyPartId = "17";
    var unknownInjuryId = "16";

    var selectors = {
        companyId: '#CompanyId',
        accidentRecordId: '#AccidentRecordId',
        isReadOnly: '#IsReadOnly',
        timeOff: '#time-off',
        Form: "#InjuryDetailsForm",
        SaveButton: "#saveButton",
        InjuredPersonAbleToCarryOutWork: "input[name='InjuredPersonAbleToCarryOutWork']",
        OtherInjuryDescription: "#otherInjuryDescription",
        OtherBodyPartDescription: "#otherBodyPartDescription",
        addRemoveInjuryButtons: '#injury-multi-select input[type=button]',
        addRemoveBodyPartButtons: '#bodypart-multi-select input[type=button]',
        Severity: "input[name='SeverityOfInjury']",
        InjuredPersonAbleToCarryOutWorkSection: "#InjuredPersonAbleToCarryOutWorkSection",
        TakenToHospitalSection: "#TakenToHospitalSection",
        addNewInjuryButton: "#AddNewInjury",
        selectedInjuryIds: "#SelectedInjuryIds",
        newInjury: "#NewInjury",
        saveAndNextButtons: '#nextBtn, a.riskassessment-tab-links',
        guidanceNotes: '#GuidanceNotes',
        validationSummary: '.validation-summary-errors'
    };

    var addNewInjuryOption = function () {

        var newOptionAdding = $(selectors.newInjury).val();
        if (newOptionAdding.length === 0) return;

        var data = {
            isNew: true,
            companyDefaultId: 0,
            companyDefaultValue: newOptionAdding,
            companyDefaultType: 'Injury',
            runMatchCheck: false,
            companyId: $(selectors.companyId).val(),
            riskAssessmentId: $(selectors.accidentRecordId).val()
        };

        var successfulCallBack = function (data) {

            if (data.Success === true) {
                $(selectors.selectedInjuryIds).
                append($("<option></option>").
                attr("value", data.Id).
                text(newOptionAdding));
                $(selectors.newInjury).val('');
            }
            else {
                window.location.replace(window.globalajaxurls.errorPage);
            }
        };


        var url = window.globalajaxurls.createRiskAssessmentDefaults;
        AjaxCall.execute(url, successfulCallBack, data, "POST");

    };


    this.initialise = function () {
        setupInjuryList();
        setupBodyPartList();
        setTimeOffDisplay();
        setBodyPartDescriptionDisplay();
        setInjuryDescriptionDisplay();
        setSeverityDisplay();


        //config event handlers
        $(selectors.SaveButton).on('click', function (event) {
            event.preventDefault();
            submitForm();
        });

        $(selectors.InjuredPersonAbleToCarryOutWork).on('click', function (event) {
            setTimeOffDisplay();
        });

        $(selectors.addRemoveInjuryButtons).on('click', function () {
            setInjuryDescriptionDisplay();
        });

        $(selectors.addRemoveBodyPartButtons).on('click', function () {
            setBodyPartDescriptionDisplay();
        });

        $(selectors.Severity).on('click', function (event) {
            setSeverityDisplay();
        });

        $(selectors.addNewInjuryButton).on('click', function (event) {
            event.preventDefault();
            var newInjury = $(this).prev('input');
            addNewInjuryOption();
        });

        $(selectors.saveAndNextButtons).click(function (event) {
            var isReadOnly = $(selectors.isReadOnly);

            if (isReadOnly.length > 0) {
                return;
            }
            
            event.preventDefault();
            saveWithoutValidation($(this).attr("href"));
        });
    };

    //if severity is fatal then HIDE taken to hospital and able to carry out work section
    //if severity is NOT fatal then DISPLAY taken to hospital and able to carry out work section
    var setSeverityDisplay = function () {
        var isFatal = ($(selectors.Severity + ":checked").val() == "Fatal");

        if (isFatal) {
            if ($(selectors.InjuredPersonAbleToCarryOutWorkSection).length > 0) {
                $(selectors.InjuredPersonAbleToCarryOutWorkSection).addClass('hide');
            }
            $(selectors.TakenToHospitalSection).addClass('hide');
            $(selectors.timeOff).addClass('hide');
        } else {
            if ($(selectors.InjuredPersonAbleToCarryOutWorkSection).length > 0)
            {
                $(selectors.InjuredPersonAbleToCarryOutWorkSection).removeClass('hide');
            }
            $(selectors.TakenToHospitalSection).removeClass('hide');
            setTimeOffDisplay();
        }

        var isMajor = ($(selectors.Severity + ":checked").val() == "Major");
        if (isMajor) 
        {
            $(selectors.guidanceNotes).removeClass('hide');         
        }
        else 
        {
            $(selectors.guidanceNotes).addClass('hide');            
        }
    };

    var setInjuryDescriptionDisplay = function () {
        var ids = _injurylistSelector.getSelectedOptions();

        //if unknown is selected
        if (jQuery.inArray(unknownInjuryId, ids) > -1) {
            $(selectors.OtherInjuryDescription).removeClass('hide');
        } else {
            $(selectors.OtherInjuryDescription).addClass('hide');
        }

    };

    var setBodyPartDescriptionDisplay = function () {
        var ids = _bodyPartSelector.getSelectedOptions();

        //if unknown is selected
        if (jQuery.inArray(unknownBodyPartId, ids) > -1) {
            $(selectors.OtherBodyPartDescription).removeClass('hide');
        } else {
            $(selectors.OtherBodyPartDescription).addClass('hide');
        }

    };

    var setTimeOffDisplay = function () {
        var displayTimeOff = ($(selectors.InjuredPersonAbleToCarryOutWork + ":checked").val() != undefined
            && $(selectors.InjuredPersonAbleToCarryOutWork + ":checked").val() == "No");

        if (displayTimeOff) {
            $(selectors.timeOff).removeClass('hide');
        } else {
            $(selectors.timeOff).addClass('hide');
        }
    };

    var setupInjuryList = function () {

        var options = {
            id: "injury-multi-select",
            companyDefaultType: "Injury",
            selectedOptionsContainer: "#SelectedInjuryIds"
        };

        _injurylistSelector = new PBS.Plugins.MultiSelectAndSearch({}, options);
    };

    var setupBodyPartList = function () {

        var options = {
            id: "bodypart-multi-select",
            companyDefaultType: "BodyPart",
            selectedOptionsContainer: "#SelectedBodyPartIds"
        };

        _bodyPartSelector = new PBS.Plugins.MultiSelectAndSearch({}, options);

    };

    var submitForm = function () {
        _bodyPartSelector.addSelectedPropertyToSelectedOptions();
        _injurylistSelector.addSelectedPropertyToSelectedOptions();
        $(selectors.Form).submit();

    };

    var saveWithoutValidation = function (destUrl) {
        

        _bodyPartSelector.addSelectedPropertyToSelectedOptions();
        _injurylistSelector.addSelectedPropertyToSelectedOptions();

        var f = $('form:first');

        $.post('/AccidentReports/InjuryDetails/SaveAndNext', f.serialize(), function (result) {
            if (result.Success === true) {
                window.location = destUrl;
            }
            else {
                if ($(selectors.validationSummary).size() === 0) {
                    f.prepend('<div><div class="validation-summary-errors alert alert-error"></div></div>');
                }
                showValidationErrors(selectors.validationSummary, result.Errors);
            }
        }).error(function (error) {
            //window.location.replace(window.globalajaxurls.errorPage);
        });
    };


    return self;
};
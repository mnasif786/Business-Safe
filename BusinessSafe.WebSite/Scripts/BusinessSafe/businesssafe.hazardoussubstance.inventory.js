BusinessSafe.HazardousSubstance.Inventory = function (suppliers) {
    var self = this;

    var selectors = {
        theForm: 'form#AddEditHazardousSubstanceForm',
        saveButton: '#AddEditHazardousSubstanceSaveButton',
        cancelButton: '#AddEditHazardousSubstanceCancelButton',
        confirmDuplicatesButton: '#ConfirmDuplicatesButton',
        cancelDuplicatesButton: '#CancelDuplicatesButton',
        addNewSupplierLink: "#AddNewSupplierLink",
        dialogContainer: '#dialogContainer',
        hazardSymbolStandard: 'input[name="HazardousSubstanceStandard"]',
        hazardSymbolsRow: '#substance-hazard-symbols',
        Id: '#Id',
        companyId: '#companyId',
        name: '#Name',
        dateOfSds: '#SdsDate',
        riskPhraseMultiSelect: '#RiskPhraseIds',
        safetyPhraseMultiSelect: '#SafetyPhraseIds'
    };

    var urls = {
        checkForDuplicates: window.globalajaxurls.checkForDuplicateHazardousSubstances
    };

    var isReadOnly = function () {
        return $("#IsReadOnly").length > 0;
    };


    var readOnly = isReadOnly();

    if (readOnly === false) {
        $("#Supplier").combobox({
            selectedId: $("#SupplierId").val(),
            initialValues: suppliers,
            url: window.globalajaxurls.getSuppliers,
            data: {
                companyId: $(selectors.companyId).val(),
                pageLimit: 100
            }
        });
    }


    // move these to seperate ViewModel class
    var handleStandardChange = function (selectedStandard) {
        var standard = $(selectedStandard).parent().attr('data-title');

        if (standard !== undefined) {
            $(selectors.hazardSymbolsRow + " li").show();
            $(selectors.hazardSymbolsRow + " li").not('.' + standard).hide();
            $(selectors.hazardSymbolsRow).slideDown();
            $(".available-options").children('li').each(function () {
                if ($(this).hasClass(standard)) {
                    $(this).show();
                } else {
                    $(this).hide();
                }
            });
        }

    };
    // hazard symbol standard toggle
    if ($(selectors.hazardSymbolStandard + ':checked').val() !== undefined) {
        $(selectors.hazardSymbolsRow).removeClass('hide');
        handleStandardChange($(selectors.hazardSymbolStandard + ':checked'));
    } else {
        handleStandardChange($(selectors.hazardSymbolStandard + ':checked'));
    }

    $(selectors.hazardSymbolStandard).change(function () {
        handleStandardChange(this);

        $(selectors.riskPhraseMultiSelect + " option").remove();
        $(selectors.safetyPhraseMultiSelect + " option").remove();
        $('.additional-information').remove();
    });

    var saveToInventory = function () {
        if ($(selectors.theForm).valid()) {
            $(selectors.riskPhraseMultiSelect).children().each(function () {
                $(this).attr('selected', 'selected');
            });

            $(selectors.safetyPhraseMultiSelect).children().each(function () {
                $(this).attr('selected', 'selected');
            });

            $('form').submit();

        }
    };

    var handleCheckForDuplicates = function (data) {
        if (data === true) {
            saveToInventory();
        } else {
            $(selectors.dialogContainer).empty().dialog({
                autoOpen: false,
                width: 820,
                modal: true,
                resizable: false,
                draggable: false
            }).append(data).dialog('open');
        }
    };

    $(selectors.saveButton).click(function (event) {
        event.preventDefault();
        var id = $(selectors.Id).val();
        var newSubstanceName = $(selectors.name).val();

        if (newSubstanceName !== undefined && newSubstanceName.length > 0 && id === "0") {
            var data = {
                Id: $(selectors.Id).val(),
                CompanyID: $(selectors.companyId).val(),
                NewSubstanceName: newSubstanceName
            };
            AjaxCall.execute(urls.checkForDuplicates, handleCheckForDuplicates, data);
        } else {
            saveToInventory();
        }
    });
    
    $(selectors.cancelDuplicatesButton).live('click', function () {
        $(selectors.dialogContainer).dialog('close');
    });

    $(selectors.confirmDuplicatesButton).live('click', function () {
        $(selectors.dialogContainer).dialog('close');
        saveToInventory();
    });

    $(selectors.addNewSupplierLink).click(function (event) {
        event.preventDefault();
        var companyId = $(selectors.companyId).val();
        BusinessSafe.HazardousSubstanceRiskAssessment.Suppliers.ViewModel(companyId, suppliers);
    });

    var riskAndSafetyPhraseOptions = {
        //id: "phrase-multi-select",
        searchFilter: function (input) {
            var searchTerm = $(input).val();
            var standard = $(selectors.hazardSymbolStandard + ":checked").parent().attr('data-title');
            $(input).parent().siblings('div.row').filter(":first").find('ul.available-options').children('li').show().each(function () {
                if ($(this).html().substr(0, searchTerm.length).toLowerCase() != searchTerm.toLowerCase() || !$(this).hasClass(standard)) {
                    $(this).hide();
                }
            });
            $('selectors.anyMultiSelect ul.selected-options').children('li').removeClass('selected');
        }
    };

    var eventBindingObject = {};
    PBS.Plugins.MultiSelectAndSearch(eventBindingObject, riskAndSafetyPhraseOptions);
    BusinessSafe.HazardousSubstance.Inventory.SafetyPhrasesAdditionalInformation(eventBindingObject);

    return self;
};


BusinessSafe.HazardousSubstance.Inventory.SafetyPhrasesAdditionalInformation = function (eventBindingObject) {
    
    var getPhraseCode = function(phrase){
        var newPhrase = $(phrase);
        var myString = newPhrase.text();
        var myRegexp = /\S*/g;
        var match = myRegexp.exec(myString);                
        return match[0];
    };

    $(eventBindingObject).bind('removed-item', function(event, removedItem) {
        var code =getPhraseCode(removedItem);
        $('#' + code).remove();

        if ($('.additional-information').length === 0){
            var safetyPhrasesAdditionalInformationPlaceHolder = $("#SafetyPhrasesAdditionalInformationPlaceHolder");
            safetyPhrasesAdditionalInformationPlaceHolder.prev('label').addClass('hide');
        }
    });


    $(eventBindingObject).bind('added-item', function(event, addedItem) {

        var parentId = $(addedItem).closest('.search-and-multi-select').attr("id");
        if(parentId === "safety-phrase-multi-select"){
            
            var phraseAdded = $(addedItem);

            var requiresAdditionalInformation = phraseAdded.attr('data-requires-additional-information');
            if(requiresAdditionalInformation === "True"){
                
                var nextIndex = $('.additional-information').length;

                var phraseId = phraseAdded.attr('data-value');
                var code = getPhraseCode(phraseAdded);

                var safetyPhrasesAdditionalInformationPlaceHolder = $("#SafetyPhrasesAdditionalInformationPlaceHolder");

                safetyPhrasesAdditionalInformationPlaceHolder.prev('label').removeClass('hide');

                var divRow = $('<div>', { 'class': 'row additional-information', id: code });
                
                
                var divLabel = $('<div>', { 'class' : "span1 offset2"});
                var codeLabel = $('<label>' + code + '</label>');
                divLabel.appendTo(divRow);
                codeLabel.appendTo(divLabel);

                var divTextField = $('<div>' , { 'class' : 'span3'});
                var textField = $('<input>', { type : 'text', 'class' : 'input-xxlarge', name : 'AdditionalInformation[' + nextIndex + '].AdditionalInformation', maxLength : '200' });
                var hiddenIdField = $('<input>', { type : 'hidden', name : 'AdditionalInformation[' + nextIndex + '].SafetyPhaseId', value : phraseId });
                var hiddenReferenceNumber = $('<input>', { type : 'hidden', name : 'AdditionalInformation[' + nextIndex + '].ReferenceNumber', value : code });
                divTextField.appendTo(divRow);
                textField.appendTo(divTextField);
                hiddenIdField.appendTo(divTextField);
                hiddenReferenceNumber.appendTo(divTextField);

                divRow.appendTo("#SafetyPhrasesAdditionalInformationPlaceHolder");
            }
        }
    });

};

 



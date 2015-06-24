BusinessSafe.HazardousSubstanceRiskAssessment.Suppliers = function () {
    
    var _loadNewSupplier = function(companyId, successfullCallBack){
        $.ajax({
            url: window.globalajaxurls.loadNewSupplier,
            type: "GET",
            data: {
                companyId: companyId                
            },
            success: function (data) {
                successfullCallBack(data);
            },
            error: function () { 
                window.location.replace(window.globalajaxurls.errorPage);
            }
        });
    };

     var _saveSupplier = function (data, successfullCallBack){

         $.ajax({
             url: window.globalajaxurls.createCompanyDefaults,
             type: "POST",
             dataType: "json",
             data: data,
             success: function (result) {

                successfullCallBack(result, data);
               
             },
             error:function(xhr, status, error) {
                  var err = eval("(" + xhr.responseText + ")");
                  alert(err.Message);
             }
         });
    };   

    return {
        loadNewSupplier : _loadNewSupplier,
        saveSupplier : _saveSupplier
    };
}();

BusinessSafe.HazardousSubstanceRiskAssessment.Suppliers.ViewModel = function (companyId, suppliers) {

    var self = this;

    var selectors = {
        dialog: "#dialogAddNewSupplierContainer",
        cancelButton: "#CancelButton",
        saveButton: "#SaveNewSupplierButton",
        supplierName: "#SupplierName",
        validationDisplay: "#validationDisplay",
        matchingNamesDisplay: ".matchingNamesDisplay",
        matchingNamesMessage: "div.matchingNameMessage"
    };

    var resetForm = function () {
        $(selectors.supplierName).val("").removeClass("input-validation-error");
        resetValidatioDisplay();
    };

    var resetValidatioDisplay = function () {
        var validationDisplay = $(selectors.validationDisplay);
        validationDisplay.empty();
        $("<ul></ul>").appendTo(validationDisplay);
    };

    var closeForm = function () {
        $(selectors.dialog).dialog("close");
    };

    var showValidationMessage = function (message) {
        var validationMessage = $(selectors.validationDisplay);
        validationMessage.val(message);
        validationMessage.removeClass("hide");

        var errorList = $("ul", validationMessage);
        errorList.append('<li>' + message + '</li>');

    };

    function showMatchingNames(matchingNames) {
        var saveButton = $(selectors.saveButton);
        saveButton.removeClass('btn-primary');
        saveButton.addClass('btn-danger');
        saveButton.text("Create New Supplier");
        saveButton.val('nomatch');

        var form = $(selectors.dialog);
        var matchingNamesDisplay = $(selectors.matchingNamesDisplay, form);
        matchingNamesDisplay.removeClass('hide');

        var ul = $("ul", matchingNamesDisplay.children());

        for (var i = matchingNames.length - 1; i >= 0; i--) {
            var matchingName = matchingNames[i];
            $("<li/>").appendTo(ul).html(matchingName);
        }

        $(selectors.matchingNamesMessage, form).empty().text('If you wish to still create Supplier select Create New Supplier.');
    }

    var saveSupplierCallBack = function (result, data) {
        if (result.Success === true) {

            // Add new supplier to the drop down and select
            
            $('#Supplier').val(data.companyDefaultValue);
            $('#SupplierId').val(result.Id);

            var newSupplier = {
                label : data.companyDefaultValue,
                value : String(result.Id)
            };

            suppliers.push(newSupplier); 

            $("#Supplier").combobox({
                selectedId: $("#SupplierId").val(),
                initialValues: suppliers,
                url: ''
            });

            closeForm();

        }
        else if (result.Success === false) {

            if (result.Matches !== undefined) {
                showMatchingNames(result.Matches);
            }
            else {
                showValidationMessage(result.Message);
            }

        }
    };

    var validate = function (data) {
        if (data.companyDefaultValue.length === 0) {
            showValidationMessage("Supplier name is required.");
            $(selectors.supplierName).addClass("input-validation-error");
            return false;
        }
        return true;
    };

    var getRunMatchCheckOnCreate = function () {
        return $(selectors.saveButton).val() === '';
    };

    var loadedNewSupplierScreenSuccessfullyCallBack = function (html){

        resetForm();

        $(selectors.dialog).dialog({
            autoOpen : false,
            width : 820,
            modal : true,
            resizable : false,
            draggable : false
        });

        $(selectors.dialog).html(html);
        $(selectors.dialog).dialog("open");

        $(selectors.cancelButton).click(function (event){
            event.preventDefault();
            closeForm();
        });

        $(selectors.saveButton).unbind('click');
        $(selectors.saveButton).click(function (event){

            event.preventDefault();

            var data = {
                companyDefaultValue : $(selectors.supplierName).val(),
                companyDefaultType : "SpecialistSuppliers",
                runMatchCheck : getRunMatchCheckOnCreate(),
                companyId : companyId
            };

            resetValidatioDisplay();

            if (validate(data) === false) {
                return;
            }

            BusinessSafe.HazardousSubstanceRiskAssessment.Suppliers.saveSupplier(data, saveSupplierCallBack);
            return;
        });
    };

    BusinessSafe.HazardousSubstanceRiskAssessment.Suppliers.loadNewSupplier(companyId, loadedNewSupplierScreenSuccessfullyCallBack);

    return self;
};


    

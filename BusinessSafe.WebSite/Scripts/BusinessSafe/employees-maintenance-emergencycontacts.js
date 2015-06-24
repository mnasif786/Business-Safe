var emergencyContactDetailsForm = function () {

    var selectors = {
        employeeId: "#EmployeeId",
        companyId: "#CompanyId",
        emergencyContactId: "#EmergencyContactId",
        title: "#Title",
        forename: "#Forename",
        surname: "#Surname",
        relationship: "#Relationship",
        addressDetails: '#address-details',
        employeeAddressDetails: '#employee-address-details',
        address1: "#addEmergencyContactDetails #Address1",
        address2: "#addEmergencyContactDetails #Address2",
        address3: "#addEmergencyContactDetails #Address3",
        town: "#addEmergencyContactDetails #Town",
        county: "#addEmergencyContactDetails #County",
        postcode: "#addEmergencyContactDetails #Postcode",
        country: "#addEmergencyContactDetails #EmergencyContactCountryId",
        workTelephone: "#addEmergencyContactDetails #WorkTelephone",
        homeTelephone: "#addEmergencyContactDetails #HomeTelephone",
        mobileTelephone: "#addEmergencyContactDetails #MobileTelephone",
        sameAddressAsEmployee: "input[type=checkbox]#SameAddressAsEmployee",
        saveButton: "#EmergencyContactDetailsSaveButton",
        cancelButton: "#EmergencyContactDetailsCancelButton",
        closeButton: "#EmergencyContactDetailsCloseButton",
        errorContainer: "#errorContainer",
        emergencyContactDetailsDialog: "#emergencyContactDetailsDialog",
        preferredCheckedContactNumber: ".preferredContactNumber:checked"
    };

    //    var getSameAsEmployeeAddress = function () {
    //        return $(selectors.sameEmployeeAddress + ":checked").length == 1 ? true : false;
    //    };

    var getPreferredContactNumberCount = function () {
        return $(selectors.preferredCheckedContactNumber).length;
    };

    var getPreferredContactNumberIndex = function () {
        return $(selectors.preferredCheckedContactNumber).val();
    };

    var getData = function () {

        // SGG: this SHOULD work in the same way as the replacement line but, for some reason, returns
        // a boolean value in some cases?
        //var preferredContactNumber = $("input[@name=PreferredContactNumber]:checked").val();
               
        var form = $("#addEmergencyContactDetails");
        var data = {
            emergencyContactId: $(selectors.emergencyContactId).val(),
            employeeId: $(selectors.employeeId).val(),
            companyId: $(selectors.companyId).val(),
            title: $(selectors.title, form).val(),
            foreName: $(selectors.forename, form).val(),
            surname: $(selectors.surname, form).val(),
            relationship: $(selectors.relationship, form).val(),
            sameAddressAsEmployee: $(selectors.sameAddressAsEmployee).is(':checked'),
            address1: $(selectors.address1).val(),
            address2: $(selectors.address2).val(),
            address3: $(selectors.address3).val(),
            town: $(selectors.town).val(),
            county: $(selectors.county).val(),
            postcode: $(selectors.postcode).val(),
            workTelephone: $(selectors.workTelephone).val(),
            homeTelephone: $(selectors.homeTelephone).val(),
            mobileTelephone: $(selectors.mobileTelephone).val(),
            preferredContactNumber: $(selectors.preferredCheckedContactNumber).val(), //preferredContactNumber,
            emergencyContactCountryId: $(selectors.country).val()
        };
        return data;
    };

    var saveEmergencyContact = function () {

        if (!validate()) {
            return;
        }

        var data = getData();

        var urls = {
            addEmergencyContactDetails: window.globalajaxurls.addEmergencyContactDetails,
            updateEmergencyContactDetails: window.globalajaxurls.updateEmergencyContactDetails
        };

        var url = data.emergencyContactId === "0" ? urls.addEmergencyContactDetails : urls.updateEmergencyContactDetails;

        var successfullyCallBack = function () {
            $(selectors.emergencyContactDetailsDialog).dialog("close");
            document.location.reload(true);
        };

        AjaxCall.execute(url, successfullyCallBack, data, "POST");

    };

    function validate() {
        $(selectors.errorContainer).empty();

        var errorList = $("<ul></ul>");
        var validationresult = true;

        var form = $("#addEmergencyContactDetails");

        $(".input-validation-error").removeClass("input-validation-error");

        var forename = $(selectors.forename, form);
        if (forename.val() === '') {
            errorList.append("<li style='color:red;'>Forename is required</li>");
            forename.addClass("input-validation-error");
            validationresult = false;
        }

        var surname = $(selectors.surname, form);
        if (surname.val() === '') {
            errorList.append("<li style='color:red;'>Surname is required</li>");
            surname.addClass("input-validation-error");
            validationresult = false;
        }

        var relationship = $(selectors.relationship, form);
        if (relationship.val() === '') {
            errorList.append("<li style='color:red;'>Relationship is required</li>");
            relationship.addClass("input-validation-error");
            validationresult = false;
        }
        if (getPreferredContactNumberCount() === 0) {
            errorList.append("<li style='color:red;'>Preferred contact number is required</li>");
            $(selectors.workTelephone).addClass("input-validation-error");
            $(selectors.homeTelephone).addClass("input-validation-error");
            $(selectors.mobileTelephone).addClass("input-validation-error");
            validationresult = false;
        }

        var preferredContactNumberIndex = getPreferredContactNumberIndex();
        if (preferredContactNumberIndex === "1") {
            if ($(selectors.workTelephone).val() === '') {
                errorList.append("<li style='color:red;'>Work Telephone is required</li>");
                $(selectors.workTelephone).addClass("input-validation-error");
                validationresult = false;
            }
        }
        if (preferredContactNumberIndex === "2") {
            if ($(selectors.homeTelephone).val() === '') {
                errorList.append("<li style='color:red;'>Home Telephone is required</li>");
                $(selectors.homeTelephone).addClass("input-validation-error");
                validationresult = false;
            }
        }
        if (preferredContactNumberIndex === "3") {
            if ($(selectors.mobileTelephone).val() === '') {
                errorList.append("<li style='color:red;'>Mobile is required</li>");
                $(selectors.mobileTelephone).addClass("input-validation-error");
                validationresult = false;
            }
        }

        if (validationresult === false) {
            $(selectors.errorContainer).append(errorList);
            $(selectors.errorContainer).show();
        }

        return validationresult;
    }

    function showHideAddressDiv() {
        if ($(selectors.sameAddressAsEmployee).is(':checked')) {
            $(selectors.employeeAddressDetails).show();
            $(selectors.addressDetails).hide();
        } else {
            $(selectors.employeeAddressDetails).hide();
            $(selectors.addressDetails).show();
        }
    }

    function initialise(data, titles, countries) {

        var contactDialog = $(selectors.emergencyContactDetailsDialog);
        contactDialog.empty();
        contactDialog.html(data);
        contactDialog.dialog({ width: 940 }).dialog('open');

        $("#Title").combobox({
            selectedId: $("#TitleId").val(),
            initialValues: titles,
            url: ''
        });

        $("#EmergencyContactCountry").combobox({
            selectedId: $("#EmergencyContactCountryId").val(),
            initialValues: countries,
            url: ''
        });

        $("#EmployeeCountry").combobox({
            selectedId: $("#EmployeeCountryId").val(),
            initialValues: countries,
            url: '',
            disabled: true
        });

        $('#EmployeeCountry').attr('readonly', true);
        $('#EmployeeCountryId').attr('readonly', true);

        // Hook up the save command
        $(selectors.saveButton).click(function (event) {
            event.preventDefault();
            saveEmergencyContact();
        });

        // Hook up the cancel command
        $(selectors.cancelButton).click(function (event) {
            event.preventDefault();
            $(selectors.emergencyContactDetailsDialog).dialog("close");
        });

        // Hook up the close button for the view only command
        $(selectors.closeButton).click(function (event) {
            event.preventDefault();
            $(selectors.emergencyContactDetailsDialog).dialog("close");
        });

        // Hook up the click same as employee address
        $(selectors.sameAddressAsEmployee).change(function (event) {
            showHideAddressDiv();
        });

        showHideAddressDiv();
    }

    return {
        initialise: initialise
    };

} ();

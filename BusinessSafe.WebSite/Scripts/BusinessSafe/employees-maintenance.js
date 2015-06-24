var employee = {};
var employeeAddEditForm = function () {

    var selectors = {
        employeeId: "#EmployeeId",
        companyId: "#CompanyId",
        emergencyContactId: "#EmergencyContactId",
        title: "#NameTitle",
        titleId: "#NameTitleId",
        dateOfBirth: "#DateOfBirth",
        expirationWorkingVisaDate: "#WorkVisaExpirationDate",
        expirationDrivingLicenseDate: "#DrivingLicenseExpirationDate",
        sexId: "#SexId",
        sex: "#Sex",
        nationalityId: "#NationalityId",
        nationality: "#Nationality",
        siteId: "#SiteId",
        site: "#Site",
        organisationUnit: "#OrganisationalUnit",
        countryId: "#CountryId",
        country: "#Country",
        employmentStatus: "#EmploymentStatusId",
        hasDisability: "#HasDisability",
        disabilityDescription: "#DisabilityDescription",
        hasCompanyVehicle: "#HasCompanyVehicle",
        companyVehicleRegistration: "#CompanyVehicleRegistration",
        companyVehicleType: "#CompanyVehicleTypeId",
        emergencyContactDetailsGrid: "#emergencyContactDetailsGrid",
        addEmergencyContactButton: "#addAnotherEmergencyContact",
        emergencyContactDetailsDialog: "#emergencyContactDetailsDialog",
        emergencyContactDetailsRemoveButton: ".emergencyContactDetailsRemoveButton",
        emergencyContactDetailsEditButton: ".emergencyContactDetailsEditButton",
        emergencyContactDetailsViewButton: ".emergencyContactDetailsViewButton",
        emergencyContactDetailsDeleteConfirmationDialog: "#emergencyContactDetailsDeleteConfirmationDialog",
        saveEmployeeButton: "#SaveEmployeeButton",
        organisationalDetailsLink: "#organisational-details-link",
        additionalDetailsLink: "#additional-details-link",
        createUserLink: "#create-user-link",
        riskAssessorLink: "#risk-assessor-details-link",    
        emergencyContactDetails: "#emergency-contact-details-fieldset",
        cancelButton: "#CancelEmployeeButton",
        email: "#Email",
        resendUserRegistrationButton: "#resendUserRegistrationButton",
        resendUserRegistrationNotice: "#resendUserRegistrationNotice",
        resendUserRegistrationDuplicateEmailNotice: '#resendUserRegistrationDuplicateEmailNotice',
        telephone: '#Telephone',
        mobile: '#Mobile',
        userSite: "#UserSite",
        userSiteId: "#UserSiteId",
        userSiteGroup: "#UserSiteGroup",
        userSiteGroupId: "#UserSiteGroupId",
        userRole: "#UserRole",
        userRoleId: "#UserRoleId",
	    userPermissionsApplyToAllSites: "#UserPermissionsApplyToAllSites",
        isRiskAssessor: "#IsRiskAssessor",
        riskAssessorSite: "#RiskAssessorSite",
        riskAssessorSiteId: "#RiskAssessorSiteId",
        riskAssessorAllSites: "#RiskAssessorHasAccessToAllSites",
        riskAssessorDetailsInputSection: "#risk-assessor-details-input-section",
        dailyRadioButton: "#daily",
        weeklyRadioButton: "#weekly",
        monthlyRadioButton: '#monthly',
        weekDays: '.weekday',
        monthlyDate: '#monthlyDate'
    };

    function setDisabilityEnabled(disabilityCheckbox) {
        var disabilityDescription = $(selectors.disabilityDescription);
        var checked = $(disabilityCheckbox).attr('checked');

        if (checked === "checked") {
            disabilityDescription.removeAttr("disabled");
        }
        else {
            disabilityDescription.attr("disabled", "disabled");
        }
    }

    function setEmailEnabled(canChangeEmail) {
        var email = $(selectors.email);
        if (canChangeEmail === 'True') {
            email.removeAttr("disabled");
        }
        else {
            email.attr("disabled", "disabled");
        }
    }

    function setCompanyVehicleDisability(companyVehicleCheckbox) {
        var companyVehicleRegistration = $(selectors.companyVehicleRegistration);
        var companyVehicleType = $(selectors.companyVehicleType);

        var checked = $(companyVehicleCheckbox).attr('checked');

        if (checked === "checked") {
            companyVehicleRegistration.removeAttr("disabled");
        }
        else {
            companyVehicleRegistration.attr("disabled", "disabled");
        }
    }

    function loadEmergencyContactCommand(url, emergencyContactId, employeeId, companyId, successfulCallBack) {
        var data = {
            employeeId: employeeId,
            emergencyContactId: emergencyContactId,
            companyId: companyId
        };

        AjaxCall.execute(url, successfulCallBack, data);
    }

    function updateUserRegistration(userId, secretAnswer, email, successfulCallBack) {

        var data = {
            userId: userId,
            securityAnswer: secretAnswer,
            email: email
        };

        var url = window.globalajaxurls.UpdateUserRegistration;

        AjaxCall.execute(url, successfulCallBack, data, "POST");
    }

    function updateEmployeeOnlineRegistrationDetails(employeeId, companyId, email, telephone, mobile, userRoles, userSite, userSiteGroup, allSitesPermission, successfulCallBack) {

        url = window.globalajaxurls.updateEmployeeDetails;

        var employeeDate = {
            EmployeeId: employeeId,
            CompanyId: companyId,
            Email: email,
            Telephone: telephone,
            Mobile: mobile,
            UserRoles: userRole,
            UserSite: userSite,
            UserSiteGroup: userSiteGroup,
            AllSitesPermission: allSitesPermission
        };

        AjaxCall.execute(url, successfulCallBack, employeeDate, "POST");
    }

    function populateRiskAssessorSiteDropDownList (siteListData) {
        siteListData.splice(0, 0, { label: '--Select Site--', value: '0' });

        $(selectors.riskAssessorSite).combobox({
            selectedId: $(selectors.riskAssessorSiteId).val(),
            initialValues: siteListData,
            url: window.globalajaxurls.getSites,
            data: {
                companyId: $(selectors.companyId).val(),
                pageLimit: 100
            },

            afterSelect: function (event, ui) {
            }
        });
    };

    function setupRiskAssessorSiteDropdownVisibility (){
        if ($(selectors.riskAssessorAllSites).is(':checked')) {
            $(selectors.riskAssessorSite).val('--Select Site--');
            $(selectors.riskAssessorSiteId).val('');
            $(selectors.riskAssessorSite).parent().hide();
        } else {
            $(selectors.riskAssessorSite).parent().show();
        }
    };

    function getSiteAndSiteGroupCallback (siteGroupData) {

        var items = [];

        //convert to autocomplete model
        $(siteGroupData).each(function () {
            var item = { "label": $(this)[0].Name, "value": $(this)[0].Id, "filterName": null, "filterValue": null };
            item.label += ($(this)[0].IsSiteGroup) ? " (Site group)" : "";
            items.push(item);
        });

        populateRiskAssessorSiteDropDownList(items);
    };

    function showHideRiskAssessorInputControls() {
        var checked = $(selectors.isRiskAssessor).attr('checked');
        
        if (checked === "checked") {
            $(selectors.riskAssessorDetailsInputSection).removeClass("hide");
        } else {
            $(selectors.riskAssessorDetailsInputSection).addClass("hide");
        }
    }

    function updateAllSitesUserPermissionsCheckbox(allSitesPermissionsCheckbox) {
        var allSitesPermissions = $(selectors.userPermissionsApplyToAllSites);

        var checked = $(allSitesPermissionsCheckbox).attr('checked');

        if (checked === "checked") {
            allSitesPermissions.removeAttr("disabled");
        }
        else {
            allSitesPermissions.attr("disabled", "disabled");
        }
    }

    function manageNotificationFrequencyControlEvents() {
        if (!$(selectors.weeklyRadioButton).is(':checked')) {
            $(selectors.weekDays).attr('disabled', true);
            $(selectors.weekDays).first().attr('checked', true);
        }

        if (!$(selectors.monthlyRadioButton).is(':checked')) {
            $(selectors.monthlyDate).attr('disabled', true);
            $(selectors.monthlyDate).attr('disabled', true);
            $(selectors.monthlyDate + ' option:first-child').attr("selected", "selected");
        }


        $(selectors.weeklyRadioButton).click(function () {
            $(selectors.monthlyDate).attr('disabled', true);
            $(selectors.weekDays).attr('disabled', false);
        });

        $(selectors.monthlyRadioButton).click(function () {
            $(selectors.weekDays).attr('disabled', true);
            $(selectors.monthlyDate).attr('disabled', false);
        });

        $(selectors.dailyRadioButton).click(function () {
            $(selectors.weekDays).attr('disabled', true);
            $(selectors.monthlyDate).attr('disabled', true);
        });
    }


    function initialise(titles, nationalities, sexes, sites, siteGroups, userRoles, countries, employmentStatuses, companyVehicleTypes, isReadOnly, canChangeEmail) {
        if (isReadOnly === 'False') {
            if ($(selectors.employeeId).val() === "") {
                $(selectors.organisationalDetailsLink).hide();
                $(selectors.additionalDetailsLink).hide();
                $(selectors.emergencyContactDetails).hide();
                $(selectors.createUserLink).hide();
                $(selectors.riskAssessorLink).hide();
                $(selectors.saveEmployeeButton).hide();
            }

            // Always hide until release
            //  $(selectors.createUserLink).hide();
            manageNotificationFrequencyControlEvents();
            $(selectors.title).combobox({
                selectedId: $(selectors.titleId).val(),
                initialValues: titles,
                url: ''
            });

            $(selectors.nationality).combobox({
                selectedId: $(selectors.nationalityId).val(),
                initialValues: nationalities,
                url: ''
            });

            $(selectors.sex).combobox({
                selectedId: $(selectors.sexId).val(),
                initialValues: sexes,
                url: ''
            });

            $(selectors.site).combobox({
                selectedId: $(selectors.siteId).val(),
                initialValues: sites,
                url: window.globalajaxurls.getSites,
                data: {
                    companyId: $(selectors.companyId).val(),
                    pageLimit: 100
                }
            });

            $(selectors.userSite).combobox({
                selectedId: $(selectors.userSiteId).val(),
                initialValues: sites,
                url: ''
            });

            $(selectors.userSiteGroup).combobox({
                selectedId: $(selectors.userSiteGroupId).val(),
                initialValues: siteGroups,
                url: ''
            });

            $(selectors.userRole).combobox({
                selectedId: $(selectors.userRoleId).val(),
                initialValues: userRoles,
                url: ''
            });


            $(selectors.country).combobox({
                selectedId: $(selectors.countryId).val(),
                initialValues: countries,
                url: ''
            });

            $("#EmploymentStatus").combobox({
                selectedId: $("#EmploymentStatusId").val(),
                initialValues: employmentStatuses,
                url: ''
            });

            $("#CompanyVehicleType").combobox({
                selectedId: $("#CompanyVehicleTypeId").val(),
                initialValues: companyVehicleTypes,
                url: ''
            });
        } else {

            $(nationalities).each(function(idx, obj) {
                if (obj.value == $(selectors.nationalityId).val() && $(selectors.nationalityId).val() != "") {
                    $(selectors.nationality).val(obj.label);
                    return;
                }
            });
        }

        $(selectors.resendUserRegistrationButton).click(function(event) {
            event.preventDefault();

            var link = $(this);
            var params = link.data();

            var secretAnswer;

            if ($(selectors.telephone).val()) {
                secretAnswer = $(selectors.telephone).val();
            } else {
                secretAnswer = $(selectors.mobile).val();
            }

            if (!secretAnswer) {
                alert("Telephone or Mobile number must be added");
                return;
            }

            var email = $(selectors.email).val();
            var userId = params.id;

            var successfulUpdateCallBack = function(result) {
                if (result.Success === true) {
                    $(selectors.resendUserRegistrationNotice).removeClass("hide");
                } else {
                    alert(result.Errors);
                }
            };

            updateUserRegistration(userId, secretAnswer, email, successfulUpdateCallBack);

            var employeeId = $(selectors.employeeId).val();
            var companyId = $(selectors.companyId).val();
            var mobile = $(selectors.mobile).val();
            var telephone = $(selectors.telephone).val();
            var userSiteRoles = $(selectors.userSiteGroup).val();
            var userSite = $(selectors.userSite).val();
            var userSiteGroups = $(selectors.userSiteGroup).val();
            var allSitePermissions = $(selectors.userPermissionsApplyToAllSites).val();

            var successfulOnlineCallBack = function(result) {
                if (result.success === false) {
                    alert(result.errors);
                }
            };

            updateEmployeeOnlineRegistrationDetails(employeeId, companyId, email, telephone, mobile, userSiteRoles, userSite, userSiteGroups, allSitePermissions, successfulOnlineCallBack);
        });

        $(selectors.hasDisability).click(function() {
            setDisabilityEnabled(this);
        });
        setDisabilityEnabled($(selectors.hasDisability));

        $(selectors.hasCompanyVehicle).click(function() {
            setCompanyVehicleDisability(this);
        });
        setCompanyVehicleDisability($(selectors.hasCompanyVehicle));

        $(selectors.emergencyContactDetailsDialog).dialog({
            autoOpen: false,
            width: 1000,
            modal: true,
            resizable: false,
            draggable: false
        });

        setEmailEnabled(canChangeEmail);

        $(selectors.emergencyContactDetailsRemoveButton).click(function(event) {
            event.preventDefault();

            var emergencyContactId = $(this).attr("data-id");
            var employeeId = $(selectors.employeeId).val();
            var companyId = $(selectors.companyId).val();

            var dialog = $(selectors.emergencyContactDetailsDeleteConfirmationDialog).dialog({
                autoOpen: false,
                resizable: false,
                height: 190,
                width: 320,
                modal: true,
                draggable: true,
                buttons: {
                    "Remove": function() {
                        var successfulCallBack = function() {
                            $(selectors.emergencyContactDetailsDeleteConfirmationDialog).dialog("close");
                            document.location.reload(true);
                        };

                        var data = {
                            employeeId: employeeId,
                            emergencyContactId: emergencyContactId,
                            companyId: companyId
                        };

                        var url = window.globalajaxurls.markEmployeeEmergencyContactAsDeleted;

                        AjaxCall.execute(url, successfulCallBack, data, "POST");
                    },
                    Cancel: function() {
                        $(this).dialog("close");
                    }
                }
            });
            dialog.dialog("open");
        });

        var successFullyLoadedEmergencyContactDetails = function(data) {
            emergencyContactDetailsForm.initialise(data, titles, countries);
        };

        $(selectors.addEmergencyContactButton).click(function(event) {
            event.preventDefault();
            var emergencyContactId = 0;
            var employeeId = $(selectors.employeeId).val();
            var companyId = $(selectors.companyId).val();
            var url = window.globalajaxurls.getEmployeeEmergencyContactDetailsUrl;

            loadEmergencyContactCommand(url, emergencyContactId, employeeId, companyId, successFullyLoadedEmergencyContactDetails);
        });

        $(selectors.emergencyContactDetailsEditButton).click(function(event) {
            event.preventDefault();

            var emergencyContactId = $(this).attr("data-id");
            var employeeId = $(selectors.employeeId).val();
            var companyId = $(selectors.companyId).val();
            var url = window.globalajaxurls.getEmployeeEmergencyContactDetailsUrl;

            loadEmergencyContactCommand(url, emergencyContactId, employeeId, companyId, successFullyLoadedEmergencyContactDetails);
        });

        $(selectors.emergencyContactDetailsViewButton).click(function(event) {
            event.preventDefault();

            var emergencyContactId = $(this).attr("data-id");
            var employeeId = $(selectors.employeeId).val();
            var companyId = $(selectors.companyId).val();
            var url = window.globalajaxurls.viewEmployeeEmergencyContactDetailsUrl;

            loadEmergencyContactCommand(url, emergencyContactId, employeeId, companyId, successFullyLoadedEmergencyContactDetails);
        });

        $(selectors.riskAssessorAllSites).click(function(event) {
            setupRiskAssessorSiteDropdownVisibility();
        });

        $(selectors.isRiskAssessor).on('click', function() {
            showHideRiskAssessorInputControls();
        });

        var getSiteAndSiteGroupsData = {
            companyId: $(selectors.companyId).val(),
            pageLimit: 100
        };

        AjaxCall.execute(window.globalajaxurls.getSiteAndSiteGroups, getSiteAndSiteGroupCallback, getSiteAndSiteGroupsData);

        setupRiskAssessorSiteDropdownVisibility();
        showHideRiskAssessorInputControls();
        $(selectors.userPermissionsApplyToAllSites).click(function() {
            updateAllSitesUserPermissionsCheckbox(this);
        });
    }

    return {
        initialise: initialise
    };

} ();
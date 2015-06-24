var userPermissionsManager = function () {

    function initialize(employees, roles, siteGroups, sites) {

        var selectors = {
            employeesDropdown: "#EmployeeId",
            companyId: "#CompanyId",
            userPermissionsController: "#UserPermissionsSelection",
            rolesDropdown: "#RoleId",
            sitesDropDown: "#SiteId",
            siteGroupsDropDown: "#SiteGroupId",
            saveCancelButtons: "#SaveCancelButtons",
            rolesDetails: "#RolesDetails",
            saveSuccessNotification: "#SaveSuccessNotification",
            saveButton: "#SaveButton",
            isUserRegistered: "#IsUserRegistered",
            isUserDeleted: "#IsUserDeleted",
            dialog: "#ReinstateUserDialog"
        };

        var getCompanyId = function () {
            return $(selectors.companyId).val();
        };

        $(selectors.saveButton).click(function (e) {
            var that = this;
            $(this).addClass('disabled');

            var isUserDeleted = $(selectors.isUserDeleted).val();
            var isUserRegistered = $(selectors.isUserRegistered).val();

            if (isUserDeleted === 'True' && isUserRegistered === "False") {
                e.preventDefault();

                $(selectors.dialog).dialog({
                    title: "Warning!",
                    buttons: {
                        "Confirm": function () {
                            $(this).dialog("close");
                            $(that).removeClass('disabled');
                            $('form').submit();
                        },
                        "Cancel": function () {
                            $(this).dialog("close");
                            $(that).removeClass('disabled');
                        }
                    },
                    resizable: false
                });
                $(selectors.dialog).dialog('open');

            }
        });

        $("#Employee").combobox({
            selectedId: $("#EmployeeId").val(),
            initialValues: employees,
            url: window.globalajaxurls.getEmployees,
            data: {
                companyId: $("#CompanyId").val(),
                pageLimit: 500
            },
            afterSelect: function (event, ui) {

                var employeeId = $("#EmployeeId").val();

                $.each(employees, function (index, value) {
                    if (value.value === employeeId) {
                        value.selected = true;
                    }
                    else {
                        value.selected = false;
                    }
                });

                $.ajax({
                    type: "GET",
                    url: window.globalajaxurls.getUserPermissionsUser + "?companyId=" + getCompanyId() + "&employeeId=" + employeeId,
                    error: function () {
                        window.location.replace(window.globalajaxurls.errorPage);
                    },
                    cache: false
                }).done(function (data) {

                    $(selectors.userPermissionsController).html(data);

                    if (employeeId != -"") {
                        $(selectors.saveCancelButtons).show();
                    }
                    else {
                        $(selectors.saveCancelButtons).hide();
                    }

                    $(selectors.saveSuccessNotification).hide();
                    userPermissionsManager.initialize(employees, roles, siteGroups, sites);
                });

                return false;
            }
        });


        $("#Role").combobox({
            selectedId: $("#RoleId").val(),
            initialValues: roles,
            url: ''
        });

        $("#SiteGroup").combobox({
            selectedId: $("#SiteGroupId").val(),
            initialValues: siteGroups,
            url: ''
        });

        $("#Site").combobox({
            selectedId: $("#SiteId").val(),
            initialValues: sites,
            url: window.globalajaxurls.getSites,
            data: {
                companyId: $("#CompanyId").val(),
                pageLimit: 100
            }
        });


        var doAnAlert = function () {
            alert('thi si an alert');
        };

        $(selectors.rolesDropdown).change(function (e) {
            e.preventDefault();
            loadUserRole();
            $(selectors.saveSuccessNotification).hide();
            return false;
        });

        $('a.icon-question-sign').on('click', function (event) {
            event.preventDefault();
            $(this).
                popover({
                    container: '#addUserContainer',
                    placement: 'right'
                });

            $(this).popover("show");
        })

        var loadUserRole = function () {
            var roleId = $(selectors.rolesDropdown).val();

            if (roleId !== "") {
                $.ajax({
                    type: "GET",
                    url: window.globalajaxurls.getUserRolePermissions + "?roleId=" + roleId + "&companyId=" + getCompanyId() + "&enableCustomRoleEditing=false",
                    error: function () {
                        window.location.replace(window.globalajaxurls.errorPage);
                    },
                    cache: false
                }).done(function (data) {
                    $(selectors.rolesDetails).html(data);
                });
            } else {
                $(selectors.rolesDetails).html("");
            }
        };

        $(selectors.sitesDropDown).change(function (e) {
            e.preventDefault();
            $(selectors.saveSuccessNotification).hide();
            return false;
        });

        $(selectors.siteGroupsDropDown).change(function (e) {
            e.preventDefault();
            $(selectors.saveSuccessNotification).hide();
            return false;
        });

        loadUserRole();
    }

    return {
        initialize: initialize
    };
} ();

// $(function () {
//     userPermissionsManager.initialize();
// });

function fireEmployeesDropDownChange() {
    alert("firedForTesting");
    $('#EmployeeId').change();
}

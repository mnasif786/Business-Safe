function DeleteUserRole(userRoleId, companyId, successCallBack, failedCallBack) {
    this.userRoleId = userRoleId;
    this.companyId = companyId;

    this.execute = function () {
        $.ajax({
            url: window.globalajaxurls.markUserRoleAsDeleted,
            type: "POST",
            dataType: "json",
            data: {
                userRoleId: this.userRoleId,
                companyId: this.companyId
            },
            success: function (data) {
                successCallBack();
            },
            error: function () {
                window.location.replace(window.globalajaxurls.errorPage);
            }
        });
    };
}

function GetUsersWithRole(companyId, userRoleId, successCallBack) {
    this.execute = function () {
        $.ajax({
            async: false,
            type: "GET",
            url: window.globalajaxurls.getUsersWithRole + "?companyId=" + companyId + "&roleId=" + userRoleId,
            error: function () {
                window.location.replace(window.globalajaxurls.errorPage);
            },
            cache: false
        }).done(function (data) {
            if (data.Users.length !== 0) {
                var usersWithRole = "<ul>";

                for (i = 0; i < data.Users.length; i++) {
                    usersWithRole = "<li>" + data.Users[i] + "</li>";
                }

                usersWithRole = usersWithRole + "</ul>";

                successCallBack(usersWithRole);
            }
        });
    };
}

var userRolesManager = function () {
    function initialize(roles) {
        var selectors = {
            roleId: "#RoleId",
            roleName: "#RoleName",
            userRolesDropdown: "#UserRoles",
            companyId: "#CompanyId",
            roleDetails: "#RolesDetails",
            editUserRoleButton: "#EditUserRoleButton",
            saveUserRoleButton: "#SaveUserRoleButton",
            deleteUserRoleButton: "#DeleteUserRoleButton",
            permissionCheckBoxes: ".permissioncheckbox:checked",
            successMessage: ".successful-saved-user-role",
            isNewUserRole: "#IsNewUserRole",
            validationDisplay: ".validation-summary-errors",
            dialogDeleteUserRole: "#dialogDeleteUserRole",
            dialogCantDeleteUserRoleRoleInUse: "#dialogCantDeleteUserRoleRoleInUse",
            dialogUsersAffectedByRoleEdit: "#dialogUsersAffectedByRoleEdit",
            userWithRolesResult: ".UsersWithRoles"
        };

        var getCompanyId = function () {
            return $(selectors.companyId).val();
        };

        var getRoleId = function () {
            return $(selectors.roleId).val();
        };

        var getRoleName = function () {
            return $(selectors.roleName).val();
        };

        var getData = function () {
            var result = {};

            result.roleId = getRoleId();
            result.companyId = getCompanyId();
            result.roleName = getRoleName();
            result.permisssions = [];
            $(selectors.permissionCheckBoxes).each(function (index, value) {
                var permissionId = $(this).attr('data-id');

                result.permisssions.push(permissionId);
            });

            return result;
        };

        var validate = function () {
            if (getRoleName() === "") {
                $(selectors.roleName).addClass("input-validation-error");
                $(selectors.validationDisplay).removeClass("hide");

                return false;
            }

            return true;
        };

        var getSaveUrl = function () {
            if (getRoleId() !== "") {
                return window.globalajaxurls.updateUserRolePermissions;
            }

            return window.globalajaxurls.createUserRolePermissions;
        };

        var doSaveRole = function () {
            var url = getSaveUrl();
            var data = getData();

            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                traditional: true,
                datatype: "json",
                error: function () {
                    window.location.replace(window.globalajaxurls.errorPage);
                },
                cache: false
            }).done(function (data) {
                if (data.RoleId !== null) {
                    // Load the user roles screen
                    window.location = window.globalajaxurls.loadUserRole + "?companyId=" + getCompanyId() + "&roleId=" + data.RoleId;
                } else {
                    $(selectors.successMessage).removeClass('hide');
                    $(selectors.editUserRoleButton).show();
                    $(selectors.saveUserRoleButton).hide();
                    $(selectors.deleteUserRoleButton).hide();
                }
            });
        };

        var loadingPermissionComplete = function (data) {

            $(selectors.roleDetails).show().html(data);

            $(selectors.editUserRoleButton).click(function (e) {
                e.preventDefault();
                $(selectors.editUserRoleButton).hide();
                $(selectors.saveUserRoleButton).show();
                $(selectors.saveUserRoleButton).removeAttr('disabled');
                $(selectors.deleteUserRoleButton).show();
                $('input[type=checkbox]').removeAttr('disabled');
            });

            $(selectors.saveUserRoleButton).click(function (e) {
                e.preventDefault();

                if (validate() === false) {
                    return;
                }
                $(selectors.saveUserRoleButton).attr('disabled', 'disabled');

                // Before we save, show the client a list of users which will be affected by this change
                // If this is a new role being saved skip this step as no users will have been assigned to this role yet
                if (getRoleId() !== "") {

                    var usersWithRole = null;

                    var successCallBack = function (data) {
                        usersWithRole = data;
                    };

                    new GetUsersWithRole(getCompanyId(), getRoleId(), successCallBack).execute();

                    if (usersWithRole !== null) {
                        $(selectors.userWithRolesResult).empty();
                        $(selectors.userWithRolesResult).append(usersWithRole);
                        $(selectors.dialogUsersAffectedByRoleEdit).dialog({
                            height: 350,
                            resizable: false,
                            width: 500,
                            modal: true,
                            buttons: {
                                "Confirm": function () {
                                    doSaveRole();
                                    $(this).dialog("close");
                                },
                                "Cancel": function () {
                                    $(this).dialog("close");
                                }
                            }
                        });

                        //                        return false;
                    }
                }

                doSaveRole();

                //                return false;
            });

            $(selectors.deleteUserRoleButton).click(function (event) {
                event.preventDefault();

                var userRoleId = $(this).attr('data-id');
                var companyId = getCompanyId();
                var usersWithRole = null;

                var successCallBack = function (data) {
                    usersWithRole = data;
                };

                new GetUsersWithRole(getCompanyId(), userRoleId, successCallBack).execute();

                if (usersWithRole === null) {
                    $(selectors.dialogDeleteUserRole).dialog({
                        buttons: {
                            "Confirm": function () {
                                var successCallBack = function () {
                                    $(selectors.dialogDeleteUserRole).dialog("close");
                                    window.location = window.globalajaxurls.loadUserRole + "?companyId=" + getCompanyId();
                                };

                                new DeleteUserRole(userRoleId, companyId, successCallBack).execute();
                            },
                            "Cancel": function () {
                                $(this).dialog("close");
                            }
                        },
                        resizable: false
                    });
                }
                else {
                    $(selectors.dialogCantDeleteUserRoleRoleInUse).dialog({
                        height: 350,
                        resizable: false,
                        width: 500,
                        modal: true,
                        buttons: { "Ok": function () { $(this).dialog("close"); } }
                    });

                    $(selectors.userWithRolesResult).empty();
                    $(selectors.userWithRolesResult).append(usersWithRole);
                }
            });

            // on load, setup edit/save/delete button visibility, and editability of permissions
            if (($(selectors.editUserRoleButton).size() > 0) && ($(selectors.deleteUserRoleButton).size() > 0)) {
                $(selectors.saveUserRoleButton).hide();
                $(selectors.deleteUserRoleButton).hide();
                $('input[type=checkbox]').attr('disabled', 'disabled');
            } else {
                $(selectors.editUserRoleButton).hide();
            }

        };

        var loadUserRole = function (roleId) {
            $.ajax({
                type: "GET",
                url: window.globalajaxurls.getUserRolePermissions + "?roleId=" + roleId + "&companyId=" + getCompanyId() + "&enableCustomRoleEditing=true",
                error: function () {
                    window.location.replace(window.globalajaxurls.errorPage);
                },
                cache: false
            }).done(function (data) {
                loadingPermissionComplete(data);
            });
        };


        if ($(selectors.isNewUserRole).val() === 'True') {
            $.ajax({
                type: "GET",
                url: window.globalajaxurls.getUserRolePermissions + "?companyId=" + getCompanyId() + "&enableCustomRoleEditing=true",
                error: function () {
                    window.location.replace(window.globalajaxurls.errorPage);
                },
                cache: false
            }).done(function (data) {
                loadingPermissionComplete(data);
            });

            return;
        }

        $("#UserRoles").combobox({
            selectedId: $("#UserRolesId").val(),
            initialValues: roles,
            url: '',
            afterSelect: function (event, ui) {
                var roleId = $("#UserRolesId").val();
                loadUserRole(roleId);

                return false;
            }
        });

        $(selectors.roleDetails).hide();
        var roleId = $("#UserRolesId").val();

        if (roleId !== '') {
            loadUserRole(roleId);
        }

    }

    return {
        initialize: initialize
    };
} ();
function EnableOrDisableUser(userId, companyId, disabled, successCallBack) {
    var failureText = disabled === true ? "Unfortunately we have encountered an error deleting this user." : "Unfortunately we have encountered an error reinstating deleted user.";

    this.execute = function () {
        $.ajax({
            url: window.globalajaxurls.markUserAsEnabledOrDisabled,
            type: "POST",
            dataType: "json",
            data: {
                userId: userId,
                companyId: companyId,
                disabled: disabled
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

function ReinstateUser(userId, companyId, email, successCallBack) {

    this.execute = function () {
        $.ajax({
            url: window.globalajaxurls.reinstateUser,
            type: "POST",
            dataType: "json",
            data: {
                userId: userId,
                companyId: companyId,
                email: email
            },
            success: function (data) {
                if (data.Success === true) {
                    successCallBack();
                } else {
                    alert(data.Data);
                }
            },
            error: function () {
                window.location.replace(window.globalajaxurls.errorPage);
            }
        });
    };
}

    function ResendRegistration(userId, successCallBack) {

        this.execute = function () {
            $.ajax({
                url: window.globalajaxurls.resetUserRegistration,
                type: "POST",
                dataType: "json",
                data: {
                    userId: userId
                },
                success: function (data) {
                    if (data.Success === true) {
                        successCallBack();
                    } else {
                        alert(data.Data);
                    }
                },
                error: function () {
                    window.location.replace(window.globalajaxurls.errorPage);
                }
            });
        };
    };

    var userSearchForm = function () {
        var selectors = {
            companyId: "#CompanyId",
            groupSitesDropDown: "#GroupSiteId",
            deleteUserLink: "a.icon-remove",
            reinstateUserLink: "a.reinstateIcon",
            showDeletedLink: "#showDeletedLink",
            showDeletedCheckBox: "#ShowDeleted",
            dialogDeleteUser: "#dialogDeleteUser",
            dialogReinstateDeletedUser: "#dialogReinstateDeletedUser",
            dialogReinstateDeletedUserWithPendingRegistration: "#dialogReinstateDeletedUserWithPendingRegistration",
            resetRegistrationLink: "a[title='Resend Registration']",
            dialogResendUserRegistrationConfirmation: "#dialogResendUserRegistrationConfirmation",
            dialogResendUserRegistration: "#dialogResendUserRegistration"
        };

        function initialise(sites) {

            $("#Site").combobox({
                selectedId: $("#SiteId").val(),
                initialValues: sites,
                url: ''
            });

            $(selectors.deleteUserLink).click(function (event) {
                event.preventDefault();

                var userId = $(this).attr('data-id');
                var companyId = $(selectors.companyId).val();
                var disabled = true;

                $(selectors.dialogDeleteUser).dialog({
                    buttons: {
                        "Confirm": function () {
                            var successCallBack = function () {
                                $(selectors.dialogDeleteUser).dialog("close");
                                $('form').submit();
                            };

                            new EnableOrDisableUser(userId, companyId, disabled, successCallBack).execute();
                        },
                        "Cancel": function () {
                            $(this).dialog("close");
                        }
                    },
                    resizable: false
                });
            });

            $(selectors.reinstateUserLink).click(function (event) {
                event.preventDefault();

                var userId = $(this).attr('data-id');
                var isRegistered = $(this).data().isRegistered;
                var companyId = $(selectors.companyId).val();
                var disabled = false;

                var dialog = selectors.dialogReinstateDeletedUser;
                if (isRegistered==false) {
                    dialog = selectors.dialogReinstateDeletedUserWithPendingRegistration;
                }

                $(dialog).dialog({
                    buttons: {
                        "Confirm": function () {
                            var successCallBack = function () {
                                $(this).dialog("close");
                                $('form').submit();
                            };

                            new ReinstateUser(userId, companyId, "", successCallBack).execute();
                        },
                        "Cancel": function () {
                            $(this).dialog("close");
                        }
                    },
                    resizable: false
                });
            });

            $(selectors.showDeletedLink).click(function (event) {
                $(selectors.showDeletedCheckBox).prop("checked", true);
                $('#Search').click();
            });

            $(selectors.resetRegistrationLink).click(function (event) {
                event.preventDefault();

                $(selectors.dialogResendUserRegistration).dialog({
                    buttons: {
                        "Confirm": function () {
                            $(this).dialog("close");
                            new ResendRegistration(userId, successCallBack).execute();
                        },
                        "Cancel": function () {
                            $(this).dialog("close");
                        }
                    },
                    resizable: false
                });

                var userId = $(this).attr('data-id');
                var successCallBack = function () {
                    $(selectors.dialogResendUserRegistrationConfirmation).dialog({
                        buttons: {
                            "Ok": function () {
                                $(selectors.dialogResendUserRegistrationConfirmation).dialog("close");
                            }
                        },
                        resizable: false
                    });
                };


            });
        }

        return { initialise: initialise };
    } ();

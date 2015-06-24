
var responsibilitiesSearch = function () {

    function _setupDropdowns(siteGroups, sites, categories) {
        var selectors = {
            selectedSiteGroup: '#SiteGroupId',
            selectedSite: '#SiteId',
            selectedCategory: '#CategoryId',
            siteGroupDropdown: '#SiteGroup',
            siteDropdown: '#Site',
            categoryDropdown: '#Category'
        };

        $(selectors.siteGroupDropdown).combobox({
            selectedId: $(selectors.selectedSiteGroup).val(),
            initialValues: siteGroups
        });

        $(selectors.siteDropdown).combobox({
            selectedId: $(selectors.selectedSite).val(),
            initialValues: sites
        });

        $(selectors.categoryDropdown).combobox({
            selectedId: $(selectors.selectedCategory).val(),
            initialValues: categories
        });
    };

    function _setupAddResponsibilityPopup() {
        var selectors = {
            openDialogButton: '#add-responsibility-link',
            cancel: '#cancel-add-responsibility',
            dialog: '#add-responsibility-dialog'
        };

        $(selectors.openDialogButton).on('click', function () {
            $(selectors.dialog).show().dialog({
                autoOpen: false,
                width: 640,
                modal: true,
                resizable: false,
                draggable: false,
                open: function (event, ui) { // removes auto focus on first link
                    $(selectors.dialog + ' a:focus').blur();
                }
            }).dialog('open');
        });

        $(selectors.cancel).on('click', function () {
            $(selectors.dialog).dialog('close');
        });
    }

    function _setupResponsibilityActions() {
        var selectors = {
            deleteResponsibility: 'a.icon-remove',
            dialogDeleteResponsibility: '#delete-responsibility-dialog',
            dialogDeleteResponsibilityWithAssociatedTasks: '#delete-responsibility-has-tasks-warning-dialog',
            showDeletedButton: 'a#show-deleted',
            searchForm: 'form#formResponsibilities',
            showDeletedFlag: 'input[name="IsShowDeleted"]',
            reinstateResponsibility: 'a.reinstate',
            dialogReinstateResponsibility: '#reinstate-responsibility-dialog',
            copyResponsibility: 'a.icon-repeat',
            validationSummary: ".validation-summary-errors"
        };

        $(selectors.showDeletedButton).click(function (event) {
            event.preventDefault();
            $(selectors.showDeletedFlag, selectors.searchForm).val(true);
            $(selectors.searchForm).submit();
        });

        $(selectors.deleteResponsibility).click(function (event) {
            event.preventDefault();

            var responsibilityId = $(this).attr('data-id');
            var companyId = $(selectors.companyId).val();

            $.ajax({
                url: window.globalajaxurls.checkResponsibilityCanBeDeletedUrl,
                type: "GET",
                dataType: "json",
                data: {
                    companyId: companyId,
                    responsibilityId: responsibilityId
                },
                success: function (data) {
                    var dialogInUse;
                    if (data.hasUndeletedTasks === true) {
                        dialogInUse = $(selectors.dialogDeleteResponsibilityWithAssociatedTasks);
                        dialogInUse.dialog({
                            buttons: {
                                "Confirm": function () {
                                    var successCallBack = function () {
                                        dialogInUse.dialog("close");
                                        window.location.reload();
                                    };

                                    new SetResponsibilityDeletedState(companyId, responsibilityId, successCallBack).setAsDeleted();
                                },
                                "Cancel": function () {
                                    $(this).dialog("close");
                                }
                            },
                            width: '392px',
                            resizable: false
                        });
                    }
                    else if (data.hasUndeletedTasks === false) {
                        dialogInUse = $(selectors.dialogDeleteResponsibility);
                        dialogInUse.dialog({
                            height: 'auto',
                            resizable: false,
                            modal: true,
                            buttons: {
                                "Confirm": function () {
                                    var successCallBack = function () {
                                        dialogInUse.dialog("close");
                                        window.location.reload();
                                    };

                                    new SetResponsibilityDeletedState(companyId, responsibilityId, successCallBack).setAsDeleted();
                                },
                                "Cancel": function () {
                                    $(this).dialog("close");
                                }
                            },
                            width: '392px'
                        });
                    }
                },
                error: function () {
                    window.location.replace(window.globalajaxurls.errorPage);
                }
            });
        });

        $(selectors.reinstateResponsibility).click(function (event) {
            event.preventDefault();

            $(selectors.validationSummary).hide();
            var responsibilityId = $(this).attr('data-id');
            var companyId = $(selectors.companyId).val();

            var dialogInUse = $(selectors.dialogReinstateResponsibility);
            dialogInUse.dialog({
                buttons: {
                    "Confirm": function () {
                        var successCallBack = function () {
                            dialogInUse.dialog("close");
                            window.location.reload();
                        };

                        new SetResponsibilityDeletedState(companyId, responsibilityId, successCallBack).setAsNotDeleted();
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                },
                width: '392px',
                resizable: false
            });
        });

        $(selectors.copyResponsibility).click(function (event) {
            event.preventDefault();

            var responsibilityId = $(this).attr('data-id');

            var titleTextBox = $("#dialogCopyResponsibility").find("#Title");
            var url = $(this).attr("data-url");
            titleTextBox.val("");
            
            $("#dialogCopyResponsibility").dialog({
                height: 'auto',
                resizable: false,
                modal: true,
                buttons: {
                    "Confirm": function () {
                        var successCallBack = function () {
                            $(this).dialog("close");
                            window.location.reload();
                        };

                        var title = titleTextBox.val();
                        new CopyResponsibility(responsibilityId, title, successCallBack).execute();
                    },
                    "Cancel": function () {
                        window.location.reload();
                        $(this).dialog("close");
                    }
                },
                width: '600px'
            }).parent().find(".ui-dialog-titlebar-close.ui-corner-all").remove();
            
            $("#dialogCopyResponsibility").dialog("open");
        });
    }

    function setupPrintButton() {
        var selectors = {
            selectedSiteGroup: '#SiteGroupId',
            selectedSite: '#SiteId',
            selectedCategory: '#CategoryId',
            siteGroupDropdown: '#SiteGroup',
            siteDropdown: '#Site',
            categoryDropdown: '#Category',
            printSearchResults: "#printSearchResults",
            formResponsibilities: "#formResponsibilities"
        };


        $(selectors.printSearchResults).click(function (event) {
            //$.post(window.globalajaxurls.printResponsibilitiesSearchResults, $(selectors.formResponsibilities).serialize());
            var data = $(selectors.formResponsibilities).serialize();
            window.location.replace(window.globalajaxurls.printResponsibilitiesSearchResults + "/ResponsibilitiesSearchResult?" + data);

        });

    }

    function _initialise(siteGroups, sites, categories) {
        _setupDropdowns(siteGroups, sites, categories);
        _setupResponsibilityActions();
        _setupAddResponsibilityPopup();
        setupPrintButton();
    }

    return { initialise: _initialise };
} ();

var CopyResponsibility = function(oldResponsibilityId, title, successCallBack) {

    this.execute = function() {
        $.ajax({
            url: window.globalajaxurls.copyResponsibility,
            error: function() {
                window.location.replace(window.globalajaxurls.errorPage);
            },
            type: "POST",
            dataType: "json",
            data: {
                responsibilityId: oldResponsibilityId,
                title: title
            },
            success: function(result) {
                if (result.Success === true) {
                    successCallBack(result.Id);
                } else if (result.Errors !== undefined) {
                    showValidationErrors("#dialogCopyResponsibility .alert-error", result.Errors);
                }
            }
        });
    };
}; 
   
var SetResponsibilityDeletedState = function (companyId, responsibilityId, successCallBack) {
    var _companyId = companyId;
    var _responsibilityId = responsibilityId;
    var _successCallBack = successCallBack;
    
    var _setAsDeleted = function () {
        var data = {
            companyId: _companyId,
            responsibilityId: _responsibilityId
        };
        AjaxCall.execute(window.globalajaxurls.deleteResponsibility, _successCallBack, data, "POST");
    };

    var _setAsNotDeleted = function () {
        var data = {
            companyId: _companyId,
            responsibilityId: _responsibilityId
        };
        AjaxCall.execute(window.globalajaxurls.undeleteResponsibility, _successCallBack, data, "POST");
    };

    return {
        setAsDeleted: _setAsDeleted,
        setAsNotDeleted: _setAsNotDeleted
    };
}
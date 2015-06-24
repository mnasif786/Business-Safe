var siteDetailsForm = function () {

    var selectors = {
        form: "#SiteDetails",
        siteDetailsContainer: "#sitedetailscontainer",
        deleteLinkBtn: "#delinkSiteBtn",
        dialogDelinkSite: "#dialogDelinkSite",
        siteName: "#Name",
        popOvers: "button[rel=popover]",
        saveButton: "#SaveSiteDetailsButton"
    };

    function showDropDowns (){

        $("#LinkToSite").combobox({
            selectedId: $("#LinkToSiteId").val(),
            initialValues: window.siteDetails.sites,
            url : window.globalajaxurls.getSites,
            data: {
                companyId: $("#ClientId").val(),
                pageLimit: 100
            }
        });
    
        $("#LinkToGroup").combobox({
            selectedId: $("#LinkToGroupId").val(),
            initialValues: window.siteDetails.siteGroups,
            url : '',
            data: {
                companyId: $("#ClientId").val(),
                pageLimit: 100
            }
        });

    }

    function initialise(data) {

        $(selectors.siteDetailsContainer).show();
        $(selectors.siteDetailsContainer).html(data);
        $(selectors.siteName).focus();
        $(".cancel").click(function () {
            $("input, select", $(selectors.form)).val('').text('').attr("disabled", "disabled");
        });

        $(selectors.popOvers).popover({ delay: { show: 1500, hide: 500} }).click(function (e) {
            e.preventDefault();
        });

        $(selectors.deleteLinkBtn).click(function (event) {
            event.preventDefault();

            $(selectors.dialogDelinkSite).dialog({
                buttons: {
                    "Confirm": function () {
                        var form = $(selectors.form);
                        form.attr("action", window.globalajaxurls.delinkSite).submit();
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                },
                resizable: false
            });

            $(".ui-dialog-buttonset button").addClass("btn");
            $(selectors.dialogDelinkSite).dialog("open");

        });

        showDropDowns();            

        $(selectors.saveButton).on("click", function () {
            $(this).attr('disabled', 'disabled');
            $(selectors.form).submit();
        });

       
    }

    function reset() {
        $(selectors.siteDetailsContainer).hide();
    }

    return {
        initialise: initialise,
        showDropDowns : showDropDowns,
        reset: reset
    };
} ();

var siteGroupDetails = function () {
        var selectors = {
            form: "#SiteGroup",
            siteGroupContainer: "#sitegroupscontainer",
            siteGroupName: "#SiteGroup input[id='Name']",
            siteGroupTextBox: "div#groupdetailsinputfields input#Name",
            header: "#sitegroupscontainer .page-header h1",
            saveButton: "#SaveSiteGroupButton",
            hiddenGroupId: "div#sitegroupscontainer input#GroupId",
            deleteLinkBtn: "#deleteSiteBtn",
            dialogDeleteSiteGroup: "#dialogDeleteSiteGroup"
        };

        function resetToAdd() {
            $(selectors.header).text("Add Site Group");
            $(selectors.hiddenGroupId).val('0');
            $(selectors.siteGroupTextBox).val('');
        }

        function hideSiteGroupDetails() {
            $(selectors.siteGroupContainer).hide();
        }

        function showSiteGroupDetails() {
            $(selectors.siteGroupContainer).show();
            resetToAdd();
        }

        function showSiteGroupContainer() {
            $(selectors.siteGroupContainer).show();
        }


        function showDropDowns (){

            $(".site-group-details-link-to-site").combobox({
                selectedId: $("#GroupLinkToSiteId").val(),
                initialValues: window.siteDetails.sites,
                url : window.globalajaxurls.getSites,
                data: {
                    companyId: $("#ClientId").val(),
                    pageLimit: 100
                }
            });
        
            $(".site-group-details-link-to-site-group").combobox({
                selectedId: $("#GroupLinkToGroupId").val(),
                initialValues: window.siteDetails.siteGroups,
                url : '',
                data: {
                    companyId: $("#ClientId").val(),
                    pageLimit: 100
                }
            });

        }

        function initialise(data) {
            $(selectors.siteGroupContainer).show();
            $(selectors.siteGroupContainer).html(data);
            $(selectors.siteGroupName).focus();

            $(selectors.deleteLinkBtn).click(function (event) {
                event.preventDefault();

                $(selectors.dialogDeleteSiteGroup).dialog({
                    buttons: {
                        "Confirm": function () {
                            var form = $(selectors.form);
                            form.attr("action", window.globalajaxurls.deleteSiteGroup).submit();
                        },
                        "Cancel": function () {
                            $(this).dialog("close");
                        }
                    },
                    resizable: false
                });

                $(".ui-dialog-buttonset button").addClass("btn");
                $(selectors.dialogDeleteSiteGroup).dialog("open");

            });

            showSiteGroupContainer();
            showDropDowns();
            
        }

        return {
            showSiteGroupDetails: showSiteGroupDetails,
            initialise: initialise,
            showDropDowns : showDropDowns,
            hideSiteGroupDetails: hideSiteGroupDetails
        };
    } ();


    $(function () {

        function ShowOrganisationalChartCommand(showLink) {

            this.execute = function () {
                var url = showLink.attr("href");
                window.open(url, "SiteOrganisationChart", "height=0, width=0");
            };
        }



        var siteTreeViewForm = function () {

            var selectors = {
                successMessage: "div.alert-success",
                linkedTree: "#tree",
                linkedTreeContainer: "#sidetreecontrol",
                unlinkedTree: "#tree-unlinked-sites",
                unlinkedTreeContainer: "#sidetreecontrolunlinkedsites",
                showOrganisationalChartBtn: "#ShowOrganisationChartButton",
                linkedTreeSiteName: "#tree div[data-type='siteaddress']",
                linkedSitesThatTriggerSiteDetailsLoad: "#tree li div",
                unlinkedSitesThatTriggerSiteDetailsLoad: "#tree-unlinked-sites li div",
                addSiteGroupButton: "#AddSiteGroupLink",
                siteDetailsContainer: "#sitedetailscontainer"
            };

            function makeAddGroupActive() {
                $(selectors.addSiteGroupButton).addClass('active');
            }

            function initialise(clientId) {

                if ($("#UpdatedSiteId").length === 1 || $("#UpdateSiteGroupIdKey").length === 1) {
                    $('#successmessageanchor').get(0).scrollIntoView(true);
                }

                $(selectors.linkedTree).treeview({
                    collapsed: false,
                    animated: "slow",
                    control: selectors.linkedTreeContainer
                });

                $(selectors.unlinkedTree).treeview({
                    collapsed: false,
                    animated: "slow",
                    control: selectors.unlinkedTreeContainer
                });


                $("div.node").css("cursor", "pointer");

                $(selectors.linkedTreeSiteName).hover(
            function () {
                $(this).css("text-decoration", "underline").css("font-weight", "bold");
            },
            function () {
                $(this).css("text-decoration", "").css("font-weight", "normal");
            }
            );

                $(selectors.linkedSitesThatTriggerSiteDetailsLoad).click(function () {
                    var siteType = $(this).attr("data-type");
                    var id = $(this).attr("data-id");

                    if (siteType === "siteaddress") {
                        var loadSiteAddressCommand = new LoadLinkedSiteAddressCommand(clientId, id);
                        loadSiteAddressCommand.execute();
                    }
                    else if (siteType === "sitegroup") {
                        var loadSiteGroupCommand = new LoadSiteGroupCommand(clientId, id);
                        loadSiteGroupCommand.execute();
                    }
                    else if (siteType === undefined) {
                        throw "site type is not set correctly. need to be either siteaddress or sitegroup";
                    }

                    return false;
                });

                $(selectors.unlinkedSitesThatTriggerSiteDetailsLoad).click(function () {

                    var siteType = $(this).attr("data-type");
                    var id = $(this).attr("data-id");

                    if (siteType === "siteaddress") {
                        var loadSiteAddressCommand = new LoadUnlinkedSiteAddressCommand(clientId, id);
                        loadSiteAddressCommand.execute();
                    }
                    else if (siteType === "sitegroup") {
                        var loadSiteGroupCommand = new LoadSiteGroupCommand(clientId, id);
                        loadSiteGroupCommand.execute();
                    }
                    else if (siteType === undefined) {
                        throw "site type is not set correctly. need to be either siteaddress or sitegroup";
                    }

                    return false;
                });

                $(selectors.showOrganisationalChartBtn).click(function (event) {
                    event.preventDefault();
                    var showLink = $(this);
                    new ShowOrganisationalChartCommand(showLink).execute();
                });


                $(selectors.addSiteGroupButton).button();

                $(selectors.addSiteGroupButton).click(function () {
                    $(selectors.siteDetailsContainer).hide();
                    var loadSiteGroupCommand = new LoadSiteGroupCommand(clientId, 0);
                    loadSiteGroupCommand.execute();
                });


                $("div[data-type='sitegroup']").addClass("siteGroupLabel");
            }

            return {
                initialise: initialise,
                makeAddGroupActive: makeAddGroupActive
            };
        } ();


        function LoadLinkedSiteAddressCommand(clientId, siteAddressId) {
            this.clientId = clientId;
            this.siteAddressId = siteAddressId;

            this.execute = function () {

                var url = window.globalajaxurls.getLinkedSiteDetails;
                var callback = function (data) {
                    siteDetailsForm.initialise(data);
                    siteGroupDetails.hideSiteGroupDetails();
                };
                var data = {
                    companyId : clientId,
                    bsoSiteId : siteAddressId
                };

                AjaxCall.execute(url, callback, data);

            };
        }

        function LoadUnlinkedSiteAddressCommand(clientId, siteAddressId) {
            this.clientId = clientId;
            this.siteAddressId = siteAddressId;

            this.execute = function () {

                var url = window.globalajaxurls.getUnlinkedSiteDetails;

                $.ajax({
                    type: "GET",
                    url: url,
                    data: {
                        companyId: clientId,
                        peninsulaSiteId: siteAddressId
                    },
                    cache: false,
                    error: function () {
                        window.location.replace(window.globalajaxurls.errorPage);
                    }
                }).done(function (data) {
                    siteDetailsForm.initialise(data);
                    siteGroupDetails.hideSiteGroupDetails();
                });

            };
        }

        function LoadSiteGroupCommand(clientId, siteGroupId) {
            this.clientId = clientId;
            this.siteGroupId = siteGroupId;

            this.execute = function () {

                

                // $.ajax({
                //     type: "GET",
                //     url: url,
                //     data: {
                //         companyId: clientId,
                //         siteGroupId: siteGroupId
                //     },
                //     cache: false,
                //     error: function () {
                //         window.location.replace(window.globalajaxurls.errorPage);
                //     }
                // }).done(function (data) {
                //     siteGroupDetails.initialise(data);
                //     siteDetailsForm.reset();
                // });

                var url = window.globalajaxurls.getSiteGroupDetails;
                var callback = function (data) {
                    siteGroupDetails.initialise(data);
                    siteDetailsForm.reset();
                };
                var data = {
                    companyId : clientId,
                    siteGroupId: siteGroupId
                };

                AjaxCall.execute(url, callback, data);

            };
        }

        $(function () {
            var clientId = $("#ClientId").val();
            siteTreeViewForm.initialise(clientId);
        });

    });
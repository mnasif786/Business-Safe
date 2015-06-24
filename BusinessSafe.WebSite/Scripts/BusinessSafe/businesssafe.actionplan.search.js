var actionPlanSearch = function () {
    function initialise(sites, siteGroups) {
        
        var selectors = {
            selectedSiteGroup: '#SiteGroupId',
            selectedSite: '#SiteId',
            siteGroupDropdown: '#SiteGroup',
            siteDropdown: '#Site',
            showArchivedLink: "#ShowArchivedLink",
            showArchivedInput: "#showArchived",
            showOpenLink: "#ShowOpenLink"
        };

        $(selectors.siteGroupDropdown).combobox({
            selectedId: $(selectors.selectedSiteGroup).val(),
            initialValues: siteGroups
        });

        $(selectors.siteDropdown).combobox({
            selectedId: $(selectors.selectedSite).val(),
            initialValues: sites
        });

        $(selectors.showArchivedLink).click(function (event) {
            $(selectors.showArchivedInput).val("true");                      
            $('#Search').click();
        });

        $(selectors.showOpenLink).click(function (event) {
            $(selectors.showArchivedInput).val("false");
            $('#Search').click();
        });
    }

    return { initialise: initialise };
} ();

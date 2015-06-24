var visitRequestManager = function() {

    function initialise(sites) {
        
        var selectors = {
            siteDropdown: '#Site',
            selectedSite: '#SiteId',
            requestVisitButton: '#RequestVisit'
        };

        $(selectors.siteDropdown).combobox({
            selectedId: $(selectors.selectedSite).val(),
            initialValues: sites
        });

        $(selectors.requestVisitButton).click(function(event) {
            if ($('.validation-summary-errors') != null) {
                $('.alert-success').hide();
            }
        });
    }

    return { initialise: initialise };
}();
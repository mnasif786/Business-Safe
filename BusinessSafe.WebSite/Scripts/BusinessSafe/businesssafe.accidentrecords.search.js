var accidentRecordsSearch = function () {
    function _setupDropdowns(sites) {
        var selectors = {
            selectedSite: '#SiteId',
            siteDropdown: '#Site',
            showDeletedLink: '#ShowDeleted'
        };

        $(selectors.siteDropdown).combobox({
            selectedId: $(selectors.selectedSite).val(),
            initialValues: sites
        });

	$(selectors.showDeletedLink).click(function (event) {
            $(selectors.showDeletedCheckBox).prop("checked", true);
            $('#Search').click();
        });
    }

    function setupViewButtons() {
        var selectors = {
            view: ".view-accident-record"
        };

        $(selectors.view).click(function(e) {
            alert('value: ' + $(this).attr('data-id'));
        });

        $(selectors.showDeletedLink).click(function (event) {
            $(selectors.showDeletedCheckBox).prop("checked", true);
            $('#Search').click();
        });
    }

    function _setupPrintButton() {
        var selectors = {
            formSearchAccidentRecords: "#formAccidentRecords",
            printSearchResultsButtons: "#printSearchResults"
        };

        $(selectors.printSearchResultsButtons).click(function(event) {
            var data = $(selectors.formSearchAccidentRecords).serialize();
            window.location.replace(window.globalajaxurls.printAccidentRecordsUrl + "/AccidentRecordsSearchResult?" + data);
        });
    }


    function _initialise(sites) {
        _setupDropdowns(sites);
        _setupPrintButton();
        
        BusinessSafe.AccidentRecord.Delete.initialise();
    }

    return { initialise: _initialise };
} ();


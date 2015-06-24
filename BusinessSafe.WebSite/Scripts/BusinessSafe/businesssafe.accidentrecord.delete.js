BusinessSafe.AccidentRecord.Delete = function () {

    var selectors = {
        deleteAccidentRecord: 'a.icon-remove',
        dialogDeleteAccidentRecord: '#delete-accident-record-dialog',
        showDeletedButton: "a#showDeleted",
        searchForm: 'form.search-form',
        showDeleted: '#IsShowDeleted',
        refreshButton: ".t-refresh",
        searchButton: "#searchButton"
    };

    var urls = {
        deleteAccidentRecordUrl: window.globalajaxurls.deleteAccidentRecordUrl
    };

    function _setupDeleteAction() {
        $(selectors.deleteAccidentRecord).on('click', function (event) {
            var that = this;
            event.preventDefault();
            $(selectors.dialogDeleteAccidentRecord).dialog({
                buttons: {
                    "Confirm": function () {
                        _deleteAccidentRecord(that);
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                        $(selectors.refreshButton).click();
                    }
                },
                resizable: false
            });
        });
    }

    function _deleteAccidentRecord(sender) {
        var data = {
            accidentRecordId: $(sender).data().id
        };

        var successfulCallBack = function () {

            window.location.reload();
            $(sender).parent().parent().remove();
            $(selectors.dialogDeleteAccidentRecord).dialog("close");
        };

        AjaxCall.execute(urls.deleteAccidentRecordUrl, successfulCallBack, data, "POST");
    }

    function _setupShowDeletedAction() {
        $(selectors.showDeletedButton).on('click', function () {

            var showDeleted = $(selectors.showDeleted).val() == 'True' ? 'False' : 'True';

            $(selectors.showDeleted).val(showDeleted);

            $(selectors.searchForm).submit();
        });
    }

    function _initialise() {
        _setupDeleteAction();
        _setupShowDeletedAction();
    }

    return { initialise: _initialise };
} ();


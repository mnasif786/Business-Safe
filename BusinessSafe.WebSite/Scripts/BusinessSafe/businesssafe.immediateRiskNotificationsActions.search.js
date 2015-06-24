var immediateRiskNotificationActionsSearch = function () {
    function initialise(status, assignedTo) {

        var selectors = {
            selectedStatusId: '#StatusId',
            selectedAssignedToId: '#AssignedToId',
            statusDropdown: '#Status',
            assignedToDropdown: '#AssignedTo'
        };

        $(selectors.statusDropdown).combobox({
            selectedId: $(selectors.selectedStatusId).val(),
            initialValues: status
        });

        $(selectors.assignedToDropdown).combobox({
            selectedId: $(selectors.selectedAssignedToId).val(),
            initialValues: assignedTo
        });
    }

    return { initialise: initialise };
} ();

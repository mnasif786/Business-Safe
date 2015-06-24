function TaskFurtherControlMeasureGridViewModel(){

    var self = this;
    
    var classNames = {
        viewFCMTaskButton: 'btn-view-further-action-task',
        editFCMTaskButton: 'btn-edit-further-action-task',
        reassignFCMTaskButton: 'btn-reassign-further-action-task',
        removeFCMTaskButton: 'btn-remove-further-action-task'
    };

    var selectors = {
        editFCMTaskButton: "button." + classNames.editFCMTaskButton,
        reassignFCMTaskButton: "button." + classNames.reassignFCMTaskButton,
        removeFCMTaskButton: "button." + classNames.removeFCMTaskButton,
        viewFCMTaskButton: "button." + classNames.viewFCMTaskButton
    };

    var areCommandButtonsAlreadyShownOnRow = function (furtherControlMeasureTaskRow) 
    {       
        return $(furtherControlMeasureTaskRow).find(selectors.editFCMTaskButton, selectors.reassignFCMTaskButton, selectors.removeFCMTaskButton, selectors.viewFCMTaskButton).length > 0;
    };

    var removeCommandButtonsFromAnyOtherRowInTheGrid = function(){
        $(selectors.viewFCMTaskButton).remove();
        $(selectors.editFCMTaskButton).remove();
        $(selectors.reassignFCMTaskButton).remove();
        $(selectors.removeFCMTaskButton).remove();

        $('.' + classNames.viewFCMTaskButton + ', .' + classNames.editFCMTaskButton + ', .' + classNames.removeFCMTaskButton).remove();
    };

    var isTaskCompleted = function (furtherControlMeasureTaskRow) {
        var statusCell = $(furtherControlMeasureTaskRow).children('td.fcm-status');
        var status = $(statusCell).html();        
        return status.indexOf('Completed') !== -1;
    };

    var isTaskNoLongerRequired = function(furtherControlMeasureTaskRow){
        var statusCell = $(furtherControlMeasureTaskRow).children('td.fcm-status');
        var status = $(statusCell).html();
        return status.indexOf('No Longer Required') !== -1;
    };

    var addEditTaskButtonToRow = function(furtherControlMeasureTaskRow){
        var editButton = $('<button id="edit-fcm-task" class="btn ' + classNames.editFCMTaskButton + '" title="Edit"><i class="icon-edit"></i></button>');
        $("td:last", $(furtherControlMeasureTaskRow)).append(editButton);
    };

    var addRemoveTaskButtonToRow = function(furtherControlMeasureTaskRow){
        var removeButton = $('<button class="btn btn-danger ' + classNames.removeFCMTaskButton + '" title="Remove"><i class="icon-remove"></i></button>');
        $("td:last", $(furtherControlMeasureTaskRow)).append(removeButton);
    };

    var addReassignTaskButtonToRow = function(furtherControlMeasureTaskRow){
        var reassignButton = $('<button class="btn ' + classNames.reassignFCMTaskButton + ' pull-right" title="Reassign"><i class="icon-share"></i></button>');
        $("td:last", $(furtherControlMeasureTaskRow)).append(reassignButton);
    };

    var addViewTaskButtonToRow = function(furtherControlMeasureTaskRow){
        var viewButtonButton = $('<button class="btn ' + classNames.viewFCMTaskButton + '" title="View"><i class="icon-search"></button>');
        $("td:last", $(furtherControlMeasureTaskRow)).append(viewButtonButton);
    };

    this.initialize = function(trigger) {
        var row = $(trigger).parent('tr');

        if (areCommandButtonsAlreadyShownOnRow(row)) {
            return;
        }

        removeCommandButtonsFromAnyOtherRowInTheGrid();


        if (isReadOnly() || (isTaskCompleted(row) === true)) {
            addViewTaskButtonToRow(row);
        }
        else {
            addEditTaskButtonToRow(row);
            addRemoveTaskButtonToRow(row);

            if (isTaskNoLongerRequired(row) === false) {
                addReassignTaskButtonToRow(row);
            }
        }

    };
}
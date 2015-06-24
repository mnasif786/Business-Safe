BusinessSafe.RiskAssessment.Tasks.Grid.ViewModel = function (tasksOptions) {
    var classNames = {
        viewFCMTaskButton: 'btn-view-further-action-task',
        editFCMTaskButton: 'btn-edit-further-action-task',
        reassignFCMTaskButton: 'btn-reassign-further-action-task',
        removeFCMTaskButton: 'btn-remove-further-action-task'
    };

    var selectors = {
        furtherControlMeasureTasksTable: "#FurtherControlMeasureTasksTable",
        addFurtherControlMeasureTask: ".add-further-action-task",
        viewFCMTaskButton: "button." + classNames.viewFCMTaskButton,
        editFCMTaskButton: "button." + classNames.editFCMTaskButton,
        reassignFCMTaskButton: "button." + classNames.reassignFCMTaskButton,
        removeFCMTaskButton: "button." + classNames.removeFCMTaskButton,
        reoccuringLabels: ".label-reoccurring-task",
        cancelButton: "#FurtherActionTaskCancelButton"
    };

    // Further Control Measure Tasks Row Click
    $(selectors.furtherControlMeasureTasksTable + " td:not(.bulk-reassign-injected-elem):nth-child(1n + 2)").live('click', function (event) {
        
        event.preventDefault();
        new TaskFurtherControlMeasureGridViewModel().initialize(this);
    });

    // Reoccuring Summary Details
    $(selectors.reoccuringLabels).live('dblclick', function (event) {
        event.preventDefault();
        taskDetailsSummaryViewModelLoader.load(this, tasksOptions);
    });

    // Add Task
    $(selectors.addFurtherControlMeasureTask).click(function (event) {
        event.preventDefault();
        taskNewViewModelLoader.load(this, tasksOptions);
    });

    // Edit Task
    $(selectors.editFCMTaskButton).live('click', function (event) {
        event.preventDefault();
        taskEditViewModelLoader.load(this, tasksOptions);
    });
       
    // Reassign Task
    $(selectors.reassignFCMTaskButton).live('click', function (event) {
        event.preventDefault();
        var data = new TaskRowParameterExtractor(this, tasksOptions).furtherControlMeasureData();
        taskReassignViewModelLoader.load(data);
    });
    
    // View Task   
    $(selectors.viewFCMTaskButton).live('click', function (event) {
        event.preventDefault();
        var data = new TaskRowParameterExtractor(this, tasksOptions).furtherControlMeasureData();
        taskViewViewModelLoader.load(data);
    });

    // Remove Task
    $(selectors.removeFCMTaskButton).live('click', function (event) {
        event.preventDefault();
        var taskDataViewModel = new TaskRowParameterExtractor(this, tasksOptions);
        new TaskRemoveViewModel(taskDataViewModel).initialize();
    });

    // Cancel Task Dialog
    $(selectors.cancelButton).live('click', function (e) {
        e.preventDefault();
        $(selectors.dialogFurtherControlMeasureTask).dialog('close');
    });
    
};

var checkFireAnswersCanBeChanged = function (){

    var self = this;

    var selectors = {
        companyId: "#CompanyId",
        fireChecklistAnswersToCheckIfSignificantFindingExist: "input[name^='YesNoNotApplicable'][value='NotApplicable'], input[name^='YesNoNotApplicable'][value='Yes']",
        noFireChecklistAnswer : "input[name^='YesNoNotApplicable'][value='No']",
        fireRiskAssessmentChecklistId : "#FireRiskAssessmentChecklistId"        
    };

    var getSignificantFindingAnswerData = function(option){
        var answersContainer = $(option).parent();
        var noAnswer = $(selectors.noFireChecklistAnswer, answersContainer);
        var questionId = answersContainer.data("question-id");

        return {
            companyId: $(selectors.companyId).val(),
            fireChecklistId: $(selectors.fireRiskAssessmentChecklistId).val(),
            fireQuestionId: questionId
        };

    };

    var reselectNoAnswer = function(option){
        var answersContainer = $(option).parent();
        var noAnswer = $(selectors.noFireChecklistAnswer, answersContainer);
        noAnswer.attr('checked', true);
    };

    
    var hookEvents = function(){
        
        $(selectors.fireChecklistAnswersToCheckIfSignificantFindingExist).live("click", function(){ 
            
            var that = this;
            var data = getSignificantFindingAnswerData(this);

            var checkFireAnswerCanBeChangedCallback = function(result){

                
                if(result.CanBeDeleted === false){
                    // Show message and bomb out!
                    new AnswerCanNotBeChangedDueToCompletedTaskViewModel().initialize({
                        
                        ok : function(){
                            reselectNoAnswer(that);                            
                        }                     

                    });
                    return;
                }   

                if(result.CanBeChanged === false){

                    new SignificantFindingRemoveViewModel().initialize({
                        
                        cancel : function(){
                            reselectNoAnswer(that);                            
                        },

                        success : function(){
                            // Everything ok
                        }

                    }, data);

                }
            };

            var url = "/FireRiskAssessments/Checklist/CheckFireAnswerCanBeChanged";
            AjaxCall.execute(url, checkFireAnswerCanBeChangedCallback, data);

        });

    };

    var initialize = function () {
        hookEvents();
    };

    return {
        initialize : initialize
    };
}();

function AnswerCanNotBeChangedDueToCompletedTaskViewModel(){

    var selectors = {
        dialog : "#dialogAnswerCanNotBeChanged"        
    };    

    var showDialog = function(callBacks){

         $(selectors.dialog).dialog({
            buttons: {
                "Ok": function () {
                    $(this).dialog("close");
                    callBacks.ok();
                }                
            },
            resizable: false
        });
    };    

    this.initialize = function(callBacks){        
        showDialog(callBacks);
    };
}


function SignificantFindingRemoveViewModel(){

    var self = this;
    
    var selectors = {
        dialog : "#dialogAnswerYesWhenQuestionHasFurtherControlMeasureTasks"        
    };    

    var showDialog = function(callBacks, data){

         $(selectors.dialog).dialog({
            buttons: {
                "Confirm": function () {
                    deleteSignificantFinding(callBacks, data);
                },
                "Cancel": function () {
                    $(this).dialog("close");
                    callBacks.cancel();
                }
            },
            resizable: false
        });

    };

    var deleteSignificantFinding = function(callBacks, deleteData){
        
        var successfulCallBack = function (result) {
            
            $(selectors.dialog).dialog("close");
            
            if (result.Success === true) {
                 callBacks.success();
            }
        };

        var url = "/FireRiskAssessments/SignificantFindings/MarkSignificantFindingAsDeleted";
        AjaxCall.execute(url, successfulCallBack, deleteData, "POST");    
    };   
    
    this.initialize = function(callBacks, data){        
        showDialog(callBacks, data);
    };
}
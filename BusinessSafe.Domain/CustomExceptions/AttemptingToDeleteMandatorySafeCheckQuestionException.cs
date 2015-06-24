using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToDeleteMandatorySafeCheckQuestionException : Exception
    {
        public AttemptingToDeleteMandatorySafeCheckQuestionException(Guid questionId, Guid checklistId)
            : base(string.Format("Attempt made to delete mandatory question with id {0} from checklist {1}", questionId, checklistId))
        { }

        public AttemptingToDeleteMandatorySafeCheckQuestionException()
        { }
    }
}
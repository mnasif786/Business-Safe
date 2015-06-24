using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.Domain.Entities
{
    public enum ActionQuestionStatus
    {
        Red = 0,
        Amber = 1,
        Green = 2
    }

    public static class ActionStatusExtensions
    {
        public static string ToString( this ActionQuestionStatus status)
        {
            string result = "";
            switch(status)
            {
                case ActionQuestionStatus.Red: result = "Red"; break;
                case ActionQuestionStatus.Amber: result = "Amber"; break;
                case ActionQuestionStatus.Green: result  = "Green"; break;                
            }

            return result;
        }
    }

}

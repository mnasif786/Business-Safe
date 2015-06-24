using System;
using System.Web.Mvc;
using EvaluationChecklist.Models;

namespace EvaluationChecklist.Helpers
{
    public interface IChecklistPdfCreator
    {
        ChecklistViewModel ChecklistViewModel { get; set; }
        string GetFileName();
        byte[] GetBytes();
    }
}
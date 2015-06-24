using System;
using System.Collections.Generic;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.WebSite.Helpers
{
    public static class RiskAssessmentTelerikOrderByHelper
    {

        public static RiskAssessmentOrderByColumn GetOrderBy(string orderBy)
        {
            var columnNameMapping = new Dictionary<string, RiskAssessmentOrderByColumn>();
            columnNameMapping.Add("Ref", RiskAssessmentOrderByColumn.Reference);
            columnNameMapping.Add("Reference", RiskAssessmentOrderByColumn.Reference);
            columnNameMapping.Add("Title", RiskAssessmentOrderByColumn.Title);
            columnNameMapping.Add("Site", RiskAssessmentOrderByColumn.Site);
            columnNameMapping.Add("AssignedTo", RiskAssessmentOrderByColumn.AssignedTo);
            columnNameMapping.Add("Status", RiskAssessmentOrderByColumn.Status);
            columnNameMapping.Add("AssessmentDateFormatted", RiskAssessmentOrderByColumn.AssessmentDate);
            columnNameMapping.Add("NextReviewDateFormatted", RiskAssessmentOrderByColumn.NextReview);
            columnNameMapping.Add("CreatedOn", RiskAssessmentOrderByColumn.CreatedOn);

            var columnName = GetColumnNameFromOrderBy(orderBy);

            if (string.IsNullOrEmpty(columnName))
            {
                return RiskAssessmentOrderByColumn.AssessmentDate;
            }

            if (columnNameMapping.ContainsKey(columnName))
            {
                return columnNameMapping[columnName];
            }
            else
            {
                return RiskAssessmentOrderByColumn.AssessmentDate;
            }

        }

        private static string GetColumnNameFromOrderBy(string orderBy)
        {
            var columnName = String.Empty;
            if (!string.IsNullOrEmpty(orderBy))
            {
                string[] parts = orderBy.Split('-');
                if (parts.Length == 2)
                {
                    columnName = parts[0];
                }
            }

            return columnName;
        }

        private static string GetOrderByDirectionFromOrderBy(string orderBy)
        {
            var orderByDirection = String.Empty;
            if (!string.IsNullOrEmpty(orderBy))
            {
                string[] parts = orderBy.Split('-');
                if (parts.Length == 2)
                {
                    orderByDirection = parts[1];
                }
            }

            return orderByDirection;
        }

        public static OrderByDirection GetOrderByDirection(string orderBy)
        {
            var orderByDirection = GetOrderByDirectionFromOrderBy(orderBy);
            if (string.IsNullOrEmpty(orderByDirection))
            {
                return OrderByDirection.Ascending;
            }

            switch (orderByDirection)
            {
                case "desc":
                    return OrderByDirection.Descending;
                case "asc":
                default:
                    return OrderByDirection.Ascending;
            }

        }
    }
}
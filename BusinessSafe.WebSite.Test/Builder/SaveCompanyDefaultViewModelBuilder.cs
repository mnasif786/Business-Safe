using System;
using BusinessSafe.WebSite.Areas.Company.ViewModels;

namespace BusinessSafe.WebSite.Tests.Builder
{
    public class SaveCompanyDefaultViewModelBuilder
    {
        private long _id;
        private string _name;
        private long _companyId;
        private long? _riskAssessmentId = null;
        private bool _runMatchCheck;
        private Guid _userId = Guid.NewGuid();
        private int[] _types = new int[] { 1 };

        public static SaveCompanyDefaultViewModelBuilder Create()
        {
            return new SaveCompanyDefaultViewModelBuilder();
        }

        public SaveCompanyDefaultViewModel Build()
        {
            return new SaveCompanyDefaultViewModel()
            {
                CompanyDefaultId = _id,
                CompanyDefaultValue = _name,
                CompanyId = _companyId,
                RiskAssessmentId = _riskAssessmentId,
                RunMatchCheck = _runMatchCheck,
                RiskAssessmentTypeApplicable = _types
            };
        }

        public SaveCompanyDefaultViewModelBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public SaveCompanyDefaultViewModelBuilder WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public SaveCompanyDefaultViewModelBuilder WithRunMatchCheck(bool runMatchCheck)
        {
            _runMatchCheck = runMatchCheck;
            return this;
        }

        public SaveCompanyDefaultViewModelBuilder WithDefaultId(int id)
        {
            _id = id;
            return this;
        }

        public SaveCompanyDefaultViewModelBuilder WithApplicableTypes(int[] types)
        {
            _types = types;
            return this;
        }
    }
}
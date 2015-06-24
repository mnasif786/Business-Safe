using System;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Tests.Builder
{
    public class SaveCompanyHazardDefaultRequestBuilder
    {
        private long _id;
        private string _name;
        private long _companyId;
        private long? _riskAssessmentId;
        private bool _runMatchCheck;
        private Guid _userId = Guid.NewGuid();
        private int[] _applicableRiskAssessmentTypes;

        public static SaveCompanyHazardDefaultRequestBuilder Create()
        {
            return new SaveCompanyHazardDefaultRequestBuilder();
        }

        public SaveCompanyHazardDefaultRequest Build()
        {
            return new SaveCompanyHazardDefaultRequest(_id, _name, _companyId, _riskAssessmentId, _runMatchCheck, _applicableRiskAssessmentTypes, _userId);
        }

        public SaveCompanyHazardDefaultRequestBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public SaveCompanyHazardDefaultRequestBuilder WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public SaveCompanyHazardDefaultRequestBuilder WithRunMatchCheck(bool runMatchCheck)
        {
            _runMatchCheck = runMatchCheck;
            return this;
        }

        public SaveCompanyHazardDefaultRequestBuilder WithDefaultId(int id)
        {
            _id = id;
            return this;
        }

        public SaveCompanyHazardDefaultRequestBuilder WithApplicableRiskAssessmentTypes(int[] strings)
        {
            _applicableRiskAssessmentTypes = strings;
            return this;
        }
    }

    public class SaveCompanyDefaultRequestBuilder
    {
        private long _id;
        private string _name;
        private long _companyId;
        private long? _riskAssessmentId;
        private bool _runMatchCheck;
        private Guid _userId = Guid.NewGuid();

        public static SaveCompanyDefaultRequestBuilder Create()
        {
            return new SaveCompanyDefaultRequestBuilder();
        }

        public SaveCompanyDefaultRequest Build()
        {
            return new SaveCompanyDefaultRequest(_id, _name, _companyId, _riskAssessmentId, _runMatchCheck, _userId);
        }

        public SaveCompanyDefaultRequestBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public SaveCompanyDefaultRequestBuilder WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public SaveCompanyDefaultRequestBuilder WithRunMatchCheck(bool runMatchCheck)
        {
            _runMatchCheck = runMatchCheck;
            return this;
        }

        public SaveCompanyDefaultRequestBuilder WithDefaultId(int id)
        {
            _id = id;
            return this;
        }
    }
}
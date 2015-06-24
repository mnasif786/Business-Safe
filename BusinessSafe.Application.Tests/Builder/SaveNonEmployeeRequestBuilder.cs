using System;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Tests.Builder
{
    public class SaveNonEmployeeRequestBuilder
    {
        private string _name;
        private long _companyId;
        private long _nonEmployeeId;
        private string _position;
        private string _companyName;
        private bool _runMatchCheck;
        private Guid _userId = Guid.Empty;
        public static SaveNonEmployeeRequestBuilder Create()
        {
            return new SaveNonEmployeeRequestBuilder();
        }

        public SaveNonEmployeeRequest Build()
        {
            return new SaveNonEmployeeRequest(_nonEmployeeId, _companyId, _name, _position, _companyName, _runMatchCheck, _userId);
        }

        public SaveNonEmployeeRequestBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public SaveNonEmployeeRequestBuilder WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public SaveNonEmployeeRequestBuilder WithNonEmployeeId(long nonEmployeeId)
        {
            _nonEmployeeId = nonEmployeeId;
            return this;
        }

        public SaveNonEmployeeRequestBuilder WithId(int id)
        {
            _nonEmployeeId = id;
            return this;
        }
    }
}
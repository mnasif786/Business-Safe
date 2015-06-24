using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Tests.Builder
{
    public class EmailTemplareBuilder
    {
        private static string _name;
        private static string _subject;
        private static string _body;

        public static EmailTemplareBuilder Build()
        {
            _name = "test name";
            _subject = "testemail";
            _body = "test body";
            return new EmailTemplareBuilder();
        }

        public EmailTemplate Create()
        {
            return EmailTemplate.Create(_name, _subject, _body);
        }

        public EmailTemplareBuilder WithSubject(string expectedSubject)
        {
            _subject = expectedSubject;
            return this;
        }
    }
}
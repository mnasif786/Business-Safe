using System.Collections.Generic;

namespace BusinessSafe.Application.Response
{
    public class GuardDefaultExistsReponse
    {
        public bool Exists { get; private set; }
        public IEnumerable<string> MatchingResults { get; private set; }

        private GuardDefaultExistsReponse()
        {
            MatchingResults = new List<string>();
        }

        private GuardDefaultExistsReponse(IEnumerable<string> matches)
        {
            MatchingResults = matches;
            Exists = true;
        }

        public static GuardDefaultExistsReponse NoMatches
        {
            get
            {
                return new GuardDefaultExistsReponse();
            }
        }

        public static GuardDefaultExistsReponse MatchesExist(IEnumerable<string> matches)
        {
            return new GuardDefaultExistsReponse(matches);
        }
    }
}
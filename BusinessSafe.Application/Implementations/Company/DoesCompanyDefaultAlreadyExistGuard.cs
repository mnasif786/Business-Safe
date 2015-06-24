using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Response;

namespace BusinessSafe.Application.Implementations.Company
{
    public interface IDoesCompanyDefaultAlreadyExistGuard
    {
        GuardDefaultExistsReponse Execute(Func<IEnumerable<MatchingName>> query);
    }

    public class DoesCompanyDefaultAlreadyExistGuard : IDoesCompanyDefaultAlreadyExistGuard
    {
        
        public GuardDefaultExistsReponse Execute(Func<IEnumerable<MatchingName>> query)
        {
            var matchingNames = query.Invoke();
            if (!matchingNames.Any())
            {
                return GuardDefaultExistsReponse.NoMatches;
            }

            var matches = matchingNames
                                    .OrderBy(x => x.Name)
                                    .Select(x => x.Name)
                                    .ToList();
            return GuardDefaultExistsReponse.MatchesExist(matches);
        }
    }

    public class MatchingName
    {
        public string Name { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.Application.IntegrationTests
{
    public static class BuildDirectoryLocations
    {

        public static string GetBuildDirectoryLocation(string machineName)
        {
            var buildDirectoryLocations = new Dictionary<string, string>() {{"pbs43758", @"D:\Code\BusinessSafe"}};

            if (buildDirectoryLocations.ContainsKey(machineName.ToLower()))
            {
                return buildDirectoryLocations[machineName.ToLower()];
            }

            return null;

        }
    }
}

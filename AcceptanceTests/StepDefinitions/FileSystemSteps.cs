using TechTalk.SpecFlow;
using System.IO;

namespace BusinessSafe.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class FileSystemSteps : BaseSteps
    {
        [Given(@"Directory '(.*)' exists")]
        public void DirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        [Given(@"File '(.*)' exists")]
        public void FileExists(string file)
        {
            if (!File.Exists(file))
                File.Create(file);
        }
    }
}

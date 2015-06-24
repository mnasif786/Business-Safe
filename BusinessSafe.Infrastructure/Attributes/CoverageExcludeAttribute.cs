using System;

namespace BusinessSafe.Infrastructure.Attributes
{
    //this is used to add attributes to methods so that NCover ignores them when calculating code coverage
    public class CoverageExcludeAttribute : Attribute {}

}
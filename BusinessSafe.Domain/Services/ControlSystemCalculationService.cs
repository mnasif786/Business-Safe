using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Domain.Services
{
    public class ControlSystemCalculationService : IControlSystemCalculationService
    {
        private readonly IControlSystemRepository _controlSystemRepository;

        public ControlSystemCalculationService(IControlSystemRepository controlSystemRepository)
        {
            _controlSystemRepository = controlSystemRepository;
        }

        public ControlSystem Calculate(string hazardousSubstanceGroupCode, MatterState? matterState, Quantity? quantity, DustinessOrVolatility? dustinessOrVolatility)
        {
            if (hazardousSubstanceGroupCode == null || !matterState.HasValue || !quantity.HasValue || !dustinessOrVolatility.HasValue)
                return _controlSystemRepository.GetById((long)ControlSystemEnum.None);

            var rules = GetHazardousSubstanceGroupRules();
            var rule = rules.FirstOrDefault(h => h.Group == hazardousSubstanceGroupCode && h.MatterState == matterState && h.Quantity == quantity && h.DustinessOrVolatility == dustinessOrVolatility);

            if(rule == null)
            {
                throw new NoneMatchingControlSystemRuleException(hazardousSubstanceGroupCode, matterState, quantity,
                                                                 dustinessOrVolatility);
            }

            return _controlSystemRepository.GetById((long)rule.ControlSystem);
        }

        private static IEnumerable<HazardousSubstanceGroupRule> GetHazardousSubstanceGroupRules()
        {
            return new List<HazardousSubstanceGroupRule>()
            {
                //Group A - Small
                new HazardousSubstanceGroupRule("A", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("A", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("A", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("A", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("A", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("A", MatterState.Solid, Quantity.Small, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem1),

                // Group A - Medium
                new HazardousSubstanceGroupRule("A", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("A", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("A", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("A", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("A", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem2),
                new HazardousSubstanceGroupRule("A", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem2),

                // Group A - High
                new HazardousSubstanceGroupRule("A", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("A", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("A", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("A", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem2),
                new HazardousSubstanceGroupRule("A", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem2),
                new HazardousSubstanceGroupRule("A", MatterState.Solid, Quantity.Large, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem2),

                //Group B - Small
                new HazardousSubstanceGroupRule("B", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("B", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("B", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("B", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("B", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("B", MatterState.Solid, Quantity.Small, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem1),

                //Group B - Medium
                new HazardousSubstanceGroupRule("B", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("B", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("B", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem2),
                new HazardousSubstanceGroupRule("B", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem2),
                new HazardousSubstanceGroupRule("B", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem2),
                new HazardousSubstanceGroupRule("B", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem2),

                //Group B - Large
                new HazardousSubstanceGroupRule("B", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("B", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("B", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem2),
                new HazardousSubstanceGroupRule("B", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem3),
                new HazardousSubstanceGroupRule("B", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem3),
                new HazardousSubstanceGroupRule("B", MatterState.Solid, Quantity.Large, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem3),

                //Group C - Small
                new HazardousSubstanceGroupRule("C", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("C", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("C", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem2),
                new HazardousSubstanceGroupRule("C", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem1),
                new HazardousSubstanceGroupRule("C", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem2),
                new HazardousSubstanceGroupRule("C", MatterState.Solid, Quantity.Small, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem2),

                //Group C - Medium
                new HazardousSubstanceGroupRule("C", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem2),
                new HazardousSubstanceGroupRule("C", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem2),
                new HazardousSubstanceGroupRule("C", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem3),
                new HazardousSubstanceGroupRule("C", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem3),
                new HazardousSubstanceGroupRule("C", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem3),
                new HazardousSubstanceGroupRule("C", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem3),

                //Group C - Large
                new HazardousSubstanceGroupRule("C", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem2),
                new HazardousSubstanceGroupRule("C", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem2),
                new HazardousSubstanceGroupRule("C", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("C", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("C", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("C", MatterState.Solid, Quantity.Large, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem4),

                //Group D - Small
                new HazardousSubstanceGroupRule("D", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem2),
                new HazardousSubstanceGroupRule("D", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem2),
                new HazardousSubstanceGroupRule("D", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem3),
                new HazardousSubstanceGroupRule("D", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem3),
                new HazardousSubstanceGroupRule("D", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem3),
                new HazardousSubstanceGroupRule("D", MatterState.Solid, Quantity.Small, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem3),

                //Group D - Medium
                new HazardousSubstanceGroupRule("D", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem3),
                new HazardousSubstanceGroupRule("D", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem3),
                new HazardousSubstanceGroupRule("D", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("D", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("D", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("D", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem4),

                //Group D - Large
                new HazardousSubstanceGroupRule("D", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem3),
                new HazardousSubstanceGroupRule("D", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem3),
                new HazardousSubstanceGroupRule("D", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("D", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("D", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("D", MatterState.Solid, Quantity.Large, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem4),

                //Group E - Small
                new HazardousSubstanceGroupRule("E", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("E", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("E", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("E", MatterState.Solid, Quantity.Small, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("E", MatterState.Liquid, Quantity.Small, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("E", MatterState.Solid, Quantity.Small, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem4),

                //Group E - Medium
                new HazardousSubstanceGroupRule("E", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("E", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("E", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("E", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("E", MatterState.Liquid, Quantity.Medium, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("E", MatterState.Solid, Quantity.Medium, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem4),

                //Group E - Large
                new HazardousSubstanceGroupRule("E", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("E", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Low, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("E", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("E", MatterState.Solid, Quantity.Large, DustinessOrVolatility.Medium, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("E", MatterState.Liquid, Quantity.Large, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem4),
                new HazardousSubstanceGroupRule("E", MatterState.Solid, Quantity.Large, DustinessOrVolatility.High, ControlSystemEnum.ControlSystem4)
            };
        }
    }

    public class HazardousSubstanceGroupRule
    {
        public string Group { get; set; }
        public MatterState MatterState { get; set; }
        public Quantity Quantity { get; set; }
        public DustinessOrVolatility DustinessOrVolatility { get; set; }
        public ControlSystemEnum ControlSystem { get; set; }

        public HazardousSubstanceGroupRule(string group, MatterState matterState, Quantity quantity, DustinessOrVolatility dustinessOrVolatility, ControlSystemEnum controlSystem)
        {
            Group = group;
            MatterState = matterState;
            Quantity = quantity;
            DustinessOrVolatility = dustinessOrVolatility;
            ControlSystem = controlSystem;
        }
    }
}

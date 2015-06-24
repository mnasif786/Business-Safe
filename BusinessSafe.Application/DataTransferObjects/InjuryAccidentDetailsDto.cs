using System.Collections.Generic;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class InjuryAccidentDetailsDto
    {
        public InjurySeverity Severity { get; private set; }
        public List<string> Injuries { get; private set; }
        public List<string> BodyPartsInjured { get; private set; }
        public string CustomInjury { get; private set; }
        public string CustomBodyPart { get; private set; }
        public InjuredToHospital TakenToHospital { get; private set; }
        public InjuredAbleToWork WasPersonAbleToWork { get; private set; }
        public UnableToWork LengthOfTimeNotWorking { get; private set; }

        public InjuryAccidentDetailsDto(InjurySeverity severity, List<string> injuries, List<string> bodyPartsInjured, string customInjury, string customBodyPart, InjuredToHospital takenToHospital, InjuredAbleToWork wasPersonAbleToWork, UnableToWork lengthOfTimeNotWorking)
        {
            Severity = severity;
            Injuries = injuries;
            BodyPartsInjured = bodyPartsInjured;
            CustomInjury = customInjury;
            CustomBodyPart = customBodyPart;
            TakenToHospital = takenToHospital;
            WasPersonAbleToWork = wasPersonAbleToWork;
            LengthOfTimeNotWorking = lengthOfTimeNotWorking;
        }
    }
}
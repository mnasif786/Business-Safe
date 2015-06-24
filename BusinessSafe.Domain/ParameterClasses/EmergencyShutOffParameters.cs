namespace BusinessSafe.Domain.ParameterClasses
{
    public class EmergencyShutOffParameters
    {
        public string ElectricityEmergencyShutOff { get; set; }
        public string GasEmergencyShutOff { get; set; }
        public string WaterEmergencyShutOff { get; set; }
        public string OtherEmergencyShutOff { get; set; }
    }
}
using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Application.DataTransferObjects
{
    [CoverageExclude]
    public sealed class ResponsibilityTaskStatusesDto
    {
        private readonly int _totalRed;
        private readonly int _totalAmber;
        private readonly int _totalGreen;
        private readonly int _total;

        public ResponsibilityTaskStatusesDto(int totalRed, int totalAmber, int totalGreen, int total)
        {
            _totalRed = totalRed;
            _totalAmber = totalAmber;
            _totalGreen = totalGreen;
            _total = total;
        }

        public int GetTotalRed() { return _totalRed; }
        public int GetTotalAmber() { return _totalAmber; }
        public int GetTotalGreen() { return _totalGreen; }
        public int GetTotal() { return _total; }
    }
}
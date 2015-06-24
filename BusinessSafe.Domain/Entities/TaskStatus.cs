namespace BusinessSafe.Domain.Entities
{
    public enum TaskStatus
    {
        Outstanding = 0,
        Completed = 1,
        NoLongerRequired = 2,
        Overdue = 3 // todo: should this live here? it is a derived value - vl 160713
    }
}
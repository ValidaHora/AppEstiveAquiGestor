namespace EstiveAqui.Repository.Abstract
{
    public interface IReleasedHoursRepository : IBaseRepository<ApiSerialize.ReleasedHours>
    {
        int CountToday();
        void ExcludeLastTwoMonths();
    }
}

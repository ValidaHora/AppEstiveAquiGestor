namespace EstiveAqui.Repository
{
    using EstiveAqui.Repository.Abstract;
    using System.Linq;

    public class ReleasedHoursRepository : BaseRepository<ApiSerialize.ReleasedHours>, IReleasedHoursRepository
    {
        public int CountToday()
        {
            var today = System.DateTime.Now.ToString("yyyyMMdd");

            return _repository.Table<ApiSerialize.ReleasedHours>().Count(b => b.Hlx.Contains(today));
        }

        public void ExcludeLastTwoMonths()
        {
            var twoMonthsAgo = System.DateTime.Now.AddMonths(-2);
            var provider = new System.Globalization.CultureInfo("en-US");

            var allReleasedHour = _repository.Table<ApiSerialize.ReleasedHours>().ToList();
            foreach (var item in allReleasedHour)
            {
                var date = System.DateTime.ParseExact(item.Hl, "yyyyMMddHHmm", provider);
                if (date < twoMonthsAgo)
                    _repository.Delete(item);
            }
        }
    }
}
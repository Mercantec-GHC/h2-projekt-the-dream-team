namespace H2Projekt.Web
{
    public static class DtoExtensions
    {
        public static DateOnly ToDateOnly(this DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime);
        }

        public static DateOnly ToDateOnly(this DateTimeOffset dateTime)
        {
            return DateOnly.FromDateTime(dateTime.DateTime);
        }
    }
}

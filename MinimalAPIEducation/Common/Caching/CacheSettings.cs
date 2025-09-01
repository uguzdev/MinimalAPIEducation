namespace MinimalAPIEducation.Common.Caching;

public static class CacheSettings
{
    public static class Keys
    {
        public static string ProductsAll => "products:all";

        public static string Product(int id)
        {
            return $"product:{id}";
        }
    }

    public static class Durations
    {
        public static TimeSpan Short => TimeSpan.FromMinutes(5);
        public static TimeSpan Medium => TimeSpan.FromMinutes(10);
        public static TimeSpan Long => TimeSpan.FromHours(1);
    }
}
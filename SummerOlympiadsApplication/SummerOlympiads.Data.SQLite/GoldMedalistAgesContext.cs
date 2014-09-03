namespace SummerOlympiads.Data.SQLite
{
    using System.Data.Entity;

    using Model.SQLite;
    public class GoldMedalistAgesContext : DbContext
    {
        public DbSet<GoldMedalistAge> GoldMedalistAges { get; set; }
    }
}

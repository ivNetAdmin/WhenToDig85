using SQLite.Net.Attributes;

namespace WhenToDig85.Models.Data
{
    public class Base
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}

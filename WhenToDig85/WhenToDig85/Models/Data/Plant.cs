
using SQLite.Net.Attributes;

namespace WhenToDig85.Models.Data
{
    public class Plant : Base
    {
        [NotNull]
        public string Name { get; set; }
        public string Type { get; set; }
        [Unique]
        public string Slug { get; set; }
        public string SowTime { get; set; }
        public string HarvestTime { get; set; }
        public string Notes { get; set; }

        public Plant() { }
    }
}

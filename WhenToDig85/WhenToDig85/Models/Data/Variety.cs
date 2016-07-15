
using SQLite.Net.Attributes;

namespace WhenToDig85.Models.Data
{
    public class Variety : Base
    {
        [NotNull]
        public string Name { get; set; }
        public string PlantNameSlug { get; set; } 
        public string PlantName { get; set; }
        public string SowNotes { get; set; }
        public string HarvestNotes { get; set; }

        public Variety() { }
    }
}

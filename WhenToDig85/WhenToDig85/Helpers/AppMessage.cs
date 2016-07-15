
namespace WhenToDig85.Helpers
{
    public static class AppMessage
    {
        private const string _plantSelectionPrompt = "Select plant...";
        private const string _missingPlantNameMessage = "You must enter a plant name...";
        private const string _missingVarietyNameMessage = "You must enter a variety name...";
        private const string _plantNamePrompt = "Plant name";
        private const string _varietyNamePrompt = "Variety name";
        
        public static string PlantSelectionPrompt
        {
            get { return _plantSelectionPrompt; }
        }

        public static string PlantNamePrompt
        {
            get { return _plantNamePrompt; }
        }

        public static string VarietyNamePrompt
        {
            get { return _varietyNamePrompt; }
        }

        public static string MissingPlantNameMessage
        {
            get { return _missingPlantNameMessage; }
        }
        
        public static string MissingVarietyNameMessage
        {
            get { return _missingVarietyNameMessage; }
        }
    }
}

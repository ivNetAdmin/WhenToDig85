
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WhenToDig85.Models.Data;
using WhenToDig85.Services;

namespace WhenToDig85.Models.View
{
    public class PlantViewModel : ViewModelBase
    {
        private readonly IPlantService _plantService;
        ObservableCollection<Plant> Plants { get; set; }

        public PlantViewModel(IPlantService plantService)
        {
            if (plantService == null) throw new ArgumentNullException("plantService");
            _plantService = plantService;
        }

        public async Task Init()
        {
            if (Plants != null) return;

            Plants = new ObservableCollection<Plant>(await _plantService.GetPlantList());
        }
    }
}

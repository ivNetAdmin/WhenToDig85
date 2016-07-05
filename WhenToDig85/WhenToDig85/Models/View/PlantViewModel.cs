
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
        public ObservableCollection<string> PlantNames { get; set; }

        public PlantViewModel(IPlantService plantService)
        {
            if (plantService == null) throw new ArgumentNullException("plantService");
            _plantService = plantService;
            Task.Run(() => Init());
        }

        public async Task Init()
        {
            if (PlantNames != null) return;

            PlantNames = new ObservableCollection<string>(await _plantService.GetPlantNames());
            RaisePropertyChanged(() => PlantNames);
            
        }
    }
}

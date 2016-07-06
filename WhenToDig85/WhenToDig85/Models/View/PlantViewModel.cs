
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WhenToDig85.Models.Data;
using WhenToDig85.Services;

namespace WhenToDig85.Models.View
{
    public class PlantViewModel : ViewModelBase, IPageLifeCycleEvents
    {
        private readonly IPlantService _plantService;
        public ObservableCollection<string> PlantNames { get; set; }

        public PlantViewModel(IPlantService plantService)
        {
            if (plantService == null) throw new ArgumentNullException("plantService");
            _plantService = plantService;

            ClearFormCallBackAction = () => { };

            SavePlantCommand = new RelayCommand(()=> {

                ClearFormCallBackAction();
            });

            Task.Run(() => Init());
        }

        public Action ClearFormCallBackAction { get; set; }
        public ICommand SavePlantCommand { get; set; }

        private string _plantName;
        public string PlantName
        {
            get { return _plantName; }
            set
            {
                _plantName = value;
                RaisePropertyChanged(() => PlantName);
            }
        }

        public async Task Init()
        {
            if (PlantNames != null) return;

            PlantNames = new ObservableCollection<string>(await _plantService.GetPlantNames());
            PlantNames.Insert(0, "Select a plant...");
            RaisePropertyChanged(() => PlantNames);
        }

        public void OnAppearing()
        {

        }
    }
}

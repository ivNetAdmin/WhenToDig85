
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
        private const string _plantSelectionPrompt = "Select or Enter new plant details...";
        private const string _newPlantAddedMessage = "New plant added...";
        private const string _missingPlantNameMessage = "ERROR! You must a plant name...";

        private readonly IPlantService _plantService;
        public ObservableCollection<string> PlantNames { get; set; }

        public PlantViewModel(IPlantService plantService)
        {
            if (plantService == null) throw new ArgumentNullException("plantService");
            _plantService = plantService;

            ClearFormCallBackAction = () => { };
            UserMessageCallBackAction = () => { };

            SavePlantCommand = new RelayCommand(() =>
            {
                UserMessage = _newPlantAddedMessage;
                if (string.IsNullOrEmpty(_plantName))
                {
                    UserMessage = _missingPlantNameMessage;
                }
                else
                {
                    ClearFormCallBackAction();
                }
                UserMessageCallBackAction();
            });

            Task.Run(() => Init());
        }

        public Action ClearFormCallBackAction { get; set; }
        public Action UserMessageCallBackAction { get; set; }
        
        public ICommand SavePlantCommand { get; set; }
       
        private string _plantSelection;
        public string PlantSelection
        {
            get { return _plantSelection; }
            set
            {
                _plantSelection = value;
                RaisePropertyChanged(() => PlantSelection);
            }
        }

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

        private string _userMessage;
        public string UserMessage
        {
            get { return _userMessage; }
            set
            {
                _userMessage = value;
                RaisePropertyChanged(() => UserMessage);
            }
        }

        public async Task Init()
        {
            if (PlantNames != null) return;

            PlantNames = new ObservableCollection<string>(await _plantService.GetPlantNames());
            PlantNames.Insert(0, _plantSelectionPrompt);
            RaisePropertyChanged(() => PlantNames);
        }

        public void OnAppearing()
        {

        }
    }
}

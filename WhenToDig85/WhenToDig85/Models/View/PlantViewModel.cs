
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
        private const string _plantListUpdatedMessage = "Plant list updated...";
        private const string _missingPlantNameMessage = "ERROR! You must a plant name...";

        private readonly IPlantService _plantService;

        public ObservableCollection<string> PlantNames { get; set; }

        public PlantViewModel(IPlantService plantService)
        {
            if (plantService == null) throw new ArgumentNullException("plantService");
            _plantService = plantService;

            ClearFormCallBackAction = () => { };
            UserMessageCallBackAction = () => { };

            SavePlantCommand = new RelayCommand(async () =>
            {
                try
                {
                    if (string.IsNullOrEmpty(_plantName))
                    {
                        UserMessage = _missingPlantNameMessage;
                    }
                    else
                    {
                        UserMessage = _plantListUpdatedMessage;
                        await _plantService.Save(PlantName, PlantType, SowTime, HarvestTime, Notes == "Notes" ? string.Empty : Notes);                      
                        GetPlantNames();                                           
                    }
                }
                catch (Exception ex)
                {
                    UserMessage = string.Format("ERROR! {0}", ex.Message);
                }
                finally
                {
                    UserMessageCallBackAction();
                }
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
                if (!string.IsNullOrEmpty(_plantSelection) && _plantSelection != _plantSelectionPrompt)
                {
                    Task.Run(() => GetPlantDetails(value));
                }
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

        private string _plantType;
        public string PlantType
        {
            get { return _plantType; }
            set
            {
                _plantType = value;
                RaisePropertyChanged(() => PlantType);
            }
        }

        private string _sowTime;
        public string SowTime
        {
            get { return _sowTime; }
            set
            {
                _sowTime = value;
                RaisePropertyChanged(() => SowTime);
            }
        }

        private string _harvestTime;
        public string HarvestTime
        {
            get { return _harvestTime; }
            set
            {
                _harvestTime = value;
                RaisePropertyChanged(() => HarvestTime);
            }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                RaisePropertyChanged(() => Notes);
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
            try
            {
                GetPlantNames();
            }
            catch (Exception ex)
            {
                UserMessage = string.Format("ERROR! {0}", ex.Message);           
                UserMessageCallBackAction();
            }
        }

        private async Task GetPlantDetails(string value)
        {
            try
            {
                var plant = await _plantService.GetPlantByName(value);
                PlantName = plant.Name;
                PlantType = plant.Type;
                SowTime = plant.SowTime;
                HarvestTime = plant.HarvestTime;
                Notes = string.IsNullOrEmpty(plant.Notes) ? "Notes" : plant.Notes;
            }
            catch (Exception ex)
            {
                UserMessage = string.Format("ERROR! {0}", ex.Message);
                UserMessageCallBackAction();
            }

        }

        public void OnAppearing()
        {

        }

        private async void GetPlantNames()
        {
            if (PlantNames != null) PlantNames.Clear();
            PlantNames = new ObservableCollection<string>(await _plantService.GetPlantNames());
            PlantNames.Insert(0, _plantSelectionPrompt);
            RaisePropertyChanged(() => PlantNames);
            ClearFormCallBackAction();
        }

        private Plant GetPlantByName(string value)
        {
            Task<Plant> task = _plantService.GetPlantByName(value);
            return task.Result;
        }

    }
}

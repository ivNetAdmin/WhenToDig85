
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WhenToDig85.Helpers;
using WhenToDig85.Models.Data;
using WhenToDig85.Services;
using WhenToDig85.Views;

namespace WhenToDig85.Models.View
{
    public class VarietyViewModel : ViewModelBase, IPageLifeCycleEvents
    {
        private readonly IPlantService _plantService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<string> PlantNames { get; set; }
        public ObservableCollection<string> VarietyNames { get; set; }

        public VarietyViewModel(INavigationService navigationService, IPlantService plantService)
        {
            if (navigationService == null) throw new ArgumentNullException("navigationService");
            _navigationService = navigationService;

            if (plantService == null) throw new ArgumentNullException("plantService");
            _plantService = plantService;

            ClearFormCallBackAction = () => { };
            UserErrorMessageCallBackAction = () => { };

            //CalendarNavigationCommand = new RelayCommand(async () =>{});
            //JobNavigationCommand = new RelayCommand(async () =>{});
            //ReviewNavigationCommand = new RelayCommand(async () =>{});
            PlantNavigationCommand = new RelayCommand(()=>{_navigationService.NavigateTo(Locator.PlantView);});
            //VarietyNavigationCommand =new RelayCommand(() => { });
           
            SaveVarietyCommand = new RelayCommand(async () =>
            {
                try
                {
                    if (string.IsNullOrEmpty(_varietyName))
                    {
                        UserMessage = AppMessage.MissingVarietyNameMessage;
                    }
                    else
                    {                       
                        await _plantService.Save(PlantSelection, VarietyName, SowNotes == "Notes" ? string.Empty : SowNotes, HarvestNotes == "Notes" ? string.Empty : HarvestNotes); 
                        GetPlantNames();                                           
                    }
                }
                catch (Exception ex)
                {
                    UserMessage = string.Format("ERROR! {0}", ex.Message);
                }
                finally
                {
                    UserErrorMessageCallBackAction();
                }
            });
            
            Task.Run(() => Init());
        }

        public Action ClearFormCallBackAction { get; set; }
        public Action UserErrorMessageCallBackAction { get; set; }

        public ICommand CalendarNavigationCommand { get; set; }
        public ICommand JobNavigationCommand { get; set; }
        public ICommand ReviewNavigationCommand { get; set; }
        public ICommand PlantNavigationCommand { get; set; }
        public ICommand VarietyNavigationCommand { get; set; }

        public ICommand SaveVarietyCommand { get; set; }


        private string _varietySelected;
        public string VarietySelected
        {
            get { return _varietySelected; }
            set
            {
                _varietySelected = value;
                RaisePropertyChanged(() => VarietySelected);
                if (!string.IsNullOrEmpty(_varietySelected))
                {
                    Task.Run(() => GetVarietyDetail());
                }
            }
        }

        private string _plantSelection;
        public string PlantSelection
        {
            get { return _plantSelection; }
            set
            {
                _plantSelection = value;
                RaisePropertyChanged(() => PlantSelection);
                if (!string.IsNullOrEmpty(_plantSelection) && _plantSelection != AppMessage.PlantNamePrompt)
                {
                    Task.Run(() => GetVarieties());
                }
            }
        }
        
        private string _varietyName;
        public string VarietyName
        {
            get { return _varietyName; }
            set
            {
                _varietyName = value;
                RaisePropertyChanged(() => VarietyName);
            }
        }
        
        private string _sowNotes;
        public string SowNotes
        {
            get { return _sowNotes; }
            set
            {
                _sowNotes = value;
                RaisePropertyChanged(() => SowNotes);
            }
        }
        
        private string _harvestNotes;
        public string HarvestNotes
        {
            get { return _harvestNotes; }
            set
            {
                _harvestNotes = value;
                RaisePropertyChanged(() => HarvestNotes);
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
                UserErrorMessageCallBackAction();
            }
        }


        public void OnAppearing()
        {

        }

        private async void GetPlantNames()
        {
            if (PlantNames != null) PlantNames.Clear();
            PlantNames = new ObservableCollection<string>(await _plantService.GetPlantNames());
            PlantNames.Insert(0, AppMessage.PlantSelectionPrompt);
            RaisePropertyChanged(() => PlantNames);           
            ClearFormCallBackAction();
        }

        private async void GetVarieties()
        {
            if (VarietyNames != null) VarietyNames.Clear();
            
            VarietyNames = new ObservableCollection<string>(await _plantService.GetPlantVarietyNames(_plantSelection));
            RaisePropertyChanged(() => VarietyNames);        
        }

        private async void GetVarietyDetail()
        {
            var variety = await _plantService.GetPlantVarietyDetails(_plantSelection, _varietySelected);
            VarietyName = variety.Name;
            SowNotes = variety.SowNotes;
            HarvestNotes = variety.HarvestNotes;
        }  
    }
}

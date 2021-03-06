﻿
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
    public class PlantViewModel : ViewModelBase, IPageLifeCycleEvents
    {
       // private const string _plantSelectionPrompt = "Select or Enter plant details...";       
       // private const string _missingPlantNameMessage = "You must a plant name...";
       // private const string _updatedPlantMessage = "Plant list updated...";

        private readonly IPlantService _plantService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<string> PlantNames { get; set; }

        public PlantViewModel(INavigationService navigationService, IPlantService plantService)
        {
            if (navigationService == null) throw new ArgumentNullException("navigationService");
            _navigationService = navigationService;

            if (plantService == null) throw new ArgumentNullException("plantService");
            _plantService = plantService;

            ClearFormCallBackAction = () => { };
            UserErrorMessageCallBackAction = () => { };

            //CalendarNavigationCommand = new RelayCommand(async () =>{});
            JobNavigationCommand = new RelayCommand(() => { _navigationService.NavigateTo(Locator.JobView); });
            //ReviewNavigationCommand = new RelayCommand(async () =>{});
             PlantNavigationCommand = new RelayCommand(() => { });
            VarietyNavigationCommand = new RelayCommand(() => { _navigationService.NavigateTo(Locator.VarietyView); });

            SavePlantCommand = new RelayCommand(async () =>
            {
                try
                {
                    if (string.IsNullOrEmpty(_plantName))
                    {
                        UserMessage = AppMessage.MissingPlantNameMessage;
                    }
                    else
                    {                       
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
     
        public ICommand SavePlantCommand { get; set; }
     
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
                UserErrorMessageCallBackAction();
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

        private Plant GetPlantByName(string value)
        {
            Task<Plant> task = _plantService.GetPlantByName(value);
            return task.Result;
        }

    }
}

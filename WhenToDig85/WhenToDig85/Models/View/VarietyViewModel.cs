
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
        private readonly INavigationService _navigationService;

        public VarietyViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
             ClearFormCallBackAction = () => { };
            UserErrorMessageCallBackAction = () => { };

            CalendarNavigationCommand = new RelayCommand(async () =>{});
            JobNavigationCommand = new RelayCommand(async () =>{});
            ReviewNavigationCommand = new RelayCommand(async () =>{});
            PlantNavigationCommand = new RelayCommand(async () =>{_navigationService.NavigateTo(Locator.PlantView);});
            VarietyNavigationCommand =new RelayCommand(() => { });
           
            Task.Run(() => Init());
        }

        public Action ClearFormCallBackAction { get; set; }
        public Action UserErrorMessageCallBackAction { get; set; }

        public ICommand CalendarNavigationCommand { get; set; }
        public ICommand JobNavigationCommand { get; set; }
        public ICommand ReviewNavigationCommand { get; set; }
        public ICommand PlantNavigationCommand { get; set; }
        public ICommand VarietyNavigationCommand { get; set; }

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

    }
}

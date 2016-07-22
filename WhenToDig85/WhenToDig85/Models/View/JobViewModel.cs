
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WhenToDig85.Models.View
{
    public class JobViewModel : ViewModelBase, IPageLifeCycleEvents
    {
        private readonly INavigationService _navigationService;

        public JobViewModel(INavigationService navigationService)
        {
            if (navigationService == null) throw new ArgumentNullException("navigationService");
            _navigationService = navigationService;

            ClearFormCallBackAction = () => { };
            UserErrorMessageCallBackAction = () => { };

            //CalendarNavigationCommand = new RelayCommand(async () =>{});
            JobNavigationCommand = new RelayCommand(() => { });
            //ReviewNavigationCommand = new RelayCommand(async () =>{});
            PlantNavigationCommand = new RelayCommand(() => { _navigationService.NavigateTo(Locator.PlantView); });
            VarietyNavigationCommand =new RelayCommand(() => { _navigationService.NavigateTo(Locator.VarietyView); });

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
               // GetPlantNames();
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

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
         
        public VarietyViewModel(IPlantService plantService)
        {
           
            ClearFormCallBackAction = () => { };
            UserErrorMessageCallBackAction = () => { };

            CalendaNavigationrCommand = new RelayCommand(async () =>{});
            JobNavigationCommand = new RelayCommand(async () =>{});
            ReviewNavigationCommand = new RelayCommand(async () =>{});
            PlantNavigationCommand = new RelayCommand(async () =>{_navigationService.NavigateTo(Locator.PlantView);});
            VarietyNavigationCommand =new RelayCommand(() => { });
           
            Task.Run(() => Init());
        }

        public Action ClearFormCallBackAction { get; set; }
        public Action UserErrorMessageCallBackAction { get; set; }

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

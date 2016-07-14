using System;
using System.Threading.Tasks;
using WhenToDig85.Controls;
using WhenToDig85.Helpers;
using WhenToDig85.Models.View;
using Xamarin.Forms;

namespace WhenToDig85.Views
{
    public partial class PlantView : ContentPage
    {
        private PlantViewModel _viewModel;
        public PlantView()
        {
            InitializeComponent();
            BindingContext = App.Locator.PlantVM;

            _viewModel = (PlantViewModel)BindingContext;

            _viewModel.ClearFormCallBackAction = () => ClearForm();
            _viewModel.UserErrorMessageCallBackAction = () => DisplayUserErrorMessage(_viewModel.UserMessage);
        }
      
        public void ClearButtonClicked(object sender, EventArgs args)
        {
            ClearForm();
        }

        public void Name_TextChanged(object sender, EventArgs args)
        {
            this.FindByName<Label>("UserMessage").Text = string.Empty;
        }

        public void Navigation_Clicked(object sender, EventArgs args)
        {
            var cakes = "";
        }

        public void Notes_TextChanged(object sender, EventArgs args)
        {
            var editor = (Editor)sender;
            editor.TextColor = editor.Text == "Notes"?Color.Gray:Color.White;
        }
        

        protected override void OnAppearing()
        {
            ClearForm();

            Context.OnAppearing();
            base.OnAppearing();
        }

        private IPageLifeCycleEvents Context
        {
            get { return (IPageLifeCycleEvents)BindingContext; }
        }

        private void ClearForm()
        {
            this.FindByName<BindablePicker>("PlantNamePicker").SelectedIndex = 0;
            this.FindByName<Entry>("PlantNameEntry").Text = string.Empty;
            this.FindByName<Entry>("PlantTypeEntry").Text = string.Empty;
            this.FindByName<Entry>("PlantSowEntry").Text = string.Empty;
            this.FindByName<Entry>("PlantHarvestEntry").Text = string.Empty;
            this.FindByName<Label>("UserMessage").Text = string.Empty;
            this.FindByName<Editor>("Notes").Text = "Notes";

            this.FindByName<Entry>("PlantNameEntry").Placeholder = AppMessage.PlantNamePrompt;
            this.FindByName<Entry>("PlantNameEntry").PlaceholderColor = Color.Default;

        }

        private void DisplayUserErrorMessage(string userMessage)
        {
            //  this.FindByName<Label>("UserMessage").TextColor = Color.Aqua;
            //  if (!string.IsNullOrEmpty(userMessage) && userMessage.IndexOf("ERROR") != -1) this.FindByName<Label>("UserMessage").TextColor = Color.Red;
            if (userMessage == AppMessage.MissingPlantNameMessage)
            {
                //this.FindByName<Label>("UserMessage").Text = userMessage;
                this.FindByName<Entry>("PlantNameEntry").Placeholder = userMessage;
                this.FindByName<Entry>("PlantNameEntry").PlaceholderColor = Color.Red;
            }

            //Task.Delay(10000).Wait();
            //this.FindByName<Label>("UserMessage").Text = string.Empty;
        }
    }
}

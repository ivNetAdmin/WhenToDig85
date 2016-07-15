
using System;
using WhenToDig85.Models.View;
using Xamarin.Forms;

namespace WhenToDig85.Views
{
    public partial class VarietyView : ContentPage
    {
        private VarietyViewModel _viewModel;
        public VarietyView()
        {
            InitializeComponent();
            BindingContext = App.Locator.VarietyVM;

            _viewModel = (VarietyViewModel)BindingContext;

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

        public void Notes_TextChanged(object sender, EventArgs args)
        {
            var editor = (Editor)sender;
            editor.TextColor = editor.Text == "Notes" ? Color.Gray : Color.White;
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
        }
    }
}

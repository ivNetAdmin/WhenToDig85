using System;
using WhenToDig85.Controls;
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
            _viewModel.UserMessageCallBackAction = () => DisplayUserMessage(_viewModel.UserMessage);
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
        }

        private void DisplayUserMessage(string userMessage)
        {
            this.FindByName<Label>("UserMessage").TextColor = Color.Aqua;
            if (userMessage.IndexOf("ERROR") != -1) this.FindByName<Label>("UserMessage").TextColor = Color.Red;
            this.FindByName<Label>("UserMessage").Text = userMessage;
        }

    }
}

﻿
using System;
using WhenToDig85.Controls;
using WhenToDig85.Helpers;
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
            this.FindByName<Entry>("VarietyNameEntry").Text = string.Empty;
            this.FindByName<Editor>("SowNotes").Text = "Notes";
            this.FindByName<Editor>("HarvestNotes").Text = "Notes";

            this.FindByName<Entry>("VarietyNameEntry").Placeholder = AppMessage.VarietyNamePrompt;
            this.FindByName<Entry>("VarietyNameEntry").PlaceholderColor = Color.Default;

            this.FindByName<ListView>("VarietyNameList").SelectedItem = null;
        }
        
        private void DisplayUserErrorMessage(string userMessage)
        {
               if (userMessage == AppMessage.MissingVarietyNameMessage)
            {
                this.FindByName<Entry>("VarietyNameEntry").Placeholder = userMessage;
                this.FindByName<Entry>("VarietyNameEntry").PlaceholderColor = Color.Red;
            }
        }
    }
}

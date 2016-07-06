﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhenToDig85.Controls;
using WhenToDig85.Models.View;
using Xamarin.Forms;

namespace WhenToDig85.Views
{
    public partial class PlantView : ContentPage
    {
        public PlantView()
        {
            InitializeComponent();
            BindingContext = App.Locator.PlantVM;
        }

        protected override void OnAppearing()
        {
            var plantNamePicker = this.FindByName<BindablePicker>("PlantNamePicker");
            plantNamePicker.SelectedIndex = 0;
            Context.OnAppearing();
            base.OnAppearing();
        }

        private IPageLifeCycleEvents Context
        {
            get { return (IPageLifeCycleEvents)BindingContext; }
        }
    }
}

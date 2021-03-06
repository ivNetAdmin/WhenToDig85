﻿using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using WhenToDig85.Models.View;
using WhenToDig85.Services;

namespace WhenToDig85
{
    public class Locator
    {
        public Locator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // ViewModels
            SimpleIoc.Default.Register<PlantViewModel>();
            SimpleIoc.Default.Register<VarietyViewModel>();
            SimpleIoc.Default.Register<JobViewModel>();



        }
        
        public const string PlantView = "PlantView";
        public const string VarietyView = "VarietyView";
        public const string JobView = "JobView";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
       "CA1822:MarkMembersAsStatic",
       Justification = "This non-static member is needed for data binding purposes.")]
        public PlantViewModel PlantVM
        {
            get { return ServiceLocator.Current.GetInstance<PlantViewModel>(); }
        }
        
         [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
       "CA1822:MarkMembersAsStatic",
       Justification = "This non-static member is needed for data binding purposes.")]
        public VarietyViewModel VarietyVM
        {
            get { return ServiceLocator.Current.GetInstance<VarietyViewModel>(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
     "CA1822:MarkMembersAsStatic",
     Justification = "This non-static member is needed for data binding purposes.")]
        public JobViewModel JobVM
        {
            get { return ServiceLocator.Current.GetInstance<JobViewModel>(); }
        }
    }
}

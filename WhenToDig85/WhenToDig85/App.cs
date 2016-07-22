using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhenToDig85.Services;
using WhenToDig85.Views;
using Xamarin.Forms;

namespace WhenToDig85
{
    public class App : Application
    {
        private static Locator _locator;
        public static Locator Locator { get { return _locator ?? (_locator = new Locator()); } }

        public App()
        {
            //_locator = new Locator();

            // Services
            var nav = new NavigationService();
            nav.Configure(Locator.PlantView, typeof(PlantView));
            nav.Configure(Locator.VarietyView, typeof(VarietyView));
            nav.Configure(Locator.JobView, typeof(JobView));
            SimpleIoc.Default.Register<INavigationService>(() => nav);

            var ps = new PlantService();
            SimpleIoc.Default.Register<IPlantService>(() => ps);

            // The root page of your application
            var page = new NavigationPage(new JobView());
            nav.Initialize(page);
            MainPage = page;
            
        }       

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

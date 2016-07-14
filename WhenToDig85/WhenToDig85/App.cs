using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhenToDig85.Views;
using Xamarin.Forms;

namespace WhenToDig85
{
    public class App : Application
    {
        private static Locator _locator;

        public App()
        {
            _locator = new Locator();

            var nav = new NavigationService();
            nav.Configure(Locator.PlantView, typeof(PlantView));
            nav.Configure(Locator.VarietyView, typeof(VarietyView));

            // The root page of your application
            MainPage = new NavigationPage(new PlantView());
            
        }

        public static Locator Locator { get { return _locator ?? (_locator = new Locator()); } }

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

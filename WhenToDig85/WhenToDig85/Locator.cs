using GalaSoft.MvvmLight.Ioc;
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


            // Services
            SimpleIoc.Default.Register<IPlantService, PlantService>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
       "CA1822:MarkMembersAsStatic",
       Justification = "This non-static member is needed for data binding purposes.")]
        public PlantViewModel PlantVM
        {
            get { return ServiceLocator.Current.GetInstance<PlantViewModel>(); }
        }
    }
}

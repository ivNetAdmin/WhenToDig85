using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

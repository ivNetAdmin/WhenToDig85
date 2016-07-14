
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
        }
        
        private void DisplayUserErrorMessage(string userMessage)
        {
        }
    }
}

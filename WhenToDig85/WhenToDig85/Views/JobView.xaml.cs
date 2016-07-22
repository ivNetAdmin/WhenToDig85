
using WhenToDig85.Helpers;
using WhenToDig85.Models.View;
using Xamarin.Forms;

namespace WhenToDig85.Views
{
    public partial class JobView : ContentPage
    {
        private JobViewModel _viewModel;
        public JobView()
        {
            InitializeComponent();
            BindingContext = App.Locator.JobVM;

            _viewModel = (JobViewModel)BindingContext;

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
            if (userMessage == AppMessage.MissingVarietyNameMessage)
            {
              //  this.FindByName<Entry>("VarietyNameEntry").Placeholder = userMessage;
              //  this.FindByName<Entry>("VarietyNameEntry").PlaceholderColor = Color.Red;
            }

        }
    }
}

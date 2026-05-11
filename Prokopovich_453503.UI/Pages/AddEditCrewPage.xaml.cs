using Prokopovich_453503.UI.ViewModels;

namespace Prokopovich_453503.UI.Pages
{
    public partial class AddEditCrewPage : ContentPage
    {
        public AddEditCrewPage(AddEditCrewViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
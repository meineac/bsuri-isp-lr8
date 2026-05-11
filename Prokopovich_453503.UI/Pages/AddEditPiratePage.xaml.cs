using Prokopovich_453503.UI.ViewModels;

namespace Prokopovich_453503.UI.Pages
{

    public partial class AddEditPiratePage : ContentPage
    {
        public AddEditPiratePage(AddEditPirateViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
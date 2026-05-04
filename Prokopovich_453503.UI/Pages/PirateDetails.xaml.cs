using Prokopovich_453503.UI.ViewModels;

namespace Prokopovich_453503.UI.Pages;

public partial class PirateDetails : ContentPage
{
	public PirateDetails(PirateDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}
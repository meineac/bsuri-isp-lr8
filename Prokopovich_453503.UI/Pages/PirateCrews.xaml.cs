using Prokopovich_453503.UI.ViewModels;

namespace Prokopovich_453503.UI.Pages;

public partial class PirateCrews : ContentPage
{
	public PirateCrews(PirateCrewsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
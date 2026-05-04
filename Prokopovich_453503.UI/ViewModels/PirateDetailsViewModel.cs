using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prokopovich_453503.UI.ViewModels
{
    [QueryProperty(nameof(Pirate), "Pirate")]
    public partial class PirateDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        Pirate pirate;
    }
}

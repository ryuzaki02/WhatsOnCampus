using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace WhatsOnCampus.ViewModel
{
    public partial class BaseViewModel: ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _title;            
    }
}


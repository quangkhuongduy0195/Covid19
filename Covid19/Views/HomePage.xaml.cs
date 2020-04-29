using System;
using System.Collections.Generic;
using Covid19.Views.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Covid19.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : PageBase
    {
        public HomePage()
        {
            InitializeComponent();
        }
    }
}

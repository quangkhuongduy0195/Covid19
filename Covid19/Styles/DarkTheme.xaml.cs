﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Covid19.Styles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DarkTheme : ResourceDictionary
    {
        public DarkTheme()
        {
            InitializeComponent();
        }
    }
}

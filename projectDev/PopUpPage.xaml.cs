﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace projectDev
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PopUpPage()
        {
            InitializeComponent();
        }
    }
}
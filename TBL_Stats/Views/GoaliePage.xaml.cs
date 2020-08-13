﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBL_Stats.Models;
using TBL_Stats.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TBL_Stats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoaliePage : ContentPage
    {
        public GoaliePage(Skater skater)
        {
            InitializeComponent();
            BindingContext = new SkaterViewModel(skater);
        }
    }
}
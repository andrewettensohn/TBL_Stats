using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBL_Stats.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TBL_Stats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SkaterPage : ContentPage
    {
        public SkaterPage(Skater skater)
        {
            InitializeComponent();
            BindingContext = skater;
        }
    }
}
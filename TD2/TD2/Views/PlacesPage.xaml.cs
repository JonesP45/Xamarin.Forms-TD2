using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Storm.Mvvm.Forms;
using Storm.Mvvm.Services;
using TD2.ViewModels;
using Xamarin.Forms;

namespace TD2
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class PlacesPage : BaseContentPage
    {
        public PlacesPage()
        {
            InitializeComponent();
            BindingContext = new PlacesViewModel();
        }
    }
}

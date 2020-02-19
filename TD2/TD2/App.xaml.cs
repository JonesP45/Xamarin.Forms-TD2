using System;
using Storm.Mvvm;
using TD2.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TD2
{
    public partial class App : MvvmApplication
    {
        public App() : base(() => new PlacesPage())
        {
            InitializeComponent();
        }
        // public App()
        // {
        //     InitializeComponent();
        //
        //     MainPage = new MainPage();
        // }
        //
        // protected override void OnStart()
        // {
        // }
        //
        // protected override void OnSleep()
        // {
        // }
        //
        // protected override void OnResume()
        // {
        // }
    }
}

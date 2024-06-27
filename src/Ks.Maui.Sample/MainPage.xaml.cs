using Ks.Mobile.Notice;
using System.Windows.Input;

namespace Ks.Maui.Sample
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            //TestClickedCommand = new Command(async () =>
            //{
            //    await App.Current!.MainPage!.DisplayAlert("Alert", "You have been alerted", "OK");
            //});
            //OnPropertyChanged(nameof(TestClickedCommand));
        }
        public ICommand TestClickedCommand { get; set; }
        //public ICommand TestClickedCommand = new Command(async() =>
        //{
        //    await App.Current!.MainPage!.DisplayAlert("Alert", "You have been alerted", "OK");
        //});

    }
}

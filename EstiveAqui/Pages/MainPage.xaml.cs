namespace EstiveAqui.Pages
{
    using Plugin.Share;
    using Services.Abstract;
    using System;
    using System.Linq;
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        private readonly INavigationService _navigationPage;
        private readonly IMessageService _messageService;
        private readonly IApiService _apiService;

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            _navigationPage = DependencyService.Get<INavigationService>();
            _messageService = DependencyService.Get<IMessageService>();
            _apiService = DependencyService.Get<IApiService>();

            this.BindingContext = new ViewModel.MainPageViewModel();
        }

        protected override void OnAppearing()
        {
            var current = this.BindingContext as ViewModel.MainPageViewModel;
            current.ReadData();

            base.OnAppearing();
        }

        private void ShowUsers(object sender, EventArgs e)
        {
            _navigationPage.PushAsync(typeof(Pages.ListUserPage), false);
        }

        private void ShowReports(object sender, EventArgs e)
        {
            _navigationPage.PushAsync(typeof(Pages.ReportsPage), false);
        }

        private void ShowPassclocks(object sender, EventArgs e)
        {
            _navigationPage.PushAsync(typeof(Pages.ListTokenPage), false);
        }

        private void ShowReleasedHours(object sender, EventArgs e)
        {
            _navigationPage.PushAsync(typeof(Pages.ListReleasedHourPage), false);
        }

        private async void Share(object sender, EventArgs e)
        {
            await CrossShare.Current.Share("Share this app with your friends.", "Estive Aqui");
        }

        private void ShowConfig(object sender, EventArgs e)
        {
            _navigationPage.PushAsync(typeof(Pages.ConfigPage), false);
        }
    }
}
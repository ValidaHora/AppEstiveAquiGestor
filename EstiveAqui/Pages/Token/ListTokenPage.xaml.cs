namespace EstiveAqui.Pages
{
    using EstiveAqui.Services.Abstract;
    using Xamarin.Forms;

    public partial class ListTokenPage : ContentPage
    {
        private readonly INavigationService _navigationPage;
        private bool isReady;

        public ListTokenPage()
        {
            InitializeComponent();

            _navigationPage = DependencyService.Get<INavigationService>();
        }

        protected override void OnAppearing()
        {
            this.BindingContext = new ViewModel.TokenViewModel();

            if (!isReady)
            {
                isReady = true;
                Lista.ItemTapped += ItemTapped;
            }

            base.OnAppearing();
        }

        private async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var current = this.BindingContext as ViewModel.TokenViewModel;
            await current.Read(e.Item);
        }

        private void ShowAdd(object sender, System.EventArgs e)
        {
            _navigationPage.PushAsync(typeof(Pages.CreateTokenPage), true);
        }
    }
}
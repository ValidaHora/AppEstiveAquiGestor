namespace EstiveAqui.Pages
{
	using Plugin.Share;
	using Repository.Abstract;
	using Services.Abstract;
	using System;
	using Xamarin.Forms;


	public partial class ListUserPage : ContentPage
    {
        private bool isReady;
		private readonly INavigationService _navigationPage;


        public ListUserPage()
        {
            InitializeComponent();

            _navigationPage = DependencyService.Get<INavigationService>();

        }

        private async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var current = this.BindingContext as ViewModel.UserViewModel;
            await current.Read(e.Item);
        }

        private async void ShowAdd(object sender, EventArgs e)
        {
            await _navigationPage.PushAsync(typeof(CreateUserPage), true);
        }


        protected override void OnAppearing()
        {
            this.BindingContext = new ViewModel.UserViewModel();

            if (!isReady)
            {
                isReady = true;
                Lista.ItemTapped += ItemTapped;
            }

            base.OnAppearing();
        }
    }
}
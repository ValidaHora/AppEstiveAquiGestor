namespace EstiveAqui.Services
{
    using Abstract;
    using System.Threading.Tasks;
    using System;
    using Xamarin.Forms;
    using Repository.Abstract;

    public class NavigationService : INavigationService
	{
		public async Task Logout()
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				App.Current.MainPage = new Pages.LoginPage();
				App.Current.Properties.Clear();
				App.Current.SavePropertiesAsync();
                var releaseHistoryRepository = DependencyService.Get<IReleasesHistoryRepository>();
                releaseHistoryRepository.DeleteAll();
            });
		}

		public async Task NavigateTo(Type type, object param)
		{
			var newInstance = Activator.CreateInstance(type, param);
			App.Current.MainPage = (Xamarin.Forms.Page)newInstance;
		}

		public async Task NavigateTo(Type type)
		{
			var newInstance = Activator.CreateInstance(type);
			App.Current.MainPage = (Xamarin.Forms.Page)newInstance;
		}

		public async Task ShowMainPage()
		{
			App.Current.MainPage = new Xamarin.Forms.NavigationPage(new Pages.MainPage());
		}

		public async Task ShowUnconfirmedPage(object param)
		{
			App.Current.MainPage = new Pages.TokenManagementPage();
		}

		public async Task PushModalAsync(Type type)
		{
			var newInstance = Activator.CreateInstance(type);
			await App.Current.MainPage.Navigation.PushModalAsync((Xamarin.Forms.Page)newInstance);
		}

		public async Task PopModalAsync()
		{
			await App.Current.MainPage.Navigation.PopModalAsync();
		}

		public async Task PopAsync()
		{
			await App.Current.MainPage.Navigation.PopAsync(true);
		}

		public async Task PushAsync(Type type, bool animated)
		{
			var newInstance = Activator.CreateInstance(type);
			await App.Current.MainPage.Navigation.PushAsync((Xamarin.Forms.Page)newInstance, animated);
		}

		public async Task PushAsync(Type type, bool animated, params object[] args)
		{
			var newInstance = Activator.CreateInstance(type, args);
			await App.Current.MainPage.Navigation.PushAsync((Xamarin.Forms.Page)newInstance, animated);
		}

		public async Task SignIn()
		{
			await App.Current.MainPage.Navigation.PushModalAsync(new Pages.SignInPage(), true);
		}
	}
}

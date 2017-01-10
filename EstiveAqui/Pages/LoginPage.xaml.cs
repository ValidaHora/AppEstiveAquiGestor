namespace EstiveAqui.Pages
{
	using Services.Abstract;
	using Xamarin.Forms;

	public partial class LoginPage : ContentPage
	{
		private readonly INavigationService _navigationPage;
		private readonly IMessageService _messageService;

		public LoginPage()
		{
			_navigationPage = DependencyService.Get<INavigationService>();
			_messageService = DependencyService.Get<IMessageService>();

			InitializeComponent();

			this.BindingContext = new ViewModel.LoginViewModel();
            
            EntryUsername.Completed += (s, e) => EntryPassword.Focus();
            EntryPassword.Completed += (s, e) => {
                var current = this.BindingContext as ViewModel.LoginViewModel;
                current.LoginCommand.Execute(null);
            };
            
        }

		private async void NavigateToSignIn(object sender, System.EventArgs e)
		{
			await _navigationPage.ShowMainPage();
		}

		private void ForgotPassword(object sender, System.EventArgs e)
		{
			var current = this.BindingContext as ViewModel.LoginViewModel;
			if (string.IsNullOrWhiteSpace(current.Username))
				_messageService.DisplayAlert("Favor inserir um e-mail válido para recuperação de senha.");
			else
				current.ForgotPassword.Execute(null);
		}
	}
}
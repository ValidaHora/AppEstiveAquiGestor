namespace EstiveAqui.Pages
{
	using EstiveAqui.Services.Abstract;
	using System.Linq;
	using Xamarin.Forms;

	public partial class SignInPage : ContentPage
	{
		private readonly IApiService _apiService;
		private readonly IMessageService _messageService;
		private readonly INavigationService _navigationService;

		public SignInPage()
		{
			_apiService = DependencyService.Get<IApiService>();
			_messageService = DependencyService.Get<IMessageService>();
			_navigationService = DependencyService.Get<INavigationService>();
			InitializeComponent();

			email.Completed += (s, e) => senha.Focus();
			senha.Completed += (s, e) => confirmarSenha.Focus();
			confirmarSenha.Completed += (s, e) =>
			{
				Registrar(s, e);
			};

		}

		private async void HaveAccount(object sender, System.EventArgs e)
		{
			await _navigationService.PopModalAsync();
		}

		private async void Registrar(object sender, System.EventArgs e)
		{
			if (email.Text == null)
			{
				await _messageService.DisplayAlert("Digite um e-mail válido!");
				return;
			}
			if (senha.Text != null && senha.Text.Equals(confirmarSenha.Text))
			{
				var result = await _apiService.CadastraComEmail(email.Text, senha.Text);
				if (result.ValidadoOk)
				{
					await _navigationService.PopModalAsync();
					await _messageService.DisplayAlert("Conta cadastrada com sucesso; um e-mail de confirmação foi enviado");
				}
				else if (result.Mensagens.Any(b => b.Codigo == "114"))
					await _messageService.DisplayAlert("E-mail já cadastrado.");
				else
					await _messageService.DisplayAlert("Ocorreu um erro, tente novamente mais tarde.");

			}
			else
				await _messageService.DisplayAlert("As senhas não coincidem.");
		}
	}
}

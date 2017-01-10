namespace EstiveAqui.Pages
{
    using Services.Abstract;
    using System.Linq;
    using Xamarin.Forms;

    public partial class TokenManagementPage : ContentPage
    {
        private readonly INavigationService _navigationService;
        private readonly IMessageService _messageService;
        private readonly IApiService _apiService;
        public TokenManagementPage()
        {
            InitializeComponent();

            _navigationService = DependencyService.Get<INavigationService>();
            _messageService = DependencyService.Get<IMessageService>();
            _apiService = DependencyService.Get<IApiService>();
        }

        private async void AddToken(object sender, System.EventArgs e)
        {
            var idApp = App.Current.Properties["IdApp"] as string;
            var data = await _apiService.LeDadosAppGestor(idApp, "0");
            if (data.ValidadoOk)
                await _navigationService.PushModalAsync(typeof(CreateTokenPage));
            else if(data.Mensagens.Any(b => b.Codigo == "116"))
            {
                await _messageService.DisplayAlert("Para continuar é preciso confirmar o endereço de e-mail.");
                var reply = await _messageService.DisplayConfirm("Deseja reenviar o e-mail de confirmação?");
                if (reply)
                {
                    var request = await _apiService.ReenviaConfirmacaoEmail(idApp);
                    if (request.ValidadoOk)
                    {
                        var email = App.Current.Properties["Email"] as string;
                        await _messageService.DisplayAlert($"Email enviado com sucesso para {email}.");
                    }
                    else
                        await _messageService.DisplayAlert("Ocorreu um erro, tente novamente mais tarde.");
                }
            }
            else
            {
                await _messageService.DisplayAlert("Ocorreu um erro interno.");
            }
        }

        private async void SignOut(object sender, System.EventArgs e)
        {
            if (await _messageService.DisplayConfirm("Tem certeza que deseja sair?"))
            {
                await _navigationService.Logout();
            }
        }
    }
}

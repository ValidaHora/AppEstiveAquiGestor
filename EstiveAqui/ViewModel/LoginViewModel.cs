namespace EstiveAqui.ViewModel
{
    using MvvmHelpers;
    using Services.Abstract;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            _apiService = DependencyService.Get<IApiService>();
            _messageService = DependencyService.Get<IMessageService>();
            _navigationPage = DependencyService.Get<INavigationService>();
        }

        #region Attributes
        private bool _isLoading;
        private string _username;
        private string _password;
        private ICommand _loginCommand;
        private ICommand _navigateToSignIn;
        private ICommand _forgotPassword;
        private readonly IApiService _apiService;
        private readonly IMessageService _messageService;
        private readonly INavigationService _navigationPage;
        #endregion

        #region Properties
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                SetProperty(ref _isLoading, value);
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                SetProperty(ref _username, value);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
            }
        }
        #endregion

        #region Pattern Command
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new Command(async () => await ExecuteLoginCommand()));
            }
        }
        public ICommand NavigateToSignIn
        {
            get
            {
                return _navigateToSignIn ?? (_navigateToSignIn = new Command(async () => await SignIn()));
            }
        }

        public ICommand ForgotPassword
        {
            get
            {
                return _forgotPassword ?? (_forgotPassword = new Command(async () => await ExecForgotPassword()));
            }
        }
        #endregion

        #region Business Requirement
        private async Task ExecuteLoginCommand()
        {
            if (string.IsNullOrWhiteSpace(this.Username) ||
                string.IsNullOrWhiteSpace(this.Password))
                await _messageService.DisplayAlert("Favor preencher o campo e-mail ou senha");
            else
            {
                this.IsLoading = true;
                var result = await _apiService.LoginComEmail(this.Username, this.Password);
                this.IsLoading = false;

                if (result.ValidadoOk)
                    await Login(result.Ig, result.Ev);
                else if (result.Mensagens.Any(b => b.Codigo == "111"))
                    await _messageService.DisplayAlert("Senha incorreta, tente novamente.");
                else if (result.Mensagens.Any(b => b.Codigo == "115"))
                    await _messageService.DisplayAlert("E-mail ou senha incorreta.");
                else
                    await _messageService.DisplayAlert("Desculpe-nos, aconteceu um erro.");
            }
        }

        private async Task SignIn()
        {
            await _navigationPage.PushModalAsync(typeof(Pages.SignInPage));
        }

        private async Task ExecForgotPassword()
        {
            if (!string.IsNullOrWhiteSpace(this.Username))
            {

                var res = await _messageService.DisplayConfirm("Deseja enviar um link ao seu email para criar uma nova senha?");
                if (res)
                {
                    var apiResult = await _apiService.RecuperaSenhaEmail(this.Username);
                    if (apiResult.ValidadoOk)
                    {
                        await _messageService.DisplayAlert("Você receberá um e-mail para resetar sua senha.");
                        await _navigationPage.NavigateTo(typeof(Pages.UpdatePasswordCodePage));
                    }
                    else
                    {
                        await _messageService.DisplayAlert("Ocorreu um erro ao enviar um e-mail.");
                    }
                }
            }
            else
            {
                await _messageService.DisplayAlert("Favor preencher o campo e-mail.");
            }
        }

        private async Task Login(string ig, bool isConfirmed)
        {
            App.Current.Properties.Add("Logged", true);
            App.Current.Properties.Add("IdApp", ig);
            App.Current.Properties.Add("Email", this.Username);
            await App.Current.SavePropertiesAsync();

            if (isConfirmed)
                await _navigationPage.ShowMainPage();
            else
                await _navigationPage.ShowUnconfirmedPage(isConfirmed);

            //await App.Current.SavePropertiesAsync();
        }
        #endregion
    }
}
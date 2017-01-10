namespace EstiveAqui.ViewModel
{
    using System.Threading.Tasks;
    using MvvmHelpers;
    using Services.Abstract;
    using Xamarin.Forms;
    using System.Linq;

    public class UpdatePasswordViewModel : BaseViewModel
    {
        public UpdatePasswordViewModel()
        {
            _apiService = DependencyService.Get<IApiService>();
            _messageService = DependencyService.Get<IMessageService>();
            _navigationPage = DependencyService.Get<INavigationService>();
        }

        #region Attributes
        private readonly IApiService _apiService;
        private readonly IMessageService _messageService;
        private readonly INavigationService _navigationPage;
        private System.Windows.Input.ICommand _updateCommand;

        #endregion

        #region Properties
        public string RecoverCode { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public System.Windows.Input.ICommand UpdateCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand = new Command(async () => await ExecuteUpdateCommand()));
            }
        }

        public System.Windows.Input.ICommand RegisterCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand = new Command(async () => await ExecuteRegisterCommand()));
            }
        }

        #endregion

        #region Command
        private async Task ExecuteUpdateCommand()
        {
            if (ReferenceEquals(this.Password, null) || ReferenceEquals(this.NewPassword, null))
            {
                await _messageService.DisplayAlert("Digite sua senha atual e sua nova senha antes de confirmar!");
                return;
            }
            if (this.NewPassword != null && this.NewPassword.Equals(this.ConfirmPassword))
            {
                var idApp = App.Current.Properties["IdApp"] as string;
                var res = await _apiService.AlteraSenhaEmail(idApp, this.Password, this.NewPassword);
                if (res.ValidadoOk)
                {
                    await _messageService.DisplayAlert("Senha alterada com sucesso.");
                    await _navigationPage.ShowMainPage();
                }
                else if (!res.ValidadoOk && res.Mensagens.Any(b => b.Codigo == "111"))
                    await _messageService.DisplayAlert("Senha incorreta. Por favor digite novamente a sua senha.");
                else if (!res.ValidadoOk && res.Mensagens.Any(b => b.Codigo == "112"))
                    await _messageService.DisplayAlert("A senha deve conter no mínimo 6 caracteres.");
                else
                    await _messageService.DisplayAlert("Ocorreu um erro, tente novamente mais tarde.");
            }
            else
                await _messageService.DisplayAlert("A nova senha e a confirmação não coincidem.");
        }

        private async Task ExecuteRegisterCommand()
        {
            if (this.Password == null)
            {
                await _messageService.DisplayAlert("Digite sua senha atual!");
                return;
            }
            if (this.NewPassword != null && this.NewPassword.Equals(this.ConfirmPassword))
            {
                var api = await _apiService.AlteraSenhaEmail2(this.NewPassword, this.RecoverCode.Replace("-", ""));
                if (api.ValidadoOk)
                {
                    await _messageService.DisplayAlert("Senha alterada com sucesso.");
                    await _navigationPage.ShowMainPage();
                }
                else if (!api.ValidadoOk && api.Mensagens.Any(b => b.Codigo == "111"))
                    await _messageService.DisplayAlert("Senha incorreta. Por favor digite novamente a sua senha.");
                else if (!api.ValidadoOk && api.Mensagens.Any(b => b.Codigo == "112"))
                    await _messageService.DisplayAlert("A senha deve conter no mínimo 6 caracteres.");
                else if (!api.ValidadoOk && api.Mensagens.Any(b => b.Codigo == "116"))
                    await _messageService.DisplayAlert("E-mail ainda não validado.");
                else
                    await _messageService.DisplayAlert("Ocorreu um erro, tente novamente mais tarde.");
            }
            else
                await _messageService.DisplayAlert("A nova senha esta diferente do campo confirmar senha");
        }
        #endregion
    }
}
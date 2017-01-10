namespace EstiveAqui.ViewModel
{
    using MvvmHelpers;
    using Repository.Abstract;
    using Services.Abstract;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ConfigViewModel : BaseViewModel
    {
        public ConfigViewModel()
        {
            _messageService = DependencyService.Get<IMessageService>();
            _navigationPage = DependencyService.Get<INavigationService>();
            _appUserRepository = DependencyService.Get<IAppUserRepository>();
            _passclockRepository = DependencyService.Get<IPassClockRepository>();
            _releasedHourRepository = DependencyService.Get<IReleasedHoursRepository>();
            _releasesHistoryRepository = DependencyService.Get<IReleasesHistoryRepository>();

            _items = new List<string>()
            {
                "Alterar Senha",
                "Sair"
            };
        }

        #region Attributes
        private ICommand _alterarSenhaCommand;
        private ICommand _logoutCommand;
        private readonly List<string> _items;
        private readonly IMessageService _messageService;
        private readonly INavigationService _navigationPage;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IPassClockRepository _passclockRepository;
        private readonly IReleasedHoursRepository _releasedHourRepository;
        private readonly IReleasesHistoryRepository _releasesHistoryRepository;

        #endregion

        #region Properties
        public List<string> Items
        {
            get { return _items; }
        }

        #endregion

        #region Pattern Command
        public ICommand AlterarSenhaCommand
        {
            get
            {
                return _alterarSenhaCommand ?? (_alterarSenhaCommand = new Command(async () => await AlterarSenha()));
            }
        }
        public ICommand LogoutCommand
        {
            get
            {
                return _logoutCommand ?? (_logoutCommand = new Command(async () => await Logout()));
            }
        }

        #endregion

        #region Business Requirement
        public async Task NavigateTo(object param)
        {
            var item = param as string;
            if (string.Equals(item, "sair", System.StringComparison.CurrentCultureIgnoreCase))
                await Logout();
            else
                await AlterarSenha();
        }

        private async Task AlterarSenha()
        {
            await _navigationPage.PushAsync(typeof(Pages.UpdatePasswordPage), false);
        }

        private async Task Logout()
        {
            var reply = await _messageService.DisplayConfirm("Tem certeza que deseja sair?");
            if (reply)
            {
                _appUserRepository.DeleteAll();
                _passclockRepository.DeleteAll();
                _releasedHourRepository.DeleteAll();
                _releasesHistoryRepository.DeleteAll();
                await _navigationPage.Logout();
            }
        }

        #endregion
    }
}
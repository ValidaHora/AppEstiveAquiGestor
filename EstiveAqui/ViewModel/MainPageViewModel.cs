namespace EstiveAqui.ViewModel
{
    using ApiSerialize;
    using MvvmHelpers;
    using Plugin.Share;
    using Repository.Abstract;
    using Services.Abstract;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            _apiService = DependencyService.Get<IApiService>();
            _messageService = DependencyService.Get<IMessageService>();
            _navigationService = DependencyService.Get<INavigationService>();
            _appUserRepository = DependencyService.Get<IAppUserRepository>();
            _passclockRepository = DependencyService.Get<IPassClockRepository>();
            _releasedHourRepository = DependencyService.Get<IReleasedHoursRepository>();
            _releasesHistoryRepository = DependencyService.Get<IReleasesHistoryRepository>();

            ReadData();
            ApiSerialize.EngineRequest.Interval(60000, ReadData, new System.Threading.CancellationToken(false));
        }

        #region Attributes
        private bool _isLoading;
        private int _countListReleaseHours;
        private ICommand _showUsersCommand;
        private ICommand _showReportsCommand;
        private ICommand _showPassclocksCommand;
        private ICommand _showReleasedHoursCommand;
        private ICommand _showUpdatePasswordCommand;
        private ICommand _showConfigCommand;
        private ICommand _shareCommand;
        private readonly IApiService _apiService;
        private readonly IMessageService _messageService;
        private readonly INavigationService _navigationService;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IPassClockRepository _passclockRepository;
        private readonly IReleasedHoursRepository _releasedHourRepository;
        private readonly IReleasesHistoryRepository _releasesHistoryRepository;

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

        public int CountListReleaseHours
        {
            get { return _countListReleaseHours; }
            set
            {
                SetProperty(ref _countListReleaseHours, value);
            }
        }

        #endregion

        #region Pattern Command
        public ICommand ShowUsersCommand
        {
            get
            {
                return _showUsersCommand ?? (_showUsersCommand = new Command(async () => await ShowUsers()));
            }
        }
        public ICommand ShowReportsCommand
        {
            get
            {
                return _showReportsCommand ?? (_showReportsCommand = new Command(async () => await ShowReports()));
            }
        }

        public ICommand ShowPassclocksCommand
        {
            get
            {
                return _showPassclocksCommand ?? (_showPassclocksCommand = new Command(async () => await ShowPassclocks()));
            }
        }

        public ICommand ShowReleasedHoursCommand
        {
            get
            {
                return _showReleasedHoursCommand ?? (_showReleasedHoursCommand = new Command(async () => await ShowReleasedHours()));
            }
        }

        public ICommand ShowUpdatePasswordCommand
        {
            get
            {
                return _showUpdatePasswordCommand ?? (_showUpdatePasswordCommand = new Command(async () => await ShowUpdatePassword()));
            }
        }

        public ICommand ShowConfigCommand
        {
            get
            {
                return _showConfigCommand ?? (_showConfigCommand = new Command(async () => await ShowConfig()));
            }
        }

        public ICommand ShareCommand
        {
            get
            {
                return _shareCommand ?? (_shareCommand = new Command(async () => await Share()));
            }
        }
        #endregion

        #region Business Requirement
        private async Task ShowUsers()
        {
            this.IsLoading = true;
            await _navigationService.PushAsync(typeof(Pages.ListUserPage), false);
            this.IsLoading = false;
        }

        private async Task ShowReports()
        {
            this.IsLoading = false;
            await _navigationService.PushAsync(typeof(Pages.ReportsPage), false);            
        }

        private async Task ShowPassclocks()
        {
            this.IsLoading = true;
            await _navigationService.PushAsync(typeof(Pages.ListTokenPage), false);
            this.IsLoading = false;
        }

        private async Task ShowReleasedHours()
        {
            this.IsLoading = true;
            await _navigationService.PushAsync(typeof(Pages.ListReleasedHourPage), false);
            this.IsLoading = false;
        }

        private async Task ShowConfig()
        {
            await _navigationService.PushAsync(typeof(Pages.ConfigPage), false);
        }

        private async Task ShowUpdatePassword()
        {
            await _navigationService.PushAsync(typeof(Pages.UpdatePasswordPage), true);
        }

        private async Task Share()
        {
            await CrossShare.Current.Share("Share this app with your friends.", "Estive Aqui");
        }

        public async void ReadData()
        {
            if (Application.Current.Properties.ContainsKey("Logged"))
            {
                var lastReleasedId = string.Empty;
                var idApp = App.Current.Properties["IdApp"] as string;
                var lastReleased = _releasesHistoryRepository.Find(a => a.Iu == idApp).FirstOrDefault();

                if (ReferenceEquals(lastReleased, null))
                    lastReleasedId = "0";
                else
                    lastReleasedId = lastReleased.Il;

                var data = await _apiService.LeDadosAppGestor(idApp, lastReleasedId);

                if (data.ValidadoOk)
                    SaveData(data);
                else if (data.Mensagens.Any(b => b.Codigo == "116"))
                    await _navigationService.ShowUnconfirmedPage(false);
                else
                    await _navigationService.Logout();
            }
        }

        private void SaveData(AppData data)
        {
            var idApp = App.Current.Properties["IdApp"] as string;

            foreach (var user in data.Uss)
            {
                var userDb = _appUserRepository.Find(a => a.Iu == user.Iu);
                var userDbFirst = userDb.FirstOrDefault();
                if (!ReferenceEquals(userDbFirst, null))
                {
                    userDbFirst.Au = user.Au;
                    userDbFirst.Ii = user.Ii;
                    userDbFirst.St = user.St;
                    userDbFirst.Xl = user.Xl;
                    userDbFirst.Ca = user.Ca;
                    _appUserRepository.Update(userDbFirst);
                }
                else
                    _appUserRepository.Save(user);
            }

            foreach (var passclock in data.Pcs)
            {
                var passclockDb = _passclockRepository.Find(a => a.Pc == passclock.Pc);
                var passclockFirst = passclockDb.FirstOrDefault();
                if (!ReferenceEquals(passclockFirst, null))
                {
                    passclockFirst.Ap = passclock.Ap;
                    passclockFirst.Cv = passclock.Cv;
                    passclockFirst.St = passclock.St;
                    _passclockRepository.Update(passclockFirst);
                }
                else
                    _passclockRepository.Save(passclock);
            }

            if (data.Lns.Any())
            {
                var lastReleasedDb = _releasesHistoryRepository.Find(a=> a.Iu == idApp).FirstOrDefault();
                var lastReleased = data.Lns.OrderByDescending(b => System.Convert.ToInt32(b.Il)).First();

                if (ReferenceEquals(lastReleasedDb, null))
                    _releasesHistoryRepository.Save(new ReleasesHistory { Il = lastReleased.Il, Iu = idApp });
                else
                {
                    lastReleasedDb.Il = lastReleased.Il;
                    _releasesHistoryRepository.Update(lastReleasedDb);
                }
            }

            this.CountListReleaseHours = _releasedHourRepository.CountToday();
            _releasedHourRepository.ExcludeLastTwoMonths();
            
            foreach (var releasedHour in data.Lns)
            {
                var releasedHourDb = _releasedHourRepository.Find(a => a.Il == releasedHour.Il);
                var releasedHourFirst = releasedHourDb.FirstOrDefault();
                if (!ReferenceEquals(releasedHourFirst, null))
                {
                    releasedHourFirst.Ap = releasedHour.Ap;
                    releasedHourFirst.Au = releasedHour.Au;
                    releasedHourFirst.Cd = releasedHour.Cd;
                    releasedHourFirst.Di = releasedHour.Di;
                    releasedHourFirst.Hd = releasedHour.Hd;
                    releasedHourFirst.He = releasedHour.He;
                    releasedHourFirst.Hl = releasedHour.Hl;
                    releasedHourFirst.Hdx = releasedHour.Hdx;
                    releasedHourFirst.Hex = releasedHour.Hex;
                    releasedHourFirst.Hlx = releasedHour.Hlx;
                    releasedHourFirst.La = releasedHour.La;
                    releasedHourFirst.Lo = releasedHour.Lo;
                    releasedHourFirst.Nt = releasedHour.Nt;
                    releasedHourFirst.Pc = releasedHour.Pc;
                    releasedHourFirst.St = releasedHour.St;
                    releasedHourFirst.Tz = releasedHour.Tz;
                    releasedHourFirst.Il = releasedHour.Il;

                    _releasedHourRepository.Update(releasedHourFirst);
                }
                else
                    _releasedHourRepository.Save(releasedHour);
            }
        }
        #endregion
    }
}
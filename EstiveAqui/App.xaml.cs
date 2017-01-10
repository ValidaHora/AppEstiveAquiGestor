namespace EstiveAqui
{
    using Repository.Abstract;
    using Services.Abstract;
    using Xamarin.Forms;

    public partial class App : Application
    {
        public bool CanRun { get; set; }
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;

        public App()
        {
            RegisterDependencies();
            InitializeComponent();

            _apiService = DependencyService.Get<IApiService>();
            _navigationService = DependencyService.Get<INavigationService>();

            if (this.Properties.ContainsKey("Logged"))
            {
                var logged = this.Properties["Logged"] as bool?;
                if (logged.HasValue && logged.Value)
                    MainPage = new NavigationPage(new Pages.MainPage());
                else
                    MainPage = new Pages.LoginPage();
            }
            else
                MainPage = new Pages.LoginPage();

            CanRun = true;
        }

        protected override void OnStart()
        {
            CanRun = true;
        }

        protected override void OnSleep()
        {
            CanRun = false;
        }

        protected override void OnResume()
        {
            CanRun = true;
        }

        private static void RegisterDependencies()
        {
            DependencyService.Register<IApiService, Services.ApiService>();
            DependencyService.Register<IMessageService, Services.MessageService>();
            DependencyService.Register<INavigationService, Services.NavigationService>();

            DependencyService.Register<IAppUserRepository, Repository.AppUserRepository>();
            DependencyService.Register<IPassClockRepository, Repository.PassClockRepository>();

            DependencyService.Register<IReportRepository, Repository.ReportRepository>();
            DependencyService.Register<IReleasedHoursRepository, Repository.ReleasedHoursRepository>();
            DependencyService.Register<IReleasesHistoryRepository, Repository.ReleasesHistoryRepository>();
        }
    }
}
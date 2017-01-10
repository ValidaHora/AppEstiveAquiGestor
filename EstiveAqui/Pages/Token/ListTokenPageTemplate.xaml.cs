namespace EstiveAqui.Pages
{
    using Repository.Abstract;
    using Services.Abstract;
    using System;
    using System.Linq;
    using Xamarin.Forms;

    public partial class ListTokenPageTemplate : ContentView
    {
        private readonly IApiService _apiService;
        private readonly IMessageService _messageService;
        private readonly IPassClockRepository _passClockRepository;

        public ListTokenPageTemplate()
        {
            InitializeComponent();
            _apiService = DependencyService.Get<IApiService>();
            _messageService = DependencyService.Get<IMessageService>();
            _passClockRepository = DependencyService.Get<IPassClockRepository>();
        }

        private async void Enable(object sender, EventArgs e)
        {
            var img = ((Image)sender);

            var passclock = img.BindingContext as Model.TokenModel;

            var reply = await _messageService.DisplayConfirm($"Tem certeza que deseja ativar o PassClock {passclock.Alias}?");

            if (reply)
            {
                var idApp = App.Current.Properties["IdApp"] as string;
                var resultApi = await _apiService.HabilitaPassclock(idApp, passclock.BarCode);
                if (resultApi.ValidadoOk)
                {
                    var passclockDb = _passClockRepository.Find(b => b.Pc == passclock.BarCode).First();
                    passclockDb.St = "0";
                    _passClockRepository.Update(passclockDb);
                    btnEnable.IsVisible = true;
                    img.IsVisible = false;
                }
            }
        }

        private async void Disable(object sender, EventArgs e)
        {
            var img = ((Image)sender);

            var passclock = img.BindingContext as Model.TokenModel;

            var reply = await _messageService.DisplayConfirm($"Tem certeza que deseja desativar o PassClock {passclock.Alias}?");

            if (reply)
            {
                var idApp = App.Current.Properties["IdApp"] as string;
                var resultApi = await _apiService.DesabilitaPassclock(idApp, passclock.BarCode);
                if (resultApi.ValidadoOk)
                {
                    var passclockDb = _passClockRepository.Find(b => b.Pc == passclock.BarCode).First();
                    passclockDb.St = "1";
                    _passClockRepository.Update(passclockDb);
                    btnDisable.IsVisible = true;
                    img.IsVisible = false;
                }
            }
        }
    }
}

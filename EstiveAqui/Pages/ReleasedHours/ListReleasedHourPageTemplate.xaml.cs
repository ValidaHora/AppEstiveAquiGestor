namespace EstiveAqui.Pages
{
    using Repository.Abstract;
    using Services.Abstract;
    using System;
    using System.Linq;
    using Xamarin.Forms;

    public partial class ListReleasedHourPageTemplate : ContentView
    {
        private readonly IApiService _apiService;
        private readonly IMessageService _messageService;
        private readonly IReleasedHoursRepository _releasedHoursRepository;

        public ListReleasedHourPageTemplate()
        {
            InitializeComponent();

            _apiService = DependencyService.Get<IApiService>();
            _messageService = DependencyService.Get<IMessageService>();
            _releasedHoursRepository = DependencyService.Get<IReleasedHoursRepository>();
        }

        private async void Enable(object sender, EventArgs e)
        {
            var img = ((Image)sender);

            var releasedHour = img.BindingContext as Model.ReleasedHourModel;

            var reply = await _messageService.DisplayConfirm($"Tem certeza que deseja ativar o lançamento {releasedHour.DataHoraLancada}?");

            if (reply)
            {
                var idApp = App.Current.Properties["IdApp"] as string;
                var resultApi = await _apiService.HabilitaHora(idApp, releasedHour.IdLancamento);
                if (resultApi.ValidadoOk)
                {
                    var releasedHourDb = _releasedHoursRepository.Find(b => b.Il == releasedHour.IdLancamento).First();
                    releasedHourDb.St = "0";
                    _releasedHoursRepository.Update(releasedHourDb);
                    btnEnable.IsVisible = true;
                    img.IsVisible = false;
                }
            }
        }

        private async void Disable(object sender, EventArgs e)
        {
            var img = ((Image)sender);

            var releasedHour = img.BindingContext as Model.ReleasedHourModel;

            var reply = await _messageService.DisplayConfirm($"Tem certeza que deseja desativar o lançamento { releasedHour.DataHoraLancada }?");

            if (reply)
            {
                var idApp = App.Current.Properties["IdApp"] as string;
                var resultApi = await _apiService.DesabilitaHora(idApp, releasedHour.IdLancamento);
                if (resultApi.ValidadoOk)
                {
                    var releasedHourDb = _releasedHoursRepository.Find(b => b.Il == releasedHour.IdLancamento).First();
                    releasedHourDb.St = "1";
                    _releasedHoursRepository.Update(releasedHourDb);
                    btnDisable.IsVisible = true;
                    img.IsVisible = false;
                }
            }
        }
    }
}
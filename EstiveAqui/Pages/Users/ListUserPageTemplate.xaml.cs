namespace EstiveAqui.Pages
{
    using Plugin.Share;
    using Repository.Abstract;
    using Services.Abstract;
    using System;
    using System.Linq;
    using Xamarin.Forms;

    public partial class ListUserPageTemplate : ContentView
    {
        private readonly IApiService _apiService;
        private readonly IMessageService _messageService;
        private readonly IAppUserRepository _appUserRepository;

        public ListUserPageTemplate()
        {
            InitializeComponent();
            _apiService = DependencyService.Get<IApiService>();
            _messageService = DependencyService.Get<IMessageService>();
            _appUserRepository = DependencyService.Get<IAppUserRepository>();
        }

        private void Share(object sender, EventArgs e)
        {
            var img = ((Image)sender);

            var user = img.BindingContext as Model.UserModel;

            CrossShare.Current.Share($"Para ativar o seu aplicativo EstiveAqui clique no link: http://www.estiveaqui.com.br/ativacao.html?code={user.ActivateCode}", "Estive aqui");
        }

        private async void Disable(object sender, EventArgs e)
        {
            var img = ((Image)sender);

            var user = img.BindingContext as Model.UserModel;

            var reply = await _messageService.DisplayConfirm($"Tem certeza que deseja desativar o usuário {user.Username}?");

            if (reply)
            {
                var idApp = App.Current.Properties["IdApp"] as string;
                var resultApi = await _apiService.DesabilitaAppUsuario(idApp, user.IdUser);
                if (resultApi.ValidadoOk)
                {
                    var userDb = _appUserRepository.Find(b => b.Iu == user.IdUser).First();
                    user.ActivateCode = userDb.Ca;
                    userDb.St = "1";
                    _appUserRepository.Update(userDb);
                    btnShare.IsVisible = true;
                    img.IsVisible = false;
                }
            }
        }
    }
}
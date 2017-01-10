namespace EstiveAqui.ViewModel
{
    using Plugin.Share;
    using Repository.Abstract;
    using Services.Abstract;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class UserViewModel : Model.UserModel
    {
        #region Constructor
        public UserViewModel(Model.UserModel model)
        {
            _appUserRepository = DependencyService.Get<IAppUserRepository>();
            _navigationService = DependencyService.Get<INavigationService>();
            _messageService = DependencyService.Get<IMessageService>();
            _apiService = DependencyService.Get<IApiService>();

            this.IdUser = model.IdUser;
            this.Username = model.Username;
            this.IntegrationId = model.IntegrationId;
            this.MaxLancamentoDia = model.MaxLancamentoDia;
        }

        public UserViewModel()
        {
            _appUserRepository = DependencyService.Get<IAppUserRepository>();
            _navigationService = DependencyService.Get<INavigationService>();
            _messageService = DependencyService.Get<IMessageService>();
            _apiService = DependencyService.Get<IApiService>();
            _idApp = App.Current.Properties["IdApp"] as string;

            BuildItems();
        }

        private void BuildItems()
        {
            items = new ObservableCollection<Model.UserModel>();

            var users = _appUserRepository.Find().ToList();

            if (!ReferenceEquals(users, null))
                Items = new ObservableCollection<Model.UserModel>(users.Select(b => new Model.UserModel
                {
                    Username = b.Au,
                    IdUser = b.Iu,
                    ActivateCode = b.Ca,
                    IntegrationId = b.Ii,
                    MaxLancamentoDia = string.IsNullOrWhiteSpace(b.Xl) ? 0 : Convert.ToInt32(b.Xl),
                    Active = b.St.Equals("0") ? true : false
                }));

        }
        #endregion

        #region Attributes
        private static ObservableCollection<Model.UserModel> items;
        private readonly IAppUserRepository _appUserRepository;
        private readonly INavigationService _navigationService;
        private readonly IMessageService _messageService;
        private readonly IApiService _apiService;
        private static string _idApp;
        private ICommand createCommand;
        private ICommand readCommand;
        private ICommand updateCommand;
        private ICommand deleteCommand;
        private ICommand shareCommand;
        private ICommand increaseCommand;
        private ICommand decreaseCommand;
        #endregion

        #region Properties
        public ObservableCollection<Model.UserModel> Items
        {
            get { return items; }
            set
            {
                SetProperty(ref items, value);
            }
        }
        #endregion

        #region Command
        public ICommand CreateCommand
        {
            get
            {
                return createCommand ?? (createCommand = new Command(async () => await Create()));
            }
        }
        public ICommand ReadCommand
        {
            get
            {
                return readCommand ?? (readCommand = new Command(async (parameter) => await Read(parameter)));
            }
        }
        public ICommand UpdateCommand
        {
            get
            {
                return updateCommand ?? (updateCommand = new Command(async () => await Update()));
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new Command(async (parameter) => await Delete(parameter)));
            }
        }
        public ICommand ShareCommand
        {
            get
            {
                return shareCommand ?? (shareCommand = new Command(async (parameter) => await Share(parameter)));
            }
        }
        public ICommand IncreaseCommand
        {
            get
            {
                return increaseCommand ?? (increaseCommand = new Command(async () => await Increase()));
            }
        }
        public ICommand DecreaseCommand
        {
            get
            {
                return decreaseCommand ?? (decreaseCommand = new Command(async () => await Decrease()));
            }
        }
        #endregion

        #region Business Requirement - CRUD
        public async Task Create()
        {
            if (string.IsNullOrWhiteSpace(this.Username))
                await _messageService.DisplayAlert("Por favor, preencha o campo usuário.");
            else
            {
                var resultApi = await _apiService.CadastraAppUsuario(_idApp, this.Username, this.MaxLancamentoDia.ToString(), this.IntegrationId);
                if (resultApi.ValidadoOk)
                {
                    var IsShared = await _messageService.DisplayConfirm("Deseja enviar o convite para o usuário criado?");

                    if (IsShared)
                        await CrossShare.Current.Share($"Olá! Utilize o código {resultApi.Us.Ca}, para acessar o Estive Aqui.", "Estive Aqui");

                    _appUserRepository.Save(new ApiSerialize.AppUser
                    {
                        Au = resultApi.Us.Au,
                        Ca = resultApi.Us.Ca,
                        Ii = resultApi.Us.Ii,
                        Iu = resultApi.Us.Iu,
                        St = resultApi.Us.St,
                        Xl = resultApi.Us.Xl
                    });

                    await _navigationService.PopAsync();
                }
                else
                {
                    await _messageService.DisplayAlert("Ocorreu um erro, tente novamente mais tarde.");
                }
            }
        }
        public async Task Read(object param)
        {
            var selected = param as Model.UserModel;
            var item = items.FirstOrDefault(b => b == selected);
            await _navigationService.PushAsync(typeof(Pages.ReadUserPage), true, item);
        }
        public async Task Update()
        {
            var resultApi = await _apiService.AtualizaAppUsuario(_idApp, this.IdUser, this.Username, this.MaxLancamentoDia.ToString(), this.IntegrationId);
            if (resultApi.ValidadoOk)
            {
                var itemDb = _appUserRepository.Find(b => b.Iu == this.IdUser).FirstOrDefault();
                if (!ReferenceEquals(itemDb, null))
                {
                    itemDb.Au = this.Username;
                    itemDb.Ii = this.IntegrationId;
                    itemDb.Xl = this.MaxLancamentoDia.ToString();
                    _appUserRepository.Update(itemDb);
                }
                await _messageService.DisplayAlert("Operação concluída com sucesso.");
                await _navigationService.PopAsync();
            }
            else
            {
                await _messageService.DisplayAlert("Ocorreu um erro, tente novamente mais tarde.");
            }
        }

        public async Task Delete(object parameter)
        {
            var item = items.First(a => a.IdUser == this.IdUser);
            items.Remove(item);
            var resultApi = await _apiService.DesabilitaAppUsuario(_idApp, item.IdUser);
            if (resultApi.ValidadoOk)
            {
                await _messageService.DisplayAlert("Operação concluída com sucesso.");
                await _navigationService.PopAsync();
            }
            else
            {
                await _messageService.DisplayAlert("Ocorreu um erro, tente novamente mais tarde.");
            }
        }

        public async Task Share(object parameter)
        {
            var message = parameter as string;
            await CrossShare.Current.Share("Estive aqui", message);
        }

        public async Task Increase()
        {
            this.MaxLancamentoDia++;
        }

        public async Task Decrease()
        {
            if (this.MaxLancamentoDia > 0)
                this.MaxLancamentoDia--;
        }
        #endregion
    }
}
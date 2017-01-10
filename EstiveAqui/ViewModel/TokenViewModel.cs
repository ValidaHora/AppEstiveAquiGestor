namespace EstiveAqui.ViewModel
{
    using Repository.Abstract;
    using Services.Abstract;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class TokenViewModel : Model.TokenModel, IBaseViewModel<Model.TokenModel>
    {
        #region Constructor
        public TokenViewModel(Model.TokenModel model)
        {
            _passclockRepository = DependencyService.Get<IPassClockRepository>();
            _navigationService = DependencyService.Get<INavigationService>();
            _messageService = DependencyService.Get<IMessageService>();
            _apiService = DependencyService.Get<IApiService>();

            if (ReferenceEquals(items, null))
                items = new ObservableCollection<Model.TokenModel>();

            this.IdToken = model.IdToken;
            this.Alias = model.Alias;
            this.Value = model.Value;
            this.BarCode = model.BarCode;
        }

        public TokenViewModel()
        {
            _passclockRepository = DependencyService.Get<IPassClockRepository>();
            _navigationService = DependencyService.Get<INavigationService>();
            _messageService = DependencyService.Get<IMessageService>();
            _apiService = DependencyService.Get<IApiService>();
            BuildItems();
        }

        private async void BuildItems()
        {
            if (ReferenceEquals(items, null))
                items = new ObservableCollection<Model.TokenModel>();

            var passclockList = _passclockRepository.Find();

            if (!ReferenceEquals(passclockList, null))
                Items = new ObservableCollection<Model.TokenModel>(passclockList.Select(p => new Model.TokenModel
                {
                    Alias = p.Ap,
                    IdToken = p.Pc,
                    BarCode = p.Pc,
                    Active = p.St.Equals("0") ? true : false
                }));
        }
        #endregion

        #region Attributes
        private static ObservableCollection<Model.TokenModel> items;
        private readonly IPassClockRepository _passclockRepository;
        private readonly INavigationService _navigationService;
        private readonly IMessageService _messageService;
        private readonly IApiService _apiService;
        private ICommand createCommand;
        private ICommand readCommand;
        private ICommand updateCommand;
        private ICommand deleteCommand;
        #endregion

        #region Properties
        public ObservableCollection<Model.TokenModel> Items
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

        #endregion

        #region Business Requirement - CRUD
        public async Task Create()
        {
            if (string.IsNullOrWhiteSpace(this.Value) ||
                string.IsNullOrWhiteSpace(this.BarCode) ||
                string.IsNullOrWhiteSpace(this.Alias))
            {
                await _messageService.DisplayAlert("Existem campos que não foram preenchidos.");
            }
            else
            {
                var idApp = App.Current.Properties["IdApp"] as string;

                var tz = (System.DateTime.UtcNow - System.DateTime.Now.ToLocalTime()).Hours + 1;
                var horaEnviada = System.DateTime.Now.AddHours(tz).ToString("yyyyMMddHHmmss");
                var res = await _apiService.CadastraPassclock("EstiveAqui", "Teste", this.BarCode, this.Value, horaEnviada, System.DateTime.Now.ToString("yyyyMMddHHmmss"), "0", "0");
                if (res.ValidadoOk)
                {
                    var res2 = await _apiService.CadastraPassclock2(idApp, this.BarCode, this.Value, this.Alias, horaEnviada, res.HashCode);
                    if (res2.ValidadoOk)
                    {
                        _passclockRepository.Save(new ApiSerialize.PassClock
                        {
                            Ap = res2.Pco.Ap,
                            Cv = res2.Pco.Cv,
                            Pc = res2.Pco.Pc,
                            St = res2.Pco.St
                        });
                        await _messageService.DisplayAlert("PassClock cadastrado com sucesso.");
                        await _navigationService.PopAsync();
                    }
                    else
                    {
                        await _messageService.DisplayAlert("Ocorreu um erro ao registrar seu Passclock.");
                    }
                }
                else
                {
                    await _messageService.DisplayAlert("Código inválido!");
                }
            }
        }
        public async Task Read(object param)
        {
            var selected = param as Model.TokenModel;
            var item = items.FirstOrDefault(a => a == selected);
            await _navigationService.PushAsync(typeof(Pages.ReadTokenPage), true, item);
        }
        public async Task Update()
        {
            var idApp = App.Current.Properties["IdApp"] as string;
            var resultApi = await _apiService.AtualizaPassclock(idApp, this.IdToken, this.Alias);
            if (resultApi.ValidadoOk)
            {
                var itemDb = _passclockRepository.Find(b => b.Pc == this.BarCode).FirstOrDefault();
                if (!ReferenceEquals(itemDb, null))
                {
                    itemDb.Ap = this.Alias;
                    _passclockRepository.Update(itemDb);
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
            var item = items.First(a => a.IdToken == this.IdToken);
            items.Remove(item);
            await _navigationService.PopAsync();
        }
        #endregion
    }
}
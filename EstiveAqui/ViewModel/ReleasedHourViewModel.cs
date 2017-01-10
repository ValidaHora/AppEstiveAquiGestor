namespace EstiveAqui.ViewModel
{
    using EstiveAqui.Services.Abstract;
    using Repository.Abstract;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ReleasedHourViewModel : Model.ReleasedHourModel, IBaseViewModel<Model.ReleasedHourModel>
    {
        #region ctor
        public ReleasedHourViewModel(Model.ReleasedHourModel model)
        {
            _apiService = DependencyService.Get<IApiService>();
            _messageService = DependencyService.Get<IMessageService>();
            _navigationService = DependencyService.Get<INavigationService>();

            this.IdLancamento = model.IdLancamento;
            this.ApelidoUsuario = model.ApelidoUsuario;
            this.ApelidoPassclock = model.ApelidoPassclock;
            this.IdPassclock = model.IdPassclock;
            this.NumeroPassclock = model.NumeroPassclock;
            this.CodigoLancado = model.CodigoLancado;
            this.IsManual = model.IsManual;
            this.DataHoraDigitada = model.DataHoraDigitada;
            this.DataHoraLancada = model.DataHoraLancada;
            this.DataHoraEnviada = model.DataHoraEnviada;
            this.Timezone = model.Timezone;
            this.Active = model.Active;
            this.MsgActive = model.MsgActive;
            this.Nota = model.Nota;
            this.Latitude = model.Latitude;
            this.Longitude = model.Longitude;
        }

        public ReleasedHourViewModel()
        {
            _apiService = DependencyService.Get<IApiService>();
            _messageService = DependencyService.Get<IMessageService>();
            _navigationService = DependencyService.Get<INavigationService>();
            _appUserRepository = DependencyService.Get<IAppUserRepository>();
            _passClockRepository = DependencyService.Get<IPassClockRepository>();
            _releasedHoursRepository = DependencyService.Get<IReleasedHoursRepository>();

            UserItems = new ObservableCollection<Model.UserModel>();
            Items = new ObservableCollection<Model.ReleasedHourModel>();
            PassclockItems = new ObservableCollection<Model.TokenModel>();

            BuildItems();
        }

        private void BuildItems()
        {
            realItems = GetReleasedHourListFromDb();

            if (!ReferenceEquals(realItems, null))
            {
                Items = new ObservableCollection<Model.ReleasedHourModel>(realItems);

                var appUserList = _appUserRepository.Find();

                if (!ReferenceEquals(appUserList, null))
                {
                    UserItems = new ObservableCollection<Model.UserModel>(appUserList.OrderBy(b => b.Au).Select(b => new Model.UserModel
                    {
                        Username = b.Au,
                        IdUser = b.Iu
                    }));
                }

                var passclockList = _passClockRepository.Find();

                if (!ReferenceEquals(passclockList, null))
                {
                    PassclockItems = new ObservableCollection<Model.TokenModel>(passclockList.OrderBy(b => b.Ap).Select(b => new Model.TokenModel
                    {
                        Alias = b.Ap,
                        BarCode = b.Pc,
                        IdToken = b.Pc
                    }));
                }
            }
        }

        private List<Model.ReleasedHourModel> GetReleasedHourListFromDb()
        {
            var releasedHours = _releasedHoursRepository.Find();
            var list = new List<Model.ReleasedHourModel>();
            foreach (var l in releasedHours)
            {
                var item = new Model.ReleasedHourModel();
                item.IdLancamento = l.Il;
                item.ApelidoUsuario = l.Au;
                item.IdentificacaoDispositivo = l.Di;
                item.ApelidoPassclock = l.Ap;
                item.NumeroPassclock = l.Pc;
                item.Nota = l.Nt;
                item.Active = l.St.Equals("0") ? true : false;
                item.MsgActive = l.St.Equals("0") ? "ATIVO" : "INATIVO";
                item.Latitude = l.La;
                item.Longitude = l.Lo;
                item.DataHoraDigitada = ToDate(l.Hdx);
                item.DataHoraEnviada = ToDate(l.Hex);
                item.DataHoraLancada = ToDate(l.Hlx);
                item.CodigoLancado = string.IsNullOrWhiteSpace(l.Cd) ? string.Empty : l.Cd;
                list.Add(item);
            }

            return list.OrderByDescending(a => a.DataHoraLancada).ToList();
        }

        private DateTime ToDate(string dt)
        {
            if (string.IsNullOrWhiteSpace(dt))
                return DateTime.Now;

            var year = Convert.ToInt32(dt.Substring(0, 4));
            var month = Convert.ToInt32(dt.Substring(4, 2));
            var day = Convert.ToInt32(dt.Substring(6, 2));
            var hour = Convert.ToInt32(dt.Substring(8, 2));
            var minute = Convert.ToInt32(dt.Substring(10, 2));

            return new DateTime(year, month, day, hour, minute, 0);
        }
        #endregion

        #region Attributes
        private bool _isLoading;
        private bool _isShowMap;
        private bool _isShowMsgError;
        private string _msgError;
        private ICommand createCommand;
        private ICommand readCommand;
        private ICommand updateCommand;
        private ICommand deleteCommand;
        private readonly IApiService _apiService;
        private readonly IMessageService _messageService;
        private readonly INavigationService _navigationService;
        private static List<Model.ReleasedHourModel> realItems;
        private static ObservableCollection<Model.UserModel> userItems;
        private static ObservableCollection<Model.ReleasedHourModel> items;
        private static ObservableCollection<Model.TokenModel> passclockItems;

        private readonly IAppUserRepository _appUserRepository;
        private readonly IPassClockRepository _passClockRepository;
        private readonly IReleasedHoursRepository _releasedHoursRepository;
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

        public bool IsShowMap
        {
            get { return _isShowMap; }
            set
            {
                SetProperty(ref _isShowMap, value);
            }
        }

        public bool IsShowMsgError
        {
            get { return _isShowMsgError; }
            set
            {
                SetProperty(ref _isShowMsgError, value);
            }
        }

        public string MsgError
        {
            get { return _msgError; }
            set
            {
                SetProperty(ref _msgError, value);
            }
        }

        public ObservableCollection<Model.ReleasedHourModel> Items
        {
            get { return items; }
            set
            {
                SetProperty(ref items, value);
            }
        }

        public ObservableCollection<Model.UserModel> UserItems
        {
            get { return userItems; }
            set
            {
                SetProperty(ref userItems, value);
            }
        }

        public ObservableCollection<Model.TokenModel> PassclockItems
        {
            get { return passclockItems; }
            set
            {
                SetProperty(ref passclockItems, value);
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
            var idApp = App.Current.Properties["IdApp"] as string;

            var selectedUser = userItems[this.ApelidoUsuarioIndex];

            var time = this.TimeHoraDigitada;

            string formattedTimeSpan = string.Format("{0:D2}{1:D2}", time.Hours, time.Minutes);

            var dateHourTyped = this.DataHoraDigitada.ToString("yyyyMMdd") + formattedTimeSpan;

            var res = await _apiService.LancaHoraManual(idApp, selectedUser.IdUser, dateHourTyped, this.Nota);
            if (res.ValidadoOk)
            {
                _releasedHoursRepository.Save(new ApiSerialize.ReleasedHours
                {
                    Ap = res.Ln.Ap,
                    Au = res.Ln.Au,
                    Cd = string.IsNullOrWhiteSpace(res.Ln.Cd) ? "" : res.Ln.Cd,
                    Di = res.Ln.Di,
                    Hd = string.IsNullOrWhiteSpace(res.Ln.Hd) ? "" : res.Ln.Hd,
                    He = string.IsNullOrWhiteSpace(res.Ln.He) ? "" : res.Ln.He,
                    Hl = string.IsNullOrWhiteSpace(res.Ln.Hl) ? "" : res.Ln.Hl,
                    Hlx = res.Ln.Hlx,
                    Hex = res.Ln.Hex,
                    Hdx = res.Ln.Hdx,
                    Il = res.Ln.Il,
                    La = res.Ln.La,
                    Lo = res.Ln.Lo,
                    Nt = res.Ln.Nt,
                    Pc = res.Ln.Pc,
                    St = res.Ln.St,
                    Tz = res.Ln.Tz
                });
                await _messageService.DisplayAlert("Hora manual lançada com sucesso.");
                await _navigationService.PopAsync();
            }
            else
                await _messageService.DisplayAlert("Ocorreu um erro ao lançar a hora manual.");
        }

        public async Task Read(object param)
        {
            var selected = param as Model.ReleasedHourModel;
            var item = items.FirstOrDefault(a => a == selected);
            await _navigationService.PushAsync(typeof(Pages.ReadReleasedHourPage), true, item);
        }

        public async Task Update()
        {
            var item = items.First(a => a.IdLancamento == this.IdLancamento);

            this.IdLancamento = item.IdLancamento;
            this.ApelidoUsuario = item.ApelidoUsuario;
            this.ApelidoPassclock = item.ApelidoPassclock;
            this.IdPassclock = item.IdPassclock;
            this.NumeroPassclock = item.NumeroPassclock;
            this.CodigoLancado = item.CodigoLancado;
            this.IsManual = item.IsManual;
            this.DataHoraDigitada = item.DataHoraDigitada;
            this.DataHoraLancada = item.DataHoraLancada;
            this.DataHoraEnviada = item.DataHoraEnviada;
            this.Timezone = item.Timezone;
            this.Active = item.Active;
            this.MsgActive = item.MsgActive;
            this.Nota = item.Nota;
            this.Latitude = item.Latitude;
            this.Longitude = item.Longitude;
            await _navigationService.PopAsync();
        }

        public async Task Delete(object parameter)
        {
            var item = items.First(a => a.IdLancamento == this.IdLancamento);
            items.Remove(item);
            await _navigationService.PopAsync();
        }

        public void FilterReleasedHours(string username, string passclock, int indexPeriodo)
        {
            var date = GetStartAndEndDate(indexPeriodo);

            //Expression<Func<Model.ReleasedHourModel, bool>> where = a => a.DataHoraLancada.Date <= date["End"];
            //Expression<Func<Model.ReleasedHourModel, bool>> filterUsername = a => a.ApelidoUsuario == username;
            //Expression<Func<Model.ReleasedHourModel, bool>> filterPassclock = a => a.ApelidoPassclock == passclock;
            realItems = GetReleasedHourListFromDb();

            var filteredItems = realItems;

            if (indexPeriodo > 0)
                filteredItems = filteredItems
                    .Where(a => a.DataHoraLancada.Date >= date["Start"] && a.DataHoraLancada.Date <= date["End"])
                    .ToList();

            if (!string.IsNullOrWhiteSpace(username) && !username.ToUpper().Equals("TODOS"))
                filteredItems = filteredItems.Where(a => a.ApelidoUsuario.ToUpper() == username.ToUpper()).ToList();

            if (!string.IsNullOrWhiteSpace(passclock) && !passclock.ToUpper().Equals("TODOS"))
                filteredItems = filteredItems.Where(a => a.ApelidoPassclock.ToUpper() == passclock.ToUpper()).ToList();

            //var filteredItems = realItems.Where(where.Compile()).ToList();
            Items = new ObservableCollection<Model.ReleasedHourModel>(filteredItems);
        }

        public static Dictionary<string, DateTime> GetStartAndEndDate(int indexPeriodo)
        {
            var date = DateTime.Now.Date;
            var dateStart = date;
            var dateEnd = dateStart;

            switch (indexPeriodo)
            {
                case 2: //ONTEM
                    dateStart = date.AddDays(-1);
                    dateEnd = date.AddDays(-1);
                    break;
                case 3: //SEMANA 
                    dateStart = GetFirstDayOfWeek(date);
                    dateEnd = GetLastDayOfWeek(date);
                    break;
                case 4: //SEMANA PASSADA
                    dateStart = date.AddDays(-7);
                    dateStart = GetFirstDayOfWeek(dateStart);
                    dateEnd = GetLastDayOfWeek(dateStart);
                    break;
                case 5: //MES
                    dateStart = GetFirstDayOfMonth(date);
                    dateEnd = GetLastDayOfMonth(date);
                    break;
                case 6: //MES PASSADO
                    dateStart = date.AddMonths(-1);
                    dateStart = GetFirstDayOfMonth(dateStart);
                    dateEnd = GetLastDayOfMonth(dateStart);
                    break;
            }
            var dic = new Dictionary<string, DateTime>();
            dic.Add("Start", dateStart);
            dic.Add("End", dateEnd);
            return dic;
        }

        public static DateTime GetFirstDayOfWeek(DateTime period)
        {
            DateTime firstDay = period;

            while (firstDay.DayOfWeek != DayOfWeek.Sunday)
                firstDay = firstDay.AddDays(-1);

            return firstDay;
        }

        public static DateTime GetLastDayOfWeek(DateTime period)
        {
            DateTime lastDay = period;

            while (lastDay.DayOfWeek != DayOfWeek.Saturday)
                lastDay = lastDay.AddDays(1);

            return lastDay;
        }

        public static DateTime GetFirstDayOfMonth(DateTime period)
        {
            DateTime firstDay = new DateTime(period.Year, period.Month, 1);
            return firstDay;
        }

        public static DateTime GetLastDayOfMonth(DateTime period)
        {
            DateTime lastDay = new DateTime(period.Year,
                                            period.Month,
                                            DateTime.DaysInMonth(period.Year,
                                                                 period.Month));
            return lastDay;
        }

        #endregion
    }
}
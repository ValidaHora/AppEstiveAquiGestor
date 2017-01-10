namespace EstiveAqui.ViewModel
{
	using EstiveAqui.Services.Abstract;
	using Repository.Abstract;
	using System.Collections.ObjectModel;
	using System.Linq;
	using Xamarin.Forms;


	public class ReportViewModel : Model.ReportModel
	{
		#region Constructor
		public ReportViewModel()
		{
			_reportRepository = DependencyService.Get<IReportRepository>();
			_apiService = DependencyService.Get<IApiService>();
			_navigationService = DependencyService.Get<INavigationService>();

			ReadData();
			BuildItems();
		}

		private async void BuildItems()
		{
			items = new ObservableCollection<Model.ReportModel>();
			var reports = _reportRepository.Find().OrderByDescending(a => a.Ms).ToList();

			if (!ReferenceEquals(reports, null))
				Items = new ObservableCollection<Model.ReportModel>(reports.Select(b => new Model.ReportModel
				{
					NomeArquivo = b.Na,
					Mes = b.Ms,
					MesAno = $"{b.Ms.Substring(4)}/{b.Ms.Substring(0, 4)}",
					Url = b.Ur
				}));
		}
		#endregion

		#region Attributes
		private bool _isLoading;
		private bool _isShowMsgError;
		private bool _isShowReports;
		private static ObservableCollection<Model.ReportModel> items;
		private readonly IReportRepository _reportRepository;
		private readonly IApiService _apiService;
		private readonly INavigationService _navigationService;
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
		public bool IsShowMsgError
		{
			get { return _isShowMsgError; }
			set
			{
				IsShowReports = !value;
				SetProperty(ref _isShowMsgError, value);
			}
		}
		public bool IsShowReports
		{
			get { return _isShowReports; }
			set
			{
				SetProperty(ref _isShowReports, value);
			}
		}
		public ObservableCollection<Model.ReportModel> Items
		{
			get { return items; }
			set
			{
				SetProperty(ref items, value);
			}
		}

		#endregion

		#region Business Requirement
		public async void ReadData()
		{
			if (Application.Current.Properties.ContainsKey("Logged"))
			{
				this.IsLoading = true;
				this.IsShowMsgError = false;
				var idApp = App.Current.Properties["IdApp"] as string;
				var data = await _apiService.GeraRelatorio(idApp);
				if (data.ValidadoOk && data.Rls.Any())
				{
					foreach (var item in data.Rls)
					{
						item.Ur = data.Ur;
						item.Ma = $"{item.Ms.Substring(4)}/{item.Ms.Substring(0, 4)}";

						var reportDb = _reportRepository.Find(a => a.Ms == item.Ms).FirstOrDefault();
						if (!ReferenceEquals(reportDb, null))
						{
							reportDb.Na = item.Na;
							reportDb.Ur = item.Ur;
							reportDb.Ma = item.Ma;
							_reportRepository.Update(reportDb);
						}
						else
							_reportRepository.Save(item);
					}
					BuildItems();
				}
				else
					this.IsShowMsgError = true;

				this.IsLoading = false;
			}
		}

		#endregion
	}
}
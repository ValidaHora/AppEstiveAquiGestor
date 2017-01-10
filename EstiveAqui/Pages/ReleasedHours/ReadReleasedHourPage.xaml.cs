namespace EstiveAqui.Pages
{
	using System;
	using System.Globalization;
	using Xamarin.Forms;
	using Xamarin.Forms.Maps;

	public partial class ReadReleasedHourPage : TabbedPage
	{
		public ReadReleasedHourPage(Model.ReleasedHourModel model)
		{
			InitializeComponent();

			this.BindingContext = new ViewModel.ReleasedHourViewModel(model);
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			var current = this.BindingContext as ViewModel.ReleasedHourViewModel;
			var latitude = current.Latitude ?? "0";
			var longitude = current.Longitude ?? "0";
			switch (latitude)
			{
				case "0":
					current.IsShowMap = false;
					current.IsShowMsgError = true;
					current.MsgError = "NÃO FOI POSSÍVEL EXIBIR A LOCALIZAÇÃO!";
					break;
				case "1000": //SEM_SUPORTE
					current.IsShowMap = false;
					current.IsShowMsgError = true;
					current.MsgError = "NÃO FOI POSSÍVEL EXIBIR A LOCALIZAÇÃO!";
					break;
				case "2000": //NAO_HABILITADO 
					current.IsShowMap = false;
					current.IsShowMsgError = true;
					current.MsgError = "GPS NÃO HABILITADO!";
					break;
				case "3000": //SEM_POSICIONAMENTO
					current.IsShowMap = false;
					current.IsShowMsgError = true;
					current.MsgError = "NÃO FOI POSSÍVEL CAPTURAR A LOCALIZAÇÃO!";
					break;
				case "4000": //ERRO_POSICIONAMENTO
					current.IsShowMap = false;
					current.IsShowMsgError = true;
					current.MsgError = "NÃO FOI POSSÍVEL EXIBIR A LOCALIZAÇÃO!";
					break;
				case "5000": //INFO_NAO_RECEBIDA
					current.IsShowMap = false;
					current.IsShowMsgError = true;
					current.MsgError = "INFORMAÇÃO NÃO RECEBIDA!";
					break;
				case "6000": //VALOR_INVALIDO
					current.IsShowMap = false;
					current.IsShowMsgError = true;
					current.MsgError = "NÃO FOI POSSÍVEL EXIBIR A LOCALIZAÇÃO!";
					break;
				case "7000": //LANCAMENTO_MANUAL
					current.IsShowMap = false;
					current.IsShowMsgError = true;
					current.MsgError = "LANÇAMENTO MANUAL!";
					break;
				default:
					current.IsShowMap = true;
					current.IsShowMsgError = false;

					var isoLang = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

					var latitudeConverted = Convert.ToDouble((isoLang.Equals("pt") ? latitude.Replace(".", ",") : latitude));
					var longitudeConverted = Convert.ToDouble((isoLang.Equals("pt") ? longitude.Replace('.', ',') : longitude));

					MainMap.MoveToRegion(MapSpan.FromCenterAndRadius(
								  new Position(latitudeConverted, longitudeConverted), Distance.FromKilometers(0.6)));

					var pin = new Pin()
					{
						Position = new Position(latitudeConverted, longitudeConverted),
						Label = current.DataHoraLancada.ToString("dd/MM/yyyy HH:mm:ss"),
						Type = PinType.Place
					};
					MainMap.Pins.Add(pin);
					break;
			}
		}
	}
}
namespace EstiveAqui.Model
{
	using EstiveAqui.Services.Abstract;
	using MvvmHelpers;

	public class ReportModel : ObservableObject, IBaseModel<ReportModel>
	{
		#region Attributes
		private string url;
		private string mes;
		private string nomeArquivo;
        private string mesAno;
		#endregion

		#region Properties
		public string Url
		{
			get { return url; }
			set
			{
				SetProperty(ref url, value);
			}
		}

		public string Mes
		{
			get { return mes; }
			set
			{
				SetProperty(ref mes, value);
			}
		}

        public string MesAno
        {
            get { return mesAno; }
            set
            {
                SetProperty(ref mesAno, value);
            }
        }

        public string NomeArquivo
		{
			get { return nomeArquivo; }
			set
			{
				SetProperty(ref nomeArquivo, value);
			}
		}


		public ReportModel Copy
		{
			get
			{
				return new ReportModel
				{
					Url = url,
					Mes = mes,
					NomeArquivo = nomeArquivo,
                    MesAno = mesAno
				};
			}
		}
		#endregion
	}
}

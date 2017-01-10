namespace EstiveAqui.Model
{
    using EstiveAqui.Services.Abstract;
    using MvvmHelpers;
    using System;

    public class ReleasedHourModel : ObservableObject, IBaseModel<ReleasedHourModel>
    {
        #region Attributes
        private string idLancamento;
        private int apelidoUsuarioIndex;
        private int apelidoPassclockIndex;
        private string apelidoUsuario;
        private string apelidoPassclock;
        private string idPassclock;
        private string numeroPassclock;
        private string codigoLancado;
        private bool isManual;
        private TimeSpan timeHoraDigitada;
        private DateTime dataHoraDigitada = DateTime.Now;
        private DateTime dataHoraLancada;
        private DateTime dataHoraEnviada;
        private int timezone;
        private bool active;
        private string msgActive;
        private string nota;
        private string latitude;
        private string longitude;

        private string identificacaoDispositivo;
        #endregion

        #region Properties
        public string IdLancamento
        {
            get { return idLancamento; }
            set
            {
                SetProperty(ref idLancamento, value);
            }
        }

        public int ApelidoUsuarioIndex
        {
            get { return apelidoUsuarioIndex; }
            set
            {
                SetProperty(ref apelidoUsuarioIndex, value);
            }
        }

        public int ApelidoPassclockIndex
        {
            get { return apelidoPassclockIndex; }
            set
            {
                SetProperty(ref apelidoPassclockIndex, value);
            }
        }

        public string ApelidoUsuario
        {
            get { return apelidoUsuario; }
            set
            {
                SetProperty(ref apelidoUsuario, value);
            }
        }

        public string ApelidoPassclock
        {
            get { return apelidoPassclock; }
            set
            {
                SetProperty(ref apelidoPassclock, value);
            }
        }

        public string IdentificacaoDispositivo
        {
            get { return identificacaoDispositivo; }
            set
            {
                SetProperty(ref identificacaoDispositivo, value);
            }
        }

        public string IdPassclock
        {
            get { return idPassclock; }
            set
            {
                SetProperty(ref idPassclock, value);
            }
        }

        public string NumeroPassclock
        {
            get { return numeroPassclock; }
            set
            {
                SetProperty(ref numeroPassclock, value);
            }
        }

        public string CodigoLancado
        {
            get { return codigoLancado; }
            set
            {
                SetProperty(ref codigoLancado, value);
            }
        }

        public bool IsManual
        {
            get { return isManual; }
            set
            {
                SetProperty(ref isManual, value);
            }
        }

        public TimeSpan TimeHoraDigitada
        {
            get { return timeHoraDigitada; }
            set
            {
                SetProperty(ref timeHoraDigitada, value);
            }
        }

        public DateTime DataHoraDigitada
        {
            get { return dataHoraDigitada; }
            set
            {
                SetProperty(ref dataHoraDigitada, value);
            }
        }

        public DateTime DataHoraLancada
        {
            get { return dataHoraLancada; }
            set
            {
                SetProperty(ref dataHoraLancada, value);
            }
        }

        public DateTime DataHoraEnviada
        {
            get { return dataHoraEnviada; }
            set
            {
                SetProperty(ref dataHoraEnviada, value);
            }
        }

        public int Timezone
        {
            get { return timezone; }
            set
            {
                SetProperty(ref timezone, value);
            }
        }

        public string Nota
        {
            get { return nota; }
            set
            {
                SetProperty(ref nota, value);
            }
        }

        public bool Active
        {
            get { return active; }
            set
            {
                SetProperty(ref active, value);
            }
        }

        public string MsgActive
        {
            get { return msgActive; }
            set
            {
                SetProperty(ref msgActive, value);
            }
        }

        public bool ActiveToggle
        {
            get { return !active; }
        }

        public string Latitude
        {
            get { return latitude; }
            set
            {
                SetProperty(ref latitude, value);
            }
        }

        public string Longitude
        {
            get { return longitude; }
            set
            {
                SetProperty(ref longitude, value);
            }
        }
        #endregion

        #region Create new instance
        public ReleasedHourModel Copy
        {
            get
            {
                return new ReleasedHourModel
                {
                    IdLancamento = this.idLancamento,
                    ApelidoUsuario = this.apelidoUsuario,
                    ApelidoPassclock = this.apelidoPassclock,
                    IdPassclock = this.idPassclock,
                    NumeroPassclock = this.numeroPassclock,
                    CodigoLancado = this.codigoLancado,
                    IsManual = this.isManual,
                    DataHoraDigitada = this.dataHoraDigitada,
                    DataHoraLancada = this.dataHoraLancada,
                    DataHoraEnviada = this.dataHoraEnviada,
                    Timezone = this.timezone,
                    Active = this.active,
                    MsgActive = this.msgActive,
                    Nota = this.nota,
                    Latitude = this.latitude,
                    Longitude = this.longitude,
                };
            }
        }
        #endregion
    }
}
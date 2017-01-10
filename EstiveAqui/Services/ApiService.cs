namespace EstiveAqui.Services
{
    using ApiSerialize;
    using EstiveAqui.Services.Abstract;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class ApiService : IApiService
    {
        #region Attributes
        private readonly string _urlSimple;
        private readonly string _url;
        private readonly string _version;
        private readonly string _timezone;
        #endregion

        #region ctor
        public ApiService()
        {
            _version = "1.0.0";
            _urlSimple = "http://app.estiveaqui.com.br/";
            _url = $"{_urlSimple}EstiveAqui/";
            var tz = (System.DateTime.Now.ToLocalTime() - System.DateTime.UtcNow);
            _timezone = tz.ToString().Substring(0, 3) + "00";
        }
        #endregion

        #region Usuários
        public async Task<ApiResult> AlteraSenhaEmail(string idApp, string oldPassword, string newPassword)
        {
            using (var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/AlteraSenhaEMail?V={_version}&TZ={_timezone}&IDAPP={idApp}&SENHAANTIGA={oldPassword}&SENHANOVA={newPassword}";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }

        public async Task<ApiResult> AlteraSenhaEmail2(string newPassword, string recoverCode)
        {
            using (var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/AlteraSenhaEMail?V={_version}&TZ={_timezone}&SENHANOVA={newPassword}&CODRECUPERASENHA={recoverCode}";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }

        public async Task<ApiResult> CadastraComEmail(string email, string password)
        {
            using (var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/CadastraComEMail?V={_version}&TZ={_timezone}&EMAIL={email}&SENHA={password}";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }

        public async Task<ApiResult> LinkConfirmaEmail(string code)
        {
            using (var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/CadastraComEMail?COD={code}";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }

        public async Task<ApiResult> LoginComEmail(string email, string password)
        {
            using (var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/LoginComEMail?V={_version}&TZ={_timezone}&EMAIL={email}&SENHA={password}";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }

        public async Task<ApiResult> RecuperaSenhaEmail(string email)
        {
            using (var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/RecuperaSenha?V={_version}&TZ={_timezone}&EMAIL={email}";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }

        public async Task<ApiResult> ReenviaConfirmacaoEmail(string idApp)
        {
            using (var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/EnviaConfirmaEMail?V={_version}&TZ={_timezone}&IDAPP={idApp}";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }

        public async Task<ApiResult> CadastraAppUsuario(string idApp, string apelido, string maxLancamentoDia, string integrationId)
        {
            using (var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/Gerencia/AppUsuario?V={_version}&TZ={_timezone}&IDAPP={idApp}&ACAO=CAD&APELIDO={apelido}&MAXLANCAMENTOSDIA={maxLancamentoDia}&IDINTEGRACAO={integrationId}";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }

        public async Task<ApiResult> AtualizaAppUsuario(string idApp, string idAppUsuario, string apelido, string maxLancamentoDia, string integrationId)
        {
            using (var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/Gerencia/AppUsuario?V={_version}&TZ={_timezone}&IDAPP={idApp}&IDAPPUSUARIO={idAppUsuario}&ACAO=UPD&APELIDO={apelido}&MAXLANCAMENTOSDIA={maxLancamentoDia}&IDINTEGRACAO={integrationId}";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }

        public async Task<ApiResult> HabilitaAppUsuario(string idApp, string idAppUsuario)
        {
            using (var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/Gerencia/AppUsuario?V={_version}&TZ={_timezone}&IDAPP={idApp}&IDAPPUSUARIO={idAppUsuario}&ACAO=ENA";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }

        public async Task<ApiResult> DesabilitaAppUsuario(string idApp, string idAppUsuario)
        {
            using (var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/Gerencia/AppUsuario?V={_version}&TZ={_timezone}&IDAPP={idApp}&IDAPPUSUARIO={idAppUsuario}&ACAO=DIS";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }
        #endregion 

        #region Carga
        public async Task<AppData> LeDadosAppGestor(string idApp, string idUltLancamento)
        {
            using (var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/LeDados?V={_version}&TZ={_timezone}&IDAPP={idApp}&IDULTLANCAMENTO={idUltLancamento}";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<AppData>(json);
                return result;
            }
        }
        #endregion

        #region Lançamentos
        public async Task<ApiResult> LancaHoraManual(string idApp, string idAppUsuario, string horaManual, string nota)
        {
            using (var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/Gerencia/Lancamento?V={_version}&TZ={_timezone}&IDAPP={idApp}&IDAPPUSUARIO={idAppUsuario}&ACAO=CAD&HORAMANUAL={horaManual}&NOTA={nota}";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }
        public async Task<ApiResult> HabilitaHora(string idApp, string idLancamento)
        {
            using(var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/Gerencia/Lancamento?V={_version}&TZ={_timezone}&IDAPP={idApp}&IDLANCAMENTO={idLancamento}&ACAO=ENA";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }

        public async Task<ApiResult> DesabilitaHora(string idApp, string idLancamento)
        {
            using (var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/Gerencia/Lancamento?V={_version}&TZ={_timezone}&IDAPP={idApp}&IDLANCAMENTO={idLancamento}&ACAO=DIS";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }
        #endregion

        #region Token
        public async Task<ApiResult> CadastraPassclock(string idCliente, string senhaCliente, string numToken, string codToken, string horaDigitada, string horaEnviada, string latitude, string longitude)
        {
            using (var client = new HttpClient())
            {                
                var request = $"{_urlSimple}ValidaHora/ValidaCodigo?V={_version}&CLI={idCliente}&SEN={senhaCliente}&TOK={numToken}&COD={codToken}&HEN={horaDigitada}";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }

        public async Task<ApiResult> CadastraPassclock2(string idApp, string numToken, string codToken, string alias, string horaCalculada, string hashCode)
        {
            using (var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/Gerencia/PassClock?V={_version}&TZ={_timezone}&IDAPP={idApp}&ACAO=CAD&NUMPASSCLOCK={numToken}&CODIGO={codToken}&APELIDO={alias}&HORAENVIADA={horaCalculada}&HASHCODE={hashCode}";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }

        public async Task<ApiResult> AtualizaPassclock(string idApp, string numToken, string alias)
        {
            using (var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/Gerencia/PassClock?V={_version}&TZ={_timezone}&IDAPP={idApp}&ACAO=UPD&NUMPASSCLOCK={numToken}&APELIDO={alias}";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }

        public async Task<ApiResult> HabilitaPassclock(string idApp, string numToken)
        {
            using (var client = new HttpClient())
            {

                var request = $"{_url}AppGestor/Gerencia/PassClock?V={_version}&TZ={_timezone}&IDAPP={idApp}&NUMPASSCLOCK={numToken}&ACAO=ENA";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }

        public async Task<ApiResult> DesabilitaPassclock(string idApp, string numToken)
        {
            using (var client = new HttpClient())
            {
                var request = $"{_url}AppGestor/Gerencia/PassClock?V={_version}&TZ={_timezone}&IDAPP={idApp}&NUMPASSCLOCK={numToken}&ACAO=DIS";
                var json = await client.GetStringAsync(request);
                var result = JsonConvert.DeserializeObject<ApiResult>(json);
                return result;
            }
        }
        #endregion

		#region Reports
		public async Task<ApiResult> GeraRelatorio(string idApp)
		{
			using (var client = new HttpClient())
			{
				var request = $"{_url}AppGestor/GeraRelatorio?V={_version}&TZ={_timezone}&IDAPP={idApp}";
				var json = await client.GetStringAsync(request);
				var result = JsonConvert.DeserializeObject<ApiResult>(json);
				return result;
			}
		}
		#endregion

    }
}
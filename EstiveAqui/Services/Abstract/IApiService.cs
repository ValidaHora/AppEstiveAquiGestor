namespace EstiveAqui.Services.Abstract
{
    using ApiSerialize;
    using System.Threading.Tasks;

    public interface IApiService
    {
        #region Gestor
        Task<ApiResult> CadastraComEmail(string email, string password);
        Task<ApiResult> LoginComEmail(string email, string password);
        Task<ApiResult> AlteraSenhaEmail(string idApp, string oldPassword, string newPassword);
        Task<ApiResult> AlteraSenhaEmail2(string newPassword, string recoverCode);
        Task<ApiResult> ReenviaConfirmacaoEmail(string idApp);
        Task<ApiResult> RecuperaSenhaEmail(string email);
        Task<ApiResult> LinkConfirmaEmail(string code);
        #endregion

        #region Carga
        Task<AppData> LeDadosAppGestor(string idApp, string idUltLancamento);
        #endregion

        #region Usuários
        Task<ApiResult> CadastraAppUsuario(string idApp, string apelido, string maxLancamentoDia, string integrationId);
        Task<ApiResult> AtualizaAppUsuario(string idApp, string idAppUsuario, string apelido, string maxLancamentoDia, string integrationId);
        Task<ApiResult> HabilitaAppUsuario(string idApp, string idAppUsuario);
        Task<ApiResult> DesabilitaAppUsuario(string idApp, string idAppUsuario);
        #endregion

        #region Lançamentos
        Task<ApiResult> LancaHoraManual(string idApp, string idAppUsuario, string horaManual, string nota);
        Task<ApiResult> HabilitaHora(string idApp, string idLancamento);
        Task<ApiResult> DesabilitaHora(string idApp, string idLancamento);
        #endregion

        #region Token
        Task<ApiResult> CadastraPassclock(string idCliente, string senhaCliente, string numToken, string codToken, string horaDigitada, 
                                          string horaEnviada, string latitude, string longitude);
        Task<ApiResult> CadastraPassclock2(string idApp, string numToken, string codToken, string alias, string horaCalculada, string hashCode);
        Task<ApiResult> AtualizaPassclock(string idApp, string numToken, string alias);
        Task<ApiResult> HabilitaPassclock(string idApp, string numToken);
        Task<ApiResult> DesabilitaPassclock(string idApp, string numToken);
		#endregion

		#region Reports
		Task<ApiResult> GeraRelatorio(string idApp);
		#endregion
    }
}
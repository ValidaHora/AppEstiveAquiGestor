using System.Collections.Generic;

namespace EstiveAqui.ApiSerialize
{
    public class ApiResult
    {
        public string Id { get; set; }
        public bool ValidadoOk { get; set; }
        public bool Ev { get; set; }
        public string Ig { get; set; }
        public string Ui { get; set; }
        public Message[] Mensagens { get; set; }
        public string HashCode { get; set; }
        public string HoraLancada { get; set; }
		public string Ur { get; set; } /** URL dos Relatórios*/
        public AppUser Us { get; set; }
        public PassClock Pco { get; set; }
        public ReleasedHours Ln { get; set; }
        public IEnumerable<Report> Rls { get; set; }
    }

    public class Report : BaseModel
    {
        public string Na { get; set; } /** Nome do arquivo */
        public string Ms { get; set; } /** Mês */
        [Newtonsoft.Json.JsonIgnore]
        public string Ur { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string Ma { get; set; } /** Mes Ano Formatado */
    }

    public class Message
    {
        public string Codigo { get; set; }
        public string Mensagem { get; set; }
    }
}

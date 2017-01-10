namespace EstiveAqui.ApiSerialize
{
    using SQLite.Net.Attributes;
    using System.Collections.Generic;

    public class AppData : ApiResult
    {
        public IEnumerable<AppUser> Uss { get; set; } /** Lista de usuários */
        public IEnumerable<ReleasedHours> Lns { get; set; } /** Lista de lançamentos */
        public IEnumerable<PassClock> Pcs { get; set; } /** Lista de passclocks */
    }

    public class ReleasedHours : BaseModel
    {
        public string Il { get; set; } /** Identificação do Lançamento */
        public string Au { get; set; } /** Apelido do AppUsuário */
        public string Di { get; set; } /** Identidificação do Dispositivo */
        public string Ap { get; set; } /** Apelido do PassClock */
        public string Pc { get; set; } /** Número do PassClock */
        public string Cd { get; set; } /** Código Lançado */
        public string Hd { get; set; } /** Hora Digitada */
        public string Hl { get; set; } /** Hora Lançada */
        public string He { get; set; } /** Hora Enviada */
        public string Tz { get; set; } /** Time Zone do cliente */
        public string Nt { get; set; } /** Nota */
        public string St { get; set; } /** Status */
        public string La { get; set; } /** Latitude */
        public string Lo { get; set; } /** Longitude */

        public string Hdx { get; set; } /** Hora Digitada GMT-0*/
        public string Hlx { get; set; } /** Hora Lançada GMT-0*/
        public string Hex { get; set; } /** Hora Enviada GMT-0*/
    }

    public class AppUser : BaseModel
    {
        public string Ii { get; set; } /** Identificador do AppUsuario */
        public string Iu { get; set; } /** Identificador do AppUsuario */
        public string Au { get; set; } /** Apelido do AppUsuário */
        public string St { get; set; } /** Status */
        public string Xl { get; set; } /** Quantidade máxima de lançamentos de um AppUsuario por dia */
        public string Ca { get; set; } /* Código de ativação, usado para ativar um AppUsuário */
    }

    public class PassClock : BaseModel
    {
        public string Ap { get; set; } /** Apelido do PassClock */
        public string St { get; set; } /** Status */
        public string Pc { get; set; } /** Número do PassClock */
        public string Cv { get; set; } /** Código de ativação virtual */
    }
}
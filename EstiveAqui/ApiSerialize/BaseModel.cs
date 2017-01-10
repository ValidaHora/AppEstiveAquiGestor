using SQLite.Net.Attributes;
using Newtonsoft.Json;
namespace EstiveAqui.ApiSerialize
{
    public abstract class BaseModel
    {
        [JsonIgnore]
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
    }
}
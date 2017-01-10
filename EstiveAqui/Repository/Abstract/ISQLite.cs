namespace EstiveAqui.Repository.Abstract
{
    using SQLite.Net.Interop;

    public interface ISQLite
    {
        string Path { get; }
        ISQLitePlatform Platform { get; }
    }
}
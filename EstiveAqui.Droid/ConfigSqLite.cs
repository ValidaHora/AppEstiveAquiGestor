using Xamarin.Forms;
[assembly: Dependency(typeof(EstiveAqui.Droid.ConfigSqLite))]

namespace EstiveAqui.Droid
{
    using EstiveAqui.Repository.Abstract;
    using SQLite.Net.Interop;

    public class ConfigSqLite : ISQLite
    {
        #region Attributes
        private string _path;
        private ISQLitePlatform _platform;
        #endregion 

        public string Path
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_path))
                    _path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

                return _path;
            }
        }

        public ISQLitePlatform Platform
        {
            get
            {
                if (ReferenceEquals(_platform, null))
                    _platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();

                return _platform;
            }
        }
    }
}
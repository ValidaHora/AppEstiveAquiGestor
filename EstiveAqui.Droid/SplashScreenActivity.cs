namespace EstiveAqui.Droid
{
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;

    [Activity(Label = "Estive Aqui", MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash",
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}
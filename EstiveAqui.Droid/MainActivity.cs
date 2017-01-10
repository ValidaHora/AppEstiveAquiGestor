namespace EstiveAqui.Droid
{
    using Android;
    using Android.App;
    using Android.Content.PM;
    using Android.OS;

    [Activity(Label = "EstiveAqui", Icon = "@drawable/Icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
        const int RequestLocationId = 0;
        const int RequestOthersId = 0;

        readonly string[] PermissionsLocation =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.AccessMockLocation,
            Manifest.Permission.LocationHardware
        };

        readonly string[] OthersPermissions =
        {
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.AccessNetworkState,
            Manifest.Permission.AccessWifiState,
            Manifest.Permission.Internet,
        };
        protected override void OnCreate(Bundle bundle)
		{

            if ((int)Build.VERSION.SdkInt >= 23)
            {
                const string permissionLocation = Manifest.Permission.AccessFineLocation;
                const string permissionCamera = Manifest.Permission.Camera;
                const string permissionWrite = Manifest.Permission.WriteExternalStorage;

                if (CheckSelfPermission(permissionLocation) != (int)Permission.Granted)
                    RequestPermissions(PermissionsLocation, RequestLocationId);

                if (CheckSelfPermission(permissionWrite) != (int)Permission.Granted)
                    RequestPermissions(OthersPermissions, RequestOthersId);
            }

            TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;
			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			LoadApplication(new App());
			Xamarin.FormsMaps.Init(this, bundle);
		}
	}
}
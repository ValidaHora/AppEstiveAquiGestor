namespace EstiveAqui
{
    using Xamarin.Forms;

    public class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            Master = new Pages.MainPage();
            Detail = new Pages.MainPage();
        }
    }
}

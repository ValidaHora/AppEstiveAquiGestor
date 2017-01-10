namespace EstiveAqui.Pages
{
    using Xamarin.Forms;

    public partial class ShareUserPage : ContentPage
    {
        public ShareUserPage()
        {
            InitializeComponent();

            this.BindingContext = new ViewModel.UserViewModel();
        }
    }
}

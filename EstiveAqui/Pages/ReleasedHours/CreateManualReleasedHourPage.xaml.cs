namespace EstiveAqui.Pages
{
    using Xamarin.Forms;

    public partial class CreateManualReleasedHourPage : ContentPage
    {
        public CreateManualReleasedHourPage()
        {
            InitializeComponent();

            this.BindingContext = new ViewModel.ReleasedHourViewModel();

            var current = this.BindingContext as ViewModel.ReleasedHourViewModel;

            foreach (var item in current.UserItems)
            {
                this.filterUser.Items.Add(item.Username);
            }
        }
    }
}
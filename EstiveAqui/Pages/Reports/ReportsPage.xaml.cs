namespace EstiveAqui.Pages
{
    using Xamarin.Forms;

    public partial class ReportsPage : ContentPage
    {
        public ReportsPage()
        {
            InitializeComponent();

			this.BindingContext = new ViewModel.ReportViewModel();
        }
    }    
}

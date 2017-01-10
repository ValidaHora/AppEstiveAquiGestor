namespace EstiveAqui.Pages
{
    using Xamarin.Forms;

    public partial class UpdatePasswordPage : ContentPage
    {
        public UpdatePasswordPage()
        {
            InitializeComponent();

            this.BindingContext = new ViewModel.UpdatePasswordViewModel();

			Password.Completed += (s, e) => NewPassword.Focus();
			NewPassword.Completed += (s, e) => ConfirmPassword.Focus();
			ConfirmPassword.Completed += (s, e) =>
			{
				var current = this.BindingContext as ViewModel.UpdatePasswordViewModel;
				current.UpdateCommand.Execute(null);
				Password.Focus();
			};
        }
    }
}

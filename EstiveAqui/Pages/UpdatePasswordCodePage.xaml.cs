namespace EstiveAqui.Pages
{
    using Xamarin.Forms;

    public partial class UpdatePasswordCodePage : ContentPage
    {
        public UpdatePasswordCodePage()
        {
            InitializeComponent();

            this.BindingContext = new ViewModel.UpdatePasswordViewModel();

			RecoverCode.Completed += (s, e) => NewPassword.Focus();
			NewPassword.Completed += (s, e) => ConfirmPassword.Focus();
			ConfirmPassword.Completed += (s, e) =>
			{
				var current = this.BindingContext as ViewModel.UpdatePasswordViewModel;
				current.RegisterCommand.Execute(null);
				RecoverCode.Focus();
			};
        }
    }
}
namespace EstiveAqui.Pages
{
    using System;
    using System.Text.RegularExpressions;
    using Xamarin.Forms;

    public partial class CreateUserPage : ContentPage
    {
        public CreateUserPage()
        {
            InitializeComponent();

            this.BindingContext = new ViewModel.UserViewModel();

            MaxEntry.Text = "4";

            MaxEntry.TextChanged += OnTextChanged;

            EntryUsername.Completed += (s, e) => EntryIntegrationId.Focus();
            EntryIntegrationId.Completed += (s, e) => MaxEntry.Focus();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            String val = entry.Text;
            Regex regex = new Regex("^[0-9]+$");
            
            if (!regex.IsMatch(val) && val.Length > 0)
                entry.Text = val.Remove(val.Length - 1);

            if (string.IsNullOrEmpty(val))
                entry.Text = "0";
                
        }
    }
}
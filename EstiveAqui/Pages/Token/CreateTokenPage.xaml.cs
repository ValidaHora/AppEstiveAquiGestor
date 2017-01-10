namespace EstiveAqui.Pages
{
    using Xamarin.Forms;

    public partial class CreateTokenPage : ContentPage
    {
        public CreateTokenPage()
        {
            InitializeComponent();

            this.BindingContext = new ViewModel.TokenViewModel();
            
            EntryAlias.TextChanged += (sender, args) =>
            {
                string _text = EntryAlias.Text;      
                if (_text.Length > 20)       
                {
                    _text = _text.Remove(_text.Length - 1); 
                    EntryAlias.Text = _text;
                }
            };

            EntryBarCode.TextChanged += (sender, args) =>
            {
                string _text = EntryBarCode.Text;
                if (_text.Length > 20)
                {
                    _text = _text.Remove(_text.Length - 1);
                    EntryBarCode.Text = _text;
                }
            };

            EntryValue.TextChanged += (sender, args) =>
            {
                string _text = EntryValue.Text;
                if (_text.Length > 6)
                {
                    _text = _text.Remove(_text.Length - 1);
                    EntryValue.Text = _text;
                }
            };

            EntryAlias.Completed += (s, e) => EntryValue.Focus();
            EntryValue.Completed += (s, e) => EntryBarCode.Focus();
            EntryBarCode.Completed += (s, e) => {
                var current = this.BindingContext as ViewModel.TokenViewModel;
                current.CreateCommand.Execute(null);
            };
        }
    }
}
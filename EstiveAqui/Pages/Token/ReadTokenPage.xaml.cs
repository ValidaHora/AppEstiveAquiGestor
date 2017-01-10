namespace EstiveAqui.Pages
{
    using Xamarin.Forms;

    public partial class ReadTokenPage : ContentPage
    {
        public ReadTokenPage(Model.TokenModel tokenModel)
        {
            InitializeComponent();

            this.BindingContext = new ViewModel.TokenViewModel(tokenModel);

            EntryAlias.Completed += (s, e) => {
                var current = this.BindingContext as ViewModel.TokenViewModel;
                current.UpdateCommand.Execute(null);
            };

            EntryAlias.TextChanged += (sender, args) =>
            {
                string _text = EntryAlias.Text;
                if (_text.Length > 20)
                {
                    _text = _text.Remove(_text.Length - 1);
                    EntryAlias.Text = _text;
                }
            };
            
        }
    }
}

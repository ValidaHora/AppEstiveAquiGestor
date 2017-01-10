using EstiveAqui.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace EstiveAqui.Pages
{
    public partial class ConfigPage : ContentPage
    {

        private readonly INavigationService _navigationPage;

        public ConfigPage()
        {
            InitializeComponent();


            this.BindingContext = new ViewModel.ConfigViewModel();

            _navigationPage = DependencyService.Get<INavigationService>();

            Lista.ItemTapped += ItemTapped;

        }

        private async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var current = this.BindingContext as ViewModel.ConfigViewModel;
            await current.NavigateTo(e.Item);
        }
    }
}

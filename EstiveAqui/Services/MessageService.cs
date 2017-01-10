namespace EstiveAqui.Services
{
    using System;
    using System.Threading.Tasks;
    using EstiveAqui.Services.Abstract;

    public class MessageService : IMessageService
    {
        public async Task DisplayAlert(string message)
        {
            await App.Current.MainPage.DisplayAlert("Estive Aqui", message, "Ok");
        }

        public async Task<bool> DisplayConfirm(string message)
        {
            return await App.Current.MainPage.DisplayAlert("Estive Aqui", message, "Sim", "Não");
        }
    }
}
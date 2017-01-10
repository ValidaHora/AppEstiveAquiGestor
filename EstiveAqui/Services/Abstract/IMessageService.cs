namespace EstiveAqui.Services.Abstract
{
    using System.Threading.Tasks;

    public interface IMessageService
    {
        Task DisplayAlert(string message);
        Task<bool> DisplayConfirm(string message);
    }
}
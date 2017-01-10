namespace EstiveAqui.Services.Abstract
{
    using System.Threading.Tasks;

    public interface INavigationService
    {
        Task PushAsync(System.Type type, bool animated, params object[] args);
        Task PushAsync(System.Type type, bool animated);
        Task NavigateTo(System.Type type, object param);
        Task PushModalAsync(System.Type type);
        Task ShowUnconfirmedPage(object param);
        Task NavigateTo(System.Type type);
        Task PopModalAsync();
        Task ShowMainPage();
        Task PopAsync();
        Task Logout();
        Task SignIn();

    }
}
namespace EstiveAqui.Services.Abstract
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public interface IBaseViewModel<TModel> where TModel : class
    {
        #region Business Requirement
        Task Create();
        Task Read(object param);
        Task Update();
        Task Delete(object param);
        #endregion

        #region Command
        ICommand CreateCommand { get; }
        ICommand ReadCommand { get; }
        ICommand UpdateCommand { get; }
        ICommand DeleteCommand { get; }
        #endregion

        #region Collection to list
        ObservableCollection<TModel> Items { get; }
        #endregion
    }
}
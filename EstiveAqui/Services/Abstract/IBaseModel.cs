namespace EstiveAqui.Services.Abstract
{
    public interface IBaseModel<T>
    {
        T Copy { get; }
    }
}
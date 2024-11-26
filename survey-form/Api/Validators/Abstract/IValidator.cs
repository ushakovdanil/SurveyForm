namespace Api.Validators.Abstract
{
    public interface IValidator<T>
    {
        bool Validate(T value);
    }
}

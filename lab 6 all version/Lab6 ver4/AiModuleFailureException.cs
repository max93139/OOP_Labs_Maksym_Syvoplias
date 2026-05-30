namespace Lab6;

/// <summary>
/// Виняток, що виникає при апаратному або програмному збої в обчислювальному модулі штучного інтелекту.
/// </summary>
public sealed class AiModuleFailureException : SmartCarException
{
    /// <summary>
    /// Ініціалізує новий виняток збою модуля штучного інтелекту.
    /// </summary>
    public AiModuleFailureException(string message) : base(message)
    {
    }
}

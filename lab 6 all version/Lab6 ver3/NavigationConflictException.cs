namespace Lab6;

/// <summary>
/// Виняток, що виникає у разі конфлікту маршрутизації або картографічних даних після оновлення ПЗ.
/// </summary>
public sealed class NavigationConflictException : SmartCarException
{
    /// <summary>
    /// Ініціалізує новий виняток конфлікту навігації.
    /// </summary>
    public NavigationConflictException(string message) : base(message)
    {
    }
}

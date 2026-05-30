namespace Lab6;

/// <summary>
/// Виняток, що виникає при критичному погіршенні фізіологічного стану або недієздатності водія.
/// </summary>
public sealed class DriverImpairmentException : SmartCarException
{
    /// <summary>
    /// Ініціалізує новий виняток недієздатності водія.
    /// </summary>
    public DriverImpairmentException(string message) : base(message)
    {
    }
}

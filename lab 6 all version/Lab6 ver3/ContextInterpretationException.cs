namespace Lab6;

/// <summary>
/// Виняток, що виникає, коли система не може інтерпретувати команду через швидку зміну дорожнього контексту.
/// </summary>
public sealed class ContextInterpretationException : SmartCarException
{
    /// <summary>
    /// Ініціалізує новий виняток інтерпретації контексту.
    /// </summary>
    public ContextInterpretationException(string message) : base(message)
    {
    }
}

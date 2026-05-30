namespace Lab6;

/// <summary>
/// Виняток, що виникає при небезпечній спробі вимкнення водного режиму посеред глибокої водойми.
/// Запобігає складанню гідродинамічного корпусу на глибині, що призвело б до затоплення.
/// </summary>
public sealed class WaterExitDepthException : SmartCarException
{
    /// <summary>
    /// Ініціалізує новий виняток небезпечного виходу з водного режиму.
    /// </summary>
    public WaterExitDepthException(string message) : base(message)
    {
    }
}

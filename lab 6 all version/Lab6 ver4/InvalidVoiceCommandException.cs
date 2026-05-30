namespace Lab6;

/// <summary>
/// Виняток, що виникає, коли голосовий помічник не може розпізнати або виконати команду водія.
/// </summary>
public sealed class InvalidVoiceCommandException : SmartCarException
{
    /// <summary>
    /// Ініціалізує новий виняток невалідної голосової команди.
    /// </summary>
    public InvalidVoiceCommandException(string message) : base(message)
    {
    }
}

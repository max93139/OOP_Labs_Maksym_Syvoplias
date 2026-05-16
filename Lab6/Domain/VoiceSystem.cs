namespace Lab6.Domain;

/// <summary>
/// Забезпечує взаємодію з людиною через розпізнавання команд і озвучення повідомлень.
/// </summary>
public sealed class VoiceSystem
{
    /// <summary>
    /// Розпізнає намір команди з тексту фрази.
    /// </summary>
    public CommandIntent RecognizeCommand(string phrase)
    {
        string normalizedPhrase = phrase.Trim().ToLowerInvariant();
        CommandIntent intent;

        if (normalizedPhrase.Contains("autopilot", StringComparison.Ordinal))
        {
            intent = CommandIntent.EnableAutopilot;
        }
        else
        {
            intent = CommandIntent.StartTrip;
        }

        return intent;
    }

    /// <summary>
    /// Створює повідомлення для пасажира.
    /// </summary>
    public string Speak(string message)
    {
        return $"Voice assistant: {message}";
    }
}

namespace Lab6;

/// <summary>
/// Забезпечує взаємодію з людиною через розпізнавання команд та розрахунок впевненості розпізнавання.
/// </summary>
public sealed class VoiceSystem : SmartDevice
{
    private double commandConfidence;

    /// <summary>
    /// Ініціалізує нову голосову систему.
    /// </summary>
    public VoiceSystem() : base("Голосова система", 0.15)
    {
        commandConfidence = 1.0;
    }

    /// <summary>
    /// Розпізнає намір команди з тексту фрази та розраховує математичну впевненість класифікатора.
    /// </summary>
    public string RecognizeCommand(string phrase)
    {
        string normalizedPhrase = phrase.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(normalizedPhrase) || normalizedPhrase.Contains("дурниц", StringComparison.Ordinal) || normalizedPhrase.Contains("nonsense", StringComparison.Ordinal))
        {
            throw new InvalidVoiceCommandException($"Голосова помилка: Невідома або некоректна команда \"{phrase}\".");
        }
        else
        {
            if (normalizedPhrase.Contains(" і ", StringComparison.Ordinal) || normalizedPhrase.Contains(" та ", StringComparison.Ordinal) || normalizedPhrase.Contains(" and ", StringComparison.Ordinal))
            {
                throw new TooManyCommandsException("Помилка управління: Виявлено надмірну кількість паралельних команд! Система не може виконувати кілька дій одночасно.");
            }
            else
            {
                string intent;

                if (normalizedPhrase.Contains("autopilot", StringComparison.Ordinal) || normalizedPhrase.Contains("self-drive", StringComparison.Ordinal))
                {
                    intent = "EnableAutopilot";
                    commandConfidence = 0.98;
                }
                else
                {
                    if (normalizedPhrase.Contains("climate", StringComparison.Ordinal) || normalizedPhrase.Contains("temperature", StringComparison.Ordinal))
                    {
                        intent = "ChangeClimate";
                        commandConfidence = 0.95;
                    }
                    else
                    {
                        if (normalizedPhrase.Contains("protect", StringComparison.Ordinal) || normalizedPhrase.Contains("safety", StringComparison.Ordinal))
                        {
                            intent = "ActivateProtection";
                            commandConfidence = 0.92;
                        }
                        else
                        {
                            if (normalizedPhrase.Contains("diagnostics", StringComparison.Ordinal) || normalizedPhrase.Contains("check", StringComparison.Ordinal))
                            {
                                intent = "ShowDiagnostics";
                                commandConfidence = 0.90;
                            }
                            else
                            {
                                intent = "StartTrip";
                                commandConfidence = 0.50;
                            }
                        }
                    }
                }

                return intent;
            }
        }
    }

    /// <summary>
    /// Створює повідомлення для пасажира з додаванням оцінки точності розпізнавання команди.
    /// </summary>
    public string Speak(string message)
    {
        return $"Голосовий асистент: {message} (впевненість розпізнавання: {commandConfidence:P1})";
    }

    /// <summary>
    /// Повертає локалізовану назву наміру команди за допомогою оператора switch.
    /// </summary>
    public string GetLocalizedIntentName(string intent)
    {
        string localizedName;
        switch (intent.ToLowerInvariant())
        {
            case "enableautopilot":
            {
                localizedName = "Увімкнути автопілот";
                break;
            }
            case "changeclimate":
            {
                localizedName = "Змінити клімат";
                break;
            }
            case "activateprotection":
            {
                localizedName = "Активувати захист";
                break;
            }
            case "showdiagnostics":
            {
                localizedName = "Показати діагностику";
                break;
            }
            case "starttrip":
            {
                localizedName = "Розпочати поїздку";
                break;
            }
            default:
            {
                localizedName = intent;
                break;
            }
        }

        return localizedName;
    }

    /// <summary>
    /// Повертає статус голосової системи.
    /// </summary>
    public override string GetStatus()
    {
        return $"Модуль '{DeviceName}' готовий до прийому голосових команд. Енергоспоживання: {PowerConsumption} кВт.";
    }
}

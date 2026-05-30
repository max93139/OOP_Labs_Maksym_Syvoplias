using System;

namespace Lab6;

/// <summary>
/// Забезпечує взаємодію з людиною через розпізнавання команд та розрахунок впевненості розпізнавання.
/// </summary>
public sealed class VoiceSystem
{
    private double _commandConfidence;
    private string _systemName;

    /// <summary>
    /// Конструктор за замовчуванням (Canonical Class Template).
    /// </summary>
    public VoiceSystem()
    {
        _commandConfidence = 1.0;
        _systemName = "Active Voice Recognition Interface";
    }

    /// <summary>
    /// Конструктор з повним набором конфігурацій.
    /// </summary>
    public VoiceSystem(double commandConfidence, string systemName)
    {
        _commandConfidence = commandConfidence;
        _systemName = systemName;
    }

    /// <summary>
    /// Конструктор копіювання (Canonical Class Template).
    /// </summary>
    public VoiceSystem(VoiceSystem other)
    {
        _commandConfidence = other.CommandConfidence;
        _systemName = other.SystemName;
    }

    /// <summary>
    /// Повертає впевненість розпізнавання останньої команди.
    /// </summary>
    public double CommandConfidence
    {
        get
        {
            return _commandConfidence;
        }
        set
        {
            _commandConfidence = value;
        }
    }

    /// <summary>
    /// Повертає назву голосової системи.
    /// </summary>
    public string SystemName
    {
        get
        {
            return _systemName;
        }
        set
        {
            _systemName = value;
        }
    }

    /// <summary>
    /// Розпізнає намір команди з тексту фрази та розраховує математичну впевненість класифікатора.
    /// </summary>
    public string RecognizeCommand(string phrase)
    {
        string normalizedPhrase = phrase.Trim().ToLowerInvariant();
        string intent;

        if (normalizedPhrase.Contains("autopilot", StringComparison.Ordinal) || 
            normalizedPhrase.Contains("self-drive", StringComparison.Ordinal))
        {
            intent = "EnableAutopilot";
            _commandConfidence = 0.98;
        }
        else if (normalizedPhrase.Contains("climate", StringComparison.Ordinal) || 
                 normalizedPhrase.Contains("temperature", StringComparison.Ordinal))
        {
            intent = "ChangeClimate";
            _commandConfidence = 0.95;
        }
        else if (normalizedPhrase.Contains("protect", StringComparison.Ordinal) || 
                 normalizedPhrase.Contains("safety", StringComparison.Ordinal))
        {
            intent = "ActivateProtection";
            _commandConfidence = 0.92;
        }
        else if (normalizedPhrase.Contains("diagnostics", StringComparison.Ordinal) || 
                 normalizedPhrase.Contains("check", StringComparison.Ordinal))
        {
            intent = "ShowDiagnostics";
            _commandConfidence = 0.90;
        }
        else
        {
            intent = "StartTrip";
            _commandConfidence = 0.50;
        }

        return intent;
    }

    /// <summary>
    /// Створює повідомлення для пасажира з додаванням оцінки точності розпізнавання команди.
    /// </summary>
    public string Speak(string message)
    {
        return $"Голосовий асистент: {message} (впевненість розпізнавання: {_commandConfidence:P1})";
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
}

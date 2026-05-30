using System;

namespace Lab6;

/// <summary>
/// Модуль розпізнавання мовлення для обробки голосових команд водія та пасажирів.
/// </summary>
public class SpeechRecognitionModule : SmartDevice
{
    private string _language;
    private int _vocabularySize;
    private double _accuracy;

    /// <summary>
    /// Конструктор за замовчуванням.
    /// </summary>
    public SpeechRecognitionModule() : base("Модуль розпізнавання мовлення", 0.05)
    {
        _language = "uk-UA";
        _vocabularySize = 1000;
        _accuracy = 0.95;
    }

    /// <summary>
    /// Конструктор з параметрами.
    /// </summary>
    public SpeechRecognitionModule(string language, int vocabularySize, double accuracy) : base("Модуль розпізнавання мовлення", 0.05)
    {
        _language = language;
        _vocabularySize = vocabularySize;
        _accuracy = accuracy;
    }

    /// <summary>
    /// Конструктор копіювання.
    /// </summary>
    public SpeechRecognitionModule(SpeechRecognitionModule other) : base(other.DeviceName, other.PowerConsumption)
    {
        _language = other.Language;
        _vocabularySize = other.VocabularySize;
        _accuracy = other.Accuracy;
    }

    public string Language
    {
        get => _language;
        set => _language = value;
    }

    public int VocabularySize
    {
        get => _vocabularySize;
        set => _vocabularySize = value;
    }

    public double Accuracy
    {
        get => _accuracy;
        set => _accuracy = value;
    }

    public string RecognizeCommand(string phrase, out double confidence)
    {
        string normalizedPhrase = phrase.Trim().ToLowerInvariant();
        string intent;

        if (normalizedPhrase.Contains("autopilot", StringComparison.Ordinal) || normalizedPhrase.Contains("self-drive", StringComparison.Ordinal))
        {
            intent = "EnableAutopilot";
            confidence = 0.98 * _accuracy;
        }
        else if (normalizedPhrase.Contains("climate", StringComparison.Ordinal) || normalizedPhrase.Contains("temperature", StringComparison.Ordinal))
        {
            intent = "ChangeClimate";
            confidence = 0.95 * _accuracy;
        }
        else if (normalizedPhrase.Contains("protect", StringComparison.Ordinal) || normalizedPhrase.Contains("safety", StringComparison.Ordinal))
        {
            intent = "ActivateProtection";
            confidence = 0.92 * _accuracy;
        }
        else if (normalizedPhrase.Contains("diagnostics", StringComparison.Ordinal) || normalizedPhrase.Contains("check", StringComparison.Ordinal))
        {
            intent = "ShowDiagnostics";
            confidence = 0.90 * _accuracy;
        }
        else
        {
            intent = "StartTrip";
            confidence = 0.50 * _accuracy;
        }

        return intent;
    }

    public string TrainNewPhrases(string phraseClass, string[] examples)
    {
        _vocabularySize += examples.Length;
        return $"[SpeechRecognitionModule]: Словник розширено на {examples.Length} фраз для наміру \"{phraseClass}\". Поточний розмір словника: {_vocabularySize}.";
    }

    public override string GetStatus()
    {
        return $"Модуль '{DeviceName}' працює. Мова: {_language}, точність: {_accuracy:P0}. Енергоспоживання: {PowerConsumption} кВт.";
    }
}

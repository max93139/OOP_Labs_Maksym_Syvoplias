using System;

namespace Lab6;

/// <summary>
/// Базовий виняток для всіх збоїв та критичних ситуацій у системі розумного автомобіля.
/// Успадкування від Exception дозволяє catch-блокам точно відрізнити доменні помилки від системних.
/// </summary>
public class SmartCarException : Exception
{
    public const string AiModuleFailure = "AiModuleFailure";
    public const string ContextInterpretation = "ContextInterpretation";
    public const string DriverImpairment = "DriverImpairment";
    public const string InvalidVoiceCommand = "InvalidVoiceCommand";
    public const string NavigationConflict = "NavigationConflict";
    public const string ProfileMismatch = "ProfileMismatch";
    public const string TooManyCommands = "TooManyCommands";
    public const string WaterExitDepth = "WaterExitDepth";

    /// <summary>
    /// Ініціалізує новий виняток із повідомленням про помилку.
    /// </summary>
    public SmartCarException(string message) : base(message)
    {
        ErrorType = nameof(SmartCarException);
    }

    /// <summary>
    /// Ініціалізує новий доменний виняток із типом помилки та повідомленням.
    /// </summary>
    public SmartCarException(string errorType, string message) : base(message)
    {
        ErrorType = errorType;
    }

    /// <summary>
    /// Ініціалізує новий виняток із повідомленням та вкладеним винятком.
    /// </summary>
    public SmartCarException(string message, Exception innerException) : base(message, innerException)
    {
        ErrorType = nameof(SmartCarException);
    }

    /// <summary>
    /// Повертає доменний тип помилки для вибіркової обробки винятку.
    /// </summary>
    public string ErrorType { get; }

    /// <summary>
    /// Перевіряє, чи відповідає виняток заданому доменному типу.
    /// </summary>
    public bool IsType(string errorType)
    {
        return ErrorType == errorType;
    }
}

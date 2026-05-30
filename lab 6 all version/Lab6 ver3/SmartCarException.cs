using System;

namespace Lab6;

/// <summary>
/// Базовий виняток для всіх збоїв та критичних ситуацій у системі розумного автомобіля.
/// Успадкування від Exception дозволяє catch-блокам точно відрізнити доменні помилки від системних.
/// </summary>
public class SmartCarException : Exception
{
    /// <summary>
    /// Ініціалізує новий виняток із повідомленням про помилку.
    /// </summary>
    public SmartCarException(string message) : base(message)
    {
    }

    /// <summary>
    /// Ініціалізує новий виняток із повідомленням та вкладеним винятком.
    /// </summary>
    public SmartCarException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

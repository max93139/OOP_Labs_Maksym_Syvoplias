namespace Lab6;

/// <summary>
/// Описує сенсор, з якого можна зчитати дані.
/// Інтерфейс дозволяє замінювати реальний апаратний сенсор на демонстраційний без змін у коді клієнтів.
/// </summary>
public interface ISensor
{
    /// <summary>
    /// Повертає назву сенсора.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Зчитує поточне значення сенсора.
    /// </summary>
    SensorReading Read();
}

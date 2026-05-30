namespace Lab6;

/// <summary>
/// Агрегує незалежні сенсори здоров'я без володіння їхнім життєвим циклом.
/// </summary>
public sealed class DriverStateSensor
{
    private readonly IReadOnlyList<ISensor> sensors;

    /// <summary>
    /// Ініціалізує сенсор стану водія з незалежних сенсорів.
    /// </summary>
    public DriverStateSensor(IReadOnlyList<ISensor> sensors)
    {
        this.sensors = sensors;
    }

    /// <summary>
    /// Зчитує всі вимірювання стану водія.
    /// </summary>
    public IReadOnlyList<SensorReading> ReadDriverState()
    {
        List<SensorReading> readings = new List<SensorReading>();

        foreach (ISensor sensor in sensors)
        {
            readings.Add(sensor.Read());
        }

        return readings;
    }
}

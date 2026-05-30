using System;
using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Агрегує незалежні сенсори здоров'я без володіння їхнім життєвим циклом.
/// </summary>
public sealed class DriverStateSensor
{
    private IReadOnlyList<ISensor> _sensors;

    /// <summary>
    /// Конструктор за замовчуванням (Canonical Class Template).
    /// </summary>
    public DriverStateSensor()
    {
        _sensors = new List<ISensor>().AsReadOnly();
    }

    /// <summary>
    /// Ініціалізує сенсор стану водія з незалежних сенсорів.
    /// </summary>
    public DriverStateSensor(IReadOnlyList<ISensor> sensors)
    {
        _sensors = sensors;
    }

    /// <summary>
    /// Конструктор копіювання (Canonical Class Template).
    /// </summary>
    public DriverStateSensor(DriverStateSensor other)
    {
        _sensors = other.Sensors;
    }

    /// <summary>
    /// Повертає список сенсорів.
    /// </summary>
    public IReadOnlyList<ISensor> Sensors
    {
        get
        {
            return _sensors;
        }
        set
        {
            _sensors = value;
        }
    }

    /// <summary>
    /// Зчитує всі вимірювання стану водія.
    /// </summary>
    public IReadOnlyList<SensorReading> ReadDriverState()
    {
        List<SensorReading> readings = new List<SensorReading>();

        foreach (ISensor sensor in _sensors)
        {
            readings.Add(sensor.Read());
        }

        return readings;
    }
}

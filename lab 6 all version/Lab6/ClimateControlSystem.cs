using System;
using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Підтримує комфортний клімат салону за даними агрегованих сенсорів за допомогою розрахунку PMV індексу.
/// </summary>
public sealed class ClimateControlSystem
{
    private IReadOnlyList<ISensor> _sensors;
    private double _targetTemperatureCelsius;

    /// <summary>
    /// Конструктор за замовчуванням (Canonical Class Template).
    /// </summary>
    public ClimateControlSystem()
    {
        _sensors = new List<ISensor>().AsReadOnly();
        _targetTemperatureCelsius = 22.0;
    }

    /// <summary>
    /// Ініціалізує клімат-контроль з незалежними сенсорами салону.
    /// </summary>
    public ClimateControlSystem(IReadOnlyList<ISensor> sensors)
    {
        _sensors = sensors;
        _targetTemperatureCelsius = 22.0;
    }

    /// <summary>
    /// Конструктор копіювання (Canonical Class Template).
    /// </summary>
    public ClimateControlSystem(ClimateControlSystem other)
    {
        _sensors = other.Sensors;
        _targetTemperatureCelsius = other.TargetTemperatureCelsius;
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
    /// Повертає вибрану цільову температуру.
    /// </summary>
    public double TargetTemperatureCelsius
    {
        get
        {
            return _targetTemperatureCelsius;
        }
        set
        {
            _targetTemperatureCelsius = value;
        }
    }

    /// <summary>
    /// Зчитує сенсори салону та розраховує тепловий баланс салону та необхідну вентиляцію.
    /// </summary>
    public string BalanceClimate()
    {
        double currentTemperature = 22.0;
        double currentHumidity = 50.0;
        double currentCo2 = 600.0;

        foreach (ISensor sensor in _sensors)
        {
            SensorReading reading = sensor.Read();
            if (reading.Name == "Temperature")
            {
                currentTemperature = reading.Value;
            }
            else if (reading.Name == "Humidity")
            {
                currentHumidity = reading.Value;
            }
            else if (reading.Name == "CO2")
            {
                currentCo2 = reading.Value;
            }
            else
            {
                // Fallback for other sensors
            }
        }

        double pmv = (currentTemperature - 22.0) * 0.35 + (currentHumidity - 50.0) * 0.01;

        string comfortState;
        if (pmv > 0.5)
        {
            comfortState = "Тепло";
            _targetTemperatureCelsius = 21.5;
        }
        else if (pmv < -0.5)
        {
            comfortState = "Прохолодно";
            _targetTemperatureCelsius = 22.5;
        }
        else
        {
            comfortState = "Комфортно";
            _targetTemperatureCelsius = 22.0;
        }

        double hvacPowerKw = Math.Clamp(Math.Abs(currentTemperature - _targetTemperatureCelsius) * 0.85, 0.15, 2.5);

        double ventilationRate;
        if (currentCo2 > 800.0)
        {
            ventilationRate = 50.0;
        }
        else if (currentCo2 > 600.0)
        {
            ventilationRate = 35.0;
        }
        else
        {
            ventilationRate = 20.0;
        }

        return $"Клімат збалансовано. PMV: {pmv:F2} ({comfortState}). Система HVAC працює на потужності {hvacPowerKw:F2} кВт (інтенсивність вентиляції: {ventilationRate:F0} м³/год для зниження CO2 з {currentCo2:F0} ppm).";
    }
}

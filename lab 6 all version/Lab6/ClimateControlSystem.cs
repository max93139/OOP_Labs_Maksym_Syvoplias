namespace Lab6;

/// <summary>
/// Підтримує комфортний клімат салону за даними агрегованих сенсорів за допомогою розрахунку PMV індексу.
/// </summary>
public sealed class ClimateControlSystem
{
    private readonly IReadOnlyList<ISensor> sensors;

    /// <summary>
    /// Ініціалізує клімат-контроль з незалежними сенсорами салону.
    /// </summary>
    public ClimateControlSystem(IReadOnlyList<ISensor> sensors)
    {
        this.sensors = sensors;
        TargetTemperatureCelsius = 22.0;
    }

    /// <summary>
    /// Повертає вибрану цільову температуру.
    /// </summary>
    public double TargetTemperatureCelsius { get; private set; }

    /// <summary>
    /// Зчитує сенсори салону та розраховує тепловий баланс салону та необхідну вентиляцію.
    /// </summary>
    public string BalanceClimate()
    {
        double currentTemperature = 22.0;
        double currentHumidity = 50.0;
        double currentCo2 = 600.0;

        foreach (ISensor sensor in sensors)
        {
            SensorReading reading = sensor.Read();
            if (reading.Name == "Temperature")
            {
                currentTemperature = reading.Value;
            }
            else
            {
                if (reading.Name == "Humidity")
                {
                    currentHumidity = reading.Value;
                }
                else
                {
                    if (reading.Name == "CO2")
                    {
                        currentCo2 = reading.Value;
                    }
                    else
                    {
                        // Fallback for other sensors
                    }
                }
            }
        }

        double pmv = (currentTemperature - 22.0) * 0.35 + (currentHumidity - 50.0) * 0.01;

        string comfortState;
        if (pmv > 0.5)
        {
            comfortState = "Тепло";
            TargetTemperatureCelsius = 21.5;
        }
        else
        {
            if (pmv < -0.5)
            {
                comfortState = "Прохолодно";
                TargetTemperatureCelsius = 22.5;
            }
            else
            {
                comfortState = "Комфортно";
                TargetTemperatureCelsius = 22.0;
            }
        }

        double hvacPowerKw = Math.Clamp(Math.Abs(currentTemperature - TargetTemperatureCelsius) * 0.85, 0.15, 2.5);

        double ventilationRate;
        if (currentCo2 > 800.0)
        {
            ventilationRate = 50.0;
        }
        else
        {
            if (currentCo2 > 600.0)
            {
                ventilationRate = 35.0;
            }
            else
            {
                ventilationRate = 20.0;
            }
        }

        return $"Клімат збалансовано. PMV: {pmv:F2} ({comfortState}). Система HVAC працює на потужності {hvacPowerKw:F2} кВт (інтенсивність вентиляції: {ventilationRate:F0} м³/год для зниження CO2 з {currentCo2:F0} ppm).";
    }
}

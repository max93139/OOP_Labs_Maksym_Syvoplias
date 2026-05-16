namespace Lab6.Domain;

/// <summary>
/// Підтримує комфортний клімат салону за даними агрегованих сенсорів.
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
    /// Зчитує сенсори салону та вибирає температурний баланс.
    /// </summary>
    public string BalanceClimate()
    {
        double totalValue = 0.0;

        foreach (ISensor sensor in sensors)
        {
            totalValue += sensor.Read().Value;
        }

        TargetTemperatureCelsius = 20.0 + Math.Clamp(totalValue / 100.0, 0.0, 4.0);
        return $"Climate balanced at {TargetTemperatureCelsius:F1} C.";
    }
}

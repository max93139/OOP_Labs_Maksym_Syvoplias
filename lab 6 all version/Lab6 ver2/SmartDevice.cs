namespace Lab6;

/// <summary>
/// Абстрактний базовий клас, який представляє будь-який розумний пристрій або модуль автомобіля.
/// </summary>
public abstract class SmartDevice
{
    /// <summary>
    /// Ініціалізує базовий розумний пристрій із назвою та енергоспоживанням.
    /// </summary>
    protected SmartDevice(string name, double powerConsumption)
    {
        DeviceName = name;
        PowerConsumption = powerConsumption;
    }

    /// <summary>
    /// Повертає назву пристрою.
    /// </summary>
    public string DeviceName { get; }

    /// <summary>
    /// Повертає енергоспоживання пристрою у кВт.
    /// </summary>
    public double PowerConsumption { get; }

    /// <summary>
    /// Повертає поточний статус або стан розумного пристрою.
    /// </summary>
    public abstract string GetStatus();
}

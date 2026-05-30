namespace Lab6;

/// <summary>
/// Представляє гальмівну підсистему з фізичним розрахунком теплового навантаження.
/// </summary>
public sealed class BrakeSystem
{
    private double brakeTemperatureCelsius;
    private double wearPercent;

    /// <summary>
    /// Ініціалізує нову гальмівну систему з початковими фізичними параметрами.
    /// </summary>
    public BrakeSystem(string brakeType, double efficiencyPercent)
    {
        BrakeType = brakeType;
        EfficiencyPercent = efficiencyPercent;
        brakeTemperatureCelsius = 25.0;
        wearPercent = 12.5;
    }

    /// <summary>
    /// Повертає тип гальм.
    /// </summary>
    public string BrakeType { get; }

    /// <summary>
    /// Повертає ефективність гальм.
    /// </summary>
    public double EfficiencyPercent { get; }

    /// <summary>
    /// Активує звичайне гальмування та розраховує приріст температури гальмівних дисків.
    /// </summary>
    public string ActivateBraking()
    {
        double speedMeterPerSecond = 27.78;
        double vehicleMassKilograms = 1830.0;
        double kineticEnergyJoules = 0.5 * vehicleMassKilograms * speedMeterPerSecond * speedMeterPerSecond;
        double frontAxleBrakingShare = 0.70;
        double energyPerDiscJoules = (kineticEnergyJoules * frontAxleBrakingShare) / 2.0;
        double discMassKilograms = 8.0;
        double specificHeatCapacityCastIron = 500.0;

        double temperatureIncrease = energyPerDiscJoules / (discMassKilograms * specificHeatCapacityCastIron);
        brakeTemperatureCelsius += temperatureIncrease;

        double fadingFactor;
        if (brakeTemperatureCelsius > 350.0)
        {
            fadingFactor = Math.Max(0.4, 1.0 - (brakeTemperatureCelsius - 350.0) * 0.002);
        }
        else
        {
            fadingFactor = 1.0;
        }

        double wearIncrease = 0.05 * (brakeTemperatureCelsius / 100.0);
        wearPercent = Math.Min(100.0, wearPercent + wearIncrease);
        double activeEfficiency = EfficiencyPercent * fadingFactor;

        return $"Гальмування {BrakeType} активовано. Теплове навантаження: +{temperatureIncrease:F1}°C (поточна: {brakeTemperatureCelsius:F1}°C). Ефективність: {activeEfficiency:F1}% (знос: {wearPercent:F2}%).";
    }

    /// <summary>
    /// Активує екстрене гальмування з підвищеним тепловим навантаженням та ABS.
    /// </summary>
    public string ActivateEmergencyBraking()
    {
        double speedMeterPerSecond = 36.11;
        double vehicleMassKilograms = 1830.0;
        double kineticEnergyJoules = 0.5 * vehicleMassKilograms * speedMeterPerSecond * speedMeterPerSecond;
        double frontAxleBrakingShare = 0.70;
        double energyPerDiscJoules = (kineticEnergyJoules * frontAxleBrakingShare) / 2.0;
        double discMassKilograms = 8.0;
        double specificHeatCapacityCastIron = 500.0;

        double temperatureIncrease = energyPerDiscJoules / (discMassKilograms * specificHeatCapacityCastIron);
        brakeTemperatureCelsius += temperatureIncrease;

        double fadingFactor;
        if (brakeTemperatureCelsius > 350.0)
        {
            fadingFactor = Math.Max(0.3, 1.0 - (brakeTemperatureCelsius - 350.0) * 0.003);
        }
        else
        {
            fadingFactor = 1.0;
        }

        double wearIncrease = 0.15 * (brakeTemperatureCelsius / 100.0);
        wearPercent = Math.Min(100.0, wearPercent + wearIncrease);
        double activeEfficiency = EfficiencyPercent * fadingFactor;

        string safetyStatus;
        if (activeEfficiency < 50.0)
        {
            safetyStatus = "ВИЯВЛЕНО КРИТИЧНЕ ЗНИЖЕННЯ ЕФЕКТИВНОСТІ! Шлях гальмування збільшено.";
        }
        else
        {
            safetyStatus = "ABS активна (18 імпульсів). Автомобіль стабільний.";
        }

        return $"Екстрене гальмування активовано! Теплове навантаження: +{temperatureIncrease:F1}°C (поточна: {brakeTemperatureCelsius:F1}°C). {safetyStatus} (знос: {wearPercent:F2}%).";
    }
}

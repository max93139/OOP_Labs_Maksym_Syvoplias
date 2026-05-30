namespace Lab6;

/// <summary>
/// Представляє електричне джерело руху з відстеженням заряду батареї та ККД режимів.
/// </summary>
public sealed class Engine
{
    private double batteryStateOfChargePercent;

    /// <summary>
    /// Ініціалізує новий двигун з базовим зарядом батареї.
    /// </summary>
    public Engine(string engineType, int powerKilowatts)
    {
        EngineType = engineType;
        PowerKilowatts = powerKilowatts;
        State = "Stopped";
        batteryStateOfChargePercent = 85.0;
    }

    /// <summary>
    /// Повертає тип двигуна.
    /// </summary>
    public string EngineType { get; }

    /// <summary>
    /// Повертає потужність двигуна в кіловатах.
    /// </summary>
    public int PowerKilowatts { get; }

    /// <summary>
    /// Повертає поточний стан двигуна ("Stopped", "Active" тощо).
    /// </summary>
    public string State { get; private set; }

    /// <summary>
    /// Запускає джерело руху, розраховуючи пускове навантаження на батарею.
    /// </summary>
    public string Start()
    {
        State = "Active";
        double startupEnergyDrawSoC = 0.85;
        batteryStateOfChargePercent = Math.Max(0.0, batteryStateOfChargePercent - startupEnergyDrawSoC);

        string translatedType;
        if (EngineType.Equals("electric", StringComparison.OrdinalIgnoreCase))
        {
            translatedType = "Електричний";
        }
        else
        {
            translatedType = EngineType;
        }

        return $"{translatedType} двигун запущено. Доступна потужність: {PowerKilowatts} кВт. Заряд батареї SoC: {batteryStateOfChargePercent:F1}%.";
    }

    /// <summary>
    /// Зупиняє джерело руху.
    /// </summary>
    public string Stop()
    {
        State = "Stopped";
        return "Двигун зупинено.";
    }

    /// <summary>
    /// Змінює режим роботи двигуна та розраховує його ККД і фактичну потужність.
    /// </summary>
    public string ChangeMode(string state)
    {
        State = state;

        double efficiencyFactor;
        switch (state.ToLowerInvariant())
        {
            case "eco":
            {
                efficiencyFactor = 0.92;
                break;
            }
            case "sport":
            {
                efficiencyFactor = 0.82;
                break;
            }
            case "active":
            {
                efficiencyFactor = 0.88;
                break;
            }
            case "emergency":
            {
                efficiencyFactor = 0.78;
                break;
            }
            default:
            {
                efficiencyFactor = 0.0;
                break;
            }
        }

        double effectivePowerKw = PowerKilowatts * efficiencyFactor;

        string stateString;
        switch (state.ToLowerInvariant())
        {
            case "stopped":
            {
                stateString = "Вимкнено";
                break;
            }
            case "active":
            {
                stateString = "Активний";
                break;
            }
            case "eco":
            {
                stateString = "Eco";
                break;
            }
            case "sport":
            {
                stateString = "Спорт";
                break;
            }
            case "emergency":
            {
                stateString = "Аварійний";
                break;
            }
            default:
            {
                stateString = state;
                break;
            }
        }

        return $"Режим двигуна змінено на {stateString}. Ефективний ККД: {efficiencyFactor:P0} (Ефективна потужність: {effectivePowerKw:F1} кВт).";
    }
}

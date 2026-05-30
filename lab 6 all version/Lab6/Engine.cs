using System;

namespace Lab6;

/// <summary>
/// Представляє електричне джерело руху з відстеженням заряду батареї та ККД режимів.
/// </summary>
public sealed class Engine
{
    private const double DEFAULT_SOC = 85.0;
    private const double STARTUP_ENERGY_DRAW_SOC = 0.85;
    private const double ECO_EFFICIENCY = 0.92;
    private const double SPORT_EFFICIENCY = 0.82;
    private const double ACTIVE_EFFICIENCY = 0.88;
    private const double EMERGENCY_EFFICIENCY = 0.78;

    private string _engineType;
    private int _powerKilowatts;
    private string _state;
    private double _batteryStateOfChargePercent;

    /// <summary>
    /// Конструктор за замовчуванням (Canonical Class Template).
    /// </summary>
    public Engine()
    {
        _engineType = "electric";
        _powerKilowatts = 420;
        _state = "Stopped";
        _batteryStateOfChargePercent = DEFAULT_SOC;
    }

    /// <summary>
    /// Конструктор з повним набором конфігурацій.
    /// </summary>
    public Engine(string engineType, int powerKilowatts)
    {
        _engineType = engineType;
        _powerKilowatts = powerKilowatts;
        _state = "Stopped";
        _batteryStateOfChargePercent = DEFAULT_SOC;
    }

    /// <summary>
    /// Конструктор копіювання (Canonical Class Template).
    /// </summary>
    public Engine(Engine other)
    {
        _engineType = other.EngineType;
        _powerKilowatts = other.PowerKilowatts;
        _state = other.State;
        _batteryStateOfChargePercent = other.BatteryStateOfChargePercent;
    }

    /// <summary>
    /// Повертає тип двигуна.
    /// </summary>
    public string EngineType
    {
        get
        {
            return _engineType;
        }
        set
        {
            _engineType = value;
        }
    }

    /// <summary>
    /// Повертає потужність двигуна в кіловатах.
    /// </summary>
    public int PowerKilowatts
    {
        get
        {
            return _powerKilowatts;
        }
        set
        {
            _powerKilowatts = value;
        }
    }

    /// <summary>
    /// Повертає поточний стан двигуна ("Stopped", "Active" тощо).
    /// </summary>
    public string State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
        }
    }

    /// <summary>
    /// Повертає поточний заряд батареї SoC.
    /// </summary>
    public double BatteryStateOfChargePercent
    {
        get
        {
            return _batteryStateOfChargePercent;
        }
        set
        {
            _batteryStateOfChargePercent = value;
        }
    }

    /// <summary>
    /// Запускає джерело руху, розраховуючи пускове навантаження на батарею.
    /// </summary>
    public string Start()
    {
        _state = "Active";
        _batteryStateOfChargePercent = Math.Max(0.0, _batteryStateOfChargePercent - STARTUP_ENERGY_DRAW_SOC);

        string translatedType;
        if (_engineType.Equals("electric", StringComparison.OrdinalIgnoreCase))
        {
            translatedType = "Електричний";
        }
        else
        {
            translatedType = _engineType;
        }

        return $"{translatedType} двигун запущено. Доступна потужність: {_powerKilowatts} кВт. Заряд батареї SoC: {_batteryStateOfChargePercent:F1}%.";
    }

    /// <summary>
    /// Зупиняє джерело руху.
    /// </summary>
    public string Stop()
    {
        _state = "Stopped";
        return "Двигун зупинено.";
    }

    /// <summary>
    /// Змінює режим роботи двигуна та розраховує його ККД і фактичну потужність.
    /// </summary>
    public string ChangeMode(string state)
    {
        _state = state;

        double efficiencyFactor;
        switch (state.ToLowerInvariant())
        {
            case "eco":
            {
                efficiencyFactor = ECO_EFFICIENCY;
                break;
            }
            case "sport":
            {
                efficiencyFactor = SPORT_EFFICIENCY;
                break;
            }
            case "active":
            {
                efficiencyFactor = ACTIVE_EFFICIENCY;
                break;
            }
            case "emergency":
            {
                efficiencyFactor = EMERGENCY_EFFICIENCY;
                break;
            }
            default:
            {
                efficiencyFactor = 0.0;
                break;
            }
        }

        double effectivePowerKw = _powerKilowatts * efficiencyFactor;

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

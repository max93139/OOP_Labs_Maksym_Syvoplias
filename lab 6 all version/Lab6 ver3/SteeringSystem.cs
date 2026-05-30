using System;

namespace Lab6;

/// <summary>
/// Представляє керування напрямком руху з розрахунком відцентрового прискорення.
/// </summary>
public sealed class SteeringSystem
{
    private string _steeringType;
    private double _sensitivity;
    private int _directionDegrees;
    private double _lateralAccelerationG;

    /// <summary>
    /// Конструктор за замовчуванням (Canonical Class Template).
    /// </summary>
    public SteeringSystem()
    {
        _steeringType = "electronic";
        _sensitivity = 0.92;
        _directionDegrees = 0;
        _lateralAccelerationG = 0.0;
    }

    /// <summary>
    /// Ініціалізує нову систему кермування з фізичною чутливістю.
    /// </summary>
    public SteeringSystem(string steeringType, double sensitivity)
    {
        _steeringType = steeringType;
        _sensitivity = sensitivity;
        _directionDegrees = 0;
        _lateralAccelerationG = 0.0;
    }

    /// <summary>
    /// Конструктор копіювання (Canonical Class Template).
    /// </summary>
    public SteeringSystem(SteeringSystem other)
    {
        _steeringType = other.SteeringType;
        _sensitivity = other.Sensitivity;
        _directionDegrees = other.DirectionDegrees;
        _lateralAccelerationG = other.LateralAccelerationG;
    }

    /// <summary>
    /// Повертає тип кермування.
    /// </summary>
    public string SteeringType
    {
        get
        {
            return _steeringType;
        }
        set
        {
            _steeringType = value;
        }
    }

    /// <summary>
    /// Повертає чутливість кермування.
    /// </summary>
    public double Sensitivity
    {
        get
        {
            return _sensitivity;
        }
        set
        {
            _sensitivity = value;
        }
    }

    /// <summary>
    /// Повертає поточний напрямок.
    /// </summary>
    public int DirectionDegrees
    {
        get
        {
            return _directionDegrees;
        }
        set
        {
            _directionDegrees = value;
        }
    }

    /// <summary>
    /// Повертає бічне прискорення в одиницях G.
    /// </summary>
    public double LateralAccelerationG
    {
        get
        {
            return _lateralAccelerationG;
        }
        set
        {
            _lateralAccelerationG = value;
        }
    }

    /// <summary>
    /// Змінює напрямок руху та оцінює бічні перевантаження для стабілізації траєкторії.
    /// </summary>
    public string ChangeDirection(int directionDegrees)
    {
        _directionDegrees = directionDegrees;

        if (directionDegrees != 0)
        {
            double speedMeterPerSecond = 16.67;
            double radiusMeter = 50.0 / Math.Max(0.01, Math.Abs(Math.Sin(directionDegrees * Math.PI / 180.0)));
            double gravityConstant = 9.81;
            _lateralAccelerationG = ((speedMeterPerSecond * speedMeterPerSecond) / (radiusMeter * gravityConstant)) * _sensitivity;
        }
        else
        {
            _lateralAccelerationG = 0.0;
        }

        string stabilityWarning;
        if (_lateralAccelerationG > 0.8)
        {
            stabilityWarning = "УВАГА: Бічна сила G перевищує безпечну межу! Активовано втручання ESP.";
        }
        else
        {
            stabilityWarning = "Автомобіль стабільний. Нормальне бічне прискорення.";
        }

        return $"Напрямок змінено на {_directionDegrees} градусів. Бічне прискорення: {_lateralAccelerationG:F2}g. {stabilityWarning}";
    }

    /// <summary>
    /// Вмикає керування напрямком через автопілот із калібруванням та поправкою траєкторії.
    /// </summary>
    public string ActivateAutopilot()
    {
        double laneOffsetCentimeters = 1.25;
        double pathPlannedAngleDegrees = 0.45;
        return $"Кермування автопілотом активовано. Планований кут траєкторії: {pathPlannedAngleDegrees:F2}° (відхилення: {laneOffsetCentimeters:F1} см).";
    }
}

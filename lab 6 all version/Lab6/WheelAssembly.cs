using System;

namespace Lab6;

/// <summary>
/// Представляє контактну підсистему руху шасі з адаптацією тиску та коефіцієнта зчеплення.
/// </summary>
public sealed class WheelAssembly
{
    private const double DEFAULT_FRICTION = 0.85;
    private const double GRAVITY_CONSTANT = 9.81;
    private const double CORNERING_RADIUS_METER = 40.0;

    private string _driveType;
    private string _state;
    private double _surfaceFrictionCoefficient;

    /// <summary>
    /// Конструктор за замовчуванням (Canonical Class Template).
    /// </summary>
    public WheelAssembly()
    {
        _driveType = "adaptive all-wheel";
        _state = "Active";
        _surfaceFrictionCoefficient = DEFAULT_FRICTION;
    }

    /// <summary>
    /// Ініціалізує нову ходову частину.
    /// </summary>
    public WheelAssembly(string driveType)
    {
        _driveType = driveType;
        _state = "Active";
        _surfaceFrictionCoefficient = DEFAULT_FRICTION;
    }

    /// <summary>
    /// Конструктор копіювання (Canonical Class Template).
    /// </summary>
    public WheelAssembly(WheelAssembly other)
    {
        _driveType = other.DriveType;
        _state = other.State;
        _surfaceFrictionCoefficient = other.SurfaceFrictionCoefficient;
    }

    /// <summary>
    /// Повертає тип приводу.
    /// </summary>
    public string DriveType
    {
        get
        {
            return _driveType;
        }
        set
        {
            _driveType = value;
        }
    }

    /// <summary>
    /// Повертає поточний стан ходової частини.
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
    /// Повертає коефіцієнт тертя поверхні.
    /// </summary>
    public double SurfaceFrictionCoefficient
    {
        get
        {
            return _surfaceFrictionCoefficient;
        }
        set
        {
            _surfaceFrictionCoefficient = value;
        }
    }

    /// <summary>
    /// Адаптує тиск у шинах та розраховує максимальну безпечну швидкість повороту за типом покриття.
    /// </summary>
    public string AdaptToSurface(string surfaceName)
    {
        _state = "Active";
        string normalizedSurface = surfaceName.ToLowerInvariant();

        if (normalizedSurface.Contains("ice", StringComparison.Ordinal) || 
            normalizedSurface.Contains("від", StringComparison.Ordinal) || 
            normalizedSurface.Contains("ожелед", StringComparison.Ordinal))
        {
            _surfaceFrictionCoefficient = 0.15;
        }
        else if (normalizedSurface.Contains("snow", StringComparison.Ordinal) || 
                 normalizedSurface.Contains("сніг", StringComparison.Ordinal))
        {
            _surfaceFrictionCoefficient = 0.30;
        }
        else if (normalizedSurface.Contains("gravel", StringComparison.Ordinal) || 
                 normalizedSurface.Contains("грав", StringComparison.Ordinal))
        {
            _surfaceFrictionCoefficient = 0.65;
        }
        else if (normalizedSurface.Contains("wet", StringComparison.Ordinal) || 
                 normalizedSurface.Contains("мокр", StringComparison.Ordinal) || 
                 normalizedSurface.Contains("волог", StringComparison.Ordinal))
        {
            _surfaceFrictionCoefficient = 0.55;
        }
        else
        {
            _surfaceFrictionCoefficient = DEFAULT_FRICTION;
        }

        double maximumSafeSpeedKph = Math.Sqrt(_surfaceFrictionCoefficient * GRAVITY_CONSTANT * CORNERING_RADIUS_METER) * 3.6;

        double targetTirePressureBar;
        if (_surfaceFrictionCoefficient < 0.40)
        {
            targetTirePressureBar = 1.9;
        }
        else if (_surfaceFrictionCoefficient < 0.60)
        {
            targetTirePressureBar = 2.1;
        }
        else
        {
            targetTirePressureBar = 2.4;
        }

        return $"Привід {_driveType} адаптовано до покриття \"{surfaceName}\" (зчеплення: {_surfaceFrictionCoefficient:F2}). Цільовий тиск у шинах: {targetTirePressureBar:F1} бар. Безпечна швидкість у повороті: {maximumSafeSpeedKph:F1} км/год.";
    }
}

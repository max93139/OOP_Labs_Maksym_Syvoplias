using System;

namespace Lab6;

/// <summary>
/// Представляє зовнішній каркас автомобіля та прораховує лобовий опір кузова при трансформації.
/// </summary>
public sealed class Body
{
    private const double WATER_FRONTAL_AREA = 1.65;
    private const double WATER_DRAG_COEFF = 0.18;
    private const double AIR_FRONTAL_AREA = 4.20;
    private const double AIR_DRAG_COEFF = 0.08;
    private const double GROUND_FRONTAL_AREA = 2.10;
    private const double GROUND_DRAG_COEFF = 0.22;

    private string _material;
    private string _color;
    private bool _isSealed;
    private string _shape;

    /// <summary>
    /// Конструктор за замовчуванням.
    /// </summary>
    public Body()
    {
        _material = "carbon composite";
        _color = "silver";
        _isSealed = true;
        _shape = "Road capsule";
    }

    /// <summary>
    /// Конструктор з усіма параметрами.
    /// </summary>
    public Body(string material, string color)
    {
        _material = material;
        _color = color;
        _isSealed = true;
        _shape = "Road capsule";
    }

    /// <summary>
    /// Конструктор копіювання.
    /// </summary>
    public Body(Body other)
    {
        _material = other.Material;
        _color = other.Color;
        _isSealed = other.IsSealed;
        _shape = other.Shape;
    }

    public string Material
    {
        get => _material;
        set => _material = value;
    }

    public string Color
    {
        get => _color;
        set => _color = value;
    }

    public bool IsSealed
    {
        get => _isSealed;
        set => _isSealed = value;
    }

    public string Shape
    {
        get => _shape;
        set => _shape = value;
    }

    /// <summary>
    /// Відкриває пасажирські двері та знімає герметизацію салону.
    /// </summary>
    public string OpenDoors()
    {
        _isSealed = false;
        return "Двері відчинено, посадка дозволена.";
    }

    /// <summary>
    /// Змінює форму кузова відповідно до режиму трансформації та розраховує коефіцієнт лобового опору.
    /// </summary>
    public string ChangeShape(string mode)
    {
        double frontalAreaSquareMeters;
        double shapeDragCoefficient;

        if (mode.Equals("Water", StringComparison.OrdinalIgnoreCase))
        {
            _shape = "Гідродинамічна човнова капсула";
            _isSealed = true;
            frontalAreaSquareMeters = WATER_FRONTAL_AREA;
            shapeDragCoefficient = WATER_DRAG_COEFF;
        }
        else if (mode.Equals("Air", StringComparison.OrdinalIgnoreCase))
        {
            _shape = "Крилатий політний модуль";
            _isSealed = true;
            frontalAreaSquareMeters = AIR_FRONTAL_AREA;
            shapeDragCoefficient = AIR_DRAG_COEFF;
        }
        else
        {
            _shape = "Дорожня капсула";
            _isSealed = true;
            frontalAreaSquareMeters = GROUND_FRONTAL_AREA;
            shapeDragCoefficient = GROUND_DRAG_COEFF;
        }

        double dragAreaProduct = frontalAreaSquareMeters * shapeDragCoefficient;
        string sealStatus = _isSealed ? "100% водонепроникний герметичний корпус під тиском" : "негерметичний";

        return $"Форму кузова змінено на {_shape}. Площа лобового перерізу: {frontalAreaSquareMeters:F2} м², коефіцієнт лобового опору (Cd*A): {dragAreaProduct:F3} м². Статус герметизації: {sealStatus}.";
    }
}

using System;
using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Представляє Біо-автомобіль, який здатен до самогенерації у випадку аварії,
/// а також адаптації до користувача за фізичними ознаками, емоціями та станом здоров'я.
/// </summary>
public class BioCar : SmartCar
{
    private double _bioregenerationCapacityPercent;
    private string _biologicalSyncStatus;

    /// <summary>
    /// Конструктор за замовчуванням.
    /// </summary>
    public BioCar() : base()
    {
        _bioregenerationCapacityPercent = 100.0;
        _biologicalSyncStatus = "Fully Synced";
    }

    /// <summary>
    /// Конструктор з усіма параметрами.
    /// </summary>
    public BioCar(
        VehicleIdentity identity,
        string bodyMaterial, string bodyColor,
        string engineType, int enginePower,
        string suspensionType, double chassisMass,
        string driveType, string brakeType, double brakeEfficiency,
        string steeringType, double steeringSensitivity, string transmissionType, int gearCount,
        string transformationMode,
        SmartSystem smartSystem)
        : base(identity, bodyMaterial, bodyColor, engineType, enginePower, suspensionType, chassisMass, driveType, brakeType, brakeEfficiency, steeringType, steeringSensitivity, transmissionType, gearCount, transformationMode, smartSystem)
    {
        _bioregenerationCapacityPercent = 100.0;
        _biologicalSyncStatus = "Fully Synced";
    }

    /// <summary>
    /// Конструктор копіювання.
    /// </summary>
    public BioCar(BioCar other) : base(other)
    {
        _bioregenerationCapacityPercent = other.BioregenerationCapacityPercent;
        _biologicalSyncStatus = other.BiologicalSyncStatus;
    }

    public double BioregenerationCapacityPercent
    {
        get => _bioregenerationCapacityPercent;
        set => _bioregenerationCapacityPercent = value;
    }

    public string BiologicalSyncStatus
    {
        get => _biologicalSyncStatus;
        set => _biologicalSyncStatus = value;
    }

    /// <summary>
    /// Симулює процес біологічної самогенерації обшивки та органічних вузлів автомобіля.
    /// </summary>
    public string PerformBioregeneration()
    {
        _bioregenerationCapacityPercent = 100.0;
        return "Активовано біологічну регенерацію органічного вуглецевого каркаса! Обшивка автомобіля повністю відновлена.";
    }

    /// <summary>
    /// Адаптує атмосферу та ергономіку під біометричний профіль пасажира.
    /// </summary>
    public string AdaptToDriverBiology(double stressLevel, double fatiguePercent)
    {
        if (stressLevel > 50.0 || fatiguePercent > 40.0)
        {
            _biologicalSyncStatus = "Biometric Alert Mode";
            return "Біо-система автомобіля виявила втому пасажира. Активовано режим заспокоєння: викид кисню в салон збільшено на 15%, сидіння переведено в масажний режим.";
        }
        else
        {
            _biologicalSyncStatus = "Optimal Sync";
            return "Біометричний зв'язок стабільний. Налаштування ергономіки оптимальні.";
        }
    }

    public override string GetStatus()
    {
        return $"Біо-автомобіль '{Identity.Model}' функціонує. Рівень біо-регенерації: {_bioregenerationCapacityPercent:F1}%, статус синхронізації: {_biologicalSyncStatus}.";
    }
}

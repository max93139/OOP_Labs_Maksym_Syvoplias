using System;
using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Представляє розумний автомобіль як головний скомпонований об'єкт (True Composition).
/// </summary>
public sealed class SmartCar
{
    private VehicleIdentity _identity;
    private readonly Body _body;
    private readonly Engine _engine;
    private readonly Chassis _chassis;
    private readonly TransformationModule _transformationModule;
    private readonly SmartSystem _smartSystem;

    /// <summary>
    /// Конструктор за замовчуванням (True Composition).
    /// </summary>
    public SmartCar()
    {
        _identity = new VehicleIdentity("SC-2040-01", "Synergy Capsule", 4);
        _body = new Body("carbon composite", "silver");
        _engine = new Engine("electric", 420);
        _chassis = new Chassis("active air suspension", 1830.0, "adaptive all-wheel", "electromagnetic", 96.5, "electronic", 0.92, "електрична двоступенева", 2);
        _transformationModule = new TransformationModule("Ground");
        _smartSystem = new SmartSystem();
    }

    /// <summary>
    /// Конструктор з повним набором конфігурацій для скомпонованих та агрегованих частин (True Composition).
    /// </summary>
    public SmartCar(
        VehicleIdentity identity,
        string bodyMaterial, string bodyColor,
        string engineType, int enginePower,
        string suspensionType, double chassisMass,
        string driveType, string brakeType, double brakeEfficiency,
        string steeringType, double steeringSensitivity, string transmissionType, int gearCount,
        string transformationMode,
        SmartSystem smartSystem)
    {
        _identity = new VehicleIdentity(identity);
        _body = new Body(bodyMaterial, bodyColor);
        _engine = new Engine(engineType, enginePower);
        _chassis = new Chassis(suspensionType, chassisMass, driveType, brakeType, brakeEfficiency, steeringType, steeringSensitivity, transmissionType, gearCount);
        _transformationModule = new TransformationModule(transformationMode);
        _smartSystem = smartSystem; // Агрегація
    }

    /// <summary>
    /// Конструктор копіювання (Глибоке копіювання).
    /// </summary>
    public SmartCar(SmartCar other)
    {
        _identity = new VehicleIdentity(other.Identity);
        _body = new Body(other.BodyComponent);
        _engine = new Engine(other.EngineComponent);
        _chassis = new Chassis(other.ChassisComponent);
        _transformationModule = new TransformationModule(other.TransformationComponent);
        _smartSystem = new SmartSystem(other.SmartSystemComponent);
    }

    public VehicleIdentity Identity
    {
        get => _identity;
        set => _identity = value;
    }

    public Body BodyComponent => _body;
    public Engine EngineComponent => _engine;
    public Chassis ChassisComponent => _chassis;
    public TransformationModule TransformationComponent => _transformationModule;
    public SmartSystem SmartSystemComponent => _smartSystem;

    /// <summary>
    /// Самостійно виконує повний цикл автономного руху за сценарієм, взаємодіючи з підсистемами.
    /// </summary>
    public IReadOnlyList<ScenarioResult> RunAutonomousCycle(ScenarioData data, Service service, List<string> protocolLines)
    {
        List<ScenarioResult> results = new List<ScenarioResult>();

        LogLine("================================================================================", service, protocolLines);
        LogLine($"СЦЕНАРІЙ {data.ScenarioNumber}: {data.ScenarioName}", service, protocolLines);
        LogLine($"АВТОМОБІЛЬ: {_identity.Model} (ID: {_identity.Identifier}, Пасажиромісткість: {_identity.PassengerCapacity})", service, protocolLines);
        LogLine("================================================================================", service, protocolLines);

        // 1. Активація автомобіля
        ExecuteActivation(service, protocolLines);

        // 2. Моніторинг здоров'я водія
        ScenarioResult healthResult = MonitorDriverHealth(service, protocolLines);
        results.Add(healthResult);

        // 3. Прогноз дорожніх ризиків
        ScenarioResult forecastResult = ForecastRisks(data, healthResult.Value, service, protocolLines);
        results.Add(forecastResult);

        // 4. Трансформація кузова
        ExecuteTransformation(data.TransformationMode, service, protocolLines);

        // 5. Голосове керування
        ProcessVoiceInteraction(data.VoiceCommand, service, protocolLines);

        // 6. Балансування клімату
        AdjustCabinClimate(service, protocolLines);

        // 7. Система безпеки та захист
        ApplyActiveSafetyMeasures(data, service, protocolLines);

        // 8. Автопілот та побудова безпечного адаптивного маршруту
        PerformAutonomousNavigation(forecastResult.Value, service, protocolLines);

        // 9. Стабілізація руху шасі з розрахунком передачі трансмісії
        ExecuteChassisStabilization(data.StabilizationSurface, data.VehicleSpeedKmh, service, protocolLines);

        // 10. Самонавчання ШІ
        PerformAILearningCycle(data, service, protocolLines);

        LogLine("", service, protocolLines);
        return results;
    }

    private void ExecuteActivation(Service service, List<string> protocolLines)
    {
        foreach (string line in Activate())
        {
            LogLine(line, service, protocolLines);
        }
    }

    private ScenarioResult MonitorDriverHealth(Service service, List<string> protocolLines)
    {
        IReadOnlyList<SensorReading> readings = _smartSystem.DriverStateSensor.ReadDriverState();
        foreach (SensorReading reading in readings)
        {
            string translatedName = reading.Name switch
            {
                "Pulse" => "Пульс",
                "Blood pressure" => "Артеріальний тиск",
                "Eye fatigue" => "Втома очей",
                _ => reading.Name
            };
            LogLine($"{translatedName}: {reading.Value:F1} {reading.Unit}", service, protocolLines);
        }

        ScenarioResult healthResult = _smartSystem.MonitorDriver();
        LogLine(healthResult.ToProtocolLine(), service, protocolLines);
        return healthResult;
    }

    private ScenarioResult ForecastRisks(ScenarioData data, double healthRiskValue, Service service, List<string> protocolLines)
    {
        ScenarioResult forecastResult = _smartSystem.ForecastRoadRisk(
            healthRiskValue,
            data.RoadCondition,
            data.HasPedestrian,
            data.HasRoadWorks
        );
        LogLine(forecastResult.ToProtocolLine(), service, protocolLines);
        return forecastResult;
    }

    private void ExecuteTransformation(string mode, Service service, List<string> protocolLines)
    {
        foreach (string line in Transform(mode))
        {
            LogLine(line, service, protocolLines);
        }
    }

    private void ProcessVoiceInteraction(string voiceCommand, Service service, List<string> protocolLines)
    {
        LogLine(_smartSystem.HandleVoiceCommand(voiceCommand), service, protocolLines);
    }

    private void AdjustCabinClimate(Service service, List<string> protocolLines)
    {
        LogLine(_smartSystem.BalanceClimate(), service, protocolLines);
    }

    private void ApplyActiveSafetyMeasures(ScenarioData data, Service service, List<string> protocolLines)
    {
        string threatName = data.HasPedestrian ? "загроза зіткнення та втомлений водій" : "мокра дорога та втомлений водій";
        foreach (string line in _smartSystem.ProtectPassengers(threatName))
        {
            LogLine(line, service, protocolLines);
        }
    }

    private void PerformAutonomousNavigation(double riskValue, Service service, List<string> protocolLines)
    {
        foreach (string line in EnableAutopilot(riskValue))
        {
            LogLine(line, service, protocolLines);
        }
    }

    private void ExecuteChassisStabilization(string surface, double speedKmh, Service service, List<string> protocolLines)
    {
        foreach (string line in Stabilize(surface, speedKmh))
        {
            LogLine(line, service, protocolLines);
        }
    }

    private void PerformAILearningCycle(ScenarioData data, Service service, List<string> protocolLines)
    {
        if (data.ScenarioNumber == 3)
        {
            LogLine("Активовано нічний режим польоту.", service, protocolLines);
            LogLine(_smartSystem.SaveExperience(data.CurrentEpisodes, data.NewEpisodes), service, protocolLines);
        }
        else if (data.ScenarioNumber == 2)
        {
            LogLine("Активовано режим сутінків.", service, protocolLines);
            LogLine(_smartSystem.SaveExperience(data.CurrentEpisodes, data.NewEpisodes), service, protocolLines);
        }
        else
        {
            LogLine("Режим день: стандартне освітлення.", service, protocolLines);
            LogLine(_smartSystem.SaveExperience(data.CurrentEpisodes, data.NewEpisodes), service, protocolLines);
        }
    }

    private void LogLine(string line, Service service, List<string> protocolLines)
    {
        service.WriteConsole(line);
        protocolLines.Add(line);
    }

    /// <summary>
    /// Активує автомобіль і готує системи руху, а також виконує початкове калібрування.
    /// </summary>
    public IReadOnlyList<string> Activate()
    {
        var calibrationRisk = _smartSystem.ForecastRoadRisk(0.0);
        return new List<string>
        {
            _body.OpenDoors(),
            _engine.Start(),
            _chassis.ChangeClearance(18.5),
            $"[Калібрування ШІ]: {calibrationRisk.Name} - {calibrationRisk.Value:F1}% ({calibrationRisk.Message})"
        };
    }

    /// <summary>
    /// Трансформує автомобіль через узгодження кузова, двигуна та модуля трансформації.
    /// </summary>
    public IReadOnlyList<string> Transform(string mode)
    {
        return new List<string>
        {
            _transformationModule.ActivateMode(mode),
            _body.ChangeShape(mode),
            _engine.ChangeMode("Eco"),
            _chassis.ShiftGear(mode.Equals("Water", StringComparison.OrdinalIgnoreCase) ? 1 : 2) // Shift transmission gear during mode changes!
        };
    }

    /// <summary>
    /// Вмикає автопілот через взаємодію шасі та smart-системи.
    /// </summary>
    public IReadOnlyList<string> EnableAutopilot(double riskValue)
    {
        return new List<string>
        {
            _chassis.ActivateAutopilot(),
            _smartSystem.BuildRoute(riskValue)
        };
    }

    /// <summary>
    /// Стабілізує рух на вибраному покритті з урахуванням швидкості.
    /// </summary>
    public IReadOnlyList<string> Stabilize(string surfaceName, double speedKmh)
    {
        return _chassis.StabilizeMovement(surfaceName, speedKmh);
    }
}

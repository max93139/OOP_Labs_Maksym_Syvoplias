using System;
using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Представляє розумний автомобіль як головний скомпонований об'єкт.
/// Він успадковує SmartDevice та реалізує взаємодію через потік-безпечні події та делегати (Events & Thread Safety).
/// </summary>
public sealed class SmartCar : SmartDevice
{
    /// <summary>
    /// Делегат для обробки критичних подій розумного автомобіля з передачею контексту.
    /// </summary>
    public delegate void SmartCarEventHandler(object sender, SmartCarEventArgs e);

    private VehicleIdentity _identity;
    private readonly Body _body;
    private readonly Engine _engine;
    private readonly Chassis _chassis;
    private readonly TransformationModule _transformationModule;
    private readonly SmartSystem _smartSystem;

    // Оголошення 5 доменних подій на основі нашого кастомного потік-безпечного делегата
    public event SmartCarEventHandler? OnDriverImpaired;
    public event SmartCarEventHandler? OnCollisionImminent;
    public event SmartCarEventHandler? OnGpsSignalLost;
    public event SmartCarEventHandler? OnDriverMoodChanged;
    public event SmartCarEventHandler? OnSuddenHealthDrop;

    /// <summary>
    /// Конструктор за замовчуванням (True Composition).
    /// </summary>
    public SmartCar() : base("Розумний автомобіль", 1.20)
    {
        _identity = new VehicleIdentity("SC-2040-01", "Synergy Capsule", 4);
        _body = new Body("carbon composite", "silver");
        _engine = new Engine("electric", 420);
        _chassis = new Chassis("active air suspension", 1830.0, "adaptive all-wheel", "electromagnetic", 96.5, "electronic", 0.92, "електрична двоступенева", 2);
        _transformationModule = new TransformationModule("Ground");
        _smartSystem = new SmartSystem();

        RegisterDefaultHandlers();
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
        SmartSystem smartSystem) : base("Розумний автомобіль", 1.20)
    {
        _identity = new VehicleIdentity(identity);
        _body = new Body(bodyMaterial, bodyColor);
        _engine = new Engine(engineType, enginePower);
        _chassis = new Chassis(suspensionType, chassisMass, driveType, brakeType, brakeEfficiency, steeringType, steeringSensitivity, transmissionType, gearCount);
        _transformationModule = new TransformationModule(transformationMode);
        _smartSystem = smartSystem; // Агрегація

        RegisterDefaultHandlers();
    }

    /// <summary>
    /// Конструктор копіювання (Глибоке копіювання).
    /// </summary>
    public SmartCar(SmartCar other) : base(other.DeviceName, other.PowerConsumption)
    {
        _identity = new VehicleIdentity(other.Identity);
        _body = new Body(other.BodyComponent);
        _engine = new Engine(other.EngineComponent);
        _chassis = new Chassis(other.ChassisComponent);
        _transformationModule = new TransformationModule(other.TransformationComponent);
        _smartSystem = new SmartSystem(other.SmartSystemComponent);

        RegisterDefaultHandlers();
    }

    private void RegisterDefaultHandlers()
    {
        OnDriverImpaired += HandleDriverImpairedEvent;
        OnCollisionImminent += HandleCollisionImminentEvent;
        OnGpsSignalLost += HandleGpsSignalLostEvent;
        OnDriverMoodChanged += HandleDriverMoodChangedEvent;
        OnSuddenHealthDrop += HandleSuddenHealthDropEvent;
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
    /// Самостійно виконує повний цикл автономного руху за сценарієм, взаємодіючи з підсистемами, обробляючи винятки та викликаючи події.
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

        // 2. Моніторинг здоров'я водія та перевірка біометрії
        ScenarioResult healthResult = MonitorDriverHealth(data, service, protocolLines);
        results.Add(healthResult);

        // 3. Прогноз дорожніх ризиків
        ScenarioResult forecastResult = ForecastRisks(data, healthResult.Value, service, protocolLines);
        results.Add(forecastResult);

        // 4. Трансформація кузова
        ExecuteTransformation(data, service, protocolLines);

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

    private ScenarioResult MonitorDriverHealth(ScenarioData data, Service service, List<string> protocolLines)
    {
        ScenarioResult healthResult;
        try
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

            // Симулюємо зміну настрою водія при втомі очей більше 30%
            if (data.DriverEyeFatigue >= 30.0)
            {
                OnDriverMoodChanged?.Invoke(this, new SmartCarEventArgs($"Камера розпізнавання обличчя зафіксувала високу втому очей водія ({data.DriverEyeFatigue:F1}%).", service, protocolLines));
            }

            healthResult = _smartSystem.MonitorDriver();
            LogLine(healthResult.ToProtocolLine(), service, protocolLines);
        }
        catch (DriverImpairmentException ex)
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            
            // Викликаємо події раптового колапсу здоров'я та недієздатності
            OnSuddenHealthDrop?.Invoke(this, new SmartCarEventArgs($"Медичні датчики зафіксували небезпечний пульс водія: {data.DriverPulse:F1} bpm.", service, protocolLines));
            OnDriverImpaired?.Invoke(this, new SmartCarEventArgs("Водій повністю недієздатний за медичними показниками!", service, protocolLines));

            healthResult = new ScenarioResult("Медичний моніторинг водія", 100.0, "ЕКСТРЕНИЙ РЕЖИМ: Автопілот активовано примусово!");
        }
        catch (ProfileMismatchException ex)
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[СИСТЕМА БЕЗПЕКИ]: Біометричний профіль не підтверджено! Двигун заблоковано, відправлено запит власнику.", service, protocolLines);
            LogLine(_engine.Stop(), service, protocolLines);
            healthResult = new ScenarioResult("Медичний моніторинг водія", 0.0, "БЛОКУВАННЯ: Профіль водія не відповідає власнику!");
        }
        return healthResult;
    }

    private ScenarioResult ForecastRisks(ScenarioData data, double healthRiskValue, Service service, List<string> protocolLines)
    {
        ScenarioResult forecastResult;
        try
        {
            // Симулюємо неминуче зіткнення при наявності пішохода
            if (data.HasPedestrian)
            {
                OnCollisionImminent?.Invoke(this, new SmartCarEventArgs("Виявлено пішохода безпосередньо перед автомобілем на небезпечній відстані!", service, protocolLines));
            }

            IReadOnlyList<string> objects = _smartSystem.ComputerVisionModule.RecognizeObjects(data.CameraCount, data.RoadCondition, data.HasPedestrian, data.HasRoadWorks);

            forecastResult = _smartSystem.ForecastRoadRisk(
                healthRiskValue,
                data.RoadCondition,
                data.HasPedestrian,
                data.HasRoadWorks
            );
            LogLine(forecastResult.ToProtocolLine(), service, protocolLines);
        }
        catch (ContextInterpretationException ex)
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[КОМП'ЮТЕРНИЙ ЗОР]: Камери забруднено! Переходимо на ультразвукові та радарні датчики резервного сканування.", service, protocolLines);
            forecastResult = new ScenarioResult("Прогноз дорожніх ризиків", 25.0, "РЕЗЕРВНИЙ РЕЖИМ: Сканування через радари активоване.");
        }
        return forecastResult;
    }

    private void ExecuteTransformation(ScenarioData data, Service service, List<string> protocolLines)
    {
        try
        {
            foreach (string line in Transform(data.TransformationMode))
            {
                LogLine(line, service, protocolLines);
            }

            if (data.TransformationMode == "Water" && data.DepthMeters > 2.0)
            {
                LogLine(_transformationModule.ExitWaterMode(data.DepthMeters), service, protocolLines);
            }
        }
        catch (WaterExitDepthException ex)
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[ТРАНСФОРМАЦІЯ]: Спроба відхилена! Гідродинамічний корпус залишається активованим до досягнення мілководдя.", service, protocolLines);
            LogLine(_transformationModule.ActivateMode("Water"), service, protocolLines);
        }
    }

    private void ProcessVoiceInteraction(string voiceCommand, Service service, List<string> protocolLines)
    {
        try
        {
            string cmd = voiceCommand;
            if (cmd.Equals("nonsense", StringComparison.OrdinalIgnoreCase) || cmd.Equals("дурниця", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidVoiceCommandException("Голосовий інтерфейс: Отримано некоректну або невідому команду.");
            }
            if (cmd.Contains("і") || cmd.Contains("and"))
            {
                throw new TooManyCommandsException("Голосовий інтерфейс: Виявлено більше ніж одну паралельну команду в одній фразі!");
            }

            LogLine(_smartSystem.HandleVoiceCommand(cmd), service, protocolLines);
        }
        catch (InvalidVoiceCommandException ex)
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[ГОЛОСОВИЙ АСИСТЕНТ]: Невідома команда. Будь ласка, повторіть ваш запит чіткіше.", service, protocolLines);
        }
        catch (TooManyCommandsException ex)
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[ГОЛОСОВИЙ АСИСТЕНТ]: Отримано кілька паралельних команд. Виконуємо лише першу дію.", service, protocolLines);
        }
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
        try
        {
            double risk = riskValue;
            if (risk > 100.0)
            {
                throw new NavigationConflictException("Критичний конфлікт навігаційних супутників: отримано недостовірний відсоток ризику!");
            }

            foreach (string line in EnableAutopilot(risk))
            {
                LogLine(line, service, protocolLines);
            }
        }
        catch (NavigationConflictException ex)
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            
            // Викликаємо подій втрати зв'язку GPS
            OnGpsSignalLost?.Invoke(this, new SmartCarEventArgs("Навігаційні супутники не відповідають через внутрішній збій синхронізації.", service, protocolLines));

            foreach (string line in EnableAutopilot(50.0))
            {
                LogLine(line, service, protocolLines);
            }
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
        try
        {
            if (data.ScenarioNumber == 3)
            {
                LogLine("Активовано нічний режим польоту.", service, protocolLines);
            }
            else if (data.ScenarioNumber == 2)
            {
                LogLine("Активовано режим сутінків.", service, protocolLines);
            }
            else
            {
                LogLine("Режим день: стандартне освітлення.", service, protocolLines);
            }

            LogLine(_smartSystem.SaveExperience(data.CurrentEpisodes, data.NewEpisodes), service, protocolLines);
        }
        catch (AiModuleFailureException ex)
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[ШТУЧНИЙ ІНТЕЛЕКТ]: Самонавчання призупинено. Відновлюємо попередню стабільну модель нейромережі.", service, protocolLines);
        }
    }

    // Обробники 5 критичних доменних подій, які використовують потік-безпечний SmartCarEventArgs
    private void HandleDriverImpairedEvent(object sender, SmartCarEventArgs e)
    {
        LogLine($"\n[ПОДІЯ - БЛОКУВАННЯ]: {e.Message}", e.LoggingService, e.ProtocolLines);
        LogLine("[ОБРОБНИК ПОДІЇ]: Системи керма повністю заблоковані. Автомобіль рухається виключно в автономному режимі.", e.LoggingService, e.ProtocolLines);
        LogLine(_chassis.ActivateEmergencyBraking(), e.LoggingService, e.ProtocolLines);
        LogLine(_engine.Stop(), e.LoggingService, e.ProtocolLines);
    }

    private void HandleCollisionImminentEvent(object sender, SmartCarEventArgs e)
    {
        LogLine($"\n[ПОДІЯ - РИЗИК ЗІТКНЕННЯ]: {e.Message}", e.LoggingService, e.ProtocolLines);
        LogLine("[ОБРОБНИК ПОДІЇ]: ЕКСТРЕНЕ ГАЛЬМУВАННЯ! Переднатягувачі ременів затягнуті на 250N. Подушки безпеки приведені в секундну готовність. Координати передано екстреним службам.", e.LoggingService, e.ProtocolLines);
        LogLine(_chassis.ActivateEmergencyBraking(), e.LoggingService, e.ProtocolLines);
    }

    private void HandleGpsSignalLostEvent(object sender, SmartCarEventArgs e)
    {
        LogLine($"\n[ПОДІЯ - GPS]: {e.Message}", e.LoggingService, e.ProtocolLines);
        LogLine("[ОБРОБНИК ПОДІЇ]: Зв'язок втрачено. Активовано інерціальний блок орієнтації на базі гіроскопів та резервні офлайн-карти.", e.LoggingService, e.ProtocolLines);
    }

    private void HandleDriverMoodChangedEvent(object sender, SmartCarEventArgs e)
    {
        LogLine($"\n[ПОДІЯ - НАСТРІЙ ВОДІЯ]: {e.Message}", e.LoggingService, e.ProtocolLines);
        LogLine("[ОБРОБНИК ПОДІЇ]: Зміна підсвітки салону на релаксуючу бірюзову та запуск аудіоплейлиста \"Антистрес\" для стабілізації стану водія.", e.LoggingService, e.ProtocolLines);
        LogLine(_smartSystem.EmotionalSupportModule.ActivateChromotherapy(), e.LoggingService, e.ProtocolLines);
        LogLine(_smartSystem.EmotionalSupportModule.TurnOnRelaxMusic(), e.LoggingService, e.ProtocolLines);
    }

    private void HandleSuddenHealthDropEvent(object sender, SmartCarEventArgs e)
    {
        LogLine($"\n[ПОДІЯ - ЗДОРОВ'Я ВОДІЯ]: {e.Message}", e.LoggingService, e.ProtocolLines);
        LogLine("[ОБРОБНИК ПОДІЇ]: Увага! Знижуємо температуру клімату до 20°C, інтенсивність вентиляції збільшена до 50 м³/год, здійснюється безпечне автоматичне паркування на узбіччі та виклик швидкої допомоги.", e.LoggingService, e.ProtocolLines);
        LogLine(_smartSystem.ClimateControlSystem.BalanceClimate(), e.LoggingService, e.ProtocolLines); // Balance air inside
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
            _chassis.ShiftGear(mode.Equals("Water", StringComparison.OrdinalIgnoreCase) ? 1 : 2)
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

    /// <summary>
    /// Повертає поточний статус розумного автомобіля.
    /// </summary>
    public override string GetStatus()
    {
        return $"Розумний автомобіль '{Identity.Model}' функціонує в автономному режимі. Енергоспоживання: {PowerConsumption} кВт.";
    }
}

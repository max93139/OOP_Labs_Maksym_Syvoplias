namespace Lab6;

/// <summary>
/// Агрегує інтелектуальні модулі, які можуть існувати поза моделлю автомобіля.
/// </summary>
public sealed class SmartSystem
{
    private readonly ComputerVisionModule computerVisionModule;
    private readonly DriverHealthDiagnostics driverHealthDiagnostics;
    private readonly PredictionModule predictionModule;
    private readonly VoiceSystem voiceSystem;
    private readonly ClimateControlSystem climateControlSystem;
    private readonly SafetySystem safetySystem;
    private readonly NavigationService navigationService;
    private readonly SelfLearningModule selfLearningModule;
    private readonly DriverStateSensor driverStateSensor; // Агрегація датчика водія

    /// <summary>
    /// Ініціалізує нову smart-систему з агрегованими модулями.
    /// </summary>
    public SmartSystem(
        int autonomyLevel,
        ComputerVisionModule computerVisionModule,
        DriverHealthDiagnostics driverHealthDiagnostics,
        PredictionModule predictionModule,
        VoiceSystem voiceSystem,
        ClimateControlSystem climateControlSystem,
        SafetySystem safetySystem,
        NavigationService navigationService,
        SelfLearningModule selfLearningModule,
        DriverStateSensor driverStateSensor)
    {
        AutonomyLevel = autonomyLevel;
        this.computerVisionModule = computerVisionModule;
        this.driverHealthDiagnostics = driverHealthDiagnostics;
        this.predictionModule = predictionModule;
        this.voiceSystem = voiceSystem;
        this.climateControlSystem = climateControlSystem;
        this.safetySystem = safetySystem;
        this.navigationService = navigationService;
        this.selfLearningModule = selfLearningModule;
        this.driverStateSensor = driverStateSensor;
    }

    /// <summary>
    /// Повертає рівень автономності.
    /// </summary>
    public int AutonomyLevel { get; }

    /// <summary>
    /// Надає доступ до агрегованого датчика стану водія для можливості зчитування поточних показників.
    /// </summary>
    public DriverStateSensor DriverStateSensor
    {
        get
        {
            return driverStateSensor;
        }
    }

    /// <summary>
    /// Виконує частину сценарію з медичним моніторингом, самостійно опитуючи агреговані сенсори здоров'я водія.
    /// </summary>
    public ScenarioResult MonitorDriver()
    {
        IReadOnlyList<SensorReading> readings = driverStateSensor.ReadDriverState();
        double healthRisk = driverHealthDiagnostics.CalculateHealthRisk(readings);
        string recommendation = driverHealthDiagnostics.BuildRecommendation(healthRisk);
        return new ScenarioResult("Медичний моніторинг водія", healthRisk, recommendation);
    }

    /// <summary>
    /// Виконує візуальне розпізнавання та прогноз ризику за замовчуванням.
    /// </summary>
    public ScenarioResult ForecastRoadRisk(double driverRisk)
    {
        return ForecastRoadRisk(driverRisk, "вологий асфальт", true, true);
    }

    /// <summary>
    /// Виконує візуальне розпізнавання та прогноз ризику з урахуванням умов сценарію.
    /// </summary>
    public ScenarioResult ForecastRoadRisk(double driverRisk, string roadCondition, bool hasPedestrian, bool hasRoadWorks)
    {
        IReadOnlyList<string> objects = computerVisionModule.RecognizeObjects(6, roadCondition, hasPedestrian, hasRoadWorks);
        double environmentRisk = computerVisionModule.EstimateEnvironmentRisk(objects);
        double accidentProbability = predictionModule.CalculateAccidentProbability(driverRisk, environmentRisk);
        string forecast = predictionModule.BuildForecast(accidentProbability);
        return new ScenarioResult("Прогноз дорожніх ризиків", accidentProbability, forecast);
    }

    /// <summary>
    /// Обробляє голосову взаємодію як асоціацію зі значенням фрази.
    /// </summary>
    public string HandleVoiceCommand(string phrase)
    {
        string intent = voiceSystem.RecognizeCommand(phrase);
        string localizedIntent = voiceSystem.GetLocalizedIntentName(intent);
        return voiceSystem.Speak($"Розпізнано команду \"{localizedIntent}\".");
    }

    /// <summary>
    /// Балансує клімат за допомогою агрегованих кліматичних сенсорів.
    /// </summary>
    public string BalanceClimate()
    {
        return climateControlSystem.BalanceClimate();
    }

    /// <summary>
    /// Активує захисні дії через підсистему безпеки.
    /// </summary>
    public IReadOnlyList<string> ProtectPassengers(string threatName)
    {
        return safetySystem.ActivateProtection(threatName);
    }

    /// <summary>
    /// Формує рекомендацію маршруту.
    /// </summary>
    public string BuildRoute(double accidentProbability)
    {
        return navigationService.BuildAdaptiveRoute(accidentProbability);
    }

    /// <summary>
    /// Оновлює модель поведінки ШІ.
    /// </summary>
    public string SaveExperience(int currentEpisodeCount, int newEpisodeCount)
    {
        int episodeCount = selfLearningModule.UpdateModel(currentEpisodeCount, newEpisodeCount);
        return selfLearningModule.BuildUpdateMessage(episodeCount);
    }
}

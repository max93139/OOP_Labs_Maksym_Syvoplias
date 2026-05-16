namespace Lab6.Domain;

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
        SelfLearningModule selfLearningModule)
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
    }

    /// <summary>
    /// Повертає рівень автономності.
    /// </summary>
    public int AutonomyLevel { get; }

    /// <summary>
    /// Виконує частину сценарію з медичним моніторингом.
    /// </summary>
    public ScenarioResult MonitorDriver(IReadOnlyList<SensorReading> readings)
    {
        double healthRisk = driverHealthDiagnostics.CalculateHealthRisk(readings);
        string recommendation = driverHealthDiagnostics.BuildRecommendation(healthRisk);
        return new ScenarioResult("Driver medical monitoring", healthRisk, recommendation);
    }

    /// <summary>
    /// Виконує візуальне розпізнавання та прогноз ризику.
    /// </summary>
    public ScenarioResult ForecastRoadRisk(double driverRisk)
    {
        IReadOnlyList<string> objects = computerVisionModule.RecognizeObjects(6);
        double environmentRisk = computerVisionModule.EstimateEnvironmentRisk(objects);
        double accidentProbability = predictionModule.CalculateAccidentProbability(driverRisk, environmentRisk);
        string forecast = predictionModule.BuildForecast(accidentProbability);
        return new ScenarioResult("Road risk forecast", accidentProbability, forecast);
    }

    /// <summary>
    /// Обробляє голосову взаємодію як асоціацію зі значенням фрази.
    /// </summary>
    public string HandleVoiceCommand(string phrase)
    {
        CommandIntent intent = voiceSystem.RecognizeCommand(phrase);
        return voiceSystem.Speak($"Recognized command {intent}.");
    }

    /// <summary>
    /// Балансує клімат за допомогою агрегованих кліматичних сенсорів.
    /// </summary>
    public string BalanceClimate()
    {
        return climateControlSystem.BalanceClimate();
    }

    /// <summary>
    /// Активує захисні дії.
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

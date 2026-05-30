using System;
using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Агрегує інтелектуальні модулі, які можуть існувати поза автомобілем (SRP/Coordinator).
/// </summary>
public sealed class SmartSystem
{
    private int _autonomyLevel;
    
    // Composed modules (aggregated in the system)
    private ComputerVisionModule _computerVisionModule;
    private DriverHealthDiagnostics _driverHealthDiagnostics;
    private PredictionModule _predictionModule;
    private VoiceSystem _voiceSystem;
    private ClimateControlSystem _climateControlSystem;
    private SafetySystem _safetySystem;
    private NavigationModule _navigationModule;
    private SelfLearningModule _selfLearningModule;
    private DriverStateSensor _driverStateSensor;

    // Table 6.2 New modules
    private AIModule _aiModule;
    private SpeechRecognitionModule _speechRecognitionModule;
    private RecommendationModule _recommendationModule;
    private EmotionalSupportModule _emotionalSupportModule;

    /// <summary>
    /// Конструктор за замовчуванням.
    /// </summary>
    public SmartSystem()
    {
        _autonomyLevel = 5;
        _computerVisionModule = new ComputerVisionModule();
        _driverHealthDiagnostics = new DriverHealthDiagnostics();
        _predictionModule = new PredictionModule();
        _voiceSystem = new VoiceSystem();
        _climateControlSystem = new ClimateControlSystem();
        _safetySystem = new SafetySystem();
        _navigationModule = new NavigationModule();
        _selfLearningModule = new SelfLearningModule();
        _driverStateSensor = new DriverStateSensor();

        _aiModule = new AIModule();
        _speechRecognitionModule = new SpeechRecognitionModule();
        _recommendationModule = new RecommendationModule();
        _emotionalSupportModule = new EmotionalSupportModule();
    }

    /// <summary>
    /// Конструктор з усіма залежностями.
    /// </summary>
    public SmartSystem(
        int autonomyLevel,
        ComputerVisionModule computerVisionModule,
        DriverHealthDiagnostics driverHealthDiagnostics,
        PredictionModule predictionModule,
        VoiceSystem voiceSystem,
        ClimateControlSystem climateControlSystem,
        SafetySystem safetySystem,
        NavigationModule navigationModule,
        SelfLearningModule selfLearningModule,
        DriverStateSensor driverStateSensor,
        AIModule aiModule,
        SpeechRecognitionModule speechRecognitionModule,
        RecommendationModule recommendationModule,
        EmotionalSupportModule emotionalSupportModule)
    {
        _autonomyLevel = autonomyLevel;
        _computerVisionModule = computerVisionModule;
        _driverHealthDiagnostics = driverHealthDiagnostics;
        _predictionModule = predictionModule;
        _voiceSystem = voiceSystem;
        _climateControlSystem = climateControlSystem;
        _safetySystem = safetySystem;
        _navigationModule = navigationModule;
        _selfLearningModule = selfLearningModule;
        _driverStateSensor = driverStateSensor;

        _aiModule = aiModule;
        _speechRecognitionModule = speechRecognitionModule;
        _recommendationModule = recommendationModule;
        _emotionalSupportModule = emotionalSupportModule;
    }

    /// <summary>
    /// Конструктор копіювання.
    /// </summary>
    public SmartSystem(SmartSystem other)
    {
        _autonomyLevel = other.AutonomyLevel;
        _computerVisionModule = new ComputerVisionModule(other.ComputerVisionModule);
        _driverHealthDiagnostics = new DriverHealthDiagnostics(other.DriverHealthDiagnostics);
        _predictionModule = new PredictionModule(other.PredictionModule);
        _voiceSystem = new VoiceSystem(other.VoiceSystem);
        _climateControlSystem = new ClimateControlSystem(other.ClimateControlSystem);
        _safetySystem = new SafetySystem(other.SafetySystem);
        _navigationModule = new NavigationModule(other.NavigationModule);
        _selfLearningModule = new SelfLearningModule(other.SelfLearningModule);
        _driverStateSensor = new DriverStateSensor(other.DriverStateSensor);

        _aiModule = new AIModule(other.AIModule);
        _speechRecognitionModule = new SpeechRecognitionModule(other.SpeechRecognitionModule);
        _recommendationModule = new RecommendationModule(other.RecommendationModule);
        _emotionalSupportModule = new EmotionalSupportModule(other.EmotionalSupportModule);
    }

    public int AutonomyLevel
    {
        get => _autonomyLevel;
        set => _autonomyLevel = value;
    }

    public ComputerVisionModule ComputerVisionModule
    {
        get => _computerVisionModule;
        set => _computerVisionModule = value;
    }

    public DriverHealthDiagnostics DriverHealthDiagnostics
    {
        get => _driverHealthDiagnostics;
        set => _driverHealthDiagnostics = value;
    }

    public PredictionModule PredictionModule
    {
        get => _predictionModule;
        set => _predictionModule = value;
    }

    public VoiceSystem VoiceSystem
    {
        get => _voiceSystem;
        set => _voiceSystem = value;
    }

    public ClimateControlSystem ClimateControlSystem
    {
        get => _climateControlSystem;
        set => _climateControlSystem = value;
    }

    public SafetySystem SafetySystem
    {
        get => _safetySystem;
        set => _safetySystem = value;
    }

    public NavigationModule NavigationModule
    {
        get => _navigationModule;
        set => _navigationModule = value;
    }

    public SelfLearningModule SelfLearningModule
    {
        get => _selfLearningModule;
        set => _selfLearningModule = value;
    }

    public DriverStateSensor DriverStateSensor
    {
        get => _driverStateSensor;
        set => _driverStateSensor = value;
    }

    public AIModule AIModule
    {
        get => _aiModule;
        set => _aiModule = value;
    }

    public SpeechRecognitionModule SpeechRecognitionModule
    {
        get => _speechRecognitionModule;
        set => _speechRecognitionModule = value;
    }

    public RecommendationModule RecommendationModule
    {
        get => _recommendationModule;
        set => _recommendationModule = value;
    }

    public EmotionalSupportModule EmotionalSupportModule
    {
        get => _emotionalSupportModule;
        set => _emotionalSupportModule = value;
    }

    /// <summary>
    /// Виконує частину сценарію з медичним моніторингом водія.
    /// </summary>
    public ScenarioResult MonitorDriver()
    {
        IReadOnlyList<SensorReading> readings = _driverStateSensor.ReadDriverState();
        double healthRisk = _driverHealthDiagnostics.CalculateHealthRisk(readings);
        string recommendation = _driverHealthDiagnostics.BuildRecommendation(healthRisk);
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
        // 6 is default camera count
        IReadOnlyList<string> objects = _computerVisionModule.RecognizeObjects(6, roadCondition, hasPedestrian, hasRoadWorks);
        double environmentRisk = _computerVisionModule.EstimateEnvironmentRisk(objects);
        double accidentProbability = _predictionModule.CalculateAccidentProbability(driverRisk, environmentRisk);
        string forecast = _predictionModule.BuildForecast(accidentProbability);
        return new ScenarioResult("Прогноз дорожніх ризиків", accidentProbability, forecast);
    }

    /// <summary>
    /// Обробляє голосову взаємодію за допомогою SpeechRecognitionModule та EmotionalSupportModule.
    /// </summary>
    public string HandleVoiceCommand(string phrase)
    {
        double confidence;
        string intent = _speechRecognitionModule.RecognizeCommand(phrase, out confidence);
        string localizedIntent = _voiceSystem.GetLocalizedIntentName(intent);

        // Calculate emotion based on phrase characteristics or simulated stress
        double stress = confidence < 0.60 ? 65.0 : 15.0;
        string emotionAdvice = _emotionalSupportModule.SuggestAdvice(stress);

        string response = _voiceSystem.Speak($"Розпізнано команду \"{localizedIntent}\". Рекомендація: {emotionAdvice}");
        return response;
    }

    /// <summary>
    /// Балансує клімат за допомогою агрегованих кліматичних сенсорів.
    /// </summary>
    public string BalanceClimate()
    {
        return _climateControlSystem.BalanceClimate();
    }

    /// <summary>
    /// Активує захисні дії через підсистему безпеки.
    /// </summary>
    public IReadOnlyList<string> ProtectPassengers(string threatName)
    {
        return _safetySystem.ActivateProtection(threatName);
    }

    /// <summary>
    /// Формує рекомендацію маршруту через NavigationModule та RecommendationModule.
    /// </summary>
    public string BuildRoute(double accidentProbability)
    {
        string routeDetails = _navigationModule.BuildAdaptiveRoute(accidentProbability);
        string recommendation = _recommendationModule.SuggestRoute(accidentProbability);
        return $"{routeDetails} {recommendation}";
    }

    /// <summary>
    /// Оновлює модель поведінки ШІ за допомогою AIModule.
    /// </summary>
    public string SaveExperience(int currentEpisodeCount, int newEpisodeCount)
    {
        int episodeCount = _aiModule.UpdateModel(currentEpisodeCount, newEpisodeCount);
        return $"[Штучний Інтелект]: Успішно оновлено нейронну мережу. Загальна кількість пройдених епізодів навчання: {episodeCount}.";
    }
}

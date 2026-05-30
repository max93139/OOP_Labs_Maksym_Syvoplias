namespace Lab6;

/// <summary>
/// Представляє легковажний пасивний контейнер даних (DTO) для показників датчиків сценарію.
/// Цей клас не містить логіки та не має залежностей від інших систем.
/// </summary>
public sealed class ScenarioData
{
    /// <summary>
    /// Ініціалізує новий об'єкт ScenarioData з параметрами за замовчуванням.
    /// </summary>
    public ScenarioData()
    {
        ScenarioName = string.Empty;
        TransformationMode = string.Empty;
        RoadCondition = string.Empty;
        StabilizationSurface = string.Empty;
        VoiceCommand = string.Empty;
    }

    /// <summary>
    /// Ініціалізує новий об'єкт ScenarioData з усіма необхідними даними датчиків.
    /// </summary>
    public ScenarioData(
        int scenarioNumber,
        string scenarioName,
        string transformationMode,
        string roadCondition,
        string stabilizationSurface,
        bool hasPedestrian,
        bool hasRoadWorks,
        string voiceCommand,
        double vehicleSpeedKmh,
        double driverPulse,
        double driverPressure,
        double driverEyeFatigue,
        double smartSystemMaxRisk,
        double smartSystemSensitivity,
        double smartSystemMaxPower)
    {
        ScenarioNumber = scenarioNumber;
        ScenarioName = scenarioName;
        TransformationMode = transformationMode;
        RoadCondition = roadCondition;
        StabilizationSurface = stabilizationSurface;
        HasPedestrian = hasPedestrian;
        HasRoadWorks = hasRoadWorks;
        VoiceCommand = voiceCommand;
        VehicleSpeedKmh = vehicleSpeedKmh;
        DriverPulse = driverPulse;
        DriverPressure = driverPressure;
        DriverEyeFatigue = driverEyeFatigue;
        SmartSystemMaxRisk = smartSystemMaxRisk;
        SmartSystemSensitivity = smartSystemSensitivity;
        SmartSystemMaxPower = smartSystemMaxPower;
    }

    /// <summary>
    /// Отримує або встановлює порядковий номер сценарію.
    /// </summary>
    public int ScenarioNumber { get; set; }

    /// <summary>
    /// Отримує або встановлює назву сценарію.
    /// </summary>
    public string ScenarioName { get; set; } = string.Empty;

    /// <summary>
    /// Отримує або встановлює режим трансформації ("Ground", "Water", "Air").
    /// </summary>
    public string TransformationMode { get; set; } = string.Empty;

    /// <summary>
    /// Отримує або встановлює стан дорожнього покриття.
    /// </summary>
    public string RoadCondition { get; set; } = string.Empty;

    /// <summary>
    /// Отримує або встановлює поверхню стабілізації руху.
    /// </summary>
    public string StabilizationSurface { get; set; } = string.Empty;

    /// <summary>
    /// Отримує або встановлює значення, яке вказує на наявність пішохода.
    /// </summary>
    public bool HasPedestrian { get; set; }

    /// <summary>
    /// Отримує або встановлює значення, яке вказує на наявність дорожніх робіт.
    /// </summary>
    public bool HasRoadWorks { get; set; }

    /// <summary>
    /// Отримує або встановлює розпізнану голосову команду.
    /// </summary>
    public string VoiceCommand { get; set; } = string.Empty;

    /// <summary>
    /// Отримує або встановлює швидкість автомобіля в км/год з датчика швидкості.
    /// </summary>
    public double VehicleSpeedKmh { get; set; }

    /// <summary>
    /// Отримує або встановлює пульс водія.
    /// </summary>
    public double DriverPulse { get; set; }

    /// <summary>
    /// Отримує або встановлює артеріальний тиск водія.
    /// </summary>
    public double DriverPressure { get; set; }

    /// <summary>
    /// Отримує або встановлює рівень втоми очей водія у відсотках.
    /// </summary>
    public double DriverEyeFatigue { get; set; }

    /// <summary>
    /// Отримує або встановлює максимальний допустимий рівень дорожнього ризику для смарт-системи.
    /// </summary>
    public double SmartSystemMaxRisk { get; set; }

    /// <summary>
    /// Отримує або встановлює чутливість датчиків смарт-системи.
    /// </summary>
    public double SmartSystemSensitivity { get; set; }

    /// <summary>
    /// Отримує або встановлює максимальну потужність електросистеми.
    /// </summary>
    public double SmartSystemMaxPower { get; set; }
}

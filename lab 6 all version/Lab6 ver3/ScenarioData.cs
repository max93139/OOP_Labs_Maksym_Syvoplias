namespace Lab6;

/// <summary>
/// Представляє легковажний пасивний контейнер даних (DTO) для показників датчиків сценарію.
/// </summary>
public sealed class ScenarioData
{
    private int _scenarioNumber;
    private string _scenarioName = string.Empty;
    private string _transformationMode = string.Empty;
    private string _roadCondition = string.Empty;
    private string _stabilizationSurface = string.Empty;
    private bool _hasPedestrian;
    private bool _hasRoadWorks;
    private string _voiceCommand = string.Empty;
    private double _vehicleSpeedKmh;
    private double _driverPulse;
    private double _driverPressure;
    private double _driverEyeFatigue;
    private double _smartSystemMaxRisk;
    private double _smartSystemSensitivity;
    private double _smartSystemMaxPower;

    // Нові доменні семантичні параметри
    private int _cameraCount;
    private double _depthMeters;
    private int _currentEpisodes;
    private int _newEpisodes;

    /// <summary>
    /// Конструктор за замовчуванням.
    /// </summary>
    public ScenarioData()
    {
        _scenarioName = string.Empty;
        _transformationMode = string.Empty;
        _roadCondition = string.Empty;
        _stabilizationSurface = string.Empty;
        _voiceCommand = string.Empty;
        _cameraCount = 6;
        _depthMeters = 0.0;
        _currentEpisodes = 5;
        _newEpisodes = 5;
    }

    /// <summary>
    /// Конструктор з усіма параметрами.
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
        double smartSystemMaxPower,
        int cameraCount,
        double depthMeters,
        int currentEpisodes,
        int newEpisodes)
    {
        _scenarioNumber = scenarioNumber;
        _scenarioName = scenarioName;
        _transformationMode = transformationMode;
        _roadCondition = roadCondition;
        _stabilizationSurface = stabilizationSurface;
        _hasPedestrian = hasPedestrian;
        _hasRoadWorks = hasRoadWorks;
        _voiceCommand = voiceCommand;
        _vehicleSpeedKmh = vehicleSpeedKmh;
        _driverPulse = driverPulse;
        _driverPressure = driverPressure;
        _driverEyeFatigue = driverEyeFatigue;
        _smartSystemMaxRisk = smartSystemMaxRisk;
        _smartSystemSensitivity = smartSystemSensitivity;
        _smartSystemMaxPower = smartSystemMaxPower;
        _cameraCount = cameraCount;
        _depthMeters = depthMeters;
        _currentEpisodes = currentEpisodes;
        _newEpisodes = newEpisodes;
    }

    /// <summary>
    /// Конструктор копіювання.
    /// </summary>
    public ScenarioData(ScenarioData other)
    {
        _scenarioNumber = other.ScenarioNumber;
        _scenarioName = other.ScenarioName;
        _transformationMode = other.TransformationMode;
        _roadCondition = other.RoadCondition;
        _stabilizationSurface = other.StabilizationSurface;
        _hasPedestrian = other.HasPedestrian;
        _hasRoadWorks = other.HasRoadWorks;
        _voiceCommand = other.VoiceCommand;
        _vehicleSpeedKmh = other.VehicleSpeedKmh;
        _driverPulse = other.DriverPulse;
        _driverPressure = other.DriverPressure;
        _driverEyeFatigue = other.DriverEyeFatigue;
        _smartSystemMaxRisk = other.SmartSystemMaxRisk;
        _smartSystemSensitivity = other.SmartSystemSensitivity;
        _smartSystemMaxPower = other.SmartSystemMaxPower;
        _cameraCount = other.CameraCount;
        _depthMeters = other.DepthMeters;
        _currentEpisodes = other.CurrentEpisodes;
        _newEpisodes = other.NewEpisodes;
    }

    public int ScenarioNumber { get => _scenarioNumber; set => _scenarioNumber = value; }
    public string ScenarioName { get => _scenarioName; set => _scenarioName = value; }
    public string TransformationMode { get => _transformationMode; set => _transformationMode = value; }
    public string RoadCondition { get => _roadCondition; set => _roadCondition = value; }
    public string StabilizationSurface { get => _stabilizationSurface; set => _stabilizationSurface = value; }
    public bool HasPedestrian { get => _hasPedestrian; set => _hasPedestrian = value; }
    public bool HasRoadWorks { get => _hasRoadWorks; set => _hasRoadWorks = value; }
    public string VoiceCommand { get => _voiceCommand; set => _voiceCommand = value; }
    public double VehicleSpeedKmh { get => _vehicleSpeedKmh; set => _vehicleSpeedKmh = value; }
    public double DriverPulse { get => _driverPulse; set => _driverPulse = value; }
    public double DriverPressure { get => _driverPressure; set => _driverPressure = value; }
    public double DriverEyeFatigue { get => _driverEyeFatigue; set => _driverEyeFatigue = value; }
    public double SmartSystemMaxRisk { get => _smartSystemMaxRisk; set => _smartSystemMaxRisk = value; }
    public double SmartSystemSensitivity { get => _smartSystemSensitivity; set => _smartSystemSensitivity = value; }
    public double SmartSystemMaxPower { get => _smartSystemMaxPower; set => _smartSystemMaxPower = value; }
    public int CameraCount { get => _cameraCount; set => _cameraCount = value; }
    public double DepthMeters { get => _depthMeters; set => _depthMeters = value; }
    public int CurrentEpisodes { get => _currentEpisodes; set => _currentEpisodes = value; }
    public int NewEpisodes { get => _newEpisodes; set => _newEpisodes = value; }
}

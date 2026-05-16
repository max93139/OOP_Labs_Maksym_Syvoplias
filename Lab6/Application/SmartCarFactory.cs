using Lab6.Domain;

namespace Lab6.Application;

/// <summary>
/// Створює налаштований демонстраційний граф об'єктів розумного автомобіля.
/// </summary>
public sealed class SmartCarFactory
{
    /// <summary>
    /// Створює розумний автомобіль з модулями для композиції, агрегації та асоціації.
    /// </summary>
    public SmartCar CreateSmartCar()
    {
        Body body = new Body("carbon composite", "silver");
        Engine engine = new Engine("hybrid electric", 420);
        Chassis chassis = CreateChassis();
        SmartSystem smartSystem = CreateSmartSystem();
        TransformationModule transformationModule = new TransformationModule(TransformationMode.Ground);
        VehicleIdentity identity = new VehicleIdentity("SC-2040-01", "Synergy Capsule", 4);

        return new SmartCar(identity, body, engine, chassis, transformationModule, smartSystem);
    }

    /// <summary>
    /// Створює агрегований набір сенсорів стану водія.
    /// </summary>
    public DriverStateSensor CreateDriverStateSensor()
    {
        return new DriverStateSensor(new List<ISensor>
        {
            new FixedSensor("Pulse", 112.0, "bpm"),
            new FixedSensor("Blood pressure", 138.0, "mmHg"),
            new FixedSensor("Eye fatigue", 42.0, "%")
        });
    }

    /// <summary>
    /// Створює smart-систему, яка використовується у сценарії програми.
    /// </summary>
    public SmartSystem CreateSmartSystem()
    {
        ClimateControlSystem climateControlSystem = new ClimateControlSystem(new List<ISensor>
        {
            new FixedSensor("Temperature", 25.0, "C"),
            new FixedSensor("Humidity", 58.0, "%"),
            new FixedSensor("CO2", 720.0, "ppm")
        });

        return new SmartSystem(
            5,
            new ComputerVisionModule(),
            new DriverHealthDiagnostics(),
            new PredictionModule(),
            new VoiceSystem(),
            climateControlSystem,
            new SafetySystem(),
            new NavigationService(),
            new SelfLearningModule());
    }

    private Chassis CreateChassis()
    {
        WheelAssembly wheelAssembly = new WheelAssembly("adaptive all-wheel");
        BrakeSystem brakeSystem = new BrakeSystem("electromagnetic", 96.5);
        SteeringSystem steeringSystem = new SteeringSystem("electronic", 0.92);
        Transmission transmission = new Transmission("adaptive automatic", 8);

        return new Chassis(
            "active air suspension",
            1830.0,
            wheelAssembly,
            brakeSystem,
            steeringSystem,
            transmission);
    }
}

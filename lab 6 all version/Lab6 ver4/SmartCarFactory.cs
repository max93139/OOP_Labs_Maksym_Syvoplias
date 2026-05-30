using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Створює налаштований демонстраційний граф об'єктів розумного автомобіля.
/// </summary>
public sealed class SmartCarFactory
{
    /// <summary>
    /// Створює розумний автомобіль з модулями, налаштованими під конкретні показання датчиків сценарію.
    /// </summary>
    public SmartCar CreateSmartCar(ScenarioData data)
    {
        Body body = new Body("carbon composite", "silver");
        Engine engine = new Engine("electric", 420);
        Chassis chassis = CreateChassis();

        DriverStateSensor driverStateSensor = CreateDriverStateSensor(data.DriverPulse, data.DriverPressure, data.DriverEyeFatigue);
        SmartSystem smartSystem = CreateSmartSystem(
            data.SmartSystemMaxRisk,
            data.SmartSystemSensitivity,
            data.SmartSystemMaxPower,
            driverStateSensor
        );

        TransformationModule transformationModule = new TransformationModule("Ground");
        VehicleIdentity identity = new VehicleIdentity("SC-2040-01", "Synergy Capsule", 4);

        return new SmartCar(identity, body, engine, chassis, transformationModule, smartSystem);
    }

    /// <summary>
    /// Створює агрегований набір сенсорів стану водія із заданими параметрами.
    /// </summary>
    public DriverStateSensor CreateDriverStateSensor(double pulse, double pressure, double fatigue)
    {
        return new DriverStateSensor(new List<ISensor>
        {
            new FixedSensor("Pulse", pulse, "bpm"),
            new FixedSensor("Blood pressure", pressure, "mmHg"),
            new FixedSensor("Eye fatigue", fatigue, "%")
        });
    }

    /// <summary>
    /// Створює smart-систему із заданими параметрами клімату та агрегованим датчиком водія.
    /// </summary>
    public SmartSystem CreateSmartSystem(double temperature, double humidity, double co2, DriverStateSensor driverStateSensor)
    {
        ClimateControlSystem climateControlSystem = new ClimateControlSystem(new List<ISensor>
        {
            new FixedSensor("Temperature", temperature, "C"),
            new FixedSensor("Humidity", humidity, "%"),
            new FixedSensor("CO2", co2, "ppm")
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
            new SelfLearningModule(),
            driverStateSensor);
    }

    private Chassis CreateChassis()
    {
        WheelAssembly wheelAssembly = new WheelAssembly("adaptive all-wheel");
        BrakeSystem brakeSystem = new BrakeSystem("electromagnetic", 96.5);
        SteeringSystem steeringSystem = new SteeringSystem("electronic", 0.92);
        Transmission transmission = new Transmission("електрична двоступенева", 2);

        return new Chassis(
            "active air suspension",
            1830.0,
            wheelAssembly,
            brakeSystem,
            steeringSystem,
            transmission);
    }
}

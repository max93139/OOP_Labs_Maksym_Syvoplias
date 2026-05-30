using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Створює налаштований демонстраційний граф об'єктів розумного автомобіля з використанням конфігураційних параметрів.
/// </summary>
public sealed class SmartCarFactory
{
    private string _factoryName;

    /// <summary>
    /// Конструктор за замовчуванням.
    /// </summary>
    public SmartCarFactory()
    {
        _factoryName = "Центральна фабрика розумних автомобілів";
    }

    /// <summary>
    /// Конструктор копіювання.
    /// </summary>
    public SmartCarFactory(SmartCarFactory other)
    {
        _factoryName = other.FactoryName;
    }

    public string FactoryName
    {
        get => _factoryName;
        set => _factoryName = value;
    }

    /// <summary>
    /// Створює розумний автомобіль з модулями. Демонструє принцип підстановки Лісков (LSP), повертаючи похідний BioCar.
    /// </summary>
    public SmartCar CreateSmartCar(ScenarioData data)
    {
        DriverStateSensor driverStateSensor = CreateDriverStateSensor(data.DriverPulse, data.DriverPressure, data.DriverEyeFatigue);
        SmartSystem smartSystem = CreateSmartSystem(
            data.SmartSystemMaxRisk,
            data.SmartSystemSensitivity,
            data.SmartSystemMaxPower,
            driverStateSensor
        );

        VehicleIdentity identity = new VehicleIdentity("SC-2040-01", "Synergy Capsule", 4);

        // Повертаємо Біо-автомобіль (BioCar), який успадковує SmartCar!
        return new BioCar(
            identity,
            "carbon composite", "silver", // кузов
            "electric", 420,             // двигун
            "active air suspension", 1830.0, // шасі
            "adaptive all-wheel", "electromagnetic", 96.5,
            "electronic", 0.92,
            "електрична двоступенева", 2,
            "Ground",
            smartSystem
        );
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
            new NavigationModule(),
            new SelfLearningModule(),
            driverStateSensor,
            new AIModule(),
            new SpeechRecognitionModule(),
            new RecommendationModule(),
            new EmotionalSupportModule()
        );
    }
}

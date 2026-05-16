using Lab6.Domain;
using Lab6.Infrastructure;

namespace Lab6.Application;

/// <summary>
/// Координує лабораторний сценарій роботи розумного автомобіля.
/// </summary>
public sealed class SmartCarApplication
{
    private readonly SmartCarFactory smartCarFactory;
    private readonly Service service;

    /// <summary>
    /// Ініціалізує програму.
    /// </summary>
    public SmartCarApplication()
    {
        smartCarFactory = new SmartCarFactory();
        service = new Service("Output");
    }

    /// <summary>
    /// Запускає повний сценарій і зберігає потрібні файли.
    /// </summary>
    public void Run()
    {
        SmartCar smartCar = smartCarFactory.CreateSmartCar();
        SmartSystem smartSystem = smartCarFactory.CreateSmartSystem();
        DriverStateSensor driverStateSensor = smartCarFactory.CreateDriverStateSensor();
        IReadOnlyList<ScenarioResult> results = RunScenario(smartCar, smartSystem, driverStateSensor);

        service.SaveCalculatedValues(results);
        service.SaveProtocol();
    }

    private IReadOnlyList<ScenarioResult> RunScenario(
        SmartCar smartCar,
        SmartSystem smartSystem,
        DriverStateSensor driverStateSensor)
    {
        List<ScenarioResult> results = new List<ScenarioResult>();

        service.WriteProtocolLine($"Smart car {smartCar.Identity.Model} ({smartCar.Identity.Identifier}) started.");
        service.WriteProtocolLines(smartCar.Activate());

        IReadOnlyList<SensorReading> driverReadings = driverStateSensor.ReadDriverState();
        WriteSensorReadings(driverReadings);

        ScenarioResult healthResult = smartSystem.MonitorDriver(driverReadings);
        results.Add(healthResult);
        service.WriteProtocolLine(healthResult.ToProtocolLine());

        ScenarioResult forecastResult = smartSystem.ForecastRoadRisk(healthResult.Value);
        results.Add(forecastResult);
        service.WriteProtocolLine(forecastResult.ToProtocolLine());

        service.WriteProtocolLines(smartCar.Transform(TransformationMode.Water));
        service.WriteProtocolLine(smartSystem.HandleVoiceCommand("Enable autopilot and projector"));
        service.WriteProtocolLine(smartSystem.BalanceClimate());
        service.WriteProtocolLines(smartSystem.ProtectPassengers("wet road and tired driver"));
        service.WriteProtocolLines(smartCar.EnableAutopilot(forecastResult.Value));
        service.WriteProtocolLines(smartCar.Stabilize("wet asphalt"));
        service.WriteProtocolLine("Night mode enabled.");
        service.WriteProtocolLine(smartSystem.SaveExperience(12, 3));

        return results;
    }

    private void WriteSensorReadings(IReadOnlyList<SensorReading> readings)
    {
        foreach (SensorReading reading in readings)
        {
            service.WriteProtocolLine(reading.ToProtocolLine());
        }
    }
}

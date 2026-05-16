namespace Lab6.Domain;

/// <summary>
/// Представляє розумний автомобіль як головний скомпонований об'єкт.
/// </summary>
public sealed class SmartCar
{
    private readonly Body body;
    private readonly Engine engine;
    private readonly Chassis chassis;
    private readonly TransformationModule transformationModule;
    private readonly SmartSystem smartSystem;

    /// <summary>
    /// Ініціалізує новий розумний автомобіль зі скомпонованих та агрегованих частин.
    /// </summary>
    public SmartCar(
        VehicleIdentity identity,
        Body body,
        Engine engine,
        Chassis chassis,
        TransformationModule transformationModule,
        SmartSystem smartSystem)
    {
        Identity = identity;
        this.body = body;
        this.engine = engine;
        this.chassis = chassis;
        this.transformationModule = transformationModule;
        this.smartSystem = smartSystem;
    }

    /// <summary>
    /// Повертає ідентифікаційні дані розумного автомобіля.
    /// </summary>
    public VehicleIdentity Identity { get; }

    /// <summary>
    /// Активує автомобіль і готує системи руху.
    /// </summary>
    public IReadOnlyList<string> Activate()
    {
        return new List<string>
        {
            body.OpenDoors(),
            engine.Start(),
            chassis.ChangeClearance(18.5)
        };
    }

    /// <summary>
    /// Трансформує автомобіль через узгодження кузова, двигуна та модуля трансформації.
    /// </summary>
    public IReadOnlyList<string> Transform(TransformationMode mode)
    {
        return new List<string>
        {
            transformationModule.ActivateMode(mode),
            body.ChangeShape(mode),
            engine.ChangeMode(ComponentState.Eco)
        };
    }

    /// <summary>
    /// Вмикає автопілот через взаємодію шасі та smart-системи.
    /// </summary>
    public IReadOnlyList<string> EnableAutopilot(double riskValue)
    {
        return new List<string>
        {
            chassis.ActivateAutopilot(),
            smartSystem.BuildRoute(riskValue)
        };
    }

    /// <summary>
    /// Стабілізує рух на вибраному покритті.
    /// </summary>
    public IReadOnlyList<string> Stabilize(string surfaceName)
    {
        return chassis.StabilizeMovement(surfaceName);
    }
}

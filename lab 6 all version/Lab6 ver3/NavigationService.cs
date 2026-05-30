namespace Lab6;

/// <summary>
/// Забезпечує адаптацію маршруту для автономної навігації з розрахунком часу прибуття та витрати енергії.
/// </summary>
public sealed class NavigationService : SmartDevice
{
    /// <summary>
    /// Ініціалізує нову службу навігації.
    /// </summary>
    public NavigationService() : base("Служба навігації", 0.20)
    {
    }
    /// <summary>
    /// Формує спокійний або оптимальний маршрут, прораховуючи час у дорозі (ETA) та витрату батареї.
    /// </summary>
    public string BuildAdaptiveRoute(double accidentProbability)
    {
        if (accidentProbability < 0.0 || accidentProbability > 100.0)
        {
            throw new NavigationConflictException("Конфлікт системи навігації: Отримано некоректні координати або несумісні карти після оновлення ПЗ!");
        }
        else
        {
            double baseTimeMinutes;
            double routeDistanceKilometers;
            string routeSelection;
            double delayFactor = 1.0 + (accidentProbability / 100.0) * 0.5;

        if (accidentProbability >= 50.0)
        {
            baseTimeMinutes = 22.0;
            routeDistanceKilometers = 11.5;
            routeSelection = "Безпечний шлях (коридор біля лікарні, низький ризик заторів)";
        }
        else
        {
            if (accidentProbability >= 20.0)
            {
                baseTimeMinutes = 18.0;
                routeDistanceKilometers = 9.0;
                routeSelection = "Екологічний шлях (об'їзна дорога, стабільна швидкість)";
            }
            else
            {
                baseTimeMinutes = 15.0;
                routeDistanceKilometers = 8.2;
                routeSelection = "Оптимальний шлях (головний проспект, прямий коридор)";
            }
        }

        double energyRateKwhPerKm;
        if (accidentProbability >= 50.0)
        {
            energyRateKwhPerKm = 0.18;
        }
        else
        {
            if (accidentProbability >= 20.0)
            {
                energyRateKwhPerKm = 0.14;
            }
            else
            {
                energyRateKwhPerKm = 0.16;
            }
        }

        double expectedTravelTimeMinutes = baseTimeMinutes * delayFactor;
        double estimatedEnergyConsumptionKwh = routeDistanceKilometers * energyRateKwhPerKm;

        return $"Адаптивний маршрут згенеровано: {routeSelection}. Дистанція: {routeDistanceKilometers:F1} км. Очікуваний час прибуття (ETA): {expectedTravelTimeMinutes:F1} хв (фактор затримки: {delayFactor:F2}x). Очікувані витрати енергії: {estimatedEnergyConsumptionKwh:F2} кВт·год.";
        }
    }

    /// <summary>
    /// Повертає статус служби навігації.
    /// </summary>
    public override string GetStatus()
    {
        return $"Модуль '{DeviceName}' будує оптимальні маршрути. Енергоспоживання: {PowerConsumption} кВт.";
    }
}

using System;

namespace Lab6;

/// <summary>
/// Точка входу в лабораторну роботу симуляції розумного автомобіля.
/// </summary>
public static class Program
{
    /// <summary>
    /// Головний метод, який ініціалізує та запускає координатор симуляції.
    /// </summary>
    public static void Main()
    {
        SimulationCoordinator coordinator = new SimulationCoordinator();
        coordinator.RunSimulation();
    }
}

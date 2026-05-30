namespace Lab6;

/// <summary>
/// Виняток, що виникає при невідповідності біометричних даних водія збереженому профілю безпеки.
/// </summary>
public sealed class ProfileMismatchException : SmartCarException
{
    /// <summary>
    /// Ініціалізує новий виняток невідповідності профілю.
    /// </summary>
    public ProfileMismatchException(string message) : base(message)
    {
    }
}

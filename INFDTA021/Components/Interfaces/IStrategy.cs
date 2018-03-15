using Assignment1.Models;

namespace Assignment1.Components.Interfaces
{
    public interface IStrategy
    {
        double Calculate(Vector vectorOne, Vector vectorTwo);
    }
}

using System.Collections.Generic;

public interface IWheelsHandler
{
    public IReadOnlyList<IWheel> Wheels { get; }
}

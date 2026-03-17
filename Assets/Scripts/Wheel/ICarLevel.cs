using System;

public interface ICarLevel
{
    public event Action Changed;
    public uint Value { get; }
}
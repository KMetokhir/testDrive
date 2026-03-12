using UnityEngine;

public interface ICarConfigSaver
{
    public void SaveCarConfig(string upgraderType, uint carLevel, uint upgradeLevel);

    public uint GetCarConfig(string upgraderType, uint carLevel, int defaultValue = 0);
}

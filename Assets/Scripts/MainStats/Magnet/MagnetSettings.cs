using UnityEngine;
public class MagnetSettings : Settings<MagnetUpgrader, IMagnetData>, IMagnetData
{
    public float MagnetRadius { get; private set; }

    protected override void ApplyUpgrade(IMagnetData data)
    {
        MagnetRadius = data.MagnetRadius;
        Debug.Log($"MagnetSettings: MagnetRadius updated to {MagnetRadius}");
    }
}
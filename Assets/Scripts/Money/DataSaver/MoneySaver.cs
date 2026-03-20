using UnityEngine;

public class MoneySaver
{
    private const string MonyeKey = "Money";

    public void SaveMoney(uint value)
    {
        PlayerPrefs.SetInt(MonyeKey, (int)value);
        PlayerPrefs.Save();
    }

    public uint GetMoney(uint defaultValue = 0)
    {
        return (uint)PlayerPrefs.GetInt(MonyeKey, (int)defaultValue);
    }
}

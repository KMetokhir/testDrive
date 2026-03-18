using UnityEngine;

public interface ICarPositionSaver
{
    public void SavePosition(uint carLevel, string sceneName, Vector3 position);

    public Vector3 GetPosition(uint carLevel, string sceneName, Vector3 defaultValue = default);

    public void SaveRotation(uint carLevel, string sceneName, Quaternion quaternion);

    public Quaternion GetRotation(uint carLevel, string sceneName, Quaternion defaultValue = default);
}
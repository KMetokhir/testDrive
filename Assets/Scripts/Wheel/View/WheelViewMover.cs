using UnityEngine;

public class WheelViewMover : MonoBehaviour
{
    public void Move(float speed, int direction, float deltaTime)
    {
        transform.Rotate(Vector3.right, speed * direction * deltaTime, Space.Self);
    }
}
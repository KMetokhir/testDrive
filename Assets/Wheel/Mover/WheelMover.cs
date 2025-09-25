using UnityEngine;

public class WheelMover : MonoBehaviour
{
    public void ForwardMove(float force, Rigidbody rigidbody, Vector3 lookDirection)
    {
        Move(force, rigidbody, lookDirection);
    }

    public void BackwardMove(float force, Rigidbody rigidbody, Vector3 lookDirection)
    {
        Vector3 direction = -lookDirection;
        Move(force, rigidbody, direction);
    }

    private void Move(float force, Rigidbody rigidbody, Vector3 direction)
    {
        rigidbody.AddRelativeForce(direction * force);
    }
}

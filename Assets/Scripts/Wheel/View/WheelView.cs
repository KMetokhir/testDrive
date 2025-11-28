using UnityEngine;

public class WheelView : MonoBehaviour
{
    [SerializeField] private WheelViewMover _mover;
    [SerializeField] private float _rotationSpeedModifier;
    [SerializeField] private ViewRotator _rotator;

    private Vector3 rightDirection;

    private Quaternion rRotation;

    public void Move( float speed, int clockRotation, float deltaTime, Vector3 lookAtDirection)
    {

        if (_mover == null)
        {
            return;
        }

        _mover.Move( speed * _rotationSpeedModifier, clockRotation, deltaTime);


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position, transform.position + rightDirection * 20);
    }

    public void RotateTo(Vector3 targetDirection)
    {

        if (_rotator == null)
        {
            return;
        }

        _rotator.RotateTo(targetDirection);
    }
}

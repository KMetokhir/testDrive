using UnityEngine;

public class CarPlacer : MonoBehaviour
{
    private const string PositionKey = "CarPosition";
    private const string RotationKey = "CarRotation";
    private Rigidbody _rigidbody;

    private Vector3 _defaultPosition;
    private Quaternion _defaultRotation;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _defaultPosition = Vector3.up;
        _defaultRotation = Quaternion.identity;

        Vector3 startPosition = PlayerPrefsManager.GetVector3(PositionKey, _defaultPosition);
        Quaternion startRotation = Quaternion.Euler(PlayerPrefsManager.GetVector3(RotationKey, _defaultRotation.eulerAngles));

        SetPosition(startPosition, startRotation);
    }

    private void OnDisable()
    {
        PlayerPrefsManager.SaveVector3(PositionKey, _rigidbody.transform.position);
        PlayerPrefsManager.SaveVector3(RotationKey, _rigidbody.transform.rotation.eulerAngles);
    }

    public void SetPosition(Vector3 position, Quaternion rotation)
    {        
        _rigidbody.transform.position = position;

        _rigidbody.transform.rotation = rotation;       
    }     
}

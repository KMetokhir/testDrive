using UnityEngine;

public class AttractableView : MonoBehaviour
{
    private MeshRenderer _mesh;

    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
    }

    public void Activate()
    {
        _mesh.enabled = true;
    }

    public void Deactivate()
    {
        _mesh.enabled = false;
    }

    public void BecameAvalible()
    {
        //becom active color
    }

    public void BecameUnavailable()
    {
      // become gray
        GetComponent<Collider>().enabled = false;
    }
}
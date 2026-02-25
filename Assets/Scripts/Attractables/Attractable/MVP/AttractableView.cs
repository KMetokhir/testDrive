using UnityEngine;

public class AttractableView : MonoBehaviour
{
    [SerializeField] private AttractablesType _type;

    public AttractablesType Type => _type;

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
       // gameObject.SetActive(false);
    }

    public void BecameAvalible()
    {

    }

    public void BecameUnavailable()
    {
        // Example: gray out or disable collider
        GetComponent<Collider>().enabled = false;
    }
}
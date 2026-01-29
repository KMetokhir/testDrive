
using UnityEngine;

public class QuadSpawnArea : MonoBehaviour
{
    public Vector3 Center => transform.position;
    public float SizeX => transform.localScale.x;
    public float SizeY => transform.localScale.y;
}

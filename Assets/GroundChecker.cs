using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
   
    [SerializeField] private LayerMask _groundLayer;

    
    //private float _rayLength = 1.5f;

    public bool IsGrounded(Vector3 position, float rayLength)
    {
        return Physics.Raycast(position, Vector3.down, out RaycastHit hit, rayLength, _groundLayer);
    }

}

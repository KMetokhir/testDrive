using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _objToFollow;

    

    private void Update()
    {
        transform.position = _objToFollow.position;
        transform.rotation = _objToFollow.rotation;
    }
}

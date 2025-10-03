using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject segmentPrefab; // Prefab of a small Rigidbody segment
    public int segmentCount = 10;    // Number of segments
    public float segmentLength = 0.5f; // Length of each segment
    public Transform startPoint;     // Starting point of the rope

    void Start()
    {
        GameObject previousSegment = null;

        for (int i = 0; i < segmentCount; i++)
        {
            // Instantiate segment
            Vector3 position = startPoint.position + Vector3.down * segmentLength * i;
            GameObject segment = Instantiate(segmentPrefab, position, Quaternion.identity);

            Rigidbody rb = segment.GetComponent<Rigidbody>();

            // Add HingeJoint
            HingeJoint joint = segment.AddComponent<HingeJoint>();
            joint.axis = Vector3.right; // or Vector3.forward depending on desired swing
            joint.useLimits = true;

            // Connect to previous segment or start point
            if (i == 0)
            {
                joint.connectedBody = startPoint.GetComponent<Rigidbody>();
            }
            else
            {
                joint.connectedBody = previousSegment.GetComponent<Rigidbody>();
            }

            previousSegment = segment;
        }
    }
}

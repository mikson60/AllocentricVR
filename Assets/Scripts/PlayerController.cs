using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Rigidbody rb;
    
    public float scale = 1f;

    private Vector3 headPosition;
    private Quaternion headRotation;
    private List<XRNodeState> nodeStates = new List<XRNodeState>();

    private Vector3 initialHeadPosition;
    private bool calibrated = false;

    private Vector3 lastHeadPosition;


    void Update()
    {
        getTracking();
        
        if (calibrated)
        {
            var lastPosition = (lastHeadPosition - initialHeadPosition);
            var currentPosition = (headPosition - initialHeadPosition);
            var velocity = currentPosition - lastPosition;

            velocity.y = 0f;
            
            rb.velocity = velocity * scale;
        }
        else
        {
            if (headPosition.x + headPosition.y + headPosition.z != 0)
            {
                // Tracking initialized

                initialHeadPosition = headPosition;
                Debug.Log($"Initialize headset on position {headPosition.ToString()}");

                calibrated = true;
            }
        }

        lastHeadPosition = headPosition;
    }

    void getTracking()
    {
        InputTracking.GetNodeStates(nodeStates);

        var headState = nodeStates.FirstOrDefault(node => node.nodeType == XRNode.Head);

        if (headState.tracked)
        {
            headState.TryGetPosition(out headPosition);
            headState.TryGetRotation(out headRotation);
        }
    }
}

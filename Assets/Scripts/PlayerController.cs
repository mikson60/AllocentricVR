using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    
    public float scale;

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
            var lastPosition = (lastHeadPosition - initialHeadPosition) * scale;
            var currentPosition = (headPosition - initialHeadPosition) * scale;
            var movement = currentPosition - lastPosition;

            var endPosition = transform.localPosition + new Vector3(movement.x, 0, movement.z);;
            
            //transform.localPosition += new Vector3(movement.x, 0, movement.z);
            transform.localRotation = Quaternion.Euler(new Vector3(0,headRotation.eulerAngles.y,0));
            
            rb.MovePosition(transform.TransformPoint(endPosition));
            
            // transform.localPosition = new Vector3(transform.localPosition.x, 0.01f, transform.localPosition.z);
        }
        else
        {
            if (headPosition.x + headPosition.y + headPosition.z != 0)
            {
                // Tracking initialized

                initialHeadPosition = headPosition;
                Debug.Log(string.Format("Initialize headset on position {0}", headPosition.ToString()));

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

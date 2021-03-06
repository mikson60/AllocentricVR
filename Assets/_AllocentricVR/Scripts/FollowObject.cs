﻿using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public enum Axis { X, Y, Z }
    
    [Header("Settings")] 
    [SerializeField] private float _followSpeed = 1f;
    [SerializeField] private bool _usePhysics;
    [SerializeField] private bool _skipFollowOnFirstUpdate = true;

    [Header("Constraints")] 
    [SerializeField] private Axis _constrainedLocalAxis;
    
    [Header("References")]
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody _follower;

    // Runtime
    private Vector3 _lastTargetPos = Vector3.zero;
    private Vector3 _targetPos = Vector3.zero;
    private bool firstFrame = true;
    
    private Quaternion _lastTargetRot = Quaternion.identity;
    
    private void Update()
    {
        if (!_usePhysics)
        {
            Follow();
        }
    }

    private void FixedUpdate()
    {
        if (_usePhysics)
        {
            Follow();
        }
    }

    private void Follow()
    {
        if (_skipFollowOnFirstUpdate && firstFrame)
        {
            firstFrame = false;
            _targetPos = _target.position;
            _lastTargetPos = _targetPos;
            _lastTargetRot = _target.rotation;
            return;
        }
        
        // POSITION
        _targetPos = _target.position;
        
        var targetMovementInWorldSpace = _targetPos - _lastTargetPos;
        var targetMovementInLocalSpace = _target.InverseTransformDirection(targetMovementInWorldSpace); // How much did we the target move on its local axis?

        targetMovementInLocalSpace = new Vector3(
            _constrainedLocalAxis == Axis.X ? 0f : targetMovementInLocalSpace.x, 
            _constrainedLocalAxis == Axis.Y ? 0f : targetMovementInLocalSpace.y, 
            _constrainedLocalAxis == Axis.Z ? 0f : targetMovementInLocalSpace.z);
        
        var movementVelocity = transform.TransformDirection(targetMovementInLocalSpace) * _followSpeed;
        
        if (_usePhysics)
        {
            _follower.velocity = movementVelocity;   
        }
        else
        {
            transform.position += movementVelocity;
        }
        
        _lastTargetPos = _targetPos;
        
        // ROTATION
        // Get target's rotation difference on the world Y axis

        var targetRotationDifference = _target.rotation * Quaternion.Inverse(_lastTargetRot);

        _follower.transform.Rotate(0f, targetRotationDifference.eulerAngles.y, 0f, Space.Self);
        
        _lastTargetRot = _target.rotation;
        
        // Debug.Log("changed angle in world Y is: " + targetRotationDifference.eulerAngles.y);
    }
}

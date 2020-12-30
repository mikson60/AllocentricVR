using System;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private float _followSpeed = 1f;
    [SerializeField] private bool _usePhysics;
    [SerializeField] private bool _skipFollowOnFirstUpdate = true;
    
    [Header("References")]
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody _follower;

    // Runtime
    private Vector3 _lastTargetPos = Vector3.zero;
    private Vector3 _targetPos = Vector3.zero;
    private bool firstFrame = true;
    
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
            return;
        }
        
        _targetPos = _target.position;
        
        var targetMovementInWorldSpace = _targetPos - _lastTargetPos;
        var targetMovementInLocalSpace = _target.InverseTransformDirection(targetMovementInWorldSpace); // How much did we move on our local axis?

        if (_usePhysics)
        {
            _follower.velocity = transform.TransformDirection(targetMovementInLocalSpace) * _followSpeed;   
        }
        else
        {
            transform.position += transform.TransformDirection(targetMovementInLocalSpace) * _followSpeed;
        }
        
        _lastTargetPos = _targetPos;
    }
}

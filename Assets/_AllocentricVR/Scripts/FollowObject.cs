using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private float _followSpeed = 1f;
    
    [Header("References")]
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody _follower;

    private Vector3 _lastTargetPos = Vector3.zero;
    private Vector3 _targetPos = Vector3.zero;
    
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        _targetPos = _target.position;
        
        var targetMovementInWorldSpace = _targetPos - _lastTargetPos;
        var targetMovementInLocalSpace = _target.InverseTransformDirection(targetMovementInWorldSpace); // How much did we move on our local axis?

        transform.position += transform.InverseTransformDirection(targetMovementInLocalSpace) * _followSpeed;
        
        _lastTargetPos = _targetPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform _target;

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        Debug.Log(transform.localPosition);
    }
}

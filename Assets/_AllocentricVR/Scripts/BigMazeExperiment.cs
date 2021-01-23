
using UnityEngine;

public class BigMazeExperiment : Experiment
{
    [SerializeField] private Transform playerCameraTransform;


    public override void ResetAndStart()
    {
        base.ResetAndStart();

        root.transform.position = new Vector3(playerCameraTransform.position.x, 0f, playerCameraTransform.position.z);
    }
}

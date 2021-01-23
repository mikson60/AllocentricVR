using UnityEngine;

public class SmallMazeExperiment : Experiment
{
    [SerializeField] private Rigidbody mazePlayer;
    [SerializeField] private Transform mazeStart;

    public override void ResetAndStart()
    {
        base.ResetAndStart();

        mazePlayer.transform.position = mazeStart.position;
    }
}

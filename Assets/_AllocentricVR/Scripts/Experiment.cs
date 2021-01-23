using UnityEngine;

public abstract class Experiment : MonoBehaviour
{
    [SerializeField] protected GameObject root;

    public virtual void ResetAndStart() 
    {
        root.SetActive(true);
    }
    public virtual void EndExperiment()
    {
        root.SetActive(false);
    }
}

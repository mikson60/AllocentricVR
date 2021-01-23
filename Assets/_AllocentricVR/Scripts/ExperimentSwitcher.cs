using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExperimentSwitcher : MonoBehaviour
{
    [SerializeField] private List<Experiment> experiments = new List<Experiment>();

    private int _currentExperimentIndex = 0;


    void Start()
    {
        experiments[_currentExperimentIndex].ResetAndStart();
    }

    void Update()
    {
        if (Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            NextExperiment();
        }
    }

    private void NextExperiment()
    {
        if (experiments[_currentExperimentIndex] != null)
        {
            experiments[_currentExperimentIndex].EndExperiment();
        }

        _currentExperimentIndex = _currentExperimentIndex == experiments.Count - 1 ? 0 : _currentExperimentIndex + 1;

        experiments[_currentExperimentIndex].ResetAndStart();
    }
}

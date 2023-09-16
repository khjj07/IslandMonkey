using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngineInternal;

public class MonkeyContext : MonoBehaviour
{
    public IStateMonkey CurrentMonkeyState { get; set; }

    private IStateMonkey _InBuildingState, _InState, _OutState, _StandState, _WalkState;

    private void Start()
    {
        _InBuildingState = gameObject.AddComponent<InBuildingStateMonkey>();
        _InState = gameObject.AddComponent<InStateMonkey>();
        _OutState = gameObject.AddComponent<OutStateMonkey>();
        _StandState = gameObject.AddComponent<StandStateMonkey>();
        _WalkState = gameObject.AddComponent<WalkStateMonkey>();
    }

    public void Update()
    {
        // current state 바뀌면 바로 전환.
        Transition();
    }

    public void Transition()
    {
        CurrentMonkeyState.Handle(this);
    }

    public void Transition(IStateMonkey state)
    {
        CurrentMonkeyState = state;
        CurrentMonkeyState.Handle(this);
    }

    public void InBuildingMonkey()
    {
        CurrentMonkeyState = _InBuildingState;
    }

    public void InMonkey()
    {
        CurrentMonkeyState = _InState;
    }

    public void OutMonkey()
    {
        CurrentMonkeyState = _OutState;
    }

    public void StandMonkey()
    {
        CurrentMonkeyState = _StandState;
    }
    public void WalkMonkey()
    {
        CurrentMonkeyState = _WalkState;
    }
}

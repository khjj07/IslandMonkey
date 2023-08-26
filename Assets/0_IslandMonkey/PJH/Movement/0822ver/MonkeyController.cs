using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MonkeyController : MonoBehaviour
{
    private Animator MonkeyAnimator;

    private RuntimeAnimatorController MonkeyAnimatorController; // stand, walk 관리

    public RuntimeAnimatorController MonkeyBuildingAnimatorController; // ComIn, InFacility, GoOut 관리

    public enum MonkeyState
    {
        Stand,
        Walk,
        ComeIn,
        InFacility,
        GoOut
    }

    private MonkeyState currentMonkeyState;
    public Subject<MonkeyState> monkeyStateSubject;

    private void Start()
    {
        MonkeyAnimator = GetComponent<Animator>();
        MonkeyAnimatorController = MonkeyAnimator.runtimeAnimatorController;

        monkeyStateSubject
            .Where(currentMonkeyState => currentMonkeyState == MonkeyState.Stand)
            .DistinctUntilChanged()
            .Subscribe(_ => HandleStandState())
            .AddTo(gameObject);

        monkeyStateSubject
            .Where(currentMonkeyState => currentMonkeyState == MonkeyState.Walk)
            .DistinctUntilChanged()
            .Subscribe(_ => HandleWalkState())
            .AddTo(gameObject);

        monkeyStateSubject
            .Where(currentMonkeyState => currentMonkeyState == MonkeyState.ComeIn)
            .DistinctUntilChanged()
            .Subscribe(_ => HandleComeInState())
            .AddTo(gameObject);

        monkeyStateSubject
            .Where(currentMonkeyState => currentMonkeyState == MonkeyState.InFacility)
            .DistinctUntilChanged()
            .Subscribe(_ => HandleInFacilityState())
            .AddTo(gameObject);

        monkeyStateSubject
            .Where(currentMonkeyState => currentMonkeyState == MonkeyState.GoOut)
            .DistinctUntilChanged()
            .Subscribe(_ => HandleGoOutState())
            .AddTo(gameObject);

    }

    private void Update()
    {
        monkeyStateSubject.OnNext(currentMonkeyState);
    }

    private void HandleStandState()
    {
        MonkeyAnimator.runtimeAnimatorController = MonkeyAnimatorController;
        MonkeyAnimator.SetBool("isWalk", false);
    }
    private void HandleWalkState()
    {
        MonkeyAnimator.runtimeAnimatorController = MonkeyAnimatorController;
        MonkeyAnimator.SetBool("isWalk", true);
    }
    private void HandleComeInState()
    {
        MonkeyAnimator.runtimeAnimatorController = MonkeyBuildingAnimatorController;
        MonkeyAnimator.SetTrigger("ComeIn");
    }
    private void HandleInFacilityState()
    {
        MonkeyAnimator.runtimeAnimatorController = MonkeyBuildingAnimatorController;

    }
    private void HandleGoOutState()
    {
        MonkeyAnimator.runtimeAnimatorController = MonkeyBuildingAnimatorController;
        MonkeyAnimator.SetTrigger("GoOut");
    }

    // 상태 변경 메서드 => MonkeyMovement에서 사용
    public void ChangeMonkeyState(MonkeyState newMonkeyState)
    {
        currentMonkeyState = newMonkeyState;
    }

}

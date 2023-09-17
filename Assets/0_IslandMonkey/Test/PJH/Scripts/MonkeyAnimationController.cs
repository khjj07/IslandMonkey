//using System.Collections;
//using System.Collections.Generic;
//using UniRx;
//using UnityEngine;
//using static MonkeyMovement;

//public class MonkeyAnimationController : MonoBehaviour
//{
//    private Animator MonkeyAnimator;
//    private RuntimeAnimatorController MonkeyAnimatorController;
//    public RuntimeAnimatorController MonkeyInBuildingAnimatorController; // 일단 지금은 public
//    //public RuntimeAnimatorController MonkeyInTargetBuildingAnimatorController; // 일단 지금은 public

//    private MonkeyMovement _monkeyMovement;

//    public enum MonkeyState
//    {
//        Stand,
//        Walk,
//        ComeIn,
//        InBuilding,
//        GoOut
//    }

//    private MonkeyState currentMonkeyState;
//    public Subject<MonkeyState> monkeyStateSubject = new Subject<MonkeyState>();

//    private void Awake()
//    {
//        MonkeyAnimator = GetComponent<Animator>();
//        _monkeyMovement = GetComponent<MonkeyMovement>();
//        MonkeyAnimatorController = MonkeyAnimator.runtimeAnimatorController;
//        currentMonkeyState = MonkeyState.Stand;
//    }

//    private void Start()
//    {
//        monkeyStateSubject
//            .DistinctUntilChanged()
//            .Subscribe(newState => HandleMonkeyState(newState))
//            .AddTo(gameObject);
//    }


//    private void HandleMonkeyState(MonkeyState newState)
//    {
//        currentMonkeyState = newState;

//        switch (currentMonkeyState)
//        {
//            case MonkeyState.Stand:
//                MonkeyAnimator.runtimeAnimatorController = MonkeyAnimatorController;
//                MonkeyAnimator.SetBool("isWalk", false);
//                break;
//            case MonkeyState.Walk:
//                MonkeyAnimator.runtimeAnimatorController = MonkeyAnimatorController;
//                Debug.Log("걸어라");
//                MonkeyAnimator.SetBool("isWalk", true);
//                MonkeyAnimator.SetTrigger("Walk");
//                break;
//            case MonkeyState.ComeIn:
//                MonkeyAnimator.runtimeAnimatorController = MonkeyInBuildingAnimatorController; // 여기서 controller 바꾸기
//                Debug.Log("건물 안");
//                MonkeyAnimator.SetBool("inFacility", true);
//                MonkeyAnimator.SetTrigger("Facility");
//                // 여기서 BuildingwithMonkey 애니메이션 true로 변환!!
//                break;
//            case MonkeyState.InBuilding:
//                //MonkeyAnimator.runtimeAnimatorController = MonkeyInBuildingAnimatorController; // 여기서 controller 바꾸기
//                //MonkeyAnimator.SetBool("inFacility", true);
//                break;
//            case MonkeyState.GoOut:
//                MonkeyAnimator.runtimeAnimatorController = MonkeyInBuildingAnimatorController; // 여기서 controller 바꾸기
//                MonkeyAnimator.SetBool("inFacility", false);
//                // 여기서 BuildingwithMonkey 애니메이션 false로 변환!!
//                break;
//        }
//    }

//    private void setMonkeyAnimatorController()
//    {

//    }

//    // 몽키 상태 변경 메서드 => MonkeyMovement에서 사용
//    public void ChangeMonkeyState(MonkeyState newMonkeyState)
//    {
//        monkeyStateSubject.OnNext(newMonkeyState);
//    }
//}

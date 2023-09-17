//using System.Collections;
//using System.Collections.Generic;
//using UniRx;
//using UnityEngine;
//using static MonkeyMovement;

//public class MonkeyAnimationController : MonoBehaviour
//{
//    private Animator MonkeyAnimator;
//    private RuntimeAnimatorController MonkeyAnimatorController;
//    public RuntimeAnimatorController MonkeyInBuildingAnimatorController; // �ϴ� ������ public
//    //public RuntimeAnimatorController MonkeyInTargetBuildingAnimatorController; // �ϴ� ������ public

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
//                Debug.Log("�ɾ��");
//                MonkeyAnimator.SetBool("isWalk", true);
//                MonkeyAnimator.SetTrigger("Walk");
//                break;
//            case MonkeyState.ComeIn:
//                MonkeyAnimator.runtimeAnimatorController = MonkeyInBuildingAnimatorController; // ���⼭ controller �ٲٱ�
//                Debug.Log("�ǹ� ��");
//                MonkeyAnimator.SetBool("inFacility", true);
//                MonkeyAnimator.SetTrigger("Facility");
//                // ���⼭ BuildingwithMonkey �ִϸ��̼� true�� ��ȯ!!
//                break;
//            case MonkeyState.InBuilding:
//                //MonkeyAnimator.runtimeAnimatorController = MonkeyInBuildingAnimatorController; // ���⼭ controller �ٲٱ�
//                //MonkeyAnimator.SetBool("inFacility", true);
//                break;
//            case MonkeyState.GoOut:
//                MonkeyAnimator.runtimeAnimatorController = MonkeyInBuildingAnimatorController; // ���⼭ controller �ٲٱ�
//                MonkeyAnimator.SetBool("inFacility", false);
//                // ���⼭ BuildingwithMonkey �ִϸ��̼� false�� ��ȯ!!
//                break;
//        }
//    }

//    private void setMonkeyAnimatorController()
//    {

//    }

//    // ��Ű ���� ���� �޼��� => MonkeyMovement���� ���
//    public void ChangeMonkeyState(MonkeyState newMonkeyState)
//    {
//        monkeyStateSubject.OnNext(newMonkeyState);
//    }
//}

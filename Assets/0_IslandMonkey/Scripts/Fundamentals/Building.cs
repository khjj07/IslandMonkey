using System;
using Assets._0_IslandMonkey.Scripts.Extension;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets._0_IslandMonkey.Scripts.Abstract
{
    public class Building : MonoBehaviour
    {
        #region 수입

        public int goldIncome = 100;
        public GameObject goldPopupImage; // 팝업 이미지 객체 참조
        public float goldPopupInterval = 6.0f; // 팝업 이미지 표시 간격

        public int bananaIncome = 100;
        public int clamIncome = 100;
        public float bananaEarnInterval = 1.0f;
        public float clamEarnInterval = 1.0f;
        public GameObject model;

        public UnityEvent goldEarnEvent;
        public UnityEvent bananaEarnEvent;
        public UnityEvent clamEarnEvent;

        public Canvas mainCanvas; // 메인 캔버스 참조
        public GameObject goldImagePrefab; // 골드 이미지 프리팹 참조
        public RectTransform targetUIPosition; // 원하는 UI 요소의 위치

        public GameObject iconBuilding;
        public GameObject iconMonkey;

        public GameObject buildingUpgradePanel;
        #endregion

        private void Start()
        {
            CreateEarnStream();
            goldPopupImage.SetActive(false); // 팝업 이미지 비활성화
            iconBuilding.SetActive(false);
            iconMonkey.SetActive(false);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse button clicked!");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log($"Ray hit: {hit.transform.name}");
                }
                else
                {
                    Debug.Log("No ray hit detected");
                }

                if (Physics.Raycast(ray, out hit))
                {
                    // 클릭된 오브젝트의 태그가 "GoldPopup"인지 확인
                    if (hit.transform.CompareTag("GoldPopup"))
                    {
                        OnGoldPopupClicked();
                        Debug.Log("goldpopup detect");
                    }
                    else if (hit.transform.name == ("BuildingArea"))
                    {
                        iconBuilding.SetActive(true);
                        iconMonkey.SetActive(true);
                    }
                    else if (hit.transform.CompareTag("iconBuilding"))
                    {
                        buildingUpgradePanel.SetActive(true);
                    }
                    else if (hit.transform.CompareTag("iconMonkey"))
                    {

                    }
                    else if (hit.transform.name == ("Plane"))
                    {
                        iconBuilding.SetActive(false);
                        iconMonkey.SetActive(false);
                    }

                }
            }
        }

        public void CreateEarnStream()
        {
            Observable.Interval(TimeSpan.FromSeconds(goldPopupInterval)).Subscribe(_ => { ShowGoldPopup(); })
                .AddTo(gameObject);

            if (bananaIncome > 0)
            {
                Observable.Interval(TimeSpan.FromSeconds(bananaEarnInterval)).Subscribe(_ =>
                {
                    GameManager.instance.EarnBanana(bananaIncome);
                    bananaEarnEvent.Invoke();
                }).AddTo(gameObject);
            }

            if (clamIncome > 0)
            {
                Observable.Interval(TimeSpan.FromSeconds(clamEarnInterval)).Subscribe(_ =>
                {
                    GameManager.instance.EarnClam(clamIncome);
                    clamEarnEvent.Invoke();
                }).AddTo(gameObject);
            }
        }

        public void ShowGoldPopup()
        {
            goldPopupImage.SetActive(true); // 팝업 이미지 활성화
        }

        public void OnGoldPopupClicked()
        {
            Debug.Log("GoldPopup was clicked!");
            Vector3 startPos = goldPopupImage.transform.position;
            AnimateGoldAcquisition(startPos);

            GameManager.instance.EarnGold(goldIncome);
            goldEarnEvent.Invoke();
            goldPopupImage.SetActive(false); // 팝업 이미지 비활성화
        }

        public void SqueezeModel()
        {
            model.transform.DOShakeScale(0.2f, Vector3.one * 0.02f);
        }

        public void SetPlace(Place place)
        {
            place = place;
            Vector3 newPosition = transform.position;
            newPosition.x = place.transform.position.x;
            newPosition.z = place.transform.position.z;
            transform.position = newPosition;
        }

        public void AnimateGoldAcquisition(Vector3 startPos)
        {
            Vector2 canvasCenter = new Vector2(720, 1400);

            int coinCount = 10; // 움직이길 원하는 코인 이미지 개수

            for (int i = 0; i < coinCount; i++)
            {
                GameObject goldImageInstance = Instantiate(goldImagePrefab, canvasCenter, Quaternion.identity,
                    mainCanvas.transform);
                goldImageInstance.transform.localScale = Vector3.one;

                // 약간의 무작위성을 추가하여 코인 이미지들이 정확히 동일한 경로를 따르지 않게 만듭니다.
                Vector2 randomOffset =
                    new Vector2(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f));

                RectTransform goldImageRect = goldImageInstance.GetComponent<RectTransform>();

                goldImageRect.DOAnchorPos(targetUIPosition.anchoredPosition + randomOffset, 1.0f)
                    .OnComplete(() => Destroy(goldImageInstance));
                goldImageInstance.GetComponent<Image>().DOFade(0f, 0.8f);
            }
        }
    }
}
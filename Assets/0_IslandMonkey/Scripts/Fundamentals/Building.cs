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
        #region ����

        public int goldIncome = 100;
        public GameObject goldPopupImage; // �˾� �̹��� ��ü ����
        public float goldPopupInterval = 6.0f; // �˾� �̹��� ǥ�� ����

        public int bananaIncome = 100;
        public int clamIncome = 100;
        public float bananaEarnInterval = 1.0f;
        public float clamEarnInterval = 1.0f;
        public GameObject model;

        public UnityEvent goldEarnEvent;
        public UnityEvent bananaEarnEvent;
        public UnityEvent clamEarnEvent;

        public Canvas mainCanvas; // ���� ĵ���� ����
        public GameObject goldImagePrefab; // ��� �̹��� ������ ����
        public RectTransform targetUIPosition; // ���ϴ� UI ����� ��ġ

        public GameObject iconBuilding;
        public GameObject iconMonkey;

        public GameObject buildingUpgradePanel;
        #endregion

        private void Start()
        {
            CreateEarnStream();
            goldPopupImage.SetActive(false); // �˾� �̹��� ��Ȱ��ȭ
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
                    // Ŭ���� ������Ʈ�� �±װ� "GoldPopup"���� Ȯ��
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
            goldPopupImage.SetActive(true); // �˾� �̹��� Ȱ��ȭ
        }

        public void OnGoldPopupClicked()
        {
            Debug.Log("GoldPopup was clicked!");
            Vector3 startPos = goldPopupImage.transform.position;
            AnimateGoldAcquisition(startPos);

            GameManager.instance.EarnGold(goldIncome);
            goldEarnEvent.Invoke();
            goldPopupImage.SetActive(false); // �˾� �̹��� ��Ȱ��ȭ
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

            int coinCount = 10; // �����̱� ���ϴ� ���� �̹��� ����

            for (int i = 0; i < coinCount; i++)
            {
                GameObject goldImageInstance = Instantiate(goldImagePrefab, canvasCenter, Quaternion.identity,
                    mainCanvas.transform);
                goldImageInstance.transform.localScale = Vector3.one;

                // �ణ�� ���������� �߰��Ͽ� ���� �̹������� ��Ȯ�� ������ ��θ� ������ �ʰ� ����ϴ�.
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
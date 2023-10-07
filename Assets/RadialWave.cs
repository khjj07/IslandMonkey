using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class RadialWave : MonoBehaviour
{
    [SerializeField]
    private Image circleImage; // �� ����� �̹���
    [SerializeField]
    private float maxScale = 2.0f; // ���� �ִ� ũ�� ����
    [SerializeField]
    private float animationDuration = 1.0f; // �ִϸ��̼� �ð�

    private void Start()
    {
        circleImage.color = new Color(circleImage.color.r, circleImage.color.g, circleImage.color.b, 0); 
        // ���� ���İ��� 0���� �����Ͽ� ����
        AnimateCircle();
    }

    void AnimateCircle()
    {
        circleImage.rectTransform.localScale = Vector3.one; // ���� ũ��� ����

        Sequence sequence = DOTween.Sequence();
        sequence.Append(circleImage.DOFade(1, 0)); // ���İ��� 1�� ����
        sequence.Append(circleImage.rectTransform.DOScale(maxScale, animationDuration).SetEase(Ease.InOutSine));
        sequence.Join(circleImage.DOFade(0, animationDuration * 1.5f)); // ���İ��� 0�� �Ǵ� �κ��� ���� �ð��� 1.5��� �ø�
        sequence.OnComplete(AnimateCircle);
    }
}

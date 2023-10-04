using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class RadialWave : MonoBehaviour
{
    [SerializeField]
    private Image circleImage; // 원 모양의 이미지
    [SerializeField]
    private float maxScale = 2.0f; // 원의 최대 크기 배율
    [SerializeField]
    private float animationDuration = 1.0f; // 애니메이션 시간

    private void Start()
    {
        circleImage.color = new Color(circleImage.color.r, circleImage.color.g, circleImage.color.b, 0); 
        // 원래 알파값을 0으로 설정하여 숨김
        AnimateCircle();
    }

    void AnimateCircle()
    {
        circleImage.rectTransform.localScale = Vector3.one; // 원래 크기로 설정

        Sequence sequence = DOTween.Sequence();
        sequence.Append(circleImage.DOFade(1, 0)); // 알파값을 1로 설정
        sequence.Append(circleImage.rectTransform.DOScale(maxScale, animationDuration).SetEase(Ease.InOutSine));
        sequence.Join(circleImage.DOFade(0, animationDuration * 1.5f)); // 알파값이 0이 되는 부분의 지속 시간을 1.5배로 늘림
        sequence.OnComplete(AnimateCircle);
    }
}

using UnityEngine;
using DG.Tweening;
using TMPro;

public class LetterZoom : MonoBehaviour
{
    [Header("Animation Settings")]
    public float idleBreathSpeed = 2f;    // Kecepatan napas
    public float idleMinScale = 0.95f;    // Zoom out tipis
    public float idleMaxScale = 1.05f;    // Zoom in tipis
    
    private RectTransform rectTransform;
    private Vector3 originalScale;
    private Sequence idleSequence;
    
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        StartIdleBreathing();
    }
    
    void OnDestroy()
    {
        if (idleSequence != null) idleSequence.Kill();
    }
    
    void StartIdleBreathing()
    {
        idleSequence = DOTween.Sequence()
            .Append(rectTransform.DOScale(originalScale * idleMaxScale, idleBreathSpeed / 2f).SetEase(Ease.InOutSine))
            .Append(rectTransform.DOScale(originalScale * idleMinScale, idleBreathSpeed / 2f).SetEase(Ease.InOutSine))
            .SetLoops(-1); // Loop forever
    }
    
    public void PlayClickZoom()
    {
        // Stop idle → Zoom OUT besar → Zoom IN bouncy → Resume idle
        if (idleSequence != null) idleSequence.Pause();
        
        rectTransform.DOScale(originalScale * 2.7f, 2.12f)
            .OnComplete(() => {
                rectTransform.DOScale(originalScale, 0.8f).SetEase(Ease.OutBack)
                    .OnComplete(() => {
                        // Resume idle breathing
                        if (idleSequence != null) idleSequence.Restart();
                    });
            });
    }
}

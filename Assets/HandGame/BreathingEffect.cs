using DG.Tweening;  // Import DOTween
using UnityEngine;
public class BreathingEffect : MonoBehaviour
{
    // Parameters for breathing effect
    public Vector3 breathScale = new Vector3(1.05f, 1.05f, 1.05f);  // Slight increase in scale to simulate breathing in
    public float breathDuration = 2f;  // Time for one complete inhale/exhale cycle

    void Start()
    {
        // Create a breathing animation loop (scale up for inhale, scale down for exhale)
        transform.DOScale(breathScale, breathDuration)
            .SetEase(Ease.InOutQuad)  // Smooth easing for a natural feel
            .SetLoops(-1, LoopType.Yoyo);  // Loop indefinitely with a Yoyo effect (back and forth)
    }
}

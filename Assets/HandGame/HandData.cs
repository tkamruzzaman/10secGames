using DG.Tweening;
using UnityEngine;

public class HandData : MonoBehaviour
{
    public int HandSpriteNumber;

    public Vector3 bumpScale = new Vector3(1.2f, 1.2f, 1.2f);  // Slightly larger scale for the bump
    public float bumpDuration = 0.2f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            ChangeHand.Instance.GameStopped = true;
            if (other.GetComponent<HandData>().HandSpriteNumber== this.HandSpriteNumber)
            {
                BounceEffect(other.gameObject);
                BounceEffect(this.gameObject);
                ChangeHand.Instance.YouWin();
            }
            else
            {
                BounceEffect(other.gameObject);
                BounceEffect(this.gameObject);
                ChangeHand.Instance.YouLose();
            }
        }
    }

    void BounceEffect(GameObject hand)
    {
        hand.transform.DOScale(bumpScale, bumpDuration)
             .SetEase(Ease.OutQuad)  // Fast out easing for a quick bump effect
             .OnComplete(() =>
             {
                 // Scale back to the original size immediately after the bump
                 hand.transform.DOScale(Vector3.one, bumpDuration * 0.5f)
                     .SetEase(Ease.InQuad);  // Fast in easing to snap back
             });
    }
}

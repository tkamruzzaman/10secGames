using UnityEngine;

public class HandData : MonoBehaviour
{
    public int HandSpriteNumber;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<HandData>().HandSpriteNumber== this.HandSpriteNumber)
            {
                ChangeHand.Instance.YouWin();
            }
            else
            {
                ChangeHand.Instance.YouLose();
            }
        }
    }
}

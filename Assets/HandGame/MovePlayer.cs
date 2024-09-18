using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Transform target;  // The target GameObject to move towards
    public float speed = 5f;  // Speed at which the object moves

    void Update()
    {
        if (target != null && ChangeHand.Instance.shouldMove)
        {
            // Get the current position and target position in 2D (x, y only)
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = target.position;

            // Move towards the target position in 2D space
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
        }
    }
}

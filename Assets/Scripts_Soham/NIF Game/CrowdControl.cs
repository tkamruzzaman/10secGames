using UnityEngine;

public class CrowdControl : MonoBehaviour
{
    public float moveSpeed = 2f;              // Speed of the crowd
    public Sprite[] defeatedSprites;          // Defeated sprites
    private SpriteRenderer spriteRenderer;
    private bool isDefeated = false;
    public bool isMoving = true;              // Crowd movement status
    private Rigidbody2D rb;
    public float diagonalForce = 200000f;        // The force to be applied to the crowd on defeat

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();  // Reference the existing Rigidbody2D
    }

    void Update()
    {
        if (isMoving && !isDefeated)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }

    public void Defeat()
    {
        if (isDefeated) return;

        isDefeated = true;
        spriteRenderer.sprite = defeatedSprites[Random.Range(0, defeatedSprites.Length)];
        isMoving = false;  // Stop the crowd from moving
        
        // Make the crowd fall by changing the gravity scale on the existing Rigidbody2D
        rb.gravityScale = 1;  

        // Change the tag so that this object no longer interacts with the door
        gameObject.tag = "DefeatedCrowd";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            // Crowd reaches the door and disappears
            GameControl.Instance.DecreaseCounter();
            Destroy(gameObject);
        }
    }
}

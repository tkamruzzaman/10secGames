using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Player movement speed
    private Rigidbody2D rb;      // Reference to Rigidbody2D
    private Vector2 movement;    // Store player's movement input
    private GameController gameController;  // Reference to the MazeGameController

    void Start()
    {
        // Get the Rigidbody2D component from the player object
        rb = GetComponent<Rigidbody2D>();
        // Find the MazeGameController in the scene
        gameController = FindAnyObjectByType<GameController>();
    }

    void Update()
    {
        // Get input from the player (arrow keys or WASD)
        movement.x = Input.GetAxisRaw("Horizontal"); // Left (-1) / Right (1)
        movement.y = Input.GetAxisRaw("Vertical");   // Down (-1) / Up (1)
    }

    void FixedUpdate()
    {
        // Move the player by applying movement to the Rigidbody2D
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // Ensure the player does not pass through walls using colliders
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);     
        if(collision.gameObject.name == gameController.targetBuilding)
        {
            gameController.GameOver(true);  // Player wins
        }
    }
}

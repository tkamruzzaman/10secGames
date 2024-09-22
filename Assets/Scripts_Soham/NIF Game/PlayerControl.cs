using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public float dashSpeed = 5f;             // Speed of the player's dash
    public float dashDuration = 0.5f;        // Duration of the dash
    public Sprite idleSprite;                // Idle sprite
    public Sprite[] attackSprites;           // Attack sprites
    public Sprite victorySprite;             // Victory sprite
    public Sprite defeatSprite;              // Defeat sprite
    private SpriteRenderer spriteRenderer;
    private bool isDashing = false;
    private float dashTimeLeft;
    private Vector3 originalPosition;

    // UI Background Image and Sprites
    public Image backgroundImage;             // Reference to the Image component in the Canvas
    public Sprite normalBackground;           // The normal background during gameplay
    public Sprite victoryBackground;          // The background to show when the player wins
    public Sprite defeatBackground;           // The background to show when the player loses
    [SerializeField]
    GameObject universalGameController;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
        spriteRenderer.sprite = idleSprite;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            StartDash();
        }

        if (isDashing)
        {
            Dash();
        }
    }

    void StartDash()
    {
        SoundManager.instance.PlayActionClip();
        isDashing = true;
        dashTimeLeft = dashDuration;
        spriteRenderer.sprite = attackSprites[Random.Range(0, attackSprites.Length)];
    }

    void Dash()
    {
        dashTimeLeft -= Time.deltaTime;
        if (dashTimeLeft > 0)
        {
            transform.Translate(Vector3.left * dashSpeed * Time.deltaTime);
        }
        else
        {
            EndDash();
        }
    }

    void EndDash()
    {
        isDashing = false;
        spriteRenderer.sprite = idleSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Crowd"))
        {
            CrowdControl crowd = collision.gameObject.GetComponent<CrowdControl>();
            if (crowd != null)
            {
                crowd.Defeat();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameWon();
    }

    public void GameWon()
    {
        SoundManager.instance.PlayWinClip();
        universalGameController.GetComponent<GameManager>().enabled = true;
        GameManager.instance.isWinCondition = true;
        spriteRenderer.sprite = victorySprite;
        // Change background to victory background
        if (backgroundImage != null && victoryBackground != null)
        {
            backgroundImage.sprite = victoryBackground;
        }
        enabled = false; // Disable player control after winning
    }

    public void GameLost()
    {
        SoundManager.instance.PlayLoseClip();
        universalGameController.GetComponent<GameManager>().enabled = true;
        GameManager.instance.isWinCondition = false;

        spriteRenderer.sprite = defeatSprite;
        // Change background to the defeat background
        if (backgroundImage != null && defeatBackground != null)
        {
            backgroundImage.sprite = defeatBackground;
        }
        enabled = false; // Disable player control after losing
    }
}

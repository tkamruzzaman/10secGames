using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject player;         // Reference to the player object
    public TextMeshProUGUI instructionText;      // UI Text to show "Find Building X"
    public TextMeshProUGUI timerText;            // UI Text to show the countdown timer
    public float timeLimit = 10f;     // 10-second timer
    private float timeRemaining;

    // Array to store building tags ('G', 'Z', 'S', 'F')
    private string[] buildingTags = { "BuildingG", "BuildingZ", "BuildingS", "BuildingF" };
    public string targetBuilding;    // The building the player needs to find

    void Start()
    {
        // Set the timer to the time limit
        timeRemaining = timeLimit;

        // Select a random building from the array
        targetBuilding = buildingTags[Random.Range(0, buildingTags.Length)];

        // Update the instruction text to show the correct building to find
        instructionText.text = "Find Building: " + targetBuilding.Substring(8); // Get the 'G', 'Z', 'S', 'F' part from tag
    }

    void Update()
    {
        // Update the countdown timer
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();

            // Check if time has run out
            if (timeRemaining <= 0)
            {
                GameOver(false);
            }
        }
    }

    // End the game and display a win or lose message
    public void GameOver(bool won)
    {
        if (won)
        {
            instructionText.text = "You win!";
        }
        else
        {
            instructionText.text = "Time's up! You lose!";
        }

        // Optionally restart the game after a delay
        Invoke("RestartGame", 2f);  // Restart after 3 seconds
    }

    // Restart the game (reload the scene)
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

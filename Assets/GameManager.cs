using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static GameManager instance;

    // Time in seconds for the countdown
    public float countdownTime = 10f;

    // This will hold the current countdown value
    private float currentTime;

    // Flag to check if the timer is running
    private bool isTimerRunning = false;

    public bool isWinCondition = false;

    public int LifeCounter=5;

    void Start()
    {
        // Start the countdown when the game starts
        StartCountdown();
    }

    // Function to start the countdown
    public void StartCountdown()
    {
        currentTime = countdownTime;
        isTimerRunning = true;
    }

    void Update()
    {
        // If the timer is running, decrease the current time
        if (isTimerRunning)
        {
            // Decrease the current time by the time passed since the last frame
            currentTime -= Time.deltaTime;

            // Clamp the current time to zero (in case it goes negative)
            currentTime = Mathf.Max(currentTime, 0);

            // Debug the remaining time
            Debug.Log("Time remaining: " + Mathf.Ceil(currentTime));

            // Check if the timer has finished
            if (currentTime <= 0)
            {
                TimerFinished();
            }
        }
    }

    // This function is called when the timer reaches zero
    private void TimerFinished()
    {
        isTimerRunning = false;
        Debug.Log("Countdown finished!");

        // Call any function you want after the countdown
        YourFunction();
    }
    // Example of a function that will be called when the timer ends
    private void YourFunction()
    {
        StartCoroutine(LoadSceneAfterSomeTime(Random.Range(0, SceneManager.sceneCountInBuildSettings)));
        // Add your custom code here
    }

    

    public IEnumerator LoadSceneAfterSomeTime(int sceneNumber)
    {
        Debug.Log("Changing scene)");
        yield return new WaitForSeconds(0f);
        SceneManager.LoadScene(sceneNumber);

    }
}

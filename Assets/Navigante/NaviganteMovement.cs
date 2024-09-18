using System.Collections;
using UnityEngine;

public class NaviganteMovement : MonoBehaviour
{
    [SerializeField] private float moveDistance = 5f;  // The distance it will move upwards
    [SerializeField] private float moveSpeed = 2f;     // Speed of the movement
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isMovingUp = true;                    // Tracks if the object is moving up
    private bool isPaused = false;
    bool insideTheBox;
   // private bool insidethebox = false;// Tracks if the movement is paused

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.up * moveDistance;
    }

    private void Update()
    {
        // Toggle pause when spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = true;
            if (insideTheBox)
            {
                
                Debug.Log("You win");
                StartCoroutine(GameManager.instance.LoadSceneAfterSomeTime(0));
            }
            else
            {

                Debug.Log("You loose");
                StartCoroutine(GameManager.instance.LoadSceneAfterSomeTime(1));
            }
        }

        // If paused, do nothing
        if (isPaused)
        {
            return;
        }

        // Move object
        MoveObject();
    }

    private void MoveObject()
    {
        // Determine the direction (up or down)
        Vector3 destination = isMovingUp ? targetPosition : startPosition;

        // Move the object toward the destination
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        // If the object has reached the target position, change direction
        if (Vector3.Distance(transform.position, destination) < 0.01f)
        {
            isMovingUp = !isMovingUp;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "machine")
        {

            insideTheBox = true;
            GetComponent<SpriteRenderer>().color = Color.blue;

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "machine")
        {

            insideTheBox = true;
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "machine")
        {

            insideTheBox = false;
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using System.Collections;
using System;

public class NaviganteMovement : MonoBehaviour
{
    [SerializeField] private float moveDistance = 5f;  // The distance it will move upwards
    [SerializeField] private float moveSpeed = 2f;     // Speed of the movement
    [SerializeField] Transform startPosition;
    [SerializeField] Transform targetPosition;
    [SerializeField] GameObject decisionPanel;
    [SerializeField] List<Sprite> decisionSprites = new List<Sprite>();
    [SerializeField] GameObject gameDoorLeft;
    [SerializeField] GameObject gameDoorRight;
    bool canPassangerMove;
    bool canPassanger2Move;
    [SerializeField] GameObject passanger;
    [SerializeField] GameObject passanger2;
    [SerializeField] GameObject passangerWinTarget;
    [SerializeField] GameObject passangerLossTarget;
    GameObject passangerTarget;
    GameObject passanger2Target;
    Sequence hopSequence;
    Sequence hopSequence2;
    private bool isMovingUp = true;                    // Tracks if the object is moving up
    private bool isPaused = false;
    bool insideTheBox;
   // private bool insidethebox = false;// Tracks if the movement is paused

    private void Start()
    {
        //startPosition = transform.position;
        //targetPosition = startPosition + Vector3.up * moveDistance;
    }

    private void Update()
    {

        MovePassanger();
        MovePassanger2();

        








        // Toggle pause when spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space)&& !isPaused)
        {
            isPaused = true;
            if (insideTheBox)
            {
                GameManager.instance.isWinCondition = true;
                SlideDoorLeft(gameDoorRight, 3,0.5f);
                SlideDoorRight(gameDoorLeft, 3,0.5f);
                passangerTarget = passangerWinTarget;
                passanger2Target = passangerWinTarget;
                canPassanger2Move = true;
                canPassangerMove = true;
                StartHopping();
                StartHopping2();

                Debug.Log("You win");
                decisionPanel.GetComponent<SpriteRenderer>().sprite = decisionSprites[1];

                if (GameManager.instance.currentTime>4)
                {
                    GameManager.instance.currentTime = 4;
                }

                //StartCoroutine(GameManager.instance.LoadSceneAfterSomeTime(0));
            }
            else
            {
                GameManager.instance.isWinCondition = false;
                SlideDoorLeft(gameDoorRight, 0.1f, 0.5f);
                SlideDoorRight(gameDoorLeft, 0.1f, 0.5f);
                passangerTarget = passangerLossTarget;
                passanger2Target = passangerWinTarget;
                canPassangerMove = true;
                canPassanger2Move= true;
                StartHopping();
                StartHopping2();    
                Debug.Log("You loose");
                decisionPanel.GetComponent<SpriteRenderer>().sprite = decisionSprites[2];
                gameDoorLeft.GetComponent<SpriteRenderer>().color = Color.red;
                gameDoorRight.GetComponent<SpriteRenderer>().color = Color.red;
                //StartCoroutine(GameManager.instance.LoadSceneAfterSomeTime(1));
                if (GameManager.instance.currentTime > 4)
                {
                    GameManager.instance.currentTime = 4;
                }
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

    private void MovePassanger()
    {
        if (!canPassangerMove) {
            return;
        }

        passanger.transform.position = Vector3.MoveTowards(passanger.transform.position, passangerTarget.transform.position, 10 * Time.deltaTime);
        Debug.Log("here");
        if (Vector3.Distance(passanger.transform.position, passangerTarget.transform.position) < 1f)
        {
            Debug.Log("not here");
            // Reached destination
            canPassangerMove =false;
            hopSequence.Kill();
            // You can add any additional logic here, like stopping the movement or triggering an event
        }
    }
    private void MovePassanger2()
    {
        if (!canPassanger2Move)
        {
            return;
        }

        passanger2.transform.position = Vector3.MoveTowards(passanger2.transform.position, passanger2Target.transform.position, 10 * Time.deltaTime);
        Debug.Log("here");
        if (Vector3.Distance(passanger2.transform.position, passanger2Target.transform.position) < 1f)
        {
            Debug.Log("not here");
            // Reached destination
            canPassanger2Move = false;
            hopSequence2.Kill();
            // You can add any additional logic here, like stopping the movement or triggering an event
        }
    }

    private void MoveObject()
    {
        // Determine the direction (up or down)
        Vector3 destination = isMovingUp ? targetPosition.position : startPosition.position;

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
           // GetComponent<SpriteRenderer>().color = Color.blue;

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "machine")
        {

            insideTheBox = true;
           // GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "machine")
        {

            insideTheBox = false;
           // GetComponent<SpriteRenderer>().color = Color.green;
        }
    }




    void SlideDoorLeft(GameObject door,float slideDistance, float slideDuration)
    {
        // Move the door to the left by subtracting slideDistance from its x-position
        door.transform.DOMoveX(door.transform.position.x - slideDistance, slideDuration)
            .SetEase(Ease.InOutQuad);  // Optional: Set easing for smooth movement
    }

    public void SlideDoorRight(GameObject door, float slideDistance, float slideDuration)
    {
        door.transform.DOMoveX(door.transform.position.x + slideDistance, slideDuration)
            .SetEase(Ease.InOutQuad);  // Optional: Set easing for smooth movement
    }

    void StartHopping()
    {
        // Create the hop sequence
        hopSequence = DOTween.Sequence();

        for (int i = 0; i < 3; i++)
        {
            hopSequence.Append(passanger.transform.DOLocalMoveY(passanger.transform.position.y + 0.5f, 0.1f).SetEase(Ease.OutQuad));
            hopSequence.Append(passanger.transform.DOLocalMoveY(passanger.transform.position.y, 0.1f).SetEase(Ease.InQuad));
        }

        // Loop the hopping if needed
        
            hopSequence.SetLoops(-1);
        
        
    }
    void StartHopping2()
    {
        // Create the hop sequence
        hopSequence2 = DOTween.Sequence();

        for (int i = 0; i < 3; i++)
        {
            hopSequence2.Append(passanger2.transform.DOLocalMoveY(passanger2.transform.position.y + 0.5f, 0.1f).SetEase(Ease.OutQuad));
            hopSequence2.Append(passanger2.transform.DOLocalMoveY(passanger2.transform.position.y, 0.1f).SetEase(Ease.InQuad));
        }

        // Loop the hopping if needed

        hopSequence2.SetLoops(-1);


    }



}

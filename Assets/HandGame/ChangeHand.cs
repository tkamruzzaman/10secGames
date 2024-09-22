using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeHand : MonoBehaviour
{
    public static ChangeHand Instance;
    [SerializeField]
    GameObject handTimer;

    bool IsHandFixed;
    int i = 0;
    [SerializeField]
    List<GameObject> leftplayerGameObject = new List<GameObject>();
    [SerializeField]
    List<GameObject> rightplayerGameObject = new List<GameObject>();
    [SerializeField]
    private float updateInterval = 0.3f;
    public bool shouldMove = true;
    [SerializeField]
    GameObject decisionpanel;
    [SerializeField]
    List<Sprite> decisionSprites = new List<Sprite>();
    int intitialNumberForLeftHand;
    bool initialState;
    public bool GameStopped;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        decisionpanel.GetComponent<SpriteRenderer>().sprite = decisionSprites[0];
        int initialNumber = Random.Range(0, leftplayerGameObject.Count);

        // Initialize initialNumberForLeftHand with the same value as initialNumber to ensure the loop starts
        intitialNumberForLeftHand = initialNumber;

        // Keep randomizing initialNumberForLeftHand until it's different from initialNumber
        while (intitialNumberForLeftHand == initialNumber)
        {
            intitialNumberForLeftHand = Random.Range(0, rightplayerGameObject.Count);
        }
        leftplayerGameObject[initialNumber].SetActive(true);
        leftplayerGameObject[initialNumber].GetComponent<HandData>().HandSpriteNumber = initialNumber;

        rightplayerGameObject[intitialNumberForLeftHand].SetActive(true);
        rightplayerGameObject[intitialNumberForLeftHand].GetComponent<HandData>().HandSpriteNumber = intitialNumberForLeftHand;
        initialState = true;

        // StartCoroutine(CallChangeGraphicsPeriodically());
    }
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& !GameStopped)
        {
            if (initialState) {

                rightplayerGameObject[intitialNumberForLeftHand].SetActive(false);
                initialState = false;
            
            }

            //IsHandFixed = true;

            if (i >= rightplayerGameObject.Count || i == 0)
            {
                i = 0;
                rightplayerGameObject[rightplayerGameObject.Count - 1].SetActive(false);
            }
            else
            {

                rightplayerGameObject[i - 1].SetActive(false);

            }


            rightplayerGameObject[i].SetActive(true);
            rightplayerGameObject[i].GetComponent<HandData>().HandSpriteNumber = i;
            i++;

        }
    }

    public void ChangeHandGraphics()
    {

        if (!IsHandFixed)
        {
            if (i >= rightplayerGameObject.Count || i == 0)
            {
                i = 0;
                rightplayerGameObject[rightplayerGameObject.Count - 1].SetActive(false);
            }
            else
            {

                rightplayerGameObject[i - 1].SetActive(false);

            }


            rightplayerGameObject[i].SetActive(true);
            rightplayerGameObject[i].GetComponent<HandData>().HandSpriteNumber = i;
            i++;
        }

    }

    public void YouWin()
    {
        SoundManager.instance.PlayWinClip();
        GameManager.instance.isWinCondition = true;
        shouldMove = false;
        IsHandFixed = true;
        decisionpanel.GetComponent<SpriteRenderer>().sprite = decisionSprites[1];
        // StartCoroutine(GameManager.instance.LoadSceneAfterSomeTime(1));
        Debug.Log("You win");
        if (GameManager.instance.currentTime > 4)
        {
            GameManager.instance.currentTime = 4;
        }
    }

    public void YouLose()
    {

        SoundManager.instance.PlayLoseClip();
        GameManager.instance.isWinCondition = false;
        IsHandFixed = true;
        shouldMove = false;
        decisionpanel.GetComponent<SpriteRenderer>().sprite = decisionSprites[2];
        //StartCoroutine(GameManager.instance.LoadSceneAfterSomeTime(0));
        Debug.Log("You Lose");
        if (GameManager.instance.currentTime > 4)
        {
            GameManager.instance.currentTime = 4;
        }
    }


    IEnumerator CallChangeGraphicsPeriodically()
    {
        while (true) // Loop indefinitely
        {
            if (!IsHandFixed)
            {
                ChangeHandGraphics(); // Call the changeGraphics function if isHandFixed is true
            }

            // Wait for 0.3 seconds before the next check
            yield return new WaitForSeconds(updateInterval);
        }
    }

    IEnumerator LoadSceneAfterSomeTime(int sceneNumber)
    {

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneNumber);

    }




}

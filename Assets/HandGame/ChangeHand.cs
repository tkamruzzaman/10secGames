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
    [SerializeField]
    GameObject leftHand;
    [SerializeField]
    GameObject rightHand;
    bool IsHandFixed;
    int i = 0;
    [SerializeField]
    List<Sprite> leftplayerSprite = new List<Sprite>();
    [SerializeField]
    List<Sprite> rightplayerSprite = new List<Sprite>();
    [SerializeField]
    private float updateInterval = 0.3f;
    public bool shouldMove = true;
    [SerializeField]
    GameObject decisionpanel;
    [SerializeField]
    List<Sprite> decisionSprites = new List<Sprite>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        decisionpanel.GetComponent<SpriteRenderer>().sprite = decisionSprites[0];
        int initialNumber  = Random.Range(0, leftplayerSprite.Count);
        leftHand.GetComponent<SpriteRenderer>().sprite = leftplayerSprite[initialNumber];
        leftHand.GetComponent<HandData>().HandSpriteNumber = initialNumber;
        StartCoroutine(CallChangeGraphicsPeriodically());
    }
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

           IsHandFixed = true;
        }
    }

    public void ChangeHandGraphics()
    {
        if (!IsHandFixed)
        {
            if (i>=rightplayerSprite.Count)
            {
                i= 0;
            }


            rightHand.GetComponent<SpriteRenderer>().sprite = rightplayerSprite[i];
            rightHand.GetComponent<HandData>().HandSpriteNumber = i;
            i++;
        }

    }

    public void YouWin()
    {
        shouldMove = false;
        IsHandFixed = true ;
        decisionpanel.GetComponent<SpriteRenderer>().sprite = decisionSprites[1];
        StartCoroutine(GameManager.instance.LoadSceneAfterSomeTime(1));
        Debug.Log("You win");
    }

    public void YouLose()
    {
        IsHandFixed = true;
        shouldMove = false;
        decisionpanel.GetComponent<SpriteRenderer>().sprite = decisionSprites[2];
        StartCoroutine(GameManager.instance.LoadSceneAfterSomeTime(0));
        Debug.Log("You Lose");
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

    
    


}

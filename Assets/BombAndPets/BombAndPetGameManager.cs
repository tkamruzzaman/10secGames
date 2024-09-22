using NUnit.Framework;
using OkapiKit;
using System.Collections.Generic;
using UnityEngine;

public class BombAndPetGameManager : MonoBehaviour
{


    [SerializeField]
    Sprite winSprite;

    [SerializeField]
    Sprite loseSprite;

    [SerializeField]
    GameObject DecisionPanel;

    [SerializeField]
    Spawner spawner;

    [SerializeField]
    GameObject boom;

    [SerializeField]
    GameObject cursor;


    [SerializeField]
    List<GameObject> pets;

    bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentTime <= 1 && !isPaused)
        {
            
            GameManager.instance.currentTime = 4;
            GameManager.instance.dontShowTimer = true;
            
            Win();

        }
    }

    public void Win(){
        foreach (GameObject gm in pets)
        {
            gm.GetComponent<BreathingEffect>().enabled = true;
        }
        SoundManager.instance.PlayWinClip();
        spawner.enabled = false;
        foreach (Transform T in spawner.transform) { 
            Destroy(T.gameObject);
        }
        isPaused = true;
        GameManager.instance.isWinCondition = true;
        DecisionPanel.GetComponent<SpriteRenderer>().sprite = winSprite;
        cursor.SetActive(false);

        foreach (GameObject gm in pets)
        {
            gm.GetComponent<BreathingEffect>().enabled = true;   
        }

    }

    public void Lose(){

        SoundManager.instance.PlayLoseClip();
        spawner.enabled = false;
        isPaused = true;
        GameManager.instance.isWinCondition = false;
        DecisionPanel.GetComponent<SpriteRenderer>().sprite = loseSprite;
        boom.SetActive(true);
        cursor.SetActive(false);
    }
}

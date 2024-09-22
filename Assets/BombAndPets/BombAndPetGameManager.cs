using UnityEngine;

public class BombAndPetGameManager : MonoBehaviour
{


    [SerializeField]
    Sprite winSprite;
    
    [SerializeField]
    GameObject DecisionPanel;

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
        isPaused = true;
        GameManager.instance.isWinCondition = true;
        DecisionPanel.GetComponent<SpriteRenderer>().sprite = winSprite;
    }

    public void Lose(){
        isPaused = true;
        GameManager.instance.isWinCondition = false;
    }
}

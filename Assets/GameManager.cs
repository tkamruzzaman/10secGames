using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadSceneAfterSomeTime(int sceneNumber)
    {
        Debug.Log("Changing scene)");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneNumber);

    }
}

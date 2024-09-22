using TMPro;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;
    public int maxTickets = 10;
    private int currentTickets;
    public TextMeshProUGUI ticketCounterText;
    public PlayerControl playerControl;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        currentTickets = maxTickets;
        UpdateTicketCounter();
    }

    void UpdateTicketCounter()
    {
        ticketCounterText.text = currentTickets.ToString();
    }

    public void DecreaseCounter()
    {
        currentTickets--;
        UpdateTicketCounter();

        if (currentTickets <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        // Use the new FindObjectsByType method instead of the deprecated FindObjectsOfType
        CrowdControl[] crowds = Object.FindObjectsByType<CrowdControl>(FindObjectsSortMode.None);

        // Stop all crowd movement and disable player controls
        foreach (CrowdControl crowd in crowds)
        {
            crowd.isMoving = false;
        }

        playerControl.GameLost();
    }
}

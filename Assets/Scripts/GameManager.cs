using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI finalMessage;

    void Start()
    {
        
    }

    void Update()
    {
        if (Score.playerScore > 55000) {
            Time.timeScale = 0f;
            // winText.gameObject.SetActive(true);
            finalMessage.text = "You Win!";
        }

        if (Soldier.playerHealth <= 0) {
            Time.timeScale = 0f;
            // loseText.gameObject.SetActive(true);
            finalMessage.text = "You Lose :(";
        }
    }
}

using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static int playerScore = 0;


    void Start()
    {

    }

    void Update()
    {
        playerScore += (int)TimeManager.Second;
        scoreText.text = playerScore.ToString();
    }

}
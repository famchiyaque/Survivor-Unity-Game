using UnityEngine;
using TMPro;

public class ShotCounter : MonoBehaviour
{
    public TextMeshProUGUI shotCounterText;
    public static int shotCounter = 0;

    void Start()
    {

    }

    void Update()
    {
        shotCounterText.text = ("Shots: " + shotCounter).ToString();
    }

}
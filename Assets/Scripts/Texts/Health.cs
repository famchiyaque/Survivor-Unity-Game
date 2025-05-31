using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public TextMeshProUGUI playerHealthText;

    void Start()
    {

    }

    void Update()
    {
        playerHealthText.text = ("Health: " + Soldier.playerHealth).ToString();
    }

}
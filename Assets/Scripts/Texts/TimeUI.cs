using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private void OnEnable()
    {
        TimeManager.OnSecondChanged += UpdateTime;
        TimeManager.OnMinuteChanged += UpdateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnSecondChanged -= UpdateTime;
        TimeManager.OnMinuteChanged -= UpdateTime;
    }

    private void UpdateTime()
    {
        timeText.text = $"{TimeManager.Minute.ToString("00")}:{TimeManager.Second:00}";
    }
}
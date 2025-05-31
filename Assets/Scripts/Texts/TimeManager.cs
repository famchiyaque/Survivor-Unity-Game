using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static Action OnSecondChanged;
    public static Action OnMinuteChanged;
    
    public static int Second { get; private set; }
    public static int Minute { get; private set; }
    
    private float secondTimer;
    private const float realTimePerSecond = 1.0f; // 1 real second = 1 game second
    
    void Start()
    {
        Second = 0;
        Minute = 0;
        secondTimer = realTimePerSecond;
    }

    void Update()
    {
        secondTimer -= Time.deltaTime;
        
        if(secondTimer <= 0)
        {
            Second++;
            OnSecondChanged?.Invoke();
            
            // Reset the timer for the next second
            secondTimer = realTimePerSecond;
            
            // Check if minute has changed
            if(Second >= 60)
            {
                Second = 0;
                Minute++;
                OnMinuteChanged?.Invoke();
            }
        }
    }
}
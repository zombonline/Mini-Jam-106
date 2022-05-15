using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField] float maxTime, unlockedTime;
    public float timeRemaining;
    [SerializeField] Image unlockedTimeImage, clockArmImage;
    [SerializeField] UnityEvent timeUp;
    bool clockMoving = false;

    public void ResetClock()
    {
        timeRemaining = unlockedTime;
        clockMoving = true;

    }
    private void Awake()
    {
        timeRemaining = unlockedTime;
    }
    private void Update()
    {
        unlockedTimeImage.fillAmount = unlockedTime / maxTime;

        clockArmImage.rectTransform.eulerAngles = new Vector3(0, 0, -((timeRemaining / maxTime) * 360));
        if (clockMoving)
        {
            timeRemaining -= Time.deltaTime;
        }
        if(timeRemaining <= 0 && clockMoving)
        {
            timeUp.Invoke();
            timeRemaining = 0;
            clockMoving = false;
        }
    }


    public void IncreaseUnlockedTime(float increaseValue)
    {
        unlockedTime += increaseValue;
        if(unlockedTime > maxTime)
        {
            unlockedTime = maxTime;
        }
    }
}

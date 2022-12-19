using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SunRot_Hours_Days : MonoBehaviour
{
    [SerializeField]
    private float timeMultiplier;

    [SerializeField]
    private float startHour;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private Light sunLight;

    [SerializeField]
    private float sunriseHour;

    [SerializeField]
    private float sunsetHour;

    [SerializeField]
    private Color dayAmbientLight;

    [SerializeField]
    private Color nightAmbientLight;

    [SerializeField]
    private AnimationCurve lightChangeCurve;

    [SerializeField]
    private float maxSunLightIntensity;

    [SerializeField]
    private Light moonLight;

    [SerializeField]
    private float maxMoonLightIntensity;

    [SerializeField]
    private TextMeshProUGUI dayCounter;

    [SerializeField]
    private float numberOfDay;

    private DateTime currentTime;
    public string currentDay;

    private TimeSpan sunriseTime;

    private TimeSpan sunsetTime;

    // Time Multiplier will be 28 units as like that 2 minutes in real world means 1 hour in game world ////// You can changed it if you need for tests but then make sure you will put 28 units again (: :) THX <3

    // Start is called before the first frame update
    void Start()
    {

        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour); //set up the current time in game
        currentDay = currentTime.ToString("d");
        sunriseTime = TimeSpan.FromHours(sunriseHour); //set up the sunrise time
        sunsetTime = TimeSpan.FromHours(sunsetHour); //set up the sunset time       

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
        DaysCounter();
    }

    private void UpdateTimeOfDay()  // to set up what format of the time will be showing at the screen + set up the multiplier of the game time clock comparing with real time clock
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if (timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
            
        }
    }

    private void RotateSun() // to make the sun rotating according to the day time
    {
        float sunLightRotation;

        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private void UpdateLightSettings() //to make the environment lighter or darker according to the day time
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }


    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime) // to make the format hour in 24 hours and reset to 0 every 24 hours
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }

    private void DaysCounter()
    {
        dayCounter.text = numberOfDay.ToString("Day " + numberOfDay); //set up the days in game screen
      

        if(currentTime.ToString("d") != currentDay) //make the date goes up everytime the clock reach 00:00h
        {
            numberOfDay += 1;
            currentDay = currentTime.ToString("d");
        }
       // else return;
    }
}

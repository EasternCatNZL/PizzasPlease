using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameTimer : MonoBehaviour {

    public float myTimer = 90.0f;
    public Text timerText;

    private bool Timer = false;
	// Use this for initialization
	void Start () {
        timerText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Timer)
        {

            myTimer -= Time.deltaTime;

            if (myTimer < 0.0f)
                myTimer = 0.0f;

            float rounded = Mathf.Floor(myTimer);

            string minutes = Mathf.Floor(myTimer / 60.0f).ToString("00");
            string seconds = Mathf.Floor(myTimer % 60.0f).ToString("00");
            string milli = Mathf.Floor((myTimer - rounded) * 100.0f).ToString("00");
            timerText.text = minutes + ":" + seconds + ":" + milli;
            if(myTimer <= 0.0f)
            {
                Timer = false;
            }
        }
	}

    public void StartTimer() { Timer = true; }
    public void SetTimer(float _NewTime) { myTimer = _NewTime; }
}

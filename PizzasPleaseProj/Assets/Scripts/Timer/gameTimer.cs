using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameTimer : MonoBehaviour {

    public float myTimer = 90;
    public Text timerText;


	// Use this for initialization
	void Start () {
        timerText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        myTimer -= Time.deltaTime;
        string minutes = Mathf.Floor(myTimer / 60).ToString("00");
        string seconds = Mathf.Floor(myTimer % 60).ToString("00");
        string milli = Mathf.Floor(myTimer % 1000).ToString("0000");
        timerText.text = minutes+":"+seconds+":"+milli;
        print(myTimer);
	}
}

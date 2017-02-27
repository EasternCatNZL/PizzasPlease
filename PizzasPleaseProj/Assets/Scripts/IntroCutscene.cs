using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroCutscene : MonoBehaviour
{

    public Image Background;
    public Image TextBacking;
    public Image[] Scenes;
    public string[] SceneText;
    public Text TextBox;
    public float FadeDelay;

    private int SceneIndex = 0;
    private bool ChangeScene = false;
    private bool FadeIn = false;
    private float lastTime = 0.0f;
    private float Increment = 0.05f;
    // Use this for initialization
    void Start()
    {
        if(TextBox != null)
            TextBox.GetComponent<TypeWriter>().SetNewText(SceneText[SceneIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        if (ChangeScene && SceneIndex < Scenes.Length)
        {
            Image tempImage = Scenes[SceneIndex].GetComponent<Image>();
            float Delay = FadeDelay * Mathf.Abs(Increment);
            Color newColor = tempImage.color;
            if (Time.time - lastTime > Delay)
            {
                newColor = tempImage.color;
                newColor.a -= Increment;
                tempImage.color = newColor;
                if(TextBox != null)
                    TextBox.color = newColor;

                lastTime = Time.time;
            }

            if (FadeIn == false && tempImage.color.a <= 0.0f)
            {
                SceneIndex++;
                Increment *= -1;
                if(TextBox != null)
                    TextBox.text = "";
                FadeIn = true;
            }
            else if (tempImage.color.a >= 1)
            {
                ChangeScene = false;
                Increment *= -1;
                if(TextBox != null)
                    TextBox.GetComponent<TypeWriter>().SetNewText(SceneText[SceneIndex]);
                FadeIn = false;
            }
            if (SceneIndex == Scenes.Length)
            {
                Increment *= -1;
                SceneIndex++;
                if(TextBacking != null)
                    TextBacking.enabled = false;
                if (TextBox != null)
                    TextBox.enabled = false;
            }
        }
        if (SceneIndex == Scenes.Length + 1 && Background.color.a > 0.0f)
        {

            float Delay = FadeDelay * Mathf.Abs(Increment);
            Color newColor = Background.color;
            if (Time.time - lastTime > Delay)
            {
                newColor = Background.color;
                newColor.a -= Increment;
                Background.color = newColor;
                if(TextBacking != null)
                    TextBacking.color = newColor;
                if(TextBox != null)
                    TextBox.color = newColor;

                lastTime = Time.time;
            }
        }
        else if (SceneIndex == Scenes.Length + 1 && Background.color.a <= 0.0f)
        {
            GameObject.Find("GameController").GetComponent<GameController>().StartRound();
            Destroy(gameObject);
        }
    }

    public void NextScene()
    {
        ChangeScene = true;
    }
}

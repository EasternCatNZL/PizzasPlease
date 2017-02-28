using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroCutscene : MonoBehaviour
{

    public Image Background;
    public Image TextBacking;
    public Image[] Scenes;
    public string[] SceneText;
    public Text TextBox;
    public float FadeDelay;
    public GameObject gameController;
    public bool isIntroScene;

    public int SceneIndex = 0;
    public bool ChangeScene = false;
    public bool FadeIn = false;
    public float lastTime = 0.0f;
    public float Increment = 0.05f;
    public AudioSource audioSource;
    public Image tempImage;

    // Use this for initialization
    void Start()
    {
        if(TextBox != null)
            TextBox.GetComponent<TypeWriter>().SetNewText(SceneText[SceneIndex]);
        if(gameController != null)
            audioSource = gameController.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float Delay;
        if (isIntroScene && ChangeScene && SceneIndex < Scenes.Length)
        {
            tempImage = Scenes[SceneIndex].GetComponent<Image>();
            Delay = FadeDelay * Mathf.Abs(Increment);
            Color newColor = tempImage.color;
            if (Time.time - lastTime > Delay)//Fade image
            {
                newColor = tempImage.color;
                newColor.a -= Increment;
                tempImage.color = newColor;
                if(TextBox != null)
                    TextBox.color = newColor;

                lastTime = Time.time;
            }

            if (FadeIn == false && tempImage.color.a <= 0.0f)//Fade out scene
            {
                SceneIndex++;
                Increment *= -1;
                if(TextBox != null)
                    TextBox.text = "";
                FadeIn = true;
            }
            else if (FadeIn == true && tempImage.color.a >= 1)//Finished fading in new scene
            {
                ChangeScene = false;
                Increment *= -1;
                if(TextBox != null)
                    TextBox.GetComponent<TypeWriter>().SetNewText(SceneText[SceneIndex]);
                FadeIn = false;
                if(!isIntroScene)
                {
                    NextScene();
                }
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
        //For ending cutscenes
        else if (!isIntroScene && ChangeScene && SceneIndex < Scenes.Length)
        {
            tempImage = Scenes[SceneIndex].GetComponent<Image>();
            Delay = FadeDelay * Mathf.Abs(Increment);
            Color newColor = tempImage.color;
            if (Time.time - lastTime > Delay)//Fade image
            {
                newColor = tempImage.color;
                newColor.a -= Increment;
                tempImage.color = newColor;
                if (TextBox != null)
                    TextBox.color = newColor;

                lastTime = Time.time;
            }

            if (FadeIn == false && tempImage.color.a <= 0.0f)//Fade in next scene
            {
                SceneIndex++;
                Increment *= -1;
                if (TextBox != null)
                    TextBox.text = "";
                FadeIn = true;
            }
            else if (FadeIn == true && tempImage.color.a >= 1.0f)//Finished fading in new scene
            {
                ChangeScene = false;
                lastTime = Time.time;
                Increment *= -1;
                if (TextBox != null)
                    TextBox.GetComponent<TypeWriter>().SetNewText(SceneText[SceneIndex]);
                FadeIn = false;
            }
            if (SceneIndex == Scenes.Length)
            {
                Increment *= -1;
                SceneIndex++;
                if (TextBacking != null)
                    TextBacking.enabled = false;
                if (TextBox != null)
                    TextBox.enabled = false;
            }
        }
        //Ending Cutscene Timer
        Delay = FadeDelay * Mathf.Abs(Increment);

        if (!isIntroScene && !ChangeScene)
        {
            if (Time.time - lastTime > 1.0f)//Fade image
            {
                ChangeScene = true;
                lastTime = Time.time;
            }
        }

        if (SceneIndex == Scenes.Length + 1 && Background.color.a > 0.0f)
        {

            Delay = FadeDelay * Mathf.Abs(Increment);
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

        if (SceneIndex == Scenes.Length + 1 && Background.color.a <= 0.0f)
        {
            if (isIntroScene)
            {
                audioSource.Play();
                GameObject.Find("GameController").GetComponent<GameController>().StartRound();
                Destroy(gameObject);
            }
        }
        else if (SceneIndex >= Scenes.Length  && !isIntroScene)
        {
            SceneManager.LoadScene(0);
        }
       
    }

    public void NextScene()
    {
        ChangeScene = true;
    }
}

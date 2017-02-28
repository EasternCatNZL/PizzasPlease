using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour
{

    public string fileName = "";
    public float timeDelay = 0.05f;

    private Text textBox;
    private string textToPrint = "";
    private float lastTime;
    private int index;
    private bool Done = false;
    // Use this for initialization
    void Awake()
    {
        textBox = gameObject.GetComponent<Text>();
        textToPrint = textBox.text;
        textBox.text = "";
    }

    void Start()
    {         
        lastTime = Time.time;
        index = 0;
        if(fileName != "")
        {
            LoadText(fileName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!Done)
        {
            TypeText();
        }
    }

    public void SetNewText(string _newText)
    {
        textBox.text = "";
        textToPrint = _newText;
        index = 0;
        Done = false;
    }

    public void LoadNewText(string _fileName)
    {
        textBox.text = "";
        LoadText(_fileName);
        index = 0;
        Done = false;
    }

    void TypeText()
    { 
        string tempString = textBox.text;
        float randomDelay = Random.Range(0.0f, 0.05f);
        if (Time.time - lastTime >= timeDelay + randomDelay && index < textToPrint.Length)
        {
            tempString += textToPrint[index];
            textBox.text = tempString;
            lastTime = Time.time;
            index++;
        }
        else if (index == textToPrint.Length && Done == false)
        {
            GameObject.Find("IntroCutscene").GetComponent<IntroCutscene>().NextScene();
            Done = true;
        }
    }

    bool LoadText(string fileName)
    {
        textToPrint = "";
        try
        {
            string line;
            // Create a new StreamReader, tell it which file to read and what encoding the file
            // was saved as
            StreamReader theReader = new StreamReader(fileName, Encoding.Default);
            // Immediately clean up the reader after this block of code is done.
            // You generally use the "using" statement for potentially memory-intensive objects
            // instead of relying on garbage collection.
            // (Do not confuse this with the using directive for namespace at the 
            // beginning of a class!)
            using (theReader)
            {
                // While there's lines left in the text file, do this:
                do
                {
                    line = theReader.ReadLine();

                    if (line != null)
                    {
                        textToPrint += line;
                    }
                }
                while (line != null);
                // Done reading, close the reader and return true to broadcast success    
                theReader.Close();
                return true;
            }
        }
        // If anything broke in the try block, we throw an exception with information
        // on what didn't work
        catch (System.Exception e)
        {
            Debug.LogException(e, this);
            return false;
        }
    }
}



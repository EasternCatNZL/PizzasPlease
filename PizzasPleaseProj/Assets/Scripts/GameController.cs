using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int DrugCount = 0;
    public Text OutputScreen;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PizzaCheck(bool Legit)
    {
        GameObject[] Drugs = GameObject.FindGameObjectsWithTag("Drugs");
        DrugCount = 0;
        foreach(GameObject Drug in Drugs)
        {
            DrugCount++;
        }
        if(Legit && DrugCount > 0)
        {
            OutputScreen.text = "You Failed. Children Have Died.";
        }
        else if (Legit && DrugCount == 0)
        {
            OutputScreen.text = "Good Job.";
        }
        else if (!Legit && DrugCount > 0)
        {
            OutputScreen.text = "You Got Them.";
        }
        else if (!Legit && DrugCount == 0)
        {
            OutputScreen.text = "You Ruined a Finne Pizza.";
        }
        else if (DrugCount < 0)
        {
            OutputScreen.text = "You Found Negative Drugs. Well Done.";
        }
    }

    void NextPizza()
    {

    }
}

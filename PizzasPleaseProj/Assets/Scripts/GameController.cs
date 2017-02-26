using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float RoundLength = 60.0f;
    public int DrugCount = 0;
    public Text OutputScreen;
    public GameObject[] Pizzas;

    private int PizzaIndex = 0;
    private GameObject CurrentPizza;
    private GameObject Timer;
    public List<GameObject> Slices;
    //Scoring
    private int TotalPizzas;
    private int WrongPizzas;
    private int WrongSlices;

    // Use this for initialization
    void Start()
    {
        Timer = GameObject.Find("Timer");
        if (Timer == null)
        {
            Debug.Log("No \"Timer\" GameObject was found.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartRound()
    {
        Timer.GetComponent<gameTimer>().SetTimer(RoundLength);
        Timer.GetComponent<gameTimer>().StartTimer();
        NextPizza();
    }

    public void PizzaCheck(bool Legit)
    {
        TotalPizzas++;
        if(!DrugCheck(Legit))
        {
            WrongPizzas++;  
        }
        WrongSlices = SliceCheck();
        if (PizzaIndex < Pizzas.Length)
        {
            NextPizza();
        }
        else
        {
            //End the game and show score.
        }
    }

    private bool DrugCheck(bool Legit)
    {
        GameObject[] Drugs = GameObject.FindGameObjectsWithTag("Drugs");
        DrugCount = 0;
        foreach (GameObject Drug in Drugs)
        {
            DrugCount++;
        }
        if (Legit && DrugCount > 0)//Passed a drugged pizza
        {
            return false;
        }
        else if (Legit && DrugCount <= 0)//Passed a drug-free pizza
        {
            return true;
        }
        else if (!Legit && DrugCount > 0)//Tossed a drugged pizza
        {
            return true;
        }
        else if (!Legit && DrugCount <= 0)//Tossed a perfectly fine pizza
        {
            return false;
        }
        else//Heck, something happened and you probably failed everything doing it
        {
            return false;
        }
    }

    private int SliceCheck()
    {
        int WrongSlices = 0;
        foreach(GameObject Slice in Slices)
        {
            if(!Slice.GetComponent<Slice>().CheckTopping())
            {
                WrongSlices++;
            }
        }
        return WrongSlices;
    }

    void NextPizza()
    {
        if (CurrentPizza != null)
        {
            Destroy(CurrentPizza);
        }
        Slices.Clear();
        
        Instantiate(Pizzas[PizzaIndex], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        CurrentPizza = GameObject.FindWithTag("Pizza");
        for(int i = 1; i < 13; ++i)
        {
            string index = i.ToString();
            Slices.Add(GameObject.Find("PizzaSlice" + index));
        }
        foreach(GameObject Slice in Slices)
        {
            Slice.GetComponent<Slice>().BakeSlice();
        }
        PizzaIndex++;
    }
}

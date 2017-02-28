using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Image Scoreboard;
    public Text ScoreWrong;
    public Text ScoreCorrect;
    public float RoundLength = 60.0f; //timer for round
    public int DrugCount = 0; //Number of drugs on pizza
    public Text OutputScreen; //reference to text in ui for messages
    public GameObject[] Pizzas; //array of possible pizzas
    public GameObject GoodCutscene;
    public GameObject BadCutscene;
    private bool GameEnded = false;
    private bool goodEnd;

    public int PizzaIndex = 0; //index of current pizze
    public GameObject CurrentPizza; //reference to current pizze
    private GameObject Timer; //reference to timer
    public List<GameObject> Slices; //list of slices on pizza
    //Scoring
    public float TotalPizzas; //total pizzas checked
    public float WrongPizzas; //total pizzas incorrect
    public float WrongSlices; //total slices incorrect

    // Use this for initialization
    void Start()
    {
        Scoreboard.gameObject.SetActive(false);
        Timer = GameObject.Find("Timer");
        if (Timer == null)
        {
            Debug.Log("No \"Timer\" GameObject was found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.GetComponent<gameTimer>().myTimer == 0.0f && TotalPizzas > 1 && !GameEnded)
        {
            string WrongPizzaText = WrongPizzas.ToString();
            string CorrectPizzaText = (TotalPizzas - WrongPizzas).ToString();
            ScoreWrong.text = WrongPizzaText;
            ScoreCorrect.text = CorrectPizzaText;
            Scoreboard.gameObject.SetActive(true);
            GameEnded = true;
            print(WrongPizzas);
            print(TotalPizzas);
            print(WrongPizzas / TotalPizzas);
            if((WrongPizzas/TotalPizzas) * 100.0f > 25.0f)
            {
                print("Bad Ending");
                StartCoroutine(Ending(false));
            }
            else
            {
                print("Good Ending");
                StartCoroutine(Ending(true));
            }
        }
    }

    //setup func for one round, till timer finishes
    public void StartRound()
    {
        Timer.GetComponent<gameTimer>().SetTimer(RoundLength);
        Timer.GetComponent<gameTimer>().StartTimer();
        NextPizza();
    }

    //checks the pizza once complete
    public void PizzaCheck(bool Legit)
    {
        //if have not yet checked all pizzas in round, go to next pizza
        if (PizzaIndex < Pizzas.Length)
        {
            //increment the number of pizzas checked so far
            TotalPizzas++;
            //check the slices individually
            WrongSlices = SliceCheck();
            //check if the pizza was checked correctly
            if (!DrugCheck(Legit))
            {
                //if wrong, increment to incorrectly checked pizzas
                print("Wrong Button");
                WrongPizzas++;
                WrongSlices = 0;
            }
            else if (Legit && WrongSlices > 0)
            {
                print("Wrong Slices");
                WrongPizzas++;
                WrongSlices = 0;
            }
            NextPizza();
        }
        else if(PizzaIndex == Pizzas.Length)
        {

        }
        else   //else end the game
        {
            //End the game and show score.
        }
    }

    //takes player input and checks if the player is correct
    private bool DrugCheck(bool Legit)
    {
        //GameObject[] Drugs = GameObject.FindGameObjectsWithTag("Drugs");
        //DrugCount = 0;
        //print(DrugCount);
        //foreach (GameObject Drug in Drugs)
        //{
        //    DrugCount++;
        //}
        DrugCount = CurrentPizza.GetComponent<Pizza>().DrugCount;
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

    //checks the slices on the pizza, and increments number by however many were present in the current pizza
    private int SliceCheck()
    {
        int _WrongSlices = 0;
        //check for each slice on pizza
        foreach (GameObject Slice in Slices)
        {
            //if slice has a drug, increment
            if (!Slice.GetComponent<Slice>().CheckTopping())
            {
                _WrongSlices++;
            }
        }
        return _WrongSlices;
    }

    //get the next pizza
    void NextPizza()
    {
        print("Next Pizza");
        if (CurrentPizza != null)
        {
            Destroy(CurrentPizza);
        }
        //CurrentPizza = GameObject.FindWithTag("Pizza");
        //for (int i = 1; i < 13; ++i)
        //{
        //    string index = i.ToString();
        //    Slices.Add(GameObject.Find("PizzaSlice" + index));
        //}
        StartCoroutine(CreatePizza());
        Slices.Clear();
        StartCoroutine(FindPizza());
        StartCoroutine(BakeSlices());
    }

    IEnumerator CreatePizza()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(Pizzas[PizzaIndex], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        CurrentPizza = GameObject.FindWithTag("Pizza");
        PizzaIndex++;
    }

    IEnumerator FindPizza()
    {
        yield return new WaitForSeconds(0.2f);   
        for (int i = 1; i < 13; ++i)
        {
            string index = i.ToString();
            Slices.Add(GameObject.Find("PizzaSlice" + index));
        }
    }

    IEnumerator BakeSlices()
    {

        yield return new WaitForSeconds(0.3f);
        foreach (GameObject Slice in Slices)
        {
            Slice.GetComponent<Slice>().BakeSlice();
        }
    }

    IEnumerator Ending(bool goodEnd)
    {
        yield return new WaitForSeconds(5.0f);
        if (goodEnd)
        {
            GoodCutscene.SetActive(true);
            GoodCutscene.GetComponent<IntroCutscene>().NextScene();
        }
        else
        {
            BadCutscene.SetActive(true);
            BadCutscene.GetComponent<IntroCutscene>().NextScene();
        }
    }
}

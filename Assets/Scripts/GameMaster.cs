using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMaster : MonoBehaviour
{
	public AudioSource timeToDeliver;
    private static int currentPizzaBalls;

    
    private static int delivered;
    private static int totalDelivered;

    public int minTime;

    private int min;
    private int secs;
    private int millisecs;
    private static TextMeshProUGUI deliveriesDone;
    public TextMeshProUGUI finalScore;
    public HighScore scoreBoard;
    private static List<GameObject> deliveryHouses;
    private static List<int> indexesPicked;
    private int amountofHouses = 1;
    private int lastAmountofHoues = 1;
    public int houseIncrementation;
    private Animator canvasAnimation;
    public static bool dead;
    

    
    


    public TimeLeft time;

	// Start is called before the first frame update
	void Start()
    {
        deliveriesDone = GameObject.FindWithTag("PizzaBallImage").GetComponent<TextMeshProUGUI>();
        deliveriesDone.text = "x" + amountofHouses;
        canvasAnimation = GameObject.Find("Canvas").GetComponent<Animator>();
        min = minTime;
        secs = 0;
        millisecs = 0;
        GameObject[] availableHouses = GameObject.FindGameObjectsWithTag("House");
        deliveryHouses = new List<GameObject>();
        indexesPicked = new List<int>();
        for(int i = 0; i< amountofHouses; i++){
            
            int pickedIndex = Random.Range(0,availableHouses.Length-1);
            if(indexesPicked.Contains(pickedIndex)){
                pickedIndex = Random.Range(0,availableHouses.Length-1);
            }
            indexesPicked.Add(pickedIndex);
            GameObject pickedHouse = availableHouses[pickedIndex];
            pickedHouse.AddComponent<deliverSpot>();
            Debug.Log(pickedHouse.transform.parent.gameObject.name);
            deliveryHouses.Add(pickedHouse);
        }
        
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Sets current pizza ball counter after starting has been set
        millisecs = (int)(millisecs - Time.deltaTime);
        if(millisecs <=0){
            secs--;
            millisecs = 59;
        }
        if(min > 0){
            if(secs <= 0){
                min--;
                secs = 59;
            }
        }
        
        if(min <= 0 && secs <= 0 && !dead){
            scoreBoard.AddHighScoreEntry(totalDelivered,"GameJamScores");
            Death();
            finalScore.text = totalDelivered +"";
            
            
        }else if(!dead){
            time.changeTime(min,secs);
        }
        if(Input.GetKey(KeyCode.Space) && dead){
            transform.GetChild(0).gameObject.SetActive(true);
            dead = false;
        }
        if(Input.GetKey(KeyCode.Escape)){
            Application.Quit();
        }

        if(currentPizzaBalls <= 0 && delivered != amountofHouses){
            time.Dead();
        }

        if(amountofHouses-delivered == 0){

            amountofHouses = lastAmountofHoues+houseIncrementation;
            lastAmountofHoues = amountofHouses;
            min = minTime;
            secs = 0;
            millisecs = 0;
            delivered = 0;
            randomPickHouse();

			timeToDeliver.Play();
        }
        

    }

    private void randomPickHouse(){
        GameObject[] availableHouses = GameObject.FindGameObjectsWithTag("House");
        deliveryHouses = new List<GameObject>();
        indexesPicked = new List<int>();
        for(int i = 0; i< amountofHouses; i++){
            
            int pickedIndex = Random.Range(0,availableHouses.Length-1);
            if(indexesPicked.Contains(pickedIndex)){
                pickedIndex = Random.Range(0,availableHouses.Length-1);
            }
            indexesPicked.Add(pickedIndex);
            GameObject pickedHouse = availableHouses[pickedIndex];
            pickedHouse.AddComponent<deliverSpot>();
            Debug.Log(pickedHouse.transform.parent.gameObject.name);
            deliveryHouses.Add(pickedHouse);
        }
        deliveriesDone.text = "x" + amountofHouses;
    }



    //Uses a pizza ball
    public static void usedPizzaBalls(){
        currentPizzaBalls--;
    }
    public static GameObject[] delieverySpots(){
        return deliveryHouses.ToArray();
    }

    public static void hasDelivered(){
        delivered++;
        totalDelivered++;
        deliveriesDone.text = "x" + (deliveryHouses.Count-delivered);
    }

    public static bool deliverDone(){
        return delivered == deliveryHouses.Count;
    }
    public static void setPizzaBall(int amount){
        currentPizzaBalls = amount;
    }


    //Temporary Death text
    public void Death(){
        canvasAnimation.SetTrigger("Dead");
        dead = true;
    }
}

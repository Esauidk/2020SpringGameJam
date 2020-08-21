using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeLeft : MonoBehaviour
{
    public TextMeshProUGUI time;
    
    public void changeTime(int min, int sec){
        if(min > -1){
            if(sec < 10){
                time.text = min + " : 0" + sec;
            }else{
                time.text = min + " : " + sec;
            }
            
        }else{
            Debug.LogError("A time that doesn't exist is trying to be set");
        }
    }

    public void Dead(){
        time.text = "Temporary:Dead";
    }
}

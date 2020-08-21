using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switchToScene : MonoBehaviour
{
    public bool OnAwakeSwitch;
    public string desiredSceneName;
    // Start is called before the first frame update
    private void Awake() {
        if(OnAwakeSwitch){
            switchScene(desiredSceneName);
        }    
    }

    public void switchScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}

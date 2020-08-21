using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deliverSpot : MonoBehaviour
{
    bool done;

	private GenerateMap map;
	private GameObject piece;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void Awake()
	{
		map = FindObjectOfType<GenerateMap>();
		map.addSpot(transform.position);
	}

	// Update is called once per frame
	void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("PizzaBall")){
            if(!done){
                GameMaster.hasDelivered();
				other.transform.position = new Vector3(10000, 10000, 10000);
                done = true;
				map.removeSpot(transform.position);
				//Component.Destroy(gameObject.GetComponent<deliverSpot>());
            }
            
        }
    }
}

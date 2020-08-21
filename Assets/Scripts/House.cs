using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class House : MonoBehaviour
{
	public Material[] wallColors;
	// Start is called before the first frame update
	private void Awake()
	{
		GetComponent<MeshRenderer>().material = wallColors[Random.Range(0, wallColors.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

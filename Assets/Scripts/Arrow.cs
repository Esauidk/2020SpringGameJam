using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	public Transform tar;
	private Vector3 target;
	private Transform follow;
    // Start is called before the first frame update
    void Start()
    {
		target = tar.position;
    }

    // Update is called once per frame
    void Update()
    {
		transform.LookAt(target);
		
		transform.position = Vector3.MoveTowards(follow.position, target, Vector3.Distance(follow.position, target) / 5);
		transform.position = new Vector3(transform.position.x, target.y, transform.position.z);
		
    }

	public void setTarget(Vector3 tar)
	{
		target = tar;
	}

	public void setFollow(Transform fol)
	{
		follow = fol;
	}
}

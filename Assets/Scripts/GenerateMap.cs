using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
	public AudioSource deliver;

	public Camera snapshotCam;
	public Material snapshotMat;
	public Transform cam;
	public GameObject roadPiece;
	public GameObject housePiece;
	public GameObject activeHousePiece;
	public GameObject grassPiece;
	public GameObject truckPiece;
	public float camDepth;
	public float roadDepth;
	public float houseDepth;
	public float activeHouseDepth;
	public float grassDepth;
	public float truckDepth;

	private GameObject truck;
	private GameObject truckP;

	private float frame = 0;
    // Start is called before the first frame update
    void Start()
    {
		GameObject[] allObjects = FindObjectsOfType<GameObject>();
		foreach (GameObject o in allObjects)
		{
			GameObject piece = null;
			switch (o.tag)
			{
				
				case("Baseplate"):
					transform.localScale = o.transform.localScale / 100;
					transform.position = new Vector3(o.transform.position.x, transform.position.y, o.transform.position.z);
					break;
				case ("House"):
					piece = Instantiate(housePiece, Vector3.zero, Quaternion.identity);
					piece.transform.localScale /= 100;
					piece.transform.position = new Vector3(o.transform.position.x / 100, transform.position.y + houseDepth, o.transform.position.z / 100);
					break;
				case ("Grass"):
					piece = Instantiate(grassPiece, Vector3.zero, Quaternion.identity);
					piece.transform.localScale /= 100;
					piece.transform.position = new Vector3(o.transform.position.x / 100, transform.position.y + grassDepth, o.transform.position.z / 100);
					break;
				case ("Road"):
					piece = Instantiate(roadPiece, Vector3.zero, Quaternion.identity);
					piece.transform.localScale /= 100;
					piece.transform.position = new Vector3(o.transform.position.x / 100, transform.position.y + roadDepth, o.transform.position.z / 100);
					break;
				case ("Truck"):
					truck = o;
					piece = Instantiate(truckPiece, Vector3.zero, Quaternion.identity);
					truckP = piece;
					piece.transform.localScale /= 100;
					piece.transform.position = new Vector3(o.transform.position.x / 100, transform.position.y - truckDepth, o.transform.position.z / 100);
					break;
			}
		}
		snapshotCam.transform.position = transform.position + Vector3.up * camDepth;
		snapshotCam.orthographicSize = transform.localScale.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        truckP.transform.position = new Vector3(truck.transform.position.x / 100, truckP.transform.position.y, truck.transform.position.z / 100);
		truckP.transform.rotation = truck.transform.rotation;
		cam.transform.position = new Vector3(truckP.transform.position.x, transform.position.y + camDepth, truckP.transform.position.z);
		cam.localEulerAngles = new Vector3(truckP.transform.localEulerAngles.x + 90, truckP.transform.localEulerAngles.y + 180, truckP.transform.localEulerAngles.z);

		if (frame == 6)
		{
			snapshotCam.targetTexture = null;
			snapshotCam.enabled = false;
			gameObject.GetComponent<MeshRenderer>().material = snapshotMat;
			foreach (GameObject o in GameObject.FindGameObjectsWithTag("Piece"))
			{
				GameObject.Destroy(o);
			}
			GameObject.Destroy(snapshotCam.gameObject);
			truckP.transform.position += Vector3.up * truckDepth * 2;

			transform.Translate(Vector3.down * activeHouseDepth * 2);
		}
		frame++;
	}

	public void addSpot(Vector3 pos)
	{
		GameObject piece = Instantiate(activeHousePiece, Vector3.zero, Quaternion.identity);
		Arrow arrow = piece.GetComponentInChildren<Arrow>();
		arrow.setTarget(piece.transform.position / 100);
		arrow.setFollow(truckP.transform);
		piece.transform.localScale /= 100;
		if (frame < 6)
		{
			piece.transform.position = new Vector3(pos.x / 100, transform.position.y - activeHouseDepth, pos.z / 100);
		}
		else
		{
			piece.transform.position = new Vector3(pos.x / 100, transform.position.y + activeHouseDepth, pos.z / 100);
		}
	}

	public void removeSpot(Vector3 pos)
	{
		DestroyObjectAtLocation(pos.x / 100, transform.position.y + activeHouseDepth, pos.z / 100, 1);
		deliver.Play();
	}

	private void DestroyObjectAtLocation(float nX, float nY, float nZ, float minDist)
	{

		Vector3 tmpLocation = new Vector3(nX, nY, nZ);
		Transform[] tiles = GameObject.FindObjectsOfType<Transform>();
		for (int i = 0; i < tiles.Length; i++)
		{
			if (Vector3.Distance(tiles[i].position, tmpLocation) <= minDist)
			{
				if (tiles[i].tag != "Arrow")
				{
					Destroy(tiles[i].gameObject);
				}
			}
		}
	}
}

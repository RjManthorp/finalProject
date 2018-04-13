using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeSpawner : MonoBehaviour {

	public Vector3 center;
	public Vector3 size;

	public GameObject node;


	// Use this for initialization
	void Start () 
	{
		//spawnNode ();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp (KeyCode.S))
			spawnNode ();
	}

	public void spawnNode()
	{
		for (int i = 0; i < 20; i++)
		{
			Vector3 pos = center + new Vector3 (Random.Range (-size.y / 2, size.y / 2), Random.Range (-size.x / 2, size.x / 2), Random.Range (-size.z / 2, size.z / 2));
			Instantiate (node, pos, Quaternion.identity);
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color (1, 0, 0, 0.5f);
		Gizmos.DrawCube (center, size);
	}

}

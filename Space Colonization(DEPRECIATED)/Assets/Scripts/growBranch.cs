using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class growBranch : MonoBehaviour {
	//public LineRenderer line;
	private LineRenderer lr;

	public GameObject trunk;
	public GameObject node;

	private Vector3 startPos;
	private Vector3 target;


	void Start()
	{

	}

	void drawBranch()
	{
		lr = GetComponent<LineRenderer>();
		lr.SetWidth (0.5f, 0.5f);
		Vector3[] positions = new Vector3[2];
		positions [0] = startPos;
		positions [1] = target;
		lr.SetPositions(positions);

	}

	// Update is called once per frame
	void Update () 
	{
		startPos = new Vector3 ((trunk.gameObject.GetComponent<Transform>().position.x), ((trunk.gameObject.GetComponent<Transform>().position.y)), ((trunk.gameObject.GetComponent<Transform>().position.z)));
		target = new Vector3 ((node.gameObject.GetComponent<Transform>().position.x), ((node.gameObject.GetComponent<Transform>().position.y)), ((node.gameObject.GetComponent<Transform>().position.z)));


		if (Input.GetKey (KeyCode.G))
			drawBranch ();

	}
}

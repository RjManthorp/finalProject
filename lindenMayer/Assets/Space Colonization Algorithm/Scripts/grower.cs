using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grower : MonoBehaviour 
{
	public float speed = 2.0f ;

	private Transform target ;

	private Rigidbody rb ;

	public GameObject treeSegStart;
	public GameObject treeSeg;

	public bool grow;
	public float spawnTime = 0.3f;

	private void Start()
	{
		target = FindTarget();
		rb = GetComponent<Rigidbody>();

		InvokeRepeating ("spawnSeg", 0.0f, spawnTime);
		InvokeRepeating ("splitBranch", 0.0f, 3.0f);
	}


	void spawnSeg()
	{
		
		if (grow == true) 
		{
			Instantiate (treeSeg, new Vector3 (treeSegStart.gameObject.GetComponent<Transform> ().position.x, treeSegStart.gameObject.GetComponent<Transform> ().position.y, treeSegStart.gameObject.GetComponent<Transform> ().position.z), Quaternion.identity);
		}
	}

	private void FixedUpdate()
	{
		if( target != null )
		{
			Vector3 direction = (target.position - transform.position).normalized;
			rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
			//transform.LookAt (target);
			//Instantiate (treeSeg, new Vector3 (treeSeg.gameObject.GetComponent<Transform> ().position.x, treeSeg.gameObject.GetComponent<Transform> ().position.y,  treeSeg.gameObject.GetComponent<Transform> ().position.z), Quaternion.identity);
		}
			

		target = FindTarget();
		rb = GetComponent<Rigidbody>();

		if (Input.GetKeyUp (KeyCode.B)) 
		{
			
			splitBranch ();
		}
			

	}

	private void OnCollisionEnter( Collision collision )
	{
		if( collision.collider.CompareTag("test") )
		{
			collision.collider.gameObject.tag = "node";
			collision.collider.gameObject.GetComponent<Renderer> ().material.color = Color.red;
		}

		Debug.Log ("collided");
		if( collision.collider.CompareTag("node") )
		{
			target = FindTarget();
		}
	}

	void splitBranch()
	{
		Debug.Log ("cheese");
		//Instantiate (treeSegStart,  new Vector3 (treeSegStart.gameObject.GetComponent<Transform> ().position.x, treeSegStart.gameObject.GetComponent<Transform> ().position.y,  treeSegStart.gameObject.GetComponent<Transform> ().position.z), Quaternion.Euler(60,60,60));
	}

	public Transform FindTarget()
	{
		GameObject[] candidates = GameObject.FindGameObjectsWithTag("node");
		float minDistance = Mathf.Infinity;
		Transform closest ;

		if (candidates.Length == 0)
			return null;

		closest = candidates[0].transform;
		for ( int i = 1 ; i < candidates.Length ; ++i )
		{
			float distance = (candidates[i].transform.position - transform.position).sqrMagnitude;

			if ( distance < minDistance )
			{
				closest = candidates[i].transform;
				minDistance = distance;
				grow = true;
			}
			if (distance <= 0.3f) 
			{
				Destroy (candidates [i].gameObject);
				grow = false;

			}
		}    
		return closest;
	}    
}
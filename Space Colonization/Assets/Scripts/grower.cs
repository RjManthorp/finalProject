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

	public float spawnTime = 0.3f;

	private void Start()
	{
		target = FindTarget();
		rb = GetComponent<Rigidbody>();

		InvokeRepeating ("spawnSeg", 0.0f, spawnTime);



	}


	void spawnSeg()
	{
		
		Instantiate (treeSeg, new Vector3 (treeSegStart.gameObject.GetComponent<Transform> ().position.x, treeSegStart.gameObject.GetComponent<Transform> ().position.y,  treeSegStart.gameObject.GetComponent<Transform> ().position.z), Quaternion.identity);
	}

	private void FixedUpdate()
	{
		if( target != null )
		{
			Vector3 direction = (target.position - transform.position).normalized;
			rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
			transform.LookAt (target);
			//Instantiate (treeSeg, new Vector3 (treeSeg.gameObject.GetComponent<Transform> ().position.x, treeSeg.gameObject.GetComponent<Transform> ().position.y,  treeSeg.gameObject.GetComponent<Transform> ().position.z), Quaternion.identity);
		}
			

		target = FindTarget();
		rb = GetComponent<Rigidbody>();
	}

	private void OnCollisionEnter( Collision collision )
	{
		Debug.Log ("collided");
		if( collision.collider.CompareTag("node") )
		{
			collision.collider.gameObject.tag = "Untagged"; // Remove the tag so that FindTarget won't return it
			Destroy( collision.collider.gameObject ) ;
			target = FindTarget();
		}
	}

	public Transform FindTarget()
	{
		GameObject[] candidates = GameObject.FindGameObjectsWithTag("node");
		float minDistance = Mathf.Infinity;
		Transform closest ;

		if ( candidates.Length == 0 )
			return null;

		closest = candidates[0].transform;
		for ( int i = 1 ; i < candidates.Length ; ++i )
		{
			float distance = (candidates[i].transform.position - transform.position).sqrMagnitude;

			if ( distance < minDistance )
			{
				closest = candidates[i].transform;
				minDistance = distance;
			}
		}    
		return closest;
	}    
}
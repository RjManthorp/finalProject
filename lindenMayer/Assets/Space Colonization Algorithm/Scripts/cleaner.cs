﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleaner : MonoBehaviour 
{
	private void OnCollisionEnter( Collision collision )
	{
		if( collision.collider.CompareTag("node") )
		{
			Destroy( collision.collider.gameObject ) ;
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : MonoBehaviour {

	public GameObject player;
	public GameObject hand;
	public GameObject objecttograb;
	private float timeCount = 0.0f;


	void Update(){
		timeCount = timeCount + Time.deltaTime;
	}
	public void setobject(){
		if (hand.transform.childCount == 0) {
			player.GetComponent<PluginWrapper> ().objectA = objecttograb;
		}

	
	}


}

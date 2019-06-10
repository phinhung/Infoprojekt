using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PluginWrapper : MonoBehaviour {

    private AndroidJavaObject javaClass;
 

	// Use this for initialization
	void Start () {
		Debug.Log ("Start");
        javaClass = new AndroidJavaObject("com.example.vrlibrary.Keys");
 
		Physics.IgnoreLayerCollision(8, 2);
	}


	bool cansnap;


	void Update () {
		cansnap = snapzo.GetComponent<snap_allowed> ().snapallow;
	}
	public GameObject hand;
	public GameObject objectA;
	public Transform objectB;
	public bool angeschaut=false;

	public bool snapallowed;
	Vector3 npos;

	public void greifen(string ok){

		if ((ok == "1") && (angeschaut == true)) {
			if (hand.transform.childCount == 0) {
				objectA.transform.position = objectB.position;
				objectA.transform.rotation = Quaternion.Euler(0,0,0);
				objectA.transform.parent = objectB;
				objectA.GetComponent<Rigidbody>().useGravity = false;
			
			}

			}
		else if ((ok == "1")&&(hand.transform.childCount == 1)){
			getpospointer ();

			npos.x = wpos.x;
			npos.z = wpos.z;
			npos.y = wpos.y+0.1f;
			objectA.transform.position = npos;
			hand.transform.DetachChildren ();
			objectA.GetComponent<Rigidbody> ().useGravity = true;

			if ((snapallowed = true)&&(objecttosnap==objectA)&&(cansnap == true)) {
				snap ();
			} else {
				if (GameObject.Find("DenebBall").name=="DenebBall") {
					objectA.GetComponent<Rigidbody> ().useGravity = false;
				} else {
					objectA.GetComponent<Rigidbody> ().useGravity = true;
				}
			}
		}
	}

    Scene szene;
	public GameObject snapzo;
	public GameObject objecttosnap;
	public GameObject snappos;
	bool enter=true;


	public void snap(){
		
		OnTriggerStay (snapzo.GetComponent<SphereCollider> ());
	}



	void OnTriggerStay(Collider other)
	{
		if( (enter)&& (snapallowed)) {

			objectA.GetComponent<Collider> ().enabled = false;
			objecttosnap.transform.position = snappos.transform.position;
			objectA.GetComponent<Rigidbody> ().useGravity = false;
		} 
	}

	public void anschauen(){
		angeschaut = true;
	}

	public void wegschauen(){
		angeschaut = false;
    }

    public GameObject pointer;
	public Vector3 wpos;

	public void getpospointer(){
		wpos = pointer.GetComponent<GvrReticlePointer> ().CurrentRaycastResult.worldPosition;
	
	}

	GameObject oA;
	bool panelactive=false;
	public GameObject camera;
	public void infopanel(string oki) {
		if ((oki == "1")&&(hand.transform.childCount == 1)&&(panelactive == false)){
			oA = objectA.transform.Find ("PanelMenu").gameObject;
			oA.SetActive (true);
			panelactive = true;
			oA.transform.LookAt (camera.transform);
		
			
		} else	if ((panelactive == true)&&(hand.transform.childCount == 1)&&(oki == "1")){
			oA.SetActive (false);
			panelactive = false;
	    }
	}
}

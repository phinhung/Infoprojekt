using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PluginWrapper : MonoBehaviour {

    private AndroidJavaObject javaClass;
 

	// Use this for initialization
	void Start () {

 
		Physics.IgnoreLayerCollision(0, 2);
	}

	public GameObject manager;
	bool cansnap;
	bool greif;


	void Update () {
		hand.transform.rotation = Quaternion.Euler (manager.GetComponent<manager> ().z_Bew, manager.GetComponent<manager> ().y_Bew, manager.GetComponent<manager> ().x_Bew);
		greif = manager.GetComponent<manager> ().gegriffen;
		greifen (greif);
		if (snapzo != null) {
			cansnap = snapzo.GetComponent<snap_allowed> ().snapallow;

		}


	}
	public GameObject hand;
	public GameObject objectA;
	public Transform objectB;
	public bool angeschaut=false;
	public Text mytext;

	public bool snapallowed;
	bool wargegriffen;
	Vector3 npos;

	public void greifen(bool grabbed){
		if ((grabbed == true) && (angeschaut == true)) {
			if (hand.transform.childCount == 0) {
				objectA.transform.position = objectB.position;
				objectA.transform.rotation = Quaternion.Euler(0,0,0);
				objectA.transform.parent = objectB;
				objectA.GetComponent<Rigidbody>().useGravity = false;
				objectA.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			
			}

			}
		else if ((grabbed == false)&&(hand.transform.childCount == 1)){
			getpospointer ();

			npos.x = wpos.x;
			npos.z = wpos.z;
			npos.y = wpos.y+0.1f;
			objectA.transform.position = npos;
			hand.transform.DetachChildren ();
			objectA.GetComponent<Rigidbody> ().useGravity = true;
			wargegriffen = false;
			objectA.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			if ((snapallowed == true)&&(objecttosnap==objectA)&&(cansnap == true)) {
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
		
		OnTriggerStay (snapzo.GetComponent<Collider> ());
	}



	void OnTriggerStay(Collider other)
	{
		if( (enter)&& (snapallowed)) {

			objectA.GetComponent<Collider> ().enabled = false;
			objecttosnap.transform.position = snappos.transform.position;
			objectA.GetComponent<Rigidbody> ().useGravity = false;
			objectA.transform.rotation = snappos.transform.rotation;
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

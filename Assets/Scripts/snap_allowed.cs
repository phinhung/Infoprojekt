using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snap_allowed : MonoBehaviour {


	public Vector3 posleft;
	Vector3 posright;
	Vector3 center = new Vector3 (0,0,0);
	public bool objectisgrabbed;
	public float Distanceri;
	public float Distancele;
	public float alloweddistance;
	public GameObject left;
	public GameObject right;
	public bool allowsnap;

	public bool snapok;

	void Update () {
		//Controller finden und deren Position einer Variable zuweisen
		posleft = left.GetComponent<PositionLeftHand> ().positionleft;

		posright = right.GetComponent<PositionRightHand> ().positionright;


		//prüfen, ob ein Planet gegriffen ist
		if (right.transform.childCount == 1){
			objectisgrabbed = true;
		}
		else if(right.transform.childCount == 0){
			objectisgrabbed = false;
		}
			


		//ausrechnen der Distanz
		if (objectisgrabbed == true) {
			Distanceri = Vector3.Distance (posright, center);
			Distancele = Vector3.Distance (posleft, center);
			//SnapDropZone aktiv/deaktiv setzen

			if (objectisgrabbed == true && (Distancele < alloweddistance | Distanceri < alloweddistance)) {
				snapzo.GetComponent<Collider> ().enabled = false;
				snapok = false;
					
			}
			if (objectisgrabbed == true && (Distancele > alloweddistance | Distanceri > alloweddistance)) {
				snapzo.GetComponent<Collider> ().enabled = true;
				snapok = true;

			}
			
		} else {
			snapzo.GetComponent<Collider>().enabled = false;
			snapok = false;
		}

		pos= player.GetComponent<PluginWrapper> ().wpos ;
		if ( (snapallow == true) && (snapok == true)) {
			player.GetComponent<PluginWrapper> ().snapzo = snapzo;
			player.GetComponent<PluginWrapper> ().objecttosnap = objecttosnap;
			player.GetComponent<PluginWrapper> ().snappos = snappos;
			player.GetComponent<PluginWrapper> ().snapallowed = true;
		} else {player.GetComponent<PluginWrapper> ().snapallowed = false;
		}

	}

	public GameObject snapzo;
	public GameObject objecttosnap;
	public GameObject snappos;
	public bool snapallow;
	public GameObject player;
	Vector3 pos;



	public void bahnanschauen(){
		
		snapallow = true;
		
	}

	public void bahnwegschauen(){
		snapallow = false;
	}
}

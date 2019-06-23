using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class teleport : MonoBehaviour {
    private AndroidJavaObject javaClass;
   
 

	public GameObject play;
	Vector3 newpos;

	public GameObject pointer;
	public Vector3 wpos;
	public GameObject manager;
	public bool z;
	bool zwischen;



	public void getpospointer(){
		wpos = pointer.GetComponent<GvrReticlePointer> ().CurrentRaycastResult.worldPosition;
		Debug.Log (wpos);
	}
 

	void Update(){
		manager = GameObject.Find ("Manager");
		z = manager.GetComponent<manager> ().gezeigt;
		if(z != zwischen){
			if ((z == true) && (zwischen == false) ){
				laufen (z);
				wait ();
			}
			zwischen = z;

		}
	}

	IEnumerator wait(){
		yield return new WaitForSeconds (0.1f);
	}

	public void laufen(bool zeig)
	{	
		if (zeig == true) {
            getpospointer();
            newpos.x = wpos.x;
            newpos.z = wpos.z;
            newpos.y = 1.66f;
            play.transform.position = newpos;
         
        }
    }
}

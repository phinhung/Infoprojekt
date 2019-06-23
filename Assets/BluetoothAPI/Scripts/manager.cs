using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArduinoBluetoothAPI;
using System;

public class manager : MonoBehaviour {

	public int k1_Bew;
	public int r1_Bew;
	public int m1_Bew;
	public int z1_Bew;
	public int d1_Bew;
	public int x_Bew;
	public int y_Bew;
	public int z_Bew;
	public float schwellwert;
	public Text mytext;
	public Text mytext2
	;
	public Text mytext3;
	public Text mytext4;
	public Text mytext5;
	public Text mytext6;
	public Text text;

	UInt16[] sensordata = new UInt16[17];
	byte[] buffer = new byte[2];
	int[] flex = new int[5];
	int[] min = new int[5];
	int[] max = new int[5];
	float[] mom = new float[5];
	bool[] greif = new bool[5];

	const int k1_index = 0;
	const int r1_index = 1;
	const int m1_index = 2;
	const int z1_index = 3;
	const int d1_index = 4;
	const int x_index = 5;
	const int y_index = 6;
	const int z_index = 7;
	public bool gegriffen;
	public bool gezeigt=false;


	// Use this for initialization
	BluetoothHelper bluetoothHelper;
	string deviceName;
	string debugfilter = "Linus: ";



	public GameObject sphere;

	string received_message;

	void Start () {
		min [0] = 1023;
		min [1] = 1023;
		min [2] = 1023;
		min [3] = 1023;
		min [4] = 1023;

		max [0] = 0;
		max [1] = 0;
		max [2] = 0;
		max [3] = 0;
		max [4] = 0;
		deviceName = "HC05"; //bluetooth should be turned ON;
		try
		{	
			bluetoothHelper = BluetoothHelper.GetInstance(deviceName);
			bluetoothHelper.OnConnected += OnConnected;
			bluetoothHelper.OnConnectionFailed += OnConnectionFailed;
			bluetoothHelper.OnDataReceived += OnMessageReceived; //read the data

		


			bluetoothHelper.setLengthBasedStream();
		

			if(bluetoothHelper.isDevicePaired())
				sphere.GetComponent<Renderer>().material.color = Color.blue;
			else
				sphere.GetComponent<Renderer>().material.color = Color.grey;
		}
		catch (Exception ex) 
		{
			sphere.GetComponent<Renderer>().material.color = Color.yellow;
			Debug.Log (ex.Message);
			text.text = ex.Message;
		}
	}

	IEnumerator blinkSphere()
	{
		sphere.GetComponent<Renderer>().material.color = Color.cyan;
		yield return new WaitForSeconds(0.5f);
		sphere.GetComponent<Renderer>().material.color = Color.green;
	}

	// Update is called once per frame
	void Update () {


		//Synchronous method to receive messages
		/*if(bluetoothHelper != null)
		if (bluetoothHelper.Available)
			received_message = bluetoothHelper.Read ();
		mytext.text = received_message ; */
	}

	//Asynchronous method to receive messages
	void OnMessageReceived()
	{

		char[] data = bluetoothHelper.Read ().ToCharArray ();
		for (int i = 0; i < data.Length / 2; i++) 
		{
			buffer[0] = (byte)data [2 * i];
			buffer[1] = (byte)data [2 * i + 1];
			sensordata [i] = BitConverter.ToUInt16 (buffer, 0);
			Debug.Log(debugfilter + "Sensor[ " + i + " ] " + sensordata[i]);

		}
		k1_Bew = sensordata[0];
		r1_Bew = sensordata[1];
		m1_Bew = sensordata[2];
		z1_Bew = sensordata[3];
		d1_Bew = sensordata[4];
		x_Bew = sensordata[5];
		y_Bew = sensordata[6];
		z_Bew = sensordata[7];

		mytext3.text = x_Bew.ToString ();

		flex [0] = k1_Bew;
		flex [1] = r1_Bew;
		flex [2] = m1_Bew;
		flex [3] = z1_Bew;
		flex [4] = d1_Bew;

		for (int m = 0; m <= 4; m++) {
			setmin (m);
			setmax (m);
			mom [m] = ((((float)flex[m]-(float)min[m])/ ((float)max[m]-(float)min[m])))*100000f;
		}

		gegriffen = true;
		for (int 	j = 0; j <= 4; j++){

			if (mom [j] > 33000) {
				greif [j] = true;
			} else if (j != 3 ) { 
				greif [j] = false;
			}
		}
		greif[3]= true;
		if ((mom [0] > 35000) && (mom [1] > 35000) && (mom [2] > 35000) && (mom [3] < 28000) && (mom [4] > 35000)) 
		{
			gezeigt = true;
			gegriffen = true;
		} else { gezeigt = false;
			for (int k = 0; k <= 4; k++) {
				if (greif [k] == false) {
					gegriffen = false;
				}

			}

		}
			
	




		mytext4.text = flex [0].ToString () + " " + flex [1].ToString () + " " + flex [2].ToString () + " " + flex [3].ToString () + " " + flex [4].ToString ();
		mytext.text = mom [0].ToString () + " " + mom [1].ToString () + " " + mom [2].ToString () + " " + mom [3].ToString () + " " + mom [4].ToString ();
		mytext5.text = max [0].ToString () + " " + max [1].ToString () + " " + max [2].ToString () + " " + max [3].ToString () + " " + max [4].ToString ();
		mytext6.text = min [0].ToString () + " " + min [1].ToString () + " " + min [2].ToString () + " " + min [3].ToString () + " " + min [4].ToString ();
		/*if (gegriffen == true) {
			mytext2.text = "true";
		} else {
			mytext2.text = "false";
		}*/

	

		mytext2.text = gezeigt.ToString ();
	
	}

	void setmin(int i){
		if (min[i] > flex [i]){
			min [i] = flex [i];
		}
	}

	void setmax(int i){
		if (max[i] < flex [i]){
			max [i] = flex [i];
		}
	}

	void OnConnected()
	{
		sphere.GetComponent<Renderer>().material.color = Color.green;
		try{
			bluetoothHelper.StartListening ();
		}catch(Exception ex){
			Debug.Log(ex.Message);
		}

	}

	void OnConnectionFailed()
	{
		sphere.GetComponent<Renderer>().material.color = Color.red;
		Debug.Log("Connection Failed");
	}

	public void button(){
		if(!bluetoothHelper.isConnected())
		if(bluetoothHelper.isDevicePaired())
			bluetoothHelper.Connect (); // tries to connect
	}
		

	void OnDestroy()
	{
		if(bluetoothHelper!=null)
			bluetoothHelper.Disconnect ();
	}
}

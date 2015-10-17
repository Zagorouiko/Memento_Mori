using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class NetworkManager : MonoBehaviour {
	
	public Camera StandbyCamera;
	SpawnSpot[] spawnSpots;
	
	void Start () {
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
		Connect();
	}
	
	
	void Connect() {
		PhotonNetwork.ConnectUsingSettings("MultiFPS");
	}
	
	void OnGUI() {
		GUILayout.Label( PhotonNetwork.connectionStateDetailed.ToString() );
	}
	
	
	void OnJoinedLobby() {
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom();
	}
	
	void OnPhotonRandomJoinFailed() {
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom( null ) ;
	}
	
	
	void OnJoinedRoom(){
		Debug.Log ("OnJoinedRoom");
		
		SpawnMyPlayer();
	}
	void SpawnMyPlayer() {
		if(spawnSpots == null) {
			Debug.LogError("no spawn spot");
			return;
		}
		
		
		SpawnSpot mySpawnSpot = spawnSpots[Random.Range(0, spawnSpots.Length)];
		GameObject myPlayerGO = PhotonNetwork.Instantiate("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation,0);
		myPlayerGO.GetComponent<CharacterController>().enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent <MouseLook>()).enabled = true;
		myPlayerGO.GetComponentInChildren<Camera>().enabled = true;
		myPlayerGO.GetComponent<AudioSource>().enabled = true;
		myPlayerGO.GetComponent<AudioListener>().enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent("FirstPersonController")).enabled = true;
		myPlayerGO.GetComponent<FirstPersonController>().enabled = true;
	}
}
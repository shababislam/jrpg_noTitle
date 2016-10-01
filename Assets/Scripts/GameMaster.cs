using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class GameMaster : MonoBehaviour {

	public static bool GamemasterExists;

	public GameObject Damien;
	public GameObject Maya;
	public GameObject Stubbs;
	public Camera GFXCam;

	public CharacterDatabase db;
	public List<GameObject> characters;

	public List<GameObject> party;
	public static bool inParty;
	public static int partySize;
	public static bool inTown;

	private static float randomeBattleCounter;
	public static bool battle;
	public static bool fighting;

	public static string currentLevel;
	public static Vector3 currentPos;
	public static Vector3 lastTownPos;
	public static Vector3 lastCameraPosition;
	public static Vector3 lastCameraRotation;

	public static Camera mainCam;

	public static OverworldCam overworldCam;
	public static SmoothFollowCamera smoothFollowCam;
	public static CanvasRotationFix cr;
	
	void Start () {
		if(!GamemasterExists){
			GamemasterExists = true;
			GameObject.DontDestroyOnLoad(this);
		} else {
			Destroy(gameObject);
		}
		Damien = (GameObject)Instantiate(Damien);
		Stubbs = (GameObject)Instantiate(Stubbs);
		Maya = (GameObject)Instantiate(Maya);
		characters = new List<GameObject>();
		characters.Add(Damien);
		characters.Add(Maya);
		party.Add(Maya);
		Stubbs.SetActive(false);

		Damien.transform.position = new Vector3(350,0,350);

		if(currentPos != new Vector3(0,0,0)){
			Damien = GameObject.FindGameObjectWithTag("Player");
			Damien.transform.position = currentPos;
			inParty = false;
		} 
		partySize = party.Count;
		BattleCounterReset();
		mainCam = Camera.main;
		overworldCam = mainCam.GetComponent<OverworldCam>();
		smoothFollowCam = mainCam.GetComponent<SmoothFollowCamera>();
		cr = mainCam.GetComponent<CanvasRotationFix>();

	}

	public void setPartyPosition(GameObject mc){
		foreach (GameObject character in party){
			character.transform.position = mc.transform.position;
		}
	}

	public void SaveGame(){
		db.list.Clear();
		foreach (GameObject character in characters){
			db.list.Add(character.GetComponent<Player>().returnEntry());
		}
		XmlSerializer serializer = new XmlSerializer(typeof(CharacterDatabase));
		FileStream stream = new FileStream(Application.dataPath + "/Files/XML/character_stats.xml", FileMode.Create);
		serializer.Serialize(stream,db);
		stream.Close();
	}

	public void LoadGame(){
		XmlSerializer serializer = new XmlSerializer(typeof(CharacterDatabase));
		FileStream stream = new FileStream(Application.dataPath + "/Files/XML/character_stats.xml", FileMode.Open);
		db = serializer.Deserialize(stream) as CharacterDatabase;
		string level = "";
		foreach (ListEntry entry in db.list){
			for (int i = 0;i<characters.Count;i++){
				level = entry.currPlace;
				if (characters[i].transform.ToString().Equals(entry.name)){
					characters[i].transform.position = new Vector3(entry.posX,entry.posY,entry.posZ);
					characters[i].transform.rotation = entry.rotation;
				}
			}
		}
		SceneManager.LoadScene(level);
		stream.Close();
	}

	public static void BattleCounterReset(){
		randomeBattleCounter = Random.Range(1000,2000);
	}

	void BattleCountDown(){
		if(randomeBattleCounter == 0){
			battle = true;
		} 
		if (!inParty)
			randomeBattleCounter--;
	}

	void BattleStarts(){
		if (battle){
			currentPos = Damien.GetComponent<DamienOverworld>().lastPos();
			string battleLoad = currentLevel+"Combat";
			SceneManager.LoadScene(battleLoad);
		}
	}

	public void changeFOV(float f){
		if(GFXCam){
			GFXCam.fieldOfView = f;
		}
	}
	
	void Update () {
		BattleCountDown();

		BattleStarts();

		if (!inParty){
			foreach (GameObject character in party){
				character.SetActive(false);
				currentLevel = "terrain_test";
			}
		} 
		if (inParty){
			foreach (GameObject character in party){
				character.SetActive(true);
			}
		}  
			
		if (Input.GetKey(KeyCode.P)){
			SaveGame();
		}

		if (Input.GetKey(KeyCode.O)){
			LoadGame();
		}
	}
}

[System.Serializable]
public class ListEntry{
	public string currPlace;
	public string name;
	public Quaternion rotation;
	public float health;
	public float currentHealth;
	public float posX, posY, posZ;
	public ListEntry(string cp,string n, Quaternion r,float h,float c,float x,float y,float z){
		currPlace = cp;
		name = n;
		rotation = r;
		health = h;
		currentHealth = c;
		posX = x;
		posY = y;
		posZ = z;
	}

	public ListEntry(){}
}

[System.Serializable]
public class CharacterDatabase{
	public List<ListEntry> list = new List<ListEntry>();
}
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class BattleScene : MonoBehaviour {
	public GameObject gm;
	public GameObject spawn;
	public GameObject enemySpawn;
	GameObject player;
	public GameObject enemy;
	public List<GameObject> enemies;
	private Player enemyHealth;
	private float counter;
	public Text battleOver;

	void Start () {
		if(!gm){
			gm = GameObject.Find("_gameMaster");
		}

		player = GameObject.FindGameObjectWithTag("Player");
		player.transform.position = spawn.transform.position;
		GameMaster.battle = false;
		GameMaster.inParty = true;
		GameMaster.fighting = true;
		gm.GetComponent<GameMaster>().setPartyPosition(player);

		enemy = Instantiate(enemies[Random.Range(0,enemies.Count-1)]);
		enemy.transform.position = enemySpawn.transform.position;
		enemyHealth = enemy.GetComponent<Player>();
		enemyHealth.currentHealth = 100f;
		counter = 5;
		battleOver.enabled = false;
		GameMaster.mainCam.transform.position = new Vector3(0,20,-25f);
		GameMaster.overworldCam.enabled = false;
		GameMaster.smoothFollowCam.enabled = true;
	}
	
	void Update () {
		if (enemyHealth.currentHealth < 0){
			battleOver.text = "You win!";
			battleOver.enabled = true;
			counter-=Time.deltaTime;
			if(counter<0){
				GameMaster.inParty = false;
				GameMaster.fighting = false;
				player.transform.position = GameMaster.currentPos;
				GameMaster.BattleCounterReset();
				SceneManager.LoadScene("terrain_test");
			}
		}	
	}
}

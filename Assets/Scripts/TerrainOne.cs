using UnityEngine;
using System.Collections;

public class TerrainOne : MonoBehaviour {

	void Start () {
		Vector3 empty = new Vector3(0,0,0);
		if(GameMaster.currentPos != empty){
			GameMaster.mainCam.transform.position = GameMaster.currentPos + new Vector3(0,40,-45f);
			GameMaster.overworldCam.enabled = true;
			GameMaster.smoothFollowCam.enabled = false;
			GameMaster.currentPos = empty;
		}
	}
}

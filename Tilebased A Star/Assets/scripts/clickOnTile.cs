using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickOnTile : MonoBehaviour {

	public ccAi ai;

    //public tilesByGeneration generator;
    public tilesByGeneration generatedTiles;

    void Start()
    {
		GameObject generatorObject = GameObject.FindGameObjectWithTag("generatorObject");
        generatedTiles = generatorObject.GetComponent<tilesByGeneration>();
    }

    void Update () {
		if (Input.GetMouseButton (0)) {
			RaycastHit hit;
			if (Physics.Raycast (GetComponent<Camera> ().ScreenPointToRay(Input.mousePosition), out hit)) {
				if (hit.transform.tag == "tile") {
                    /*ai.FindPath(new orderedPair(Mathf.RoundToInt(hit.transform.position.x),
												Mathf.RoundToInt(hit.transform.position.z))); */
					Vector3 currentPosition = new Vector3 (ai.xPos, 0f, ai.yPos);
					//Vector3 destination = new Vector3 (6f,0f,0f);
					ai.FollowPath(generatedTiles.PathfindWithVectors (currentPosition, hit.transform.position));
                    print("Location: " + hit.transform.position);
				}
			}
		}
	}
}

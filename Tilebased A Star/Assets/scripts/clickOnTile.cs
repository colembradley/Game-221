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
                    print("Location: " + hit.transform.position + " Node: ?" );
				}
			}
		}
	}
}

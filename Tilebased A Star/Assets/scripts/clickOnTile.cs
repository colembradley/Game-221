using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickOnTile : MonoBehaviour {

	public ccAi ai;

	void Update () {
		if (Input.GetMouseButton (0)) {
			RaycastHit hit;
			if (Physics.Raycast (GetComponent<Camera> ().ScreenPointToRay(Input.mousePosition), out hit)) {
				if (hit.transform.tag == "tile") {
					ai.FindPath(new orderedPair(Mathf.RoundToInt(hit.transform.position.x),
												Mathf.RoundToInt(hit.transform.position.z)));
				}
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile {
	public int x;
	public int y;

	public GameObject tileObject;

	public bool isObstacle = false;

	public tile(int inputX, int inputY, GameObject inputTile, bool obstacle){
		x = inputX;
		y = inputY;
		tileObject = inputTile;
		isObstacle = obstacle;
		if (obstacle == true) {
			tileObject.GetComponent<Renderer> ().material.color = Color.red;
		}
	}
}

public class tileGenerator : MonoBehaviour {

	public int x;
	public int y;

	public List<tile> tiles = new List<tile>();

	private int currentX;
	private int currentY;

	public GameObject tile;

	void Start () {
		SpawnTile (currentX, currentY, false);
		currentX++;
		while (currentY < y) {
			while (currentX < x) {
				SpawnTile (currentX, currentY, Random.Range(0, 10) < 1);
				currentX++;
			}
			currentX = 0;
			currentY++;
		}
	}

	void SpawnTile(int inputX, int inputY, bool isObstacle){
		tiles.Add (new tile(inputX, inputY, Instantiate (tile, new Vector3((float)inputX,0f,(float)inputY),
			Quaternion.Euler(new Vector3(90f,0f,0f))), isObstacle));
	}

}

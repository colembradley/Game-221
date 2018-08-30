using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orderedPair{
	public int x = 0;
	public int y = 0;

	public orderedPair(int inputX, int inputY){
		x = inputX;
		y = inputY;
	}
}

public class node{
	public orderedPair location;
	public node from;
	public bool traversable = true;
	public int cost = 0;

	public node(orderedPair inputLocation, bool inputTraversable){
		location = inputLocation;
		traversable = inputTraversable;
	}

	public node(orderedPair inputLocation, bool inputTraversable, int inputCost){
		location = inputLocation;
		traversable = inputTraversable;
		cost = inputCost;
	}

	public node(orderedPair inputLocation, bool inputTraversable, node inputFrom, int inputCost){
		location = inputLocation;
		traversable = inputTraversable;
		from = inputFrom;
		cost = inputCost;
	}
}

public class ccAi : MonoBehaviour {

	public float gravity = 1f;
	CharacterController controller;
	public tileGenerator tileGenerator;

	public Transform target;
	public bool seek = false;
	public float moveSpeed = 2f;
	public float satisfactionRadius = 0.25f;

	//Pathfinding
	public int xPos;
	public int yPos;
	public orderedPair location;
	public List<node> graph = new List<node>();
	public List<node> open = new List<node>();
	public List<node> closed = new List<node>();
	public List<node> path = new List<node>();

	void Start () {
		controller = GetComponent<CharacterController> ();
		location = new orderedPair(0,0);
		//foreach(tile tile in tileGenerator.tiles){
		//	graph.Add (new node(new orderedPair(tile.x, tile.y), tile.isObstacle));
		//}
	}

	void Update () {
		controller.Move (new Vector3(0f, -gravity * Time.deltaTime, 0f));
		if (seek) {
			Vector3 offset = (target.position - transform.position);
			controller.Move ((offset.normalized *moveSpeed) * Time.deltaTime);
			if (offset.magnitude < satisfactionRadius) {
				seek = false;
				location = new orderedPair (Mathf.RoundToInt(target.position.x), Mathf.RoundToInt(target.position.z));
			}
		}



		//xPos = location.x;
		//yPos = location.y;
	}

	public void FindPath(orderedPair destination){
		int cost = 0;
		int iteration = 0;
		open.Add (new node(location, true, cost));
		node currentNode = open [iteration];
		cost++;

		while(open.Count > 0){
			//Make open nodes
			if(currentNode.location.x-1 != -1){
				node targetNode = FindNodeWithOrderedPair (graph, new orderedPair (currentNode.location.x - 1, currentNode.location.y));
				node newNode = new node (targetNode.location, targetNode.traversable, open[0], cost);
				newNode.location.x -= 1;
				open.Add (newNode);
			}
			if(currentNode.location.x+1 <= tileGenerator.x){
				node targetNode = FindNodeWithOrderedPair (graph, new orderedPair (currentNode.location.x + 1, currentNode.location.y));
				node newNode = new node (targetNode.location, targetNode.traversable, open[0], cost);
				newNode.location.x += 1;
				open.Add (newNode);
			}
			if(currentNode.location.y-1 != -1){
				node targetNode = FindNodeWithOrderedPair (graph, new orderedPair (currentNode.location.x, currentNode.location.y - 1));
				node newNode = new node (targetNode.location, targetNode.traversable, open[0], cost);
				newNode.location.y -= 1;
				open.Add (newNode);
			}
			if(currentNode.location.y+1 <= tileGenerator.y){
				node targetNode = FindNodeWithOrderedPair (graph, new orderedPair (currentNode.location.x, currentNode.location.y + 1));
				node newNode = new node (targetNode.location, targetNode.traversable, open[0], cost);
				newNode.location.y += 1;
				open.Add (newNode);
			}
			//close previous node
			closed.Add(open[0]);
			open.RemoveAt(0);
		}

	}

	node FindNodeWithOrderedPair(List<node> inputNodes, orderedPair orderedPair){
		foreach (node node in inputNodes) {
			if (node.location.x == orderedPair.x && node.location.y == orderedPair.y) {
				return node;
			}
		}
		return null;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilesByGeneration : MonoBehaviour {

    public int gridWidth = 10;
    public int gridHeight = 10;

    public GameObject tileTemplate;

    public Dictionary<GameObject, Node> tilesToNode = new Dictionary<GameObject, Node>();

	public Dictionary<Vector3, Node> nodesByPosition = new Dictionary<Vector3, Node>();

	void Start () {

        

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                GameObject newTile = Instantiate(tileTemplate);
                newTile.transform.position = new Vector3(x, 0, z);
				if ((x+ (z%2)) % 2 == 0) {
					newTile.GetComponent<Renderer> ().material.color = Color.gray;
				}

                Node tileNode = new Node(newTile.transform.position);
                nodesByPosition.Add(tileNode.position, tileNode);
				print (tileNode.position);

                //newTile.GetComponent<NodeBinding>().node = tileNode;
                tilesToNode.Add(newTile, tileNode);

                //Todo
                //How could I do it in an arbitrary scene?

                
            }
        }

        foreach(Vector3 nodePosition in nodesByPosition.Keys)
        {
            Node currentNode = nodesByPosition[nodePosition];
            Dictionary<Node, float> weightedConnections = currentNode.connections;

            Node right = LookupNode(nodePosition + Vector3.right);
            if (right != null)
                currentNode.connections.Add(right, 1);

            Node left = LookupNode(nodePosition + Vector3.left);
            if (left != null)
                currentNode.connections.Add(left, 1);

            Node up = LookupNode(nodePosition + Vector3.up);
            if (up != null)
                currentNode.connections.Add(up, 1);

            Node down = LookupNode(nodePosition + Vector3.down);
            if (down != null)
                currentNode.connections.Add(down, 1);
        }
	}

    public Node LookupNode(Vector3 lookup)
    {
		if (!nodesByPosition.ContainsKey (lookup)) {
			return null;
		} else {
			return nodesByPosition [lookup];
		}
    }
	
	void Update () {
		
	}

	public List<Vector3> PathfindWithVectors(Vector3 start, Vector3 end){
		return Dijkstra.Pathfind(LookupNode(start), LookupNode(end));
	}
}

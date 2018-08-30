using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilesByGeneration : MonoBehaviour {

    public int gridWidth = 10;
    public int gridHeight = 10;

    public GameObject tileTemplate;

    public Dictionary<GameObject, Node> tilesToNode = new Dictionary<GameObject, Node>();

	void Start () {

        Dictionary<Vector3, Node> nodesByPosition = new Dictionary<Vector3, Node>();

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                GameObject newTile = Instantiate(tileTemplate);
                newTile.transform.position = new Vector3(x, 0, z);

                Node tileNode = new Node(newTile.transform.position);
                nodesByPosition.Add(tileNode.position, tileNode);

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

            Node right = LookupNode(nodesByPosition, nodePosition + Vector3.right);
            if (right != null)
                currentNode.connections.Add(right, 1);

            Node left = LookupNode(nodesByPosition, nodePosition + Vector3.left);
            if (left != null)
                currentNode.connections.Add(left, 1);

            Node up = LookupNode(nodesByPosition, nodePosition + Vector3.up);
            if (up != null)
                currentNode.connections.Add(up, 1);

            Node down = LookupNode(nodesByPosition, nodePosition + Vector3.down);
            if (down != null)
                currentNode.connections.Add(down, 1);
        }
	}

    Node LookupNode(Dictionary<Vector3, Node> nodes, Vector3 lookup)
    {
        if (!nodes.ContainsKey(lookup))
            return null;
        return nodes[lookup];
    }
	
	void Update () {
		
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNodesOnSurface : MonoBehaviour {

    public float tileSize = 1.0f;

    public GameObject spherePlaceholder;

	void Start () {

        Dictionary<Vector3, Node> nodesByPosition = new Dictionary<Vector3, Node>();

        int gridWidth = Mathf.RoundToInt(transform.localScale.x);
        int gridHeight = Mathf.RoundToInt(transform.localScale.y);

        for (int x = 0; x < gridWidth; x++){
            for (int z = 0; z < gridHeight; z++){

                Vector3 nodePosition = new Vector3(x, transform.position.y + 1, z);

                Node tileNode = new Node(nodePosition);
                nodesByPosition.Add(tileNode.position, tileNode);

                /*
                Vector3 nodePosition = new Vector3();

                Node tileNode = new Node(nodePosition);
                nodesByPosition.Add(tileNode.position, tileNode);
                */
            }
        }
        foreach (Vector3 nodePosition in nodesByPosition.Keys)
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

    bool NoObstruction(Node currentNode, Node targetNode)
    {
        if(!Physics.Raycast(currentNode.position, targetNode.position - currentNode.position))
        {
            return true;
        }
        return false;
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

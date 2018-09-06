using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float rotationRate = 180f;


	void Update () {
        transform.Rotate(Vector3.up, rotationRate * Time.deltaTime);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCount : MonoBehaviour {

    public int numberOfValues = 1000;

    public float countBudgetMs = 10;

    private int nextCountValue = 0;

    private float runningTotal = 0;

	void Start () {
		
	}
	
	void Update () {

        System.DateTime endTime = System.DateTime.Now.AddMilliseconds(countBudgetMs);

        while(System.DateTime.Now < endTime && nextCountValue++ < numberOfValues)
        {
            runningTotal += UnityEngine.Random.Range(-1f, 1f);
        }

        if(nextCountValue >= numberOfValues)
        {
            print(runningTotal);
            runningTotal = 0f;
            nextCountValue = 0;
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour {

    public float measurmentWindow = 1.0f;

    private float frameRateTimer = 0f;
    public float frameRate = 0f;
    private int frameCount = 0;

    private System.DateTime lastFrameUpdate;

	// Use this for initialization
	void Start () {
        lastFrameUpdate = System.DateTime.Now;
        ResetTimer();
	}

    void ResetTimer()
    {
        frameRateTimer = 0f;
        frameCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        float elapsedTime = (float)(System.DateTime.Now - lastFrameUpdate).TotalSeconds;
        lastFrameUpdate = System.DateTime.Now;

        frameCount++;
        frameRateTimer += elapsedTime;
        if(frameRateTimer >= measurmentWindow)
        {
            frameRate = (float)frameCount / frameRateTimer;
            ResetTimer();
        }

	}

    private void OnGUI()
    {
        if(frameRate > 0)
        {
            GUILayout.Label(frameRate.ToString("0.00"));
        }
    }
}

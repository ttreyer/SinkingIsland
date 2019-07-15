using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeScaler : MonoBehaviour {
    public float size16_9 = 6f;
    public float size5_4 = 7.5f;

    float map(float s, float a1, float a2, float b1, float b2) {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    // Update is called once per frame
    void Update() {
        float w = Screen.currentResolution.width;
        float h = Screen.currentResolution.height;
        float ratio = w / h;
        Camera.main.orthographicSize = map(ratio, 5f / 4f, 16f / 9f, size5_4, size16_9);
    }
}

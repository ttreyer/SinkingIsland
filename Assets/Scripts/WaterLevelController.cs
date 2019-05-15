using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HeightMap {
    public GameObject border;
    public GameObject inland;
    public GameObject decoration;
    public GameObject resources;
}

public class WaterLevelController : MonoBehaviour {
    public HeightMap[] heights;

    [Range(0, 3)]
    public int height = 0;

    private void SetHeight(int newHeight) {
        height = newHeight;
        for (int h = 0; h < heights.Length; ++h) {
            HeightMap hm = heights[h];
            hm.border.SetActive(h == height);
            hm.inland.SetActive(h >= height);
            hm.decoration.SetActive(h >= height);
            hm.resources.SetActive(h >= height);
        }
    }

    public void RaiseWaterLevel() {
        SetHeight(height + 1);
    }

    public void Start() {
        SetHeight(0);
    }

    private void Update() {
        SetHeight(height);
    }
}
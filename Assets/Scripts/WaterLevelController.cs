using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HeightMap {
    public GameObject border;
    public GameObject inland;
}

public class WaterLevelController : MonoBehaviour {
    public HeightMap[] heights;

    public int height;

    private void SetHeight(int newHeight) {
		height = newHeight;
		for (int h = 0; h < heights.Length; ++h) {
			heights[h].border.SetActive(h == height);
			heights[h].inland.SetActive(h >= height);
		}
    }

	public void RaiseWaterLevel()
	{
		SetHeight(height + 1);
	}

	public void Start()
	{
		SetHeight(0);
	}

	private void Update()
	{
		SetHeight(height);
	}
}

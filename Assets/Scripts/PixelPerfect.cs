using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfect : MonoBehaviour {
    public static float PixToUnit = 1f;
    public static float Scale = 1f;
    public Vector2 NativeRes = new Vector2(240, 160);

    void Awake () {
        var camera = GetComponent<Camera>();
        if (camera.orthographic)
        {
            Scale = Screen.height / NativeRes.y;
            PixToUnit *= Scale;
            camera.orthographicSize = (Screen.height / 2.0f) / PixToUnit;
        }
        Debug.Log("Camera");
	}
	
}

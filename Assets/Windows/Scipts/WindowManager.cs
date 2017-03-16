using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour {
    public GenericWindow[] windows;
    public int currentWindowID;
    static public int defaultWindowID;

    public GenericWindow GetWindow(int value)
    {
        return windows[value];
    }

    private void ToggleVisibility(int value)
    {
        var total = windows.Length;

        for (var i = 0; i < total; i++)
        {
            var window = windows[i];
            if (i == value)
                window.Open();
            else if (window.gameObject.activeSelf)
                window.Close();
        }
    }

    public GenericWindow Open(int value)
    {
        if (value < 0 || value >= windows.Length)
            return null;

        currentWindowID = value;

        ToggleVisibility(currentWindowID);

        return GetWindow(currentWindowID);
    }

    void Start()
    {
        GenericWindow.Manager = this;
        Open(defaultWindowID);
    }
}

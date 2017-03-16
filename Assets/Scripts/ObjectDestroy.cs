using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : MonoBehaviour {
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

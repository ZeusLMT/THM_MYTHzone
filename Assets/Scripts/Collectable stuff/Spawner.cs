using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] prefabs;
    [SerializeField]
    private GameObject Instance;
	private float delay = 2.0f;
    public float delayMax;
    public float delayMin;
    public bool active = true;
    [SerializeField]
    private bool ReadyToDestroy;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(ObjectsGenerator());
        delay = Random.Range(delayMin, delayMax);

    }
	IEnumerator ObjectsGenerator(){
		yield return new WaitForSeconds (delay);
        if (ReadyToDestroy && Instance == null) Destroy(this.gameObject);
        if (active) {
			var newTransform = transform;
            Instance = Instantiate(prefabs[Random.Range(0, prefabs.Length)], newTransform.position, Quaternion.identity);
            Instance.transform.parent = this.gameObject.transform;
        }
        delay = Random.Range(delayMin, delayMax);
        StartCoroutine (ObjectsGenerator ());

	}

    void OnTriggerEnter2D(Collider2D Player)
    {
        if(Player.gameObject.tag == ("Player") && Instance != null)
        {
            if (Instance.name == "FireFLower(Clone)" || Instance.name == "WaterSplash(Clone)") ReadyToDestroy = true;
        }
    }

}

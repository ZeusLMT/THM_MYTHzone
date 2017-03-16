using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	private GameObject player;
	private GameObject floor;
	private RecyclingSpawner spawner;
    [SerializeField]
    private float maxTime = 30;
    static public float curTime = 0;
    [SerializeField]
    private string nextScene;
    private bool loadLock;

 //   void Awake(){
	//	floor = GameObject.Find ("Foreground");
	//	spawner = GameObject.Find ("Spawner").GetComponent<RecyclingSpawner> ();
      
	//}

	// Use this for initialization
	void Start () {

        floor = GameObject.Find("Foreground");
        spawner = GameObject.Find("Spawner").GetComponent<RecyclingSpawner>();
        var floorHeight = floor.transform.localScale.y;

		var pos = floor.transform.position;
		pos.x = 0;
		pos.y = -((Screen.height / PixelPerfect.PixToUnit) / 2) + (floorHeight / 2);
		floor.transform.position = pos;

		spawner.active = false;

		ResetGame ();
	}
	
	// Update is called once per frame
	void Update () {
        curTime += Time.deltaTime;
        if (curTime >= (maxTime + 1) && !loadLock)
        {
            loadScene(nextScene);
        }
    }

	void OnPlayerKilled(){
		spawner.active = false;

		var playerDestroyScript = player.GetComponent<DestroyOffscreen> ();
		playerDestroyScript.DestroyCallback -= OnPlayerKilled;

		player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
        if (!loadLock)
        {
            PlayerPrefs.SetString("currentscene", "RunningLevel");
            loadScene("GameOver");
        }
	}

	void ResetGame(){
        curTime = 0;
		spawner.active = true;
        loadLock = false;
		player = GameObjectUtil.Instantiate(playerPrefab, new Vector3(0, (Screen.height/PixelPerfect.PixToUnit) /2, 0));

		var playerDestroyScript = player.GetComponent<DestroyOffscreen> ();
		playerDestroyScript.DestroyCallback += OnPlayerKilled;
	}
    void loadScene(string nextScene)
    {
        loadLock = true;
        SceneManager.LoadScene(nextScene);
    }
}

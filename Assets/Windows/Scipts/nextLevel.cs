using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class nextLevel : MonoBehaviour
{
    [SerializeField]
    private Text PopupPrefab;
    private Text Popup;
    public Canvas Canvas;
    [SerializeField]
    private string Line1;
    [SerializeField]
    private string Line2;
    [SerializeField]
    private float OffsetX;
    [SerializeField]
    private float OffsetY;
    private bool loadLock;
    public string nextScene;


    void Start()
    {
        loadLock = false;
        Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Popup != null)
        {
            Popup.transform.position = new Vector3(transform.position.x + OffsetX, transform.position.y + OffsetY, 0);

        }
    }
    void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.gameObject.tag == "Player" && Popup == null)
        {
            Popup = Instantiate(PopupPrefab, new Vector3(transform.position.x + OffsetX, transform.position.y + OffsetY, 0), Quaternion.identity);
            Popup.text = Line1 + "\n" + Line2;
            Popup.transform.SetParent(Canvas.transform);
        }
    }

    void OnTriggerExit2D(Collider2D Other)
    {
        if (Other.gameObject.tag == "Player")
        {
            Destroy(Popup);
        }
    }

    void OnTriggerStay2D()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !loadLock)
        {
            loadScene();
        }
    }
    void loadScene()
    {
        loadLock = true;
        SceneManager.LoadScene(nextScene);
    }
}

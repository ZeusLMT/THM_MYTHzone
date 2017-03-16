using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpImage : MonoBehaviour
{
    [SerializeField]
    private Image PopupPrefab;
    private Image Popup;
    public Canvas Canvas;
    [SerializeField]
    private float OffsetX;
    [SerializeField]
    private float OffsetY;


    void Start()
    {
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
            Popup.transform.SetParent(Canvas.transform);
            Popup.transform.SetAsFirstSibling();
        }
    }

    void OnTriggerExit2D(Collider2D Other)
    {
        if (Other.gameObject.tag == "Player")
        {
            Destroy(Popup.gameObject);
        }
    }
}

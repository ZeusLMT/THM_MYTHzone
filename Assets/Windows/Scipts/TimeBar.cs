using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    public float maxTime = 30;
    public Image Fill;
    private Slider Slider;
    [SerializeField]
    private Text PopupPrefab;
    [SerializeField]
    private string Line1;
    [SerializeField]
    private string Line2;
    private Text Popup;
    [SerializeField]
    private float OffsetX;
    [SerializeField]
    private float OffsetY;


    void Start()
    {
        Slider = gameObject.GetComponent<Slider>();
        Popup = Instantiate(PopupPrefab, new Vector3(transform.position.x + OffsetX, transform.position.y + OffsetY, 0), Quaternion.identity);
        Popup.text = Line1 + "\n" + Line2;
        Popup.transform.SetParent(this.transform);
    }

    void Update()
    {
        if (GameObject.Find("Player2(Clone)") != null)
        {
            Slider.value = GameManager.curTime / maxTime;
            Fill.color = Color.Lerp(Color.white, Color.blue, GameManager.curTime / maxTime);
        }
        else
        {
            Slider.value = 0;
            Fill.color = Color.Lerp(Color.red, Color.white, 0);
            return;
        }
    }
}

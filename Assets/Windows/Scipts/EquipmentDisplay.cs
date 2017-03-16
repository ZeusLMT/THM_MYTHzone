using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentDisplay : MonoBehaviour {
    [SerializeField]
    private Image Item;
    [SerializeField]
    private Text Remain;
	// Use this for initialization
	void Start () {
        Remain = GetComponentInChildren<Text>();
        //Item = Image.FindObjectOfType<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Player.Instance != null)
        {
            if (Player.Instance.throwstone.name == "dagger")
            {
                Remain.text = " ";
                Item.sprite = Resources.Load<Sprite>("DaggerIcon");
            }
            else if (Player.Instance.throwstone.name == "FireBall")
            {
                Remain.text = Player.Instance.ThrowRemains.ToString();
                Item.sprite = Resources.Load<Sprite>("FireBallIcon");
            }
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public float maxHealth = 100;
    public float DecreasingRate = 1f;
    public Image Fill;
    private Slider Slider;
    
    
	void Start () {
        Slider = gameObject.GetComponent<Slider>();
	}
    
    void Update()
    {
        if (Player.Instance != null)
        {

            if (Player.Instance.Health > 0)
            {
                Player.Instance.Health -= Time.deltaTime * DecreasingRate;
            }
            Slider.value = Player.Instance.Health / maxHealth;
            Fill.color = Color.Lerp(Color.red, Color.green, Player.Instance.Health / maxHealth);
        }
        else
        {
            Slider.value = 0;
            Fill.color = Color.Lerp(Color.red, Color.white, 0);
            return;
        }
    }
}

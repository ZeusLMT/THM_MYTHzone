using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : MonoBehaviour {
    [SerializeField]
    private GameObject BossPrefab;
    private GameObject Boss;
    [SerializeField]
    private PlatformLayer instance;
    public Jail[] Spike;
    private bool Lock;
    private float Timer = 0;
    private int counter;
    [SerializeField]
    private AudioClip jailbreakSound;
    private AudioSource AudioPlayer;

    void Start()
    {
        AudioPlayer = gameObject.AddComponent<AudioSource>();
        counter = Spike.Length-1;
    }
    void Update () {
		if(instance.PlayerActive == true && !Lock)
        {
            Boss = Instantiate(BossPrefab, transform.position, Quaternion.identity);
            Lock = true;
        }

        if(Boss != null && Boss.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Die"))
        {
            Timer += Time.deltaTime;
            if(Timer >= 3 && counter >= 0)
            {
                if (jailbreakSound) AudioPlayer.PlayOneShot(jailbreakSound);
                Spike[counter].Escape();
                counter -= 1;
                Timer = 0;   
            }
        }

    }
}

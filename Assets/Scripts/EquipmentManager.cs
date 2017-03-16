using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
    
	private Animator animator;
    public int currentItem = 0;

	void Start()
	{
		animator = GetComponent<Animator> ();

	}
    public void Equip(int ItemID, int Remains, AudioClip Sound)
    {
		animator.SetInteger ("EquippedItem", ItemID);
        Player.Instance.Freeze = true;
        if (Player.Instance.throwstone.name == "dagger") currentItem = 0;
        if(currentItem == ItemID)
        {
            Player.Instance.ThrowRemains += Remains;
        }
        else
        {
            Player.Instance.ThrowRemains = Remains;
            switch (ItemID)
            {
                case 1:
                    {
                        Player.Instance.throwstone = (GameObject)Resources.Load("FireBall");
                        Player.Instance.throwSound = Sound;
                        break;
                    }
            }
            currentItem = ItemID;
        }
		StartCoroutine(BackToNormal ());
	}

		IEnumerator BackToNormal(){
			yield return new WaitForSeconds(1.5f);
		animator.SetInteger ("EquippedItem", 0);
        Player.Instance.Freeze = false;


	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlower : Collectible {
	public int ItemId = 1;
	private int number = 5;
    [SerializeField]
    private AudioClip Sound;

    void Start()
    {
    }
	override protected void OnCollect(GameObject target){
		var equipBehavior = target.GetComponent<EquipmentManager> ();
		if (equipBehavior != null) {
            equipBehavior.Equip(ItemId, number, Sound);
		}

	}
}

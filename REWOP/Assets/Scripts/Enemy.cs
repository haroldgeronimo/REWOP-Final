using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharStats))]
public class Enemy : Interactable {
    PlayerManager pM;
    CharStats myStats;
    private void Start()
    {
        pM = PlayerManager.instance;
        myStats = GetComponent<CharStats>();
    }

    public override void Interact() {
        base.Interact();
        //Attack
        CharacterCombat pC = pM.player.GetComponent<CharacterCombat>();
        if (pC != null) {
            pC.Attack(myStats);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    public float radius = 3f;
    public Transform interactionTransform;
    bool isFocus = false;
    Transform player;
    bool hasInteracted = false;

    public virtual void Interact() {
        //overidden methods
        Debug.Log("Interacting with" + transform.name);
    }

    private void Update()
    {
        if (isFocus && !hasInteracted) {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius) {
                //interact
                hasInteracted = true;
            }
        }
    }
    public void OnFocused(Transform playerTransform) {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;

    }

    public void OnDefocused(Transform playerTransform)
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionObjects : MonoBehaviour {
       public List<GameObject> items;
    private void Start()
    {
        //collider = GetComponent<SphereCollider>();

    }
    private void Update()
    {
       
    }
    void OnTriggerEnter(Collider collider)
    {
        

    if(!items.Contains(collider.gameObject))
        if (collider.gameObject.tag == ("Enemy"))
        {
            items.Add(collider.gameObject);
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (items.Contains(collider.gameObject))
            items.Remove(collider.gameObject);

    }
}

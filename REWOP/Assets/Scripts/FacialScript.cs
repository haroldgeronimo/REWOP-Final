using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacialScript : MonoBehaviour {
    public List<Facial> facials;
    public Sprite Died;
    public Image FaceUI;

    public void ShowDamage(int i)
    {
        if (i < facials.Count && i >= 0)
            FaceUI.sprite = facials[i].Hit;
        else Debug.Log("Index " + i + " is out of facial range ");
    }
    public void ShowNormal(int i)
    {
        if (i < facials.Count && i >= 0)
            FaceUI.sprite = facials[i].Normal;
        else Debug.Log("Index " + i + " is out of facial range ");
    }

    public void ShowDead()
    {
        FaceUI.sprite = Died;
    }
    [System.Serializable()]
    public class Facial
    {
        public Sprite Normal;
        public Sprite Hit;
    }
}

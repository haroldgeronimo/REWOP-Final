using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacialScript : MonoBehaviour {
    public List<Facial> facials;
    int currentIndex;
    public Image FaceUI;
    int currentHealth;
    int maxHealth;
    PlayerStats ps;
    void Start()
    {
        FaceUI = GetComponent<Image>();
        ps = PlayerManager.instance.player.GetComponent<PlayerStats>();
    }
    public IEnumerator ShowDamage(int i)
    {
        FaceUI.sprite = facials[i].Hit;
        yield return new WaitForSeconds(1);  

    }
    [System.Serializable()]
    public class Facial
    {
        public Sprite Normal;
        public Sprite Hit;
    }
}

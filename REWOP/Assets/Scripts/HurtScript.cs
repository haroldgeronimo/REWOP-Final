using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HurtScript : MonoBehaviour {
    Image HurtAccentImg;
    public float fadeSpeed = 0.1f;
    public float DamageSensitivity = 4.5f;
    public float ReturnSensitivity = 4f;
    bool IsFading = false;
    private void Start()
    {
        HurtAccentImg = GetComponent<Image>();
      
    }
    public IEnumerator HurtAccent()
    {
        IsFading = true;
        while (HurtAccentImg.color.a > 0f)
        {
            yield return new WaitForSeconds(fadeSpeed);
   
          
            HurtAccentImg.color = new Color(HurtAccentImg.color.r, HurtAccentImg.color.g, HurtAccentImg.color.b, HurtAccentImg.color.a - ((1f/255f) * ReturnSensitivity));
        }
        IsFading = false;
    }

    public void HurtAccentShow(int IDamage)
    {
   float Damage = (float)IDamage * DamageSensitivity;
        if(HurtAccentImg.color.a + (Damage/255f) <= 1)
        HurtAccentImg.color = new Color(HurtAccentImg.color.r, HurtAccentImg.color.g, HurtAccentImg.color.b, HurtAccentImg.color.a + (Damage/255));

        if (!IsFading) {
            StartCoroutine(HurtAccent());
        }
    }
    private void OnEnable()
    {
        HurtAccentShow(0);
    }
}

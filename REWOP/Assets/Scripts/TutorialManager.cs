using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour {
    public TutorialSlide[] tutSlides;
    public Queue<TutorialSlide> slides;

    public TextMeshProUGUI Title;
    public Image image;
    public TextMeshProUGUI Description;

    public GameObject tutorialPanel;
    public UnityEvent tutorialStartEvent;
    public UnityEvent tutorialEndEvent;

    public bool showOnStart = false;

    public void Start()
    {
        slides = new Queue<TutorialSlide>();
        
        if (showOnStart)
        {
            StartPresentation();
        }
    }
   public void StartPresentation()
    {
        for (int i = 0; i < tutSlides.Length; i++)
        {
            slides.Enqueue(tutSlides[i]);
        }
        tutorialStartEvent.Invoke();
        tutorialPanel.SetActive(true);
        DisplayNextSlide();
    }
    public void DisplayNextSlide()
    {
        if(slides.Count <= 0 || tutSlides.Length == 0)
        {
            EndPresentation();
        }
        TutorialSlide slide = slides.Dequeue();
        Title.text = slide.Title;
        image.sprite = slide.Image;
        Description.text = slide.Description;

    }
    public void EndPresentation()
    {
        tutorialPanel.SetActive(false);
        tutorialEndEvent.Invoke();
    }


    [System.Serializable()]
    public class TutorialSlide {
        public string Title;
        public Sprite Image;
        [TextArea(4,3)]
        public string Description;
    }
}

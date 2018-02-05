using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WalkthroughManager : MonoBehaviour {
    [Header("UI")]
   public TextMeshProUGUI captionUI;
    [Space(3)]
    [Header("Dragging")]
    public Image dragHand;
    public Vector3 offset;
    public float smoothSpeed;
    public float fadeSpeed;
    [Space(3)]
    [Header("Pointing")]
    public Image arrow;
    public float stopingDistance;
    public float speed;
    [Space(4)]
    [Header("Walkthrough")]
    public bool showOnStart;
    public WalkthroughStep[] walkthroughSteps;
    public Queue<WalkthroughStep> steps;
    public int currentCount;

    private Vector3 startPos;
    void Start () {
        steps = new Queue<WalkthroughStep>();
        startPos = dragHand.transform.position;

        if (showOnStart)
        {
         
            StartWalkthrough();
        }
    }

    private void StartWalkthrough()
    {
        currentCount = 0;
        Debug.Log("Starting Walkthrough");
        for (int i = 0; i < walkthroughSteps.Length; i++)
        {
            steps.Enqueue(walkthroughSteps[i]);
        }

        captionUI.gameObject.SetActive(true);
        arrow.gameObject.SetActive(true);
        dragHand.gameObject.SetActive(true);
        NextStep();
    }


    public void NextStep()
    {
        //init for next step
        currentCount++;
        arrow.transform.position = dragHand.transform.position = startPos;

        //check if there is more steps
        if (steps.Count <= 0 || walkthroughSteps.Length == 0)
        {
            EndWalkthrough();
        }
        WalkthroughStep step = steps.Dequeue();
        captionUI.text = step.caption;
        StopAllCoroutines();
        if (step.IsDrag)
        {
            StartCoroutine(AnimateDrag(step.objectPoint1.position + offset, step.objectPoint2.position + offset, fadeSpeed, smoothSpeed, true));
        }
        else
        {
            StartCoroutine(AnimatePoint(step.objectPoint1.position, speed));
        }

    }

    public void EndWalkthrough() {
        Debug.Log("Ending Walkthrough");
        StopAllCoroutines();
        captionUI.gameObject.SetActive(false);
        arrow.gameObject.SetActive(false);
        dragHand.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator AnimateDrag(Vector3 obj1, Vector3 obj2, float fadeTime, float animateSpeed, bool IsRepeat)
    {

        Vector3 start = obj1;
        do
        {
            Debug.Log("Starting Drag!");
           // yield return null;
            //fade in
            obj1 = start;
            //start an alpha of 0
            dragHand.color = new Color(1,1,1, 0);  
            //object to start
            dragHand.transform.position = start;
            // loop to 0-1/255
            float startAlpha = 0;
            while (dragHand.color.a < 1)
            {
               yield return new WaitForSeconds(fadeTime);
                startAlpha += (10f / 255f);
                dragHand.color = new Color(1, 1, 1, startAlpha);

            }
            yield return new WaitForSeconds(fadeTime);
         
         
            //animate through Vectors
            while (Vector3.Distance(dragHand.transform.position,obj2) > .1f)
            {
                Debug.Log("Dragging");
                yield return null;
                dragHand.transform.position =  Vector3.Lerp(dragHand.transform.position, obj2, animateSpeed);
            }

            yield return new WaitForSeconds(fadeTime);
            //fade out
            while (dragHand.color.a > 0)
            {
                Debug.Log("Fade out");
                yield return new WaitForSeconds(fadeTime);
                startAlpha -= (10f / 255f);
                dragHand.color = new Color(1, 1, 1, startAlpha);

            }


            dragHand.transform.position = Vector3.zero;

            Debug.Log("Ending Drag!");
        } while (IsRepeat);

    }

    IEnumerator AnimatePoint(Vector3 obj1,float speed)
    {
        float startAlpha = 1;
        while (true) {
            yield return null;
            //rotation
            Vector2 direction = obj1 - arrow.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            arrow.transform.rotation = Quaternion.Slerp(arrow.transform.rotation, rotation, speed);
            //postion
            if (Vector3.Distance(arrow.transform.position,obj1) > stopingDistance)
                arrow.transform.position = Vector3.Lerp(arrow.transform.position, obj1, speed);
            //blink (opsyonal)
           if(arrow.color.a >= 1)
            {
                startAlpha -= (1f / 255f);
                arrow.color = new Color(1, 1, 1, startAlpha);
            }
           else if(arrow.color.a <= 0.5f){
                startAlpha += (1f / 255f);
                arrow.color = new Color(1, 1, 1, startAlpha);
            }
        }
    }

    [System.Serializable()]
    public class WalkthroughStep
    {   [TextArea(2,4)]
        public string caption;
        public bool IsDrag;
        public Transform objectPoint1;
        public Transform objectPoint2;
    }
}

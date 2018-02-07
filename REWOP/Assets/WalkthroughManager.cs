using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WalkthroughManager : MonoBehaviour {
    [HideInInspector]
    public static WalkthroughManager instance;
    [Header("UI")]
   public TextMeshProUGUI captionUI;
    [Space(3)]
    [Header("Dragging")]
    public Image dragHand;
    public Vector3 offset;
    public float smoothSpeed;
    public float fadeSpeed;
    public float dragDistance;
    [Space(3)]
    [Header("Pointing")]
    public Image arrow;
    public float stopingDistance;
    public float speed;
    public float blinkspeed;
    [Space(4)]
    [Header("Walkthrough")]
    public bool showOnStart;
    public WalkthroughStep[] walkthroughSteps;
    public Queue<WalkthroughStep> steps;
    public int currentCount;
    [Space(4)]
    [Header("Controls")]
    public GameObject[] controls;

    private Vector3 startPos;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

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
        DeInitializeNotUsed();
        if (steps.Count == 0 || walkthroughSteps.Length == 0)
        {
            EndWalkthrough();
            return;
        }
        WalkthroughStep step = steps.Dequeue();
        //for initializations
        List<GameObject> usedObj = new List<GameObject>();

            usedObj.Add(step.objectPoint1.gameObject);
        if (step.IsDrag)
            usedObj.Add(step.objectPoint2.gameObject);
        if(step.objectsUsed.Length > 0)
        {
            foreach (GameObject objUsed in step.objectsUsed)
                usedObj.Add(objUsed);
        }

        GameObject[] initObj = new GameObject[usedObj.Count];
        for (int i = 0; i < usedObj.Count; i++)
        {
            initObj[i] = usedObj[i];
        }

      //end for initilizations
      InitializeNotUsed(initObj);
        captionUI.text = step.caption;
        StopAllCoroutines();
        if (step.IsDrag)
        {
            StartCoroutine(AnimateDrag(step.objectPoint1, step.objectPoint2, fadeSpeed, smoothSpeed, true));
        }
        else
        {
            StartCoroutine(AnimatePoint(step.objectPoint1, speed));
        }

    }

    public void EndWalkthrough() {
        Debug.Log("Ending Walkthrough");
        StopAllCoroutines();
        captionUI.gameObject.SetActive(false);
        arrow.gameObject.SetActive(false);
        dragHand.gameObject.SetActive(false);
    }
    List<GameObject> controlswithCG = new List<GameObject>(); //controls that have already CG
    public void InitializeNotUsed(GameObject[] ctrsToBeUsed)
    {
        List<GameObject> controlswithCG = new List<GameObject>();
        //init all game object
        for (int i = 0; i < controls.Length; i++) {
            if(controls[i].GetComponent<CanvasGroup>() != null)
            {
                controlswithCG.Add(controls[i]);
                    continue;
            }
            controls[i].AddComponent<CanvasGroup>();
            controls[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        //init game objects with Canvas Group
        foreach (GameObject ctr in controlswithCG)
        {
            ctr.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        //init used game objects
        foreach (GameObject ctr in ctrsToBeUsed)
        {
            for (int i = 0; i < controls.Length; i++)
            {
               if(ctr == controls[i])
                {
                    controls[i].GetComponent<CanvasGroup>().blocksRaycasts = true;
                }
            }
        }
    }

    public void DeInitializeNotUsed()
    {
        //remove all canvas groups 
        foreach(GameObject ctr in controls)
        {
            if(controlswithCG.Count > 0)
            if (controlswithCG.Contains(ctr))
            {
                ctr.GetComponent<CanvasGroup>().blocksRaycasts = true;
                continue;
            }
       
                CanvasGroup cg = ctr.GetComponent<CanvasGroup>();
                Destroy(cg);
           
        }

        controlswithCG = new List<GameObject>();
    }



    IEnumerator AnimateDrag(Transform obj1, Transform obj2, float fadeTime, float animateSpeed, bool IsRepeat)
    {

        Vector3 start = obj1.position;
        do
        {
           // Debug.Log("Starting Drag!");
           // yield return null;
            //fade in
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
            while (Vector3.Distance(dragHand.transform.position,obj2.position) > dragDistance)
            {
            //    Debug.Log("Dragging");
                yield return null;
                dragHand.transform.position =  Vector3.Lerp(dragHand.transform.position, obj2.position, animateSpeed);
            }

            yield return new WaitForSeconds(fadeTime);
            //fade out
            while (dragHand.color.a > 0)
            {
            //    Debug.Log("Fade out");
                yield return new WaitForSeconds(fadeTime);
                startAlpha -= (10f / 255f);
                dragHand.color = new Color(1, 1, 1, startAlpha);

            }


            dragHand.transform.position = Vector3.zero;

            //Debug.Log("Ending Drag!");
        } while (IsRepeat);

    }

    IEnumerator AnimatePoint(Transform obj1,float speed)
    {
        float startAlpha = 1;
        while (true) {
            yield return null;
            //rotation
            Vector2 direction = obj1.position - arrow.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            arrow.transform.rotation = Quaternion.Slerp(arrow.transform.rotation, rotation, speed);
            //postion
            if (Vector3.Distance(arrow.transform.position,obj1.position) > stopingDistance)
                arrow.transform.position = Vector3.Lerp(arrow.transform.position, obj1.position, speed);
            //blink (opsyonal)
           if(arrow.color.a >= 1)
            {
                startAlpha = Mathf.Lerp(startAlpha, 0f, Time.deltaTime * blinkspeed);
                arrow.color = new Color(1, 1, 1, startAlpha);
            }
           else if(arrow.color.a <= 0.01f){
                startAlpha = Mathf.Lerp(startAlpha, 1f, Time.deltaTime * blinkspeed);
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
      [Header("Optional")]
  public GameObject[] objectsUsed;
    }
}

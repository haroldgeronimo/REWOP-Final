using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeleteArea : MonoBehaviour, IDropHandler , IPointerEnterHandler, IPointerExitHandler {
    public Transform CodeCanvas;
    public Sprite openSprite;
    public Sprite closeSprite;
    public Transform returnParent;
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.gameObject.tag == "decision"
           || eventData.pointerDrag.gameObject.tag == "repeat"
           || eventData.pointerDrag.gameObject.tag == "codeblock")
        {
            BlockDestroy(eventData.pointerDrag.gameObject);

        }
        else if (eventData.pointerDrag.gameObject.tag == "codeblockHolder")
        {
            eventData.pointerDrag.GetComponent<CodeCatalog>().droppedFunction = null; 
            BlockDestroy(eventData.pointerDrag.GetComponent<CodeCatalog>().codeblk);
        }
    }
    private void BlockDestroy(GameObject go)
    {
        Destroy(go);
        GetComponent<CanvasGroup>().alpha = .5f;
        transform.GetChild(0).GetComponent<Image>().sprite = closeSprite;
    }
    private void BlockEnter(GameObject go) {

        GetComponent<CanvasGroup>().alpha = 1;
        transform.GetChild(0).GetComponent<Image>().sprite = openSprite;
        go.AddComponent<Outline>();
        go.GetComponent<Outline>().effectColor = new Color(1, 0, 0);
        go.GetComponent<Outline>().effectDistance = new Vector2(4, 4);
        go.transform.parent = CodeCanvas.parent.parent;
    }
    private void BlockExit(GameObject go)
    {

        GetComponent<CanvasGroup>().alpha = .5f;
        transform.GetChild(0).GetComponent<Image>().sprite = closeSprite;
        Outline outline = go.GetComponent<Outline>();
        Destroy(outline);
        go.transform.parent = CodeCanvas;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.tag);
        if (eventData.pointerDrag.gameObject.tag == "decision"
          || eventData.pointerDrag.gameObject.tag == "repeat"
          || eventData.pointerDrag.gameObject.tag == "codeblock") {
            BlockEnter(eventData.pointerDrag.gameObject);
        }
       else if (eventData.pointerDrag.gameObject.tag == "codeblockHolder")
        {
            Debug.Log("enter codeblock Handler");
            BlockEnter(eventData.pointerDrag.GetComponent<CodeCatalog>().codeblk);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        if (eventData.pointerDrag.gameObject.tag == "decision"
          || eventData.pointerDrag.gameObject.tag == "repeat"
          || eventData.pointerDrag.gameObject.tag == "codeblock")
        {
            BlockExit(eventData.pointerDrag);
        }
       else if (eventData.pointerDrag.gameObject.tag == "codeblockHolder")
        {
            Debug.Log("exit codeblock Handler");
            BlockExit(eventData.pointerDrag.GetComponent<CodeCatalog>().codeblk);
        }
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

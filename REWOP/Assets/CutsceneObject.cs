using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneObject: MonoBehaviour {
    public int cutSceneNumber;
    public CutSceneManager CSM;
    public DialogueManager DM;
    public List<CutScene> cutscenes;
    private Queue<CutScene> cs;
    public UnityEvent StartEvent;
    public UnityEvent EndEvent;

    private void Start()
    {
        DM = FindObjectOfType<DialogueManager>();
    }
    public void StartCutscene()
    {
        cs = new Queue<CutScene>();
        foreach(CutScene cutscene in cutscenes)
        {
            cs.Enqueue(cutscene);
        }
        StartEvent.Invoke();
        ShowNextScene();
    }
    Dialogue dialouge;
    CutScene cutscene;
    public void ShowNextScene()
    {
        if (cs.Count > 0)
        {
            cutscene = cs.Dequeue();
            CSM.mainCamera.gameObject.SetActive(false);
            cutscene.camera.gameObject.SetActive(true);
           
            DM.StartDialogue(cutscene.dialogue);
            StartCoroutine(CheckDialogue());
        }
        else
        {
            cutscene.camera.gameObject.SetActive(false);
            EndCutscene();
        }
    }
    public void EndCutscene()
    {
        Debug.Log("EndCutscene");
        CSM.sceneCompleted[cutSceneNumber] = true;
        CSM.mainCamera.gameObject.SetActive(true);
        EndEvent.Invoke();


    }

    IEnumerator CheckDialogue()
    {
        while (!DM.IsDone)
            yield return null;

        cutscene.camera.gameObject.SetActive(false);
        ShowNextScene();
        yield return null;
     }
    [System.Serializable()]
    public class CutScene
    {
        public Transform target;
        public Vector3 offset;
        public Dialogue dialogue;
        public Camera camera;
    }
}

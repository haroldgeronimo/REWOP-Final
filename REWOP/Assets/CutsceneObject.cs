using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneObject: MonoBehaviour {
    public int cutSceneNumber;
    private CutSceneManager CSM;
    public DialogueManager DM;
    public List<CutScene> cutscenes;
    private Queue<CutScene> cs;
    public UnityEvent StartEvent;
    public UnityEvent EndEvent;

    private void Start()
    {
        DM = FindObjectOfType<DialogueManager>();
        CSM = CutSceneManager.instance;
    }
    public void StartCutscene()
    {
        if (CSM.ActiveCutScene == cutSceneNumber)//if this is already active dont start
            return;

        cs = new Queue<CutScene>();
        foreach (CutScene cutscene in cutscenes)
        {
            cs.Enqueue(cutscene);
        }
        StartEvent.Invoke();
        CSM.ActiveCutScene = this.cutSceneNumber;
        ShowNextScene();

    }
    Dialogue dialouge;
    CutScene cutscene;
    public void ShowNextScene()
    {
        Debug.Log("cs count:" + cs.Count);
        if (cs.Count > 0)
        {
        
            cutscene = cs.Dequeue();
            cutscene.startSceneEvent.Invoke();
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
        CSM.ActiveCutScene = -1;
        CSM.sceneCompleted[cutSceneNumber] = true;
        CSM.mainCamera.gameObject.SetActive(true);
        EndEvent.Invoke();
        CutSceneState.Save();

    }

    IEnumerator CheckDialogue()
    {
        while (!DM.IsDone)
            yield return null;
        Debug.Log("cs count:" + cs.Count);
       cutscene.endSceneEvent.Invoke();
        cutscene.camera.gameObject.SetActive(false);
        ShowNextScene();
        yield return null;
     }
    [System.Serializable()]
    public class CutScene
    {
        public UnityEvent startSceneEvent;
        public Transform target;
        public Vector3 offset;
        public Dialogue dialogue;
        public Camera camera;
        public UnityEvent endSceneEvent;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES2SaveManager : MonoBehaviour {
    public string sceneObjectFile = "SaveData/sceneobjects.dat";
    public static ES2SaveManager instance;

    /*
	 * This is where we save certain aspects of our instantiated prefabs.
	 * This will be called when the application is quit.
	 */

    public void Awake()
    {
        
        instance = this;
    }
    /* We also save on application pause in iOS, as OnAppicationQuit isn't always called */
#if UNITY_IPHONE && !UNITY_EDITOR
	public void OnApplicationPause()
	{
		Save();
	}
#endif

    /*
	 * This is where we'll load our data.
	 * 
	 */
    public void Start()
    {
        // If there's scene object data to load
        if (ES2.Exists(sceneObjectFile))
        {
            // Get how many scene objects we need to load, and then try to load each.
            // We load scene objects first as created objects may be children of them.
            int sceneObjectCount = ES2.Load<int>(sceneObjectFile + "?tag=sceneObjectCount");

            for (int i = 0; i < sceneObjectCount; i++)
                LoadObject(i, sceneObjectFile);
        }

     
    }

    public void Save()
    {
        // Save how many scene objects we're saving so we know how many to load.
        ES2.Save(ES2ObjectManager.SceneObjects.Length, sceneObjectFile + "?tag=sceneObjectCount");

        // Iterate over each scene object
        for (int i = 0; i < ES2ObjectManager.SceneObjects.Length; i++)
            SaveObject(ES2ObjectManager.SceneObjects[i], i, sceneObjectFile);

    }

    /*
	 * Saves an Object
	 * 'i' is the number of the object we are saving.
	 */
    private void SaveObject(GameObject obj, int i, string file)
    {
        // Let's get the UniqueID object, as we'll need this.
        ES2UniqueID uID = obj.GetComponent<ES2UniqueID>();


        //Note that we're appending the 'i' to the end of the path so that
        //we know which object each piece of data belongs to.
        ES2.Save(uID.id, file + "?tag=uniqueID" + i);
        Debug.Log("Saving " + i);
        ES2.Save(uID.prefabName, file + "?tag=prefabName" + i);
        // Save whether the GameObject this UniqueID is attached to is active or not.
#if UNITY_3_5
		ES2.Save(uID.gameObject.active, file+"?tag=active"+i);
#else
        ES2.Save(uID.gameObject.activeSelf, file + "?tag=active" + i);
#endif


        // You could add many more components here, inlcuding custom components.
        // For simplicity, we're only going to save the Transform component.
        Transform t = obj.GetComponent<Transform>();
        if (t != null)
        {
            ES2.Save(t, file + "?tag=transform" + i);
            // We'll also save the UniqueID of the parent object here, or -1
            // string if it doesn't have a parent.
            ES2UniqueID parentuID = ES2UniqueID.FindUniqueID(t.parent);
            if (parentuID == null)
                ES2.Save(-1, file + "?tag=parentID" + i);
            else
                ES2.Save(parentuID.id, file + "?tag=parentID" + i);
        }
    }

    /*
	 * Loads an Object
	 * 'i' is the number of the object we are loading.
	 */
    private void LoadObject(int i, string file)
    {
        int uniqueID = ES2.Load<int>(file + "?tag=uniqueID" + i);
        string prefabName = ES2.Load<string>(file + "?tag=prefabName" + i);

        // Create or get an object based on our loaded id and prefabName
        GameObject loadObject;
        // If our prefab name is blank, we're loading a scene object.
        if (prefabName == "")
            loadObject = ES2UniqueID.FindTransform(uniqueID).gameObject;
        else
            loadObject = ES2ObjectManager.InstantiatePrefab(prefabName);

        // Load whether this GameObject is active or not.
#if UNITY_3_5
		loadObject.active = ES2.Load<bool>(file+"?tag=active"+i);
#else
        loadObject.SetActive(ES2.Load<bool>(file + "?tag=active" + i));
#endif

        Transform t = loadObject.GetComponent<Transform>();
        if (t != null)
        {
            // Auto-assigning Load is the best way to load Components.
            ES2.Load<Transform>(file + "?tag=transform" + i, t);
            // Now we'll get the parent object, if any.
            int parentuID = ES2.Load<int>(file + "?tag=parentID" + i);
            Transform parent = ES2UniqueID.FindTransform(parentuID);
            if (parent != null)
                t.parent = parent;
        }
    }
}

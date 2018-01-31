using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementView : MonoBehaviour {
    public AchievementData AD;
    public GameObject achievementObject;
    public Transform achievementPanel;

    /// <summary>
    //Start can be deleted after this class has been reference to the Game Manager
    /// </summary>
    private void Start()
    {
        LoadAchievements();
    }

    public void LoadAchievements()
    {
        foreach (AchievementMeta AM in AD.Achievements)
        {
            GameObject achievementObj = Instantiate(achievementObject, achievementPanel);
            achievementObj.GetComponent<AchievementObject>().UpdateView(AM);

        }
    }
    public void UpdateAchievements()
    {
        int i = 0;
        foreach (Transform achievementObj in achievementPanel)
        {
            AchievementMeta AM = AD.Achievements[i];
            achievementObj.GetComponent<AchievementObject>().UpdateView(AM);
            i++;
        }
    }
}

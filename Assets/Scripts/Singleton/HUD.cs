using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : Singleton<HUD>
{
    [SerializeField] Image environmentHealth;
    [Header("Small task")]
    [SerializeField] TextMeshProUGUI smallTaskTMP;
    [SerializeField] Image smallTaskLoading;

    private void Start() {
        StopSmallTaskLoading();
    }
    private void Update() {

    }

    public void SetEnvironmentHealth(float val) {
        if (val < 0 || val > 1) throw new Exception("health value should be between 0 and 1");
        environmentHealth.fillAmount = val;
    }

    public void IncreaseEnvironmentHealth(float delta) {
        SetEnvironmentHealth(environmentHealth.fillAmount + delta);
    }

    [SerializeField] TextMeshProUGUI missionDisplay;
    private List<string> missions = new();
    public void AddMission(string mission) {
        missions.Add(mission);
        updateMissionDisplay();
    }
    public void RemoveMission(string mission) {
        int i = mission.IndexOf(mission);
        if (i != -1) {
            mission.Remove(i);
        }
        updateMissionDisplay();
    }

    private void updateMissionDisplay() {
        missionDisplay.text = "";
        string str = "";
        missions.ForEach(e => str += "- e \n");
    }

    public void SetSmallTaskLoading(string taskName, float duration) {
        
    }

    public void StopSmallTaskLoading() {
        smallTaskLoading.fillAmount = 0f;
        smallTaskTMP.text = "";
    }

}

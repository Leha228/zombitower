using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour
{
    private void Start() {
        UpdateHerousList();
        UpdateTowerList();
    }

    private void UpdateTowerList() {
        foreach (var item in PersonModel.singleton.towerSelectList)
        {
            if (item.name == PlayerPrefs.GetInt(UserModel.ACTIVE_TOWER, 0).ToString())
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }

    private void UpdateHerousList() {
        foreach (var item in PersonModel.singleton.herousSelectList)
        {
            if (item.name == PlayerPrefs.GetInt(UserModel.ACTIVE_HEROUS, 0).ToString())
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }

    public void SelectTab(string name) {
        foreach (var tab in PersonModel.singleton.tabs) {
            if (tab.name == name)
                tab.SetActive(true);
            else
                tab.SetActive(false);
        }
    }

    public void SelectTower(int tower) {
        PlayerPrefs.SetInt(UserModel.ACTIVE_TOWER, tower);
        MainMenuManager.singleton.RenameDataAsset();
        UpdateTowerList();
    }

    public void SelectHerous(int herous) {
        PlayerPrefs.SetInt(UserModel.ACTIVE_HEROUS, herous);
        MainMenuManager.singleton.RenameDataAsset();
        UpdateHerousList();
    }

    public void NextTower(int currentTower) {
        int indexTower = UserModel.singleton.towers.IndexOf(currentTower);
        int nameTower = CheckIndexTower(indexTower + 1);

        if (nameTower == -1) return;

        foreach (var item in PersonModel.singleton.towerList)
        {
            if (item.name == nameTower.ToString())
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }

    public void PrevTower(int currentTower) {
        int indexTower = UserModel.singleton.towers.IndexOf(currentTower);
        int nameTower = CheckIndexTower(indexTower - 1);

        if (nameTower == -1) return;

        foreach (var item in PersonModel.singleton.towerList)
        {
            if (item.name == nameTower.ToString())
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }

    public void NextHerous(int currentHerous) {
        int indexHerous = UserModel.singleton.towers.IndexOf(currentHerous);
        int nameHerous = CheckIndexHerous(indexHerous + 1);

        if (nameHerous == -1) return;

        foreach (var item in PersonModel.singleton.herousList)
        {
            if (item.name == nameHerous.ToString())
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }

    public void PrevHerous(int currentHerous) {
        int indexHerous = UserModel.singleton.towers.IndexOf(currentHerous);
        int nameHerous = CheckIndexHerous(indexHerous - 1);

        if (nameHerous == -1) return;

        foreach (var item in PersonModel.singleton.herousList)
        {
            if (item.name == nameHerous.ToString())
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }

    private int CheckIndexTower(int index) {
        try { return Convert.ToInt32(UserModel.singleton.towers[index]); }
        catch { return -1; }
    }

    private int CheckIndexHerous(int index) {
        try { return Convert.ToInt32(UserModel.singleton.herous[index]); }
        catch { return -1; }
    }
}

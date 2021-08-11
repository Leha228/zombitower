using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour
{
    private void Start() => UpdateTowerList();

    private void UpdateTowerList() {
        foreach (var item in PersonModel.singleton.towerSelectList)
        {
            if (item.name == PlayerPrefs.GetInt(UserModel.ACTIVE_TOWER, 0).ToString()) 
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }

    public void SelectTower(int tower) {
        PlayerPrefs.SetInt(UserModel.ACTIVE_TOWER, tower);
        UpdateTowerList();
    }

    public void NextTower(int currentTower) {
        int indexTower = UserModel.singleton.towers.IndexOf(currentTower);
        int nameTower = CheckIndex(indexTower + 1);

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
        int nameTower = CheckIndex(indexTower - 1);

        if (nameTower == -1) return;
    
        foreach (var item in PersonModel.singleton.towerList)
        {
            if (item.name == nameTower.ToString())
                item.SetActive(true);
            else
                item.SetActive(false); 
        }
    }

    private int CheckIndex(int index) {
        try { return Convert.ToInt32(UserModel.singleton.towers[index]); }
        catch { return -1; }
    }
}

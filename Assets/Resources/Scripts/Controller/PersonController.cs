using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour
{
    private void Start() {
        UpdatePlayerList();
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

    private void UpdatePlayerList() {
        foreach (var item in PersonModel.singleton.playerSelectList)
        {
            if (item.name == PlayerPrefs.GetInt(UserModel.ACTIVE_PLAYER, 0).ToString())
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
        UpdateTowerList();
    }

    public void SelectPlayer(int player) {
        PlayerPrefs.SetInt(UserModel.ACTIVE_PLAYER, player);
        UpdatePlayerList();
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

    public void NextPlayer(int currentPlayer) {
        int indexPlayer = UserModel.singleton.towers.IndexOf(currentPlayer);
        int namePlayer = CheckIndexPlayer(indexPlayer + 1);

        if (namePlayer == -1) return;

        foreach (var item in PersonModel.singleton.playerList)
        {
            if (item.name == namePlayer.ToString())
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }

    public void PrevPlayer(int currentPlayer) {
        int indexPlayer = UserModel.singleton.towers.IndexOf(currentPlayer);
        int namePlayer = CheckIndexPlayer(indexPlayer - 1);

        if (namePlayer == -1) return;

        foreach (var item in PersonModel.singleton.playerList)
        {
            if (item.name == namePlayer.ToString())
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }

    private int CheckIndexTower(int index) {
        try { return Convert.ToInt32(UserModel.singleton.towers[index]); }
        catch { return -1; }
    }

    private int CheckIndexPlayer(int index) {
        try { return Convert.ToInt32(UserModel.singleton.players[index]); }
        catch { return -1; }
    }
}

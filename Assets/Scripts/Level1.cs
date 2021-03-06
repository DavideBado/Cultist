﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Level1 : MonoBehaviour
{
    public GameManager manager;
    public LevelManager levelManager;
    private void OnEnable() // Quando ti attivi
    {
        Save();
        manager.CultistOnOff(true);
    }

    private void OnDisable() // Quando ti spegni
    {
        levelManager.LoadPositions(); // Posiziona nuovamente i cultisti
    }
    void Save()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager.NewGame == true) // Se è il gioco è appena iniziato
        {
            levelManager.SavePositions(); // Salva le posizioni dei cultisti
            gameManager.NewGame = false; // Il gioco ora è iniziato da abbastanza
        }
       
    }
}

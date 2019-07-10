using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    GameManager gameManager;
    public Canvas WinCanvas;
    public GameObject Level;
    float time = 0;
    public bool Ingame = false;
    public Text Cultists, TimerText, TotalScore;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {       
        InUpdate();
    }
    void InUpdate()
    {
        UpdateTime();
    }

    void UpdateTime()
    {
        if (Ingame) // Se è iniziata la partita
        {
            time += Time.deltaTime; // Tieni il tempo
        }
        else time = 0; // In alternativa aspetta
    }
    public void Win(int CultistNum) // Pare che qualcuno abbia vinto, fatti dire quanti cultisti ha salvato
    {
        Level.SetActive(false); // Spegni il livello che non serve più
        WinCanvas.gameObject.SetActive(true); // Accendi la schermata di vittoria
        Cultists.text = (CultistNum + "X100"); // Spiega un po' quanti punti vengono assegnati per cultista salvato
        TimerText.text = ((int)time + "X10"); // E quanti punti rimossi per secondo sprecato
        TotalScore.text = (((CultistNum * 100) - ((int)time * 10)).ToString()); // Ora calcola e visualizza il totale
        Ingame = false; // Già, è un po' che non stiamo giocando
    }

    public void StartTheGame() // Pare che qualcuno voglia giocare
    {
        Ingame = true; // Ok, siamo in partita
    }   
}

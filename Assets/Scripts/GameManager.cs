using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Level, Target;
    public bool NewGame;
    public List<Cultist> Cultists = new List<Cultist>();
    // Start is called before the first frame update
    void Start()
    { 
        NewGame = true; // Sì, abbiamo iniziato ora
    }
    
    public void PlayNow()
    {
        SendMessage("StartTheGame"); // Avvisa, in questo caso solo il timer, che il gioco è iniziato
        Level.SetActive(true); // Accendi il livello
        Target.SetActive(true); // Accendi il puntatore
        //CultistOnOff();
    }

    public void CultistOnOff(bool _On)
    {
        foreach (Cultist _cultist in Cultists)
        {
            _cultist.gameObject.SetActive(_On);
        }
    }
}

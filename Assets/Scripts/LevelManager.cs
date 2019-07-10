using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject Cultistpref;
    CultistsData cultistsData;

    public void SavePositions() // Salva
    {
        cultistsData = new CultistsData(); // Qui dentro vanno messi i dati da salvare
        List<Cultist> cultists = FindObjectsOfType<Cultist>().ToList(); // Trova un po' tutti i cultisti in scena
        foreach (var _cultist in cultists) // Ora per ogni cultista
        {
            cultistsData.CultistsPosition.Add(_cultist.transform.position); // Metti la sua posizione nella lista di vettori da salvare
        }
        string json = JsonUtility.ToJson(cultistsData); // Converti tutti i dati da salvare in stringa
        PlayerPrefs.SetString("Cultists", json); // Dai un nome alla stringa e mettila nella lista di cose da salvare
        PlayerPrefs.Save(); // Salva tutto quello che aspettava di essere salvato


    }
    public void LoadPositions() // Carica
    {
       string json = PlayerPrefs.GetString("Cultists"); // Ora vai a prendere la stringa in memoria
         cultistsData = JsonUtility.FromJson<CultistsData>(json); // Converti da stringa a CultistData

        //foreach (var _dataposition in cultistsData.CultistsPosition) // Dovrebbe esserci una lista di vettori, ecco per ogni vettore della lista
        //{
        //    GameObject _cultist = Instantiate(Cultistpref, _dataposition, Quaternion.identity); // Metti un cultista
        //    _cultist.transform.parent = GetComponent<GameManager>().Level.transform;
        //}
        for (int i = 0; i < cultistsData.CultistsPosition.Count; i++)
        {
           GetComponent<GameManager>().Cultists[i].transform.position = cultistsData.CultistsPosition[i];          
        }
       Clear(); // Prima togli la roba vecchia
    }

    public void Clear() // Pulisci la scena
    {
        List<Cultist> cultists = FindObjectsOfType<Cultist>().ToList(); // Trova un po' tutti i cultisti
        foreach (var _cultist in cultists) // Ora che li hai trovati
        {
            _cultist.gameObject.SetActive(false); // Eliminali
        }

    }

}

[Serializable]
public class CultistsData
    {
    public List<Vector3> CultistsPosition = new List<Vector3>(); // Un po' di vettori
    }
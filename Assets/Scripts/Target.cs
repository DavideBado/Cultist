using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Target : MonoBehaviour
{
    private NavMeshAgent[] navAgents;
    public Transform targetMarker;
    List<Cultist> cultistsInScene = new List<Cultist>();
    List<Cultist> cultistsInArea = new List<Cultist>();

    private void OnEnable()
    {
        cultistsInArea.Clear();
    }
    private void UpdateTargets(Vector3 targetPosition) // Aggiorna la destinazione dei cultisti
    {
        navAgents = FindObjectsOfType(typeof(NavMeshAgent)) as NavMeshAgent[]; // Cerca tutti i NavMeshAgent, in questo caso i cultisti
        foreach (NavMeshAgent agent in navAgents) // Per ogni cultista
        {
            agent.destination = targetPosition; // Imposta la destinazione
        }
    }   
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Se è stato premuto il pulsante sinistro del mouse
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Fai partire un raggio dalla posizione del mouse
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit)) // Se entra in collisione con un oggetto
            {
                Vector3 targetPosition = hit.point; // La destinazione è il punto di collisione
                UpdateTargets(targetPosition); // Aggiorna la destinazione
                targetMarker.position = targetPosition; // Aggiorna la posizione per il debug
            }
        }
    }
    
    private void OnDrawGizmos() // Debug per il target
    { // Disegna una linea rossa verticale per visualizzare il target
        Debug.DrawLine(targetMarker.position, targetMarker.position + Vector3.up * 5, Color.red);
    }

    private void OnTriggerStay(Collider other) // Se un oggetto entra nell'area di arrivo
    {
        UpdateCultists(); // Controlla quanti cultisti ci sono in scena
        if (other.GetComponent<Cultist>() != null) // Se l'oggetto che è appena entrato è un cultista
        {
            bool CanAddThisCultist = true;
            foreach (Cultist _cultist in cultistsInArea)
            {
                if(other.GetComponent<Cultist>() == _cultist)
                {
                    CanAddThisCultist = false;
                }
            }
            if (CanAddThisCultist)
            {
                cultistsInArea.Add(other.GetComponent<Cultist>()); // Aggiungilo alla lista di cultisti nel traguardo
            }

            if (cultistsInArea.Count == cultistsInScene.Count && cultistsInScene.Count > 1) // Se i cultisti nel traguardo sono tutti i cultisti in scena
            {
                Debug.Log("Hai vinto"); // Il giocatore ha vinto
                GetComponentInParent<ScoreManager>().Win(cultistsInArea.Count); // Fai partire il finale di vittoria            
                foreach (var item in cultistsInScene)
                {
                    item.gameObject.SetActive(false);
                }
                gameObject.SetActive(false); // Spegniti
            }
        }
    }   
            
    private void OnTriggerExit(Collider other)
    {
        UpdateCultists(); // Aggiorna i cultisti in scena
        if (other.GetComponent<Cultist>() != null) // Se ad uscire è un cultista
        {
            cultistsInArea.Remove(other.GetComponent<Cultist>()); // Rimuovilo dalla lista dei cultisti che hanno raggiunto il traguardo
        }
    }

    void UpdateCultists()
    {
        cultistsInScene.Clear();
        cultistsInScene = FindObjectsOfType<Cultist>().ToList(); // Trova tutti i cultisti in scena
    }
}
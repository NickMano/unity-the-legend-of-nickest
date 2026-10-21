using System.Collections;
using UnityEngine;

public class ExitControler : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    [Tooltip("Mapa objetivo")]
    [SerializeField]
    private GameObject targetMap;

    [Tooltip("Donde estan los enemigos")]
    [SerializeField]
    private GameObject EnemigosPreb;

    [SerializeField] private AreaTransitionController transitionController;

    [SerializeField] private AreaController area;

    IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            yield return transitionController.StartTransition(col, area, spawnPoint, targetMap, EnemigosPreb);
        }
    }
}

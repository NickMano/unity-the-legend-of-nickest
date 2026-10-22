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

    [SerializeField] private AreaCoordinator coordinator;

    [SerializeField] private AreaNameView area;

    IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            yield return coordinator.StartTransition(col, area, spawnPoint, targetMap, EnemigosPreb);
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class ExitControler : MonoBehaviour
{
    [Tooltip("Warp objetivo")]
    [FormerlySerializedAs("target")]
    [SerializeField]
    private GameObject targetPortal;

    [Tooltip("Mapa objetivo")]
    [SerializeField]
    private GameObject targetMap;

    [Tooltip("Donde estan los enemigos")]
    [SerializeField]
    private GameObject EnemigosPreb;

    [SerializeField] private AreaTransitionController transitionController;

    [SerializeField] private AreaController area;

    // Opacidad inicial del cuadrado de transición
    // Transición de 1 segundo

    IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            yield return transitionController.StartTransition(col, area, targetPortal, targetMap, EnemigosPreb);
        }
    }
}

using System.Collections;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private AreaCoordinator coordinator;
    [SerializeField] private AreaNameView area;
    [SerializeField] private AreaContext targetArea;

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out var player))
        {
            yield return coordinator.StartTransition(player, area, spawnPoint, targetArea);
        }
    }
}

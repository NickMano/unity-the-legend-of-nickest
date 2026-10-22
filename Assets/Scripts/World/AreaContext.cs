using UnityEngine;

public class AreaContext : MonoBehaviour
{
    [SerializeField] private string areaId;
    [SerializeField] private string displayName;
    [SerializeField] private Transform defaultSpawnPoint;
    [SerializeField] private Collider2D cameraBounds;
    [SerializeField] private GameObject enemiesPrefab;

    public string AreaId => areaId;
    public string DisplayName => displayName;
    public Transform DefaultSpawnPoint => defaultSpawnPoint;
    public Collider2D CameraBounds => cameraBounds;
    public GameObject EnemiesPrefab => enemiesPrefab;
}
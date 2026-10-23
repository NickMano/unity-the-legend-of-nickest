using UnityEngine;

public class AreaContext : MonoBehaviour
{
    [SerializeField] private string areaId;
    [SerializeField] private string displayName;
    [SerializeField] private Transform defaultSpawnPoint;
    [SerializeField] private GameObject enemiesPrefab;
    [SerializeField] private GameObject map;

    public string AreaId => areaId;
    public string DisplayName => displayName;
    public Transform DefaultSpawnPoint => defaultSpawnPoint;
    public GameObject EnemiesPrefab => enemiesPrefab;
    public GameObject Map => map;
}
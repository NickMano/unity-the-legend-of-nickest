using UnityEngine;
using System.Collections;

public class AreaCoordinator : MonoBehaviour
{
    private bool inTransition = false;

    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private CamaraFollow camera;
    [SerializeField] private AreaContext initialArea;

    public AreaContext CurrentArea { get; private set; }

    void Start()
    {
        PrepareArea(initialArea);
    }

    public IEnumerator StartTransition(PlayerController player, AreaNameView area, Transform spawnPoint, AreaContext targetMap)
    {
        if (inTransition) yield break;

        try
        {
            inTransition = true;

            player.SetTransitionLocked(true);
            yield return screenFader.FadeToBlack();

            yield return area.FadeOut(targetMap.DisplayName);

            ChangePositionAndCheckPoint(player, spawnPoint.position);
            PrepareArea(targetMap);

            yield return screenFader.FadeToClear();
        }
        finally
        {
            player.SetTransitionLocked(false);
            inTransition = false;
        }

    }

    private void ChangePositionAndCheckPoint(PlayerController player, Vector3 position)
    {
        player.SpawnTo(position);
        player.SetPosIni(position);
    }

    public void PrepareArea(AreaContext area)
    {
        camera.SetBound(area.Map);
        UpdateEnemies(area.EnemiesPrefab);
        CurrentArea = area;
    }

    private void UpdateEnemies(GameObject enemies)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemigo");
        if (objects.Length != 0)
            foreach (GameObject obj in objects)
                Destroy(obj);

        //Genero los enemigos del nuevo mapa
        if (enemies) Instantiate(enemies);
    }
}

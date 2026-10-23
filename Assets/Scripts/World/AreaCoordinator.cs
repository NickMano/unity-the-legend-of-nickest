using UnityEngine;
using System.Collections;

public class AreaCoordinator : MonoBehaviour
{
    [SerializeField] private GameSession session;
    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private CamaraFollow camera;

    public AreaContext CurrentArea { get; private set; }

    public IEnumerator StartTransition(PlayerController player, AreaNameView area, Transform spawnPoint, AreaContext targetMap)
    {
        if (session.State != GameSessionState.Playing) yield break;

        try
        {
            session.StartTransition();

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
            session.EndTransition();
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

        if (enemies) Instantiate(enemies);
    }
}

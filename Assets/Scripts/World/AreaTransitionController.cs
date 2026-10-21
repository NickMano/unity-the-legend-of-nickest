using UnityEngine;
using System.Collections;

public class AreaTransitionController : MonoBehaviour
{
    private bool inTransition = false;

    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private CamaraFollow camera;

    public IEnumerator StartTransition(Collider2D player, AreaController area, Transform spawnPoint, GameObject targetMap, GameObject enemies)
    {
        if (inTransition) yield break;

        var playerController = player.GetComponent<PlayerController>();
        if (playerController == null) yield break;

        try
        {
            inTransition = true;

            playerController.SetTransitionLocked(true);
            yield return screenFader.FadeToBlack();

            yield return area.FadeOut(targetMap.name);

            ChangePositionAndCheckPoint(playerController, spawnPoint.position);
            camera.SetBound(targetMap);
            UpdateEnemies(enemies);

            yield return screenFader.FadeToClear();
        }
        finally
        {
            playerController.SetTransitionLocked(false);
            inTransition = false;
        }

    }

    private void ChangePositionAndCheckPoint(PlayerController player, Vector3 position)
    {
        player.SpawnTo(position);
        player.SetPosIni(position);
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

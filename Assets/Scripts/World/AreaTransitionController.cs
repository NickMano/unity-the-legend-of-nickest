using UnityEngine;
using System.Collections;

public class AreaTransitionController : MonoBehaviour
{
    private bool inTransition = false;
    private bool isFadeIn = false;
    private float alpha = 0;
    private readonly float fadeTime = 1f;

    public IEnumerator StartTransition(Collider2D player, AreaController area, GameObject targetPortal, GameObject targetMap, GameObject enemies)
    {
        if (inTransition) yield break;
        inTransition = true;

        BlockPlayerMovement(player);
        isFadeIn = true;
        yield return ChangeArea(area, targetMap.name);
        ChangePosition(player, targetPortal);
        ChangeCheckpoint(player);
        UpdateCamera(targetMap);
        UpdateEnemies(enemies);
        isFadeIn = false;
        UnblockPlayerMovement(player);
    }

    void OnGUI()
    {
        if (!inTransition) return;

        // Si ha empezado creamos un color con una opacidad inicial a 0
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);

        // Creamos una textura temporal para rellenar la pantalla
        Texture2D tex;
        tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.black);
        tex.Apply();

        // Dibujamos la textura sobre toda la pantalla
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);

        // Controlamos la transparencia
        if (isFadeIn)
        {
            // Si es la de aparecer le sumamos opacidad
            alpha = Mathf.Lerp(alpha, 1.1f, fadeTime * Time.deltaTime);
        }
        else
        {
            // Si es la de desaparecer le restamos opacidad
            alpha = Mathf.Lerp(alpha, -0.1f, fadeTime * Time.deltaTime);
            // Si la opacidad llega a 0 desactivamos la transición
            if (alpha < 0) inTransition = false;
        }

    }

    private void BlockPlayerMovement(Collider2D player)
    {
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player.GetComponent<Animator>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;

    }

    private IEnumerator ChangeArea(AreaController area, string name)
    {
        StartCoroutine(area.FadeOut(name));
        yield return new WaitForSeconds(fadeTime);
    }
    private void ChangePosition(Collider2D player, GameObject target)
    {
        player.transform.position = target.transform.GetChild(0).transform.position;
    }

    private void ChangeCheckpoint(Collider2D player)
    {
        player.SendMessage("SetPosIni", player.transform.position);
    }

    private void UpdateCamera(GameObject target)
    {
        Camera.main.GetComponent<CamaraFollow>().SetBound(target);
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

    private void UnblockPlayerMovement(Collider2D player)
    {
        player.GetComponent<Animator>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class Selector : MonoBehaviour
{
    [SerializeField] GameObject pointer;
    [SerializeField] GameObject list;

    private int indice = 0;

    void Start()
    {
        Dibujar();
    }

    void Update()
    {
        if (ShouldGoUp())
        {
            indice--;
            TryMove();
            return;
        }

        if (ShouldGoDown())
        {
            indice++;
            TryMove();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
            PerformAction();
    }

    private void Dibujar()
    {
        Transform opcion = list.transform.GetChild(indice);
        pointer.transform.position = new Vector3(pointer.transform.position.x, opcion.position.y - 0.2566187f, 0f); // Pivote a la izquierda
    }

    private bool ShouldGoUp() => Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
    private bool ShouldGoDown() => Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);

    private void TryMove()
    {
        if (indice > list.transform.childCount - 1)
            indice = 0;
        else if (indice < 0)
            indice = list.transform.childCount - 1;

        Dibujar();
    }

    private void PerformAction()
    {
        Transform option = list.transform.GetChild(indice);

        if (option.TryGetComponent<ISelectorAction>(out var action))
            action.Perform();
    }
}
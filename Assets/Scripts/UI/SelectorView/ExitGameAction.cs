using UnityEngine;

public class ExitGameAction : MonoBehaviour, ISelectorAction
{
    public void Perform()
    {
        Application.Quit();
    }
}
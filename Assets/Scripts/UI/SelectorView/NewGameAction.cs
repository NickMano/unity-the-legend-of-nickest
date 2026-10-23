using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameAction : MonoBehaviour, ISelectorAction
{
    public void Perform()
    {
        SceneManager.LoadScene("World");
    }
}
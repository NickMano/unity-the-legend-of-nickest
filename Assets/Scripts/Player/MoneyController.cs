using UnityEngine;

public class MoneyController : MonoBehaviour
{
    [SerializeField, Min(0f)] private int currentMoney;
    [SerializeField, Min(0f)] private int maxMoney;

    public void AddMoney(int valor)
    {
        currentMoney += valor;
        if (currentMoney > maxMoney)
            currentMoney = maxMoney;
        Debug.Log("ahora tenes " + currentMoney);
    }
}

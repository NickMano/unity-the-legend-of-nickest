using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField, Min(0f)] private int currentMoney;
    [SerializeField, Min(0f)] private int maxMoney;

    public void Add(int amount)
    {
        if (!IsValidAmount(amount)) return;
        currentMoney = Mathf.Min(currentMoney + amount, maxMoney);
    }

    public void Remove(int amount)
    {
        if (!IsValidAmount(amount)) return;
        currentMoney = Mathf.Max(0, currentMoney - amount);
    }

    public bool TrySpend(int amount)
    {
        if (!IsValidAmount(amount)) return false;
        if (currentMoney < amount) return false;

        currentMoney = Mathf.Max(0, currentMoney - amount);
        return true;
    }

    private bool IsValidAmount(int amount) => amount > 0;
}

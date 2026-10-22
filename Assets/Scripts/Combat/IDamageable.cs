interface IDamageable
{
    void TakeDamage(DamageInfo info);
}

public readonly struct DamageInfo
{
    public int Amount { get; }

    public DamageInfo(int amount)
    {
        Amount = amount;
    }
}
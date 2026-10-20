interface IDamageable
{
    void TakeDamage(DamageInfo info);
}

public class DamageInfo
{
    int amount;
}
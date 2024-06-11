public interface IDamageable
{
    void TakeDamage(int damage, DamageType damageType = DamageType.Player);
}

public enum DamageType
{
    Enemy,
    Player,
}
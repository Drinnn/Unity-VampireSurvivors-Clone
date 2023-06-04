public interface IDamageable
{
    void TakeDamage(float amount);
    void TakeDamageWithKnockBack(float amount, float knockBackTime, float knockBackMultiplier);
}

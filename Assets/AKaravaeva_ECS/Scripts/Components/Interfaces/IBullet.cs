public interface IBullet
{
    void Live();

    void Die();

    // Пока на будующее
    float ToDamage();

    void AddValDemage(float val);
}

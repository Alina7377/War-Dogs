using UnityEngine;

public class StandartBullet : MonoBehaviour, IBullet
{
    [SerializeField] protected float _speedFly;
    [SerializeField] protected float _timeToLive;
    [SerializeField] private float _valDamage;
    [SerializeField] private GameObject _dieEffect;

    protected float _timeCreate = 0f;

    private void Start()
    {
        _timeCreate = Time.time;
    }

    private void Update()
    {
        Live();
    }

    public void Die()
    {
        if (_dieEffect!=null)
            Instantiate(_dieEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public virtual void Live()
    {
        if (Time.time < _timeCreate + _timeToLive)
        {
            Vector3 newPosition = transform.position;
            newPosition += transform.forward * _speedFly * Time.deltaTime;
            transform.position = newPosition;
        }
        else
            Die();
    }

    public float ToDamage()
    {
        return _valDamage;
    }

    public void AddValDemage(float val)
    {
        _valDamage += val;
    }
}

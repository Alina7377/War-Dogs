using UnityEngine;
using Zenject;

public class JerkAbility : MonoBehaviour,IAbilityRetBool
{
    [SerializeField] private float _speed;
    [SerializeField] private float _duration;
    [SerializeField] private float _delay;
    [SerializeField] private SkinnedMeshRenderer _meshRender;
    [SerializeField] private Material _effectMaterial;
    [SerializeField] private GameObject _effectObject;


    [Inject]
    private IGameConfig _gameConfig;

    private float _jerkDelayTime = 0f;
    private float _jerkStartTime = 0f;
    private bool _isJerkBegin = false;
    private bool _isJerkReload = false;
    private Material _baseMaterial;
    private CharacterController _characterContorl;

    private void Start()
    {
        _speed = _gameConfig.GetJerkSpeed;
        _baseMaterial = _meshRender.material;
        _characterContorl = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_isJerkReload && Time.time > _jerkDelayTime + _delay)
        {
            VisualizeEffect(_baseMaterial);
            _isJerkReload = false;
        }
    }

    private void UpdateStatusJark()
    {
        _jerkStartTime = Time.time;
        _isJerkBegin = false;
        _jerkDelayTime = Time.time;
        _isJerkReload = true;
        _effectObject.SetActive(false);


    }

    private void VisualizeEffect(Material material)
    {
        _meshRender.material = material;
    }

    public bool Execute()
    {
        if (_isJerkReload)
        {
            Debug.Log("[JERK ABILITY] Перезарядка рывка");
            return false;
        }

        if (_isJerkBegin)
        {
            if (Time.time < _jerkStartTime + _duration)
            {
                Vector3 newPos = transform.forward * _speed * Time.deltaTime;
                _characterContorl.Move(newPos);
                return true;
            }
            else
            {
                UpdateStatusJark();
                _characterContorl.Move(Vector3.zero);
                return false;
            }
        }
        _effectObject.SetActive(true);
        VisualizeEffect(_effectMaterial);
        _isJerkBegin = true;
        _jerkStartTime = Time.time;

        return true;

    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    GameObject quiver;
    [SerializeField]
    Projectile prefabProjectile;
    [SerializeField]
    float speedRotateProjectile;


    public DynamicJoystick joystick;

    Projectile _currentProjectile;
    float _vertical;
    float _horizontal;
    bool _isShooting;

    #region Methods

    private void Start()
    {
        StartCoroutine(GetNewProjectile());
    }

    private void OnEnable()
    {
        DynamicJoystick.ShotAction += Shot;
    }

    private void OnDisable()
    {
        DynamicJoystick.ShotAction -= Shot;
    }

    void Shot()
    {
        if (quiver.transform.childCount > 0)
        {
            Debug.Log("Shot");
            Rigidbody rb = _currentProjectile.GetComponent<Rigidbody>();
            _isShooting = true;
            rb.useGravity = true;
            rb.AddForce(Vector3.forward * 2 * Mathf.Abs(_horizontal), ForceMode.Impulse);

            _currentProjectile.transform.parent = null;
            StartCoroutine(GetNewProjectile());
        }
    }

    void Update()
    {
        var dir = joystick.Direction;
        _vertical = dir.y;
        _horizontal = dir.x;

        if (_horizontal < -0f)
        {
            //spriteWeapon.sprite = spritesWeapon[0];
        }


        if (_horizontal < -0.3f)
        {
            //spriteWeapon.sprite = spritesWeapon[1];
        }

        if (_horizontal < -0.6f)
        {
            //spriteWeapon.sprite = spritesWeapon[2];
        }

        if (!_isShooting && _currentProjectile != null)
        {
            Debug.Log(_vertical);
            
            _currentProjectile.transform.rotation = Quaternion.Euler(_currentProjectile.transform.eulerAngles.x,
            _currentProjectile.transform.eulerAngles.y,
            Mathf.Clamp(_vertical * speedRotateProjectile, 60, 120));

            //transform.rotation = Quaternion.Euler(0, -90, _currentProjectile.transform.eulerAngles.x * -1);
        }
    }
    #endregion



    IEnumerator GetNewProjectile()
    {
        yield return new WaitForSeconds(0.5f);

        if (quiver.transform.childCount == 0)
        {
            var newProjectile = Instantiate(prefabProjectile, quiver.transform);
            _currentProjectile = newProjectile;
            _isShooting = false;
        }
        else
        {
            _currentProjectile = quiver.transform.GetComponentInChildren<Projectile>();
        }
    }




}

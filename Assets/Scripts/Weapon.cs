using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{

    [SerializeField]
    List<Sprite> spritesWeapon = new List<Sprite>();
    [SerializeField]
    Projectile projectile;
    [SerializeField]
    float speedRotateProjectile;
    [SerializeField]
    SpriteRenderer spriteWeapon;

    public DynamicJoystick joystick;


    float _vertical;
    float _horizontal;
    bool _isShooting;

    private void OnEnable()
    {
        DynamicJoystick.UpFinger += Stop;
    }

    private void OnDisable()
    {
        DynamicJoystick.UpFinger -= Stop;
    }

    void Stop()
    {
        Debug.Log("Stop");
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        _isShooting = true;
        rb.useGravity = true;
        rb.AddRelativeForce(Vector3.forward * 20 * Mathf.Abs(_horizontal), ForceMode.Impulse);
        //StartCoroutine(ResetProjectile());
    }

    IEnumerator ResetProjectile()
    {
        yield return new WaitForSeconds(0.5f);
        _isShooting = false;
    }

    void Update()
    {
        var dir = joystick.Direction;
        _vertical = dir.y;
        _horizontal = dir.x;

        //Debug.Log("horizontal: " + _horizontal);

        if (_horizontal < -0f)
        {
            spriteWeapon.sprite = spritesWeapon[0];
        }


        if (_horizontal < -0.3f)
        {
            spriteWeapon.sprite = spritesWeapon[1];
        }

        if (_horizontal < -0.6f)
        {
            spriteWeapon.sprite = spritesWeapon[2];
        }

        if (!_isShooting)
        {
            projectile.transform.rotation = Quaternion.Euler(Mathf.Clamp(_vertical * speedRotateProjectile, -30, 30),
            projectile.transform.eulerAngles.y,
            projectile.transform.eulerAngles.z);

            spriteWeapon.transform.rotation = Quaternion.Euler(0, -90, projectile.transform.eulerAngles.x * -1);
        } 
        //else
        //{
        //    projectile.transform.LookAt(Vector3.forward);
        //}

    }


}

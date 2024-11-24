using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator _animator;
    [SerializeField]
    Rigidbody[] rigidbodies;
    [SerializeField]
    CharacterJoint[] characterJoints;

    private void Start()
    {
        characterJoints = GetComponentsInChildren<CharacterJoint>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();

        Initial();
    }

    void Initial()
    {
        Debug.Log(rigidbodies.Length);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            _animator.enabled = false;

            foreach (Rigidbody rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = false;
            }

            GetComponent<BoxCollider>().enabled = false;
        }
    }
}

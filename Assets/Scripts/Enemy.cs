using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator _animator;
    private Transform _transform;
    [SerializeField]private Target target;
    [SerializeField]private float agroRange;
    [SerializeField] private float attackDistance=1.5F;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
    }
    
    private void Update()
    {
        
        if (!target || target.die || !IsRange(agroRange))
        {
            _animator.SetBool("run",false);
            _animator.SetBool("attack",false);
            return;
        }

        Rotate();
        
        if (IsRange(attackDistance))
        {
            _animator.SetBool("run",false);
            _animator.SetBool("attack",true);
        }
        else 
        {
            _animator.SetBool("attack",false);
            Move();
            _animator.SetBool("run",true);
        }
    }

    private void Move()
    {
        var newPos= Vector3.MoveTowards(_transform.position, target.transform.position, 2F * Time.deltaTime);
        _transform.position = newPos;
    }
    
    private bool IsRange(float dist)
    {
        return Vector3.Distance(_transform.position,target.transform.position) <=dist;
    }
    
    private void Rotate()
    {
        var directionVector = (target.transform.position - transform.position).normalized;
        var look = Quaternion.Slerp(_transform.rotation, 
            Quaternion.LookRotation(directionVector), 5F*Time.deltaTime);
        look.x =0;
        look.z = 0;

        _transform.rotation = look;
    }
}

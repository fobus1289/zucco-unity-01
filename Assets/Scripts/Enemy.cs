using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,ITakeDamage,ISpawn
{
    private Animator _animator;
    private Transform _transform;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField]private Target target;
    [SerializeField]private float agroRange;
    [SerializeField] private float attackDistance=1.5F;
    [SerializeField] private float HP = 0F;
    private float maxHP = 0F;
    private float spawnTime = 15F;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
        if (target == null)
        {
            target = FindObjectOfType<Target>();
        }
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

    public void TakeDamage(int damage)
    {
        HP -= damage;
        
        if (HP <= 0)
        {
            _animator.SetBool("attack",false);
            _animator.SetBool("die",true);
            Invoke(nameof(Death),3F);
        }
        
    }

    private void Death()
    {
        GameManagerScript.Instance.SetSpawn(this);
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }

    public void DeSpawn()
    {
        gameObject.SetActive(false);
    }

    public float RespawnTime()
    {
        return spawnTime;
    }
}

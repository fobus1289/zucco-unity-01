using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour,ITakeDamage,ISpawn
{
    private Animator _animator;
    private Transform _transform;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField]private Target target;
    [SerializeField]private float agroRange;
    [SerializeField] private float attackDistance=1.5F;
    [SerializeField] private float HP = 0F;
    public Slider Slider;
    [SerializeField]private Slider hpSlider;
    private float maxHP = 0F;
    private float spawnTime = 5F;
    public bool IsDie => HP <= 0F;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
        
        
    }
    
    private void Update()
    {



        hpSlider.transform.rotation = Camera.main.transform.rotation;
           // Quaternion.LookRotation(Vector3.forward, Camera.main.transform.position);


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

    public void SetHP(float hp)
    {
        maxHP = hp;
        HP = hp;
        hpSlider.maxValue = maxHP;
        hpSlider.value = hp;
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

    private void OnMouseDown()
    {
        Slider.gameObject.SetActive(true);
        Slider.maxValue = maxHP;
        Slider.value = HP;
    }
    
    public void TakeDamage(int damage)
    {
        if (!target)
        {
            target = FindObjectOfType<Target>();
        }
        
        HP -= damage;
        Slider.value = HP;
        hpSlider.value = HP;
        if (HP <= 0)
        {
            target = null;
            
            _animator.SetBool("run",false);
            _animator.SetBool("attack",false);
            _animator.SetBool("die",true);
            Invoke(nameof(Death),3F);
        }
        
    }

    private void Death()
    {
        Slider.gameObject.SetActive(false);
        GameManagerScript.Instance.SetSpawn(this);
    }

    public void Spawn()
    {
        _animator.SetBool("die",false);
        target = null;
        HP = maxHP;
        hpSlider.value = maxHP;
    }

    public void DeSpawn()
    {
        gameObject.SetActive(false);
    }

    public float RespawnTime()
    {
        return spawnTime;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CapsuleScript : MonoBehaviour
{

    private Transform _transform;
    private Animator _animator;
    [SerializeField]private Target _target;

    public float distance = 0F;
    private static readonly int Attack = Animator.StringToHash("attack");
    private static readonly int Attack2 = Animator.StringToHash("attack_2");
    private float timeAttack = 0F ;
    private void Awake()
    {
        //print("Awake"); 
        _transform = transform;
        _animator = GetComponent<Animator>();
    }
    
    private  void Update()
    {

        if (_target.die)
        {
            return;            
        }
        
        var currentDist = Vector3.Distance(_transform.position,_target.transform.position);
        if (currentDist <= distance)
        {
            var directionVector = (_target.transform.position - transform.position).normalized;
            var quaternion = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionVector), 5F*Time.deltaTime);
            quaternion.x =0;
            quaternion.z = 0;
            transform.rotation = quaternion;

            if (currentDist > 1.5F)
            {
               // _animator.SetBool(Attack,false);
                var newPos= Vector3.MoveTowards(_transform.position, _target.transform.position, 2F * Time.deltaTime);

                _transform.position = newPos; 
            }
            else if (timeAttack <= 0)
            {
               // _animator.SetBool(Attack,true);
                _animator.SetTrigger(Attack2);
                _target.TakeDamage(10);
                timeAttack = 1F;
            }

            timeAttack -= Time.deltaTime;
        }
    }
    
    
}

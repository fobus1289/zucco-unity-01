using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour,ITakeDamage,ISpawn
{

    private bool _canMove;
    private Coroutine _moveCoroutine;
    private Animator _animator;
    private Vector3 clickPosition;
    [SerializeField] private float xMaxSpeed = 300F;
    [SerializeField] private CinemachineFreeLook _cinemachineFreeLook;
    [SerializeField] private float HP = 500F;    
    
    private void Start()
    {
        _animator = this.GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        
        if (HP <= 0)
        {
            GameManagerScript.Instance.SetSpawn(this);
        }
        
    }

    private IEnumerator Move(Vector3 point)
    {
        while (Vector3.Distance(transform.position,point) > 1F)
        {
           
            var newPos = Vector3.MoveTowards(
                transform.position, 
                point, 
                    2F * Time.deltaTime
                );
            _animator.SetBool("run",true);
            yield return null;
            Rotate(point);
            transform.position = newPos;
        }
        _animator.SetBool("run",false);

        _moveCoroutine = null;
    }
    
    private void Update()
    {

        if (Input.GetKey(KeyCode.Mouse1))
        {
            _cinemachineFreeLook.m_YAxis.Value = 0;
            _cinemachineFreeLook.m_XAxis.m_MaxSpeed = xMaxSpeed;
        }
        else
        {
            _cinemachineFreeLook.m_XAxis.m_MaxSpeed = 0F;
        }
        
        if (Input.GetMouseButton(0))
        {
            var position = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(position,out var point ))
            {

                switch (point.collider.tag)
                {
                    case "Ground":
                        
                        if (_moveCoroutine !=null)
                        {
                            StopCoroutine(_moveCoroutine);
                        }
                        
                        _moveCoroutine = StartCoroutine(Move(point.point));
                        
                        break;
                }
            }
            
        }
    }
    
    private void Rotate(Vector3 point)
    {
        var directionVector = (point - transform.position).normalized;
        var look = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(directionVector), 5F*Time.deltaTime);
        look.x =0;
        look.z = 0;

        transform.rotation = look;
    }

    public void Spawn()
    {
       
    }

    public void DeSpawn()
    {
        
    }

    public float RespawnTime()
    {
        return 5F;
    }
}

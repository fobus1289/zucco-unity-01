using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour,ITakeDamage
{

    private bool _canMove;
    private Coroutine _moveCoroutine;
    private Animator _animator;
    private Vector3 clickPosition;
    private void Start()
    {
        _animator = this.GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        
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
        if (Input.GetMouseButton(0))
        {
            var position = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(position,out var point ))
            {

                switch (point.collider.tag)
                {
                    case "Ground":
                        clickPosition = point.point;
                        if (_moveCoroutine ==null)
                        {
                            _moveCoroutine = StartCoroutine(Move(clickPosition));
                        }

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
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour,ISpawn
{

    private bool _canMove;
    private Coroutine _moveCoroutine;
    private Coroutine _moveAndAttackCoroutine;
    private Animator _animator;
    private Vector3 clickPosition;
    public Slider Slider;
    [SerializeField] private float xMaxSpeed = 300F;
    [SerializeField] private CinemachineFreeLook _cinemachineFreeLook;
    [SerializeField] private float HP = 500F;
    [SerializeField] private Transform weaponSlot;
    [SerializeField] public List<WeaponScript> Weapons;
    [SerializeField] private Button WeaponUI;
    [SerializeField] private Transform ActionPanelUI;
    private WeaponScript _currenWeapon = null;
    private bool _canAttack => _currenWeapon;
    private void Start()
    {
        _animator = this.GetComponent<Animator>();

        foreach (var weapon in Weapons)
        {
            
            var newWeaponUI = Instantiate(WeaponUI,ActionPanelUI);
            newWeaponUI.transform.SetParent(ActionPanelUI);
            
            newWeaponUI.onClick.AddListener(() =>
            {
                if (_currenWeapon)
                {
                    Destroy(_currenWeapon.gameObject);
                }
                
                var newWeapon = Instantiate(weapon,weaponSlot);
                newWeapon.transform.SetParent(weaponSlot);
                _currenWeapon = newWeapon;
            });
            
        }
        
    }
    
    public void ChangeWeapon(int index)
    {
        if (_currenWeapon)
        {
            Destroy(_currenWeapon.gameObject);
        }

        var weaponScript = Weapons[index];

        var newWeapon = Instantiate(weaponScript,weaponSlot);
        newWeapon.transform.SetParent(weaponSlot);
        _currenWeapon = newWeapon;
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


    private IEnumerator MoveAndAttack(Transform target)
    {
        var enemy =  target.GetComponent<Enemy>();

        if (enemy == null)
        {
            yield break;
        }

        while (!enemy.IsDie)
        {
            Rotate(target.position);
            if (Vector3.Distance(transform.position,target.position) > 1.2F)
            {
                _animator.SetBool("attack",false);
                _animator.SetBool("run",true);
                var newPos = Vector3.MoveTowards(
                    transform.position, 
                    target.position, 
                    2F * Time.deltaTime
                );
                transform.position = newPos;
            }
            else
            {
                _animator.SetBool("run",false);
                _animator.SetBool("attack",_canAttack);
            }
            
            yield return null;
        }
        _animator.SetBool("attack",false);
        _animator.SetBool("run",false);
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

                if (_currenWeapon)
                {
                    _currenWeapon.GetComponent<BoxCollider>().enabled = false;
                }
                
                switch (point.collider.tag)
                {
                    case "Ground":
                        _animator.SetBool("attack",false);
                        
                        Slider.gameObject.SetActive(false);
                        if (_moveAndAttackCoroutine!=null)
                        {
                            StopCoroutine(_moveAndAttackCoroutine);
                        }
                        if (_moveCoroutine !=null)
                        {
                            StopCoroutine(_moveCoroutine);
                        }
                        
                        _moveCoroutine = StartCoroutine(Move(point.point));
                        
                        break;
                    case "Mob":
                        if (_moveAndAttackCoroutine!=null)
                        {
                            StopCoroutine(_moveAndAttackCoroutine);
                        }
                        if (_moveCoroutine !=null)
                        {
                            StopCoroutine(_moveCoroutine);
                        }
                        _moveAndAttackCoroutine = StartCoroutine(MoveAndAttack(point.transform));
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
    
    public void EnableCollider(int enable)
    {
        if (_currenWeapon)
        {
            _currenWeapon.GetComponent<BoxCollider>().enabled = enable ==1;
        }
    }
    
    public void DeSpawn()
    {
        
    }

    public float RespawnTime()
    {
        return 5F;
    }
    
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}

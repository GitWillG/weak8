﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicAI : MonoBehaviour
{
    public List<GameObject> targetpoints;
    public bool walking;    
    private GameObject currentTarget;
    public bool isAlive;
    int newrand;
    [SerializeField]
    public Animator _anim;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        walking = false;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (isAlive)
        {
            if (targetpoints.Count != 0 && walking == false)
            {
                int rand = Random.Range(0, targetpoints.Count - 1);
                currentTarget = targetpoints[rand];
                walking = true;
            }

            if (walking == true)
            {
                transform.position = Vector3.MoveTowards(this.transform.position, currentTarget.transform.position, speed * Time.deltaTime);
                
                _anim.SetBool("_isMoving", true);
                float dir = currentTarget.transform.position.x - this.transform.position.x; 
                if (dir < 0)
                {
                    _spriteRenderer.flipX = true;
                }
                else if (dir > 0)
                {
                    _spriteRenderer.flipX = false;
                }
            }

            float dist = Vector3.Distance(currentTarget.transform.position, transform.position);
            
            if (dist <= 1f)
            {
                _anim.SetBool("_isMoving", false);
                newrand = Random.Range(0, targetpoints.Count);
                if (targetpoints[newrand].gameObject == currentTarget.gameObject)
                {
                    newrand = Random.Range(0, targetpoints.Count);
                }
                else
                {
                    StartCoroutine(wait1sec());
                    Debug.Log("oopp");
                }
               
            }
        }
        else
        {
            _anim.SetBool("_isMoving", false);
            Debug.Log("stopped moving");
        }
    }
    IEnumerator wait1sec()
    {
        yield return new WaitForSeconds(1);
        currentTarget = targetpoints[newrand];
        _anim.SetBool("_isMoving", true);
    }

}
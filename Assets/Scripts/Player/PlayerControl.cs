using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool isMovable;
    public Vector3 clickPoint;

    private PlayerData data;

    private Rigidbody rigid;
    private Animator anim;

    private void Awake()
    {
        isMovable = true;

        data = GetComponent<PlayerData>();

        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        if (isMovable)
        {
            Vector3 deltaMove = Vector3.MoveTowards(transform.position, clickPoint, data.moveSpeed * Time.deltaTime);
            rigid.MovePosition(deltaMove);

            Vector3 dir = clickPoint - transform.position;
            dir.y = 0;

            if (dir != Vector3.zero)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), data.rotateSpeed * Time.deltaTime * 100);

            if (dir.sqrMagnitude < 0.1f * 0.1f)
            {
                anim.SetFloat("speed", 0.0f);
                isMovable = false;
            }
        }
    }

    private void Rotate()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private Camera mainCam;

    private Rigidbody rigid;
    private Animator anim;

    public float moveSpeed;
    public float rotateSpeed;

    private void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        ableMove = false;
    }

    private void Update()
    {
        ClickDestPos();
    }

    private void FixedUpdate()
    {
        Move();
    }

    RaycastHit hit;

    private void ClickDestPos()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000, 1 << 8))
            {
                if (moveSpeed <= 4)
                {
                    anim.SetFloat("speed", 0.5f);
                }
                else
                {
                    anim.SetFloat("speed", 1.0f);
                }
                ableMove = true;
            }
        }
    }

    private bool ableMove;

    private void Move()
    {
        if (ableMove == true)
        {
            Vector3 deltaMove = Vector3.MoveTowards(transform.position, hit.point, moveSpeed * Time.deltaTime);
            rigid.MovePosition(deltaMove);

            Vector3 dir = hit.point - transform.position;
            dir.y = 0;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.deltaTime * 100);

            if (dir.sqrMagnitude < 0.1f * 0.1f)
            {
                anim.SetFloat("speed", 0.0f);
                ableMove = false;
            }
        }
    }
}

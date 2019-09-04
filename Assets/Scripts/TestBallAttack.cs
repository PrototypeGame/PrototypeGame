using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBallAttack : MonoBehaviour
{
    private Rigidbody rigid;
    public float shootPower;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine("BallShoot");
    }

    public IEnumerator BallShoot()
    {
        rigid.AddRelativeForce(Vector3.forward * shootPower, ForceMode.Impulse);

        yield return new WaitForSeconds(5);

        Destroy(gameObject);
    }
}

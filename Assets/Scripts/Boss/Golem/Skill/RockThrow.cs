using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrow : AttackActionBase
{
    List<Transform> rockPool;

    public float interval = 1.0f;
    public float projectSpeed = 20.0f;
    public GameObject rockOb;

    private Transform tr;
    private Vector3[] anglePoints = new Vector3[12];

    private WaitForSeconds throwInterval;
    // Start is called before the first frame update
    void Awake()
    {
        rockPool = new List<Transform>();
        throwInterval = new WaitForSeconds(interval);
        tr = GetComponent<Transform>();

        float angleTemp = Mathf.PI / 3;

        anglePoints[0] = new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0));
        anglePoints[1] = new Vector3(Mathf.Cos(angleTemp), 0, Mathf.Sin(angleTemp));
        anglePoints[2] = new Vector3(Mathf.Cos(angleTemp * 2), 0, Mathf.Sin(angleTemp * 2));
        anglePoints[3] = new Vector3(Mathf.Cos(angleTemp * 3), 0, Mathf.Sin(angleTemp * 3));
        anglePoints[4] = new Vector3(Mathf.Cos(angleTemp * 4), 0, Mathf.Sin(angleTemp * 4));
        anglePoints[5] = new Vector3(Mathf.Cos(angleTemp * 5), 0, Mathf.Sin(angleTemp * 5));

        float temp = angleTemp / 2;

        anglePoints[6] = new Vector3(Mathf.Cos(temp), 0, Mathf.Sin(temp));
        anglePoints[7] = new Vector3(Mathf.Cos(temp * 3), 0, Mathf.Sin(temp * 3));
        anglePoints[8] = new Vector3(Mathf.Cos(temp * 5), 0, Mathf.Sin(temp * 5));
        anglePoints[9] = new Vector3(Mathf.Cos(temp * 7), 0, Mathf.Sin(temp * 7));
        anglePoints[10] = new Vector3(Mathf.Cos(temp * 9), 0, Mathf.Sin(temp * 9));
        anglePoints[11] = new Vector3(Mathf.Cos(temp * 11), 0, Mathf.Sin(temp * 11));

        for (int i = 0; i < 12; i++)
        {
            var rock = Instantiate(rockOb, tr.position + anglePoints[i] * 2.0f, Quaternion.identity).GetComponent<Transform>();
            rock.hideFlags = HideFlags.HideInHierarchy;
            rock.gameObject.SetActive(false);
            rockPool.Add(rock);
        }
    }

    public void Update()
    {
        for (int i = 0; i < 12; i++)
        {
            if (rockPool[i].gameObject.activeSelf)
            {
                rockPool[i].Translate(anglePoints[i] * projectSpeed * Time.deltaTime);
                if ((rockPool[i].position - tr.position).sqrMagnitude > 15.0f * 15.0f)
                {
                    rockPool[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public override void ExcuteSkill()
    {
        Debug.Log("RockThrow");
        StartCoroutine(Skill());
    }

    IEnumerator Skill()
    {
        int i = 0;
        for (; i < 6; i++)
        {
            rockPool[i].position = tr.position + anglePoints[i];
            rockPool[i].gameObject.SetActive(true);
        }

        yield return throwInterval;

        for (; i < 12; i++)
        {
            rockPool[i].position = tr.position + anglePoints[i];
            rockPool[i].gameObject.SetActive(true);
        }

        yield return null;
    }
}

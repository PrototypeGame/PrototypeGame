using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : AttackActionBase
{
    public Projector attackChargingPro;
    public Projector attackRangePro;
    public GameObject effect;
    public float activeTime = 2;

    private float orSizePerSec = 0.0f;
    private float rockStartY = 0.0f;
    // Start is called before the first frame update
    void Awake()
    {
        attackChargingPro.gameObject.SetActive(false);
        attackRangePro.gameObject.SetActive(false);
        effect.SetActive(false);

        attackChargingPro.orthographicSize = 0.0f;
        attackRangePro.orthographicSize = attackRange;
        orSizePerSec = (attackRange / activeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackChargingPro.gameObject.activeSelf && attackRangePro.gameObject.activeSelf)
        {
            attackChargingPro.orthographicSize += orSizePerSec * Time.deltaTime;

            if (attackChargingPro.orthographicSize >= attackRange)
            {
                effect.SetActive(true);
                attackChargingPro.orthographicSize = 0.0f;
                attackChargingPro.gameObject.SetActive(false);
                attackRangePro.gameObject.SetActive(false);
            }
        }
    }

    public override void ExcuteSkill()
    {
        Debug.Log("ShockWave");
        effect.SetActive(false);
        attackChargingPro.gameObject.SetActive(true);
        attackRangePro.gameObject.SetActive(true);
    }
}

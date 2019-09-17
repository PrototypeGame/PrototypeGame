using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : MonoBehaviour, IEffectItemBase
{
    private Transform target;

    private Renderer itemRender;
    private Collider itemCollider;

    public float speedMultiple;
    public float remainSecond;

    public void Awake()
    {
        itemRender = GetComponent<Renderer>();
        itemCollider = GetComponent<Collider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            target = other.transform;

            ItemEffects();
        }
    }

    public void ItemEffects()
    {
        target.gameObject.GetComponent<PlayerControl>().moveSpeed *= speedMultiple;

        StartCoroutine("ReleaseEffects");
    }

    public IEnumerator ReleaseEffects()
    {
        itemRender.enabled = false;
        itemCollider.enabled = false;

        yield return new WaitForSeconds(remainSecond);
        target.gameObject.GetComponent<PlayerControl>().moveSpeed /= speedMultiple;

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tree : MonoBehaviour, IDamageable
{
    [SerializeField] string _damageCalloutMessage = "-99999";
    [SerializeField] float _calloutOffsetY = 2f;
    public UnityEvent Attacked;
    public GameObject TreeCat;
    public GameObject Cat;

    public void TakeDamage()
    {
        // do whatever needs to happen on death
        //Attacked?.Invoke();
        StartCoroutine(TakingUpdate());

        MasterUI.Instance.DamageCallout(transform.position + new Vector3(0, _calloutOffsetY, 0), _damageCalloutMessage);
    }

    IEnumerator TakingUpdate()
    {
        foreach (var obj in gameObject.transform.GetComponentsInChildren<Collider>())
        {
            obj.enabled = false;
        }
        foreach (var obj in gameObject.transform.GetComponentsInChildren<MeshRenderer>())
        {
            obj.enabled = false;
        }

        float timeCount = 0.0f;
        while (TreeCat.transform.rotation != Cat.transform.rotation || TreeCat.transform.position != Cat.transform.position)
        {
            timeCount = timeCount + Time.deltaTime;
            TreeCat.transform.rotation = Quaternion.Slerp(TreeCat.transform.rotation, Cat.transform.rotation, timeCount * 0.2f);
            TreeCat.transform.position = Vector3.MoveTowards(TreeCat.transform.position, Cat.transform.position, Time.deltaTime * 8);

           
            yield return null;
        }
        TreeCat.SetActive(false);
        Cat.SetActive(true);
        Destroy(gameObject);
    }
}

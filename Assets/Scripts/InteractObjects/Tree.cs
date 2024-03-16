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
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        float timeCount = 0.0f;
        while (TreeCat.transform.rotation != Cat.transform.rotation || TreeCat.transform.position != Cat.transform.position)
        {
            timeCount = timeCount + Time.deltaTime;
            transform.rotation = Quaternion.Slerp(TreeCat.transform.rotation, Cat.transform.rotation, timeCount * 0.1f);
            TreeCat.transform.position = Vector3.MoveTowards(TreeCat.transform.position, Cat.transform.position, Time.deltaTime * 6);

           
            yield return null;
        }
        Cat.SetActive(true);
        Destroy(gameObject);
    }
}

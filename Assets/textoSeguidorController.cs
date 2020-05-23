using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textoSeguidorController : MonoBehaviour
{
    public string texto;
    public Transform FollowThis;

    void Start()
    {
        this.GetComponent<Text>().text = texto;
    }

    void Update()
    {
        if (FollowThis == null)
        {
            return;
        }

        Vector2 sp = Camera.main.WorldToScreenPoint(FollowThis.position);

        this.transform.position = sp;
    }

    void OnEnable()
    {
        // this is here because there can be a single frame where the position is incorrect
        // when the object (or its parent) is activated.
        if (gameObject.activeInHierarchy)
        {
            Update();
        }
    }
}

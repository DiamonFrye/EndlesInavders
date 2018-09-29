﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightColliderScript : MonoBehaviour {

    private GameObject pObject;
    // Use this for initialization
    void Start()
    {
        pObject = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            pObject.GetComponent<EnemyMove>().rightCollision = true;
//            Debug.Log(this.name + " Right collision");
        }
    }
}

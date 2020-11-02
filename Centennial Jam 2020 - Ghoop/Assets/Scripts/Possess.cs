﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possess : MonoBehaviour
{
    public GameObject ghost;
    public GameObject Corpse;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Corpse")
        {
            Debug.Log("collision");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E");
                possessCorpse();

            }
        }
    }


    public void possessCorpse()
    {
        this.GetComponent<Collider2D>().isTrigger = true;
        this.GetComponent<playerController>().enabled = false;
        this.gameObject.tag = "Corpse";
        GameObject ghostOb = Instantiate(ghost, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        ghostOb.transform.parent = null;
        Camera.main.GetComponent<CameraFollower>().target = ghostOb.transform;
    }
}
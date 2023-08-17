using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.0f;
    void Start()
    {
        //Managers mg = Managers.GetInstance();
        //Debug.Log("mg : " +  mg);
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(h, 0, v).normalized;
        transform.Translate(movement * speed * Time.deltaTime);
    }
}

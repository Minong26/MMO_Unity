using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.0f;
    private float yAngle = 0;

    void Start()
    {
        //Managers mg = Managers.GetInstance();
        //Debug.Log("mg : " +  mg);

        Managers.Input.KeyAction -= OnKeyBoardInput;
        Managers.Input.KeyAction += OnKeyBoardInput;
    }

    private void Update()
    {
        //yAngle = Time.deltaTime * speed;
        //transform.eulerAngles = new Vector3 (0, yAngle, 0);
        //transform.rotation = Quaternion.Euler(new Vector3(0, yAngle, 0));
        //transform.Rotate(new Vector3(0, yAngle, 0));

        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(h, 0, v).normalized;
        //transform.Translate(movement * speed * Time.deltaTime);
    }

    void OnKeyBoardInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), speed);
            transform.position += Vector3.forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.back);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), speed);
            transform.position += Vector3.back * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.left);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), speed);
            transform.position += Vector3.left * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.right);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), speed);
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
    }
}

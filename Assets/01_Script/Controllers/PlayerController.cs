using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;

    private bool _moveToDest = false;
    private Vector3 _destPos;
    private float wait_n_run_ratio = 0;

    void Start()
    {
        //Managers.Input.KeyAction -= OnKeyBoardInput;
        //Managers.Input.KeyAction += OnKeyBoardInput;

        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;  //구독 신청
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
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

        if (_moveToDest)
        {
            Vector3 dir = _destPos - transform.position;
            if (dir.magnitude < 0.0001f)
            {
                _moveToDest = false;
            }
            else
            {
                float mouseDist = Mathf.Clamp(speed * Time.deltaTime, 0, dir.magnitude);
                transform.position += dir.normalized * mouseDist;
                wait_n_run_ratio = Mathf.Lerp(wait_n_run_ratio, 1, 5 * Time.deltaTime);
                Animator anim = GetComponent<Animator>();
                anim.SetFloat("Wait_n_Run_ratio", wait_n_run_ratio);
                anim.Play("Wait_n_Run_blend");

                if (dir.magnitude > .01f)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 30 * Time.deltaTime);
                }
            }
        }else
        {
            wait_n_run_ratio = Mathf.Lerp(wait_n_run_ratio, 0, 5 * Time.deltaTime);
            Animator anim = GetComponent<Animator>();
            anim.SetFloat("Wait_n_Run_ratio", wait_n_run_ratio);
            anim.Play("Wait_n_Run_blend");
        }
    }

    private void OnMouseClicked(Define.MouseEvent obj)
    {
        if (obj != Define.MouseEvent.Click)
            return;

        Debug.Log("OnMouseClicked !");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _moveToDest = true;
        }
    }

    private void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += Vector3.forward * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += Vector3.back * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += Vector3.right * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += Vector3.left * Time.deltaTime * speed;
        }

        _moveToDest = false;
    }

    //void OnKeyBoardInput()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        //transform.rotation = Quaternion.LookRotation(Vector3.forward);
    //        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), speed);
    //        this.transform.position += Vector3.forward * Time.deltaTime * speed;
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        //transform.rotation = Quaternion.LookRotation(Vector3.back);
    //        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), speed);
    //        this.transform.position += Vector3.back * Time.deltaTime * speed;
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        //transform.rotation = Quaternion.LookRotation(Vector3.left);
    //        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), speed);
    //        this.transform.position += Vector3.left * Time.deltaTime * speed;
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        //transform.rotation = Quaternion.LookRotation(Vector3.right);
    //        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), speed);
    //        this.transform.position += Vector3.right * Time.deltaTime * speed;
    //    }
    //}
}

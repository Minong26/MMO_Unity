using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;

    private Vector3 _destPos;
    private float wait_n_run_ratio = 0;

    void Start()
    {
        //Managers.Input.KeyAction -= OnKeyboard;
        //Managers.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    private enum PlayerState
    {
        Die,
        Idle,
        Running
    }

    private PlayerState _state = PlayerState.Idle;
    private void Update()
    {
        switch (_state)
        {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Running:
                UpdateRunning();
                break;
            case PlayerState.Die:
                UpdateDie();
                break;
        }
    }

    private void OnMouseClicked(Define.MouseEvent obj)
    {
        if (_state == PlayerState.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1.0f);

        RaycastHit hit;
        if (Input.GetMouseButtonUp(0) && Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _state = PlayerState.Running;
        }
    }

    //private void OnKeyboard()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
    //        transform.position += Vector3.forward * Time.deltaTime * speed;
    //    }

    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
    //        transform.position += Vector3.back * Time.deltaTime * speed;
    //    }

    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
    //        transform.position += Vector3.right * Time.deltaTime * speed;
    //    }

    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
    //        transform.position += Vector3.left * Time.deltaTime * speed;
    //    }
    //}

    private void UpdateIdle()
    {
        wait_n_run_ratio = Mathf.Lerp(wait_n_run_ratio, 0, 5 * Time.deltaTime);
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("Speed_f", 0);
    }
    private void UpdateRunning()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
            return;
        }

        float mouseDist = Mathf.Clamp(speed * Time.deltaTime, 0, dir.magnitude);
        transform.position += dir.normalized * mouseDist;

        if (dir.magnitude > .01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 30 * Time.deltaTime);
        }

        //Play Animation
        wait_n_run_ratio = Mathf.Lerp(wait_n_run_ratio, 1, 5 * Time.deltaTime);
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("Speed_f", speed);
    }
    private void UpdateDie()
    {
        Debug.Log("You Died");
    }
}

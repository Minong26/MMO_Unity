using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private PlayerStat _stat;
    private Vector3 _destPos;

    //private float wait_n_run_ratio = 0;

    void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
    }

    private enum PlayerState
    {
        Die,
        Idle,
        Moving,
        Skill
    }

    [SerializeField]
    private PlayerState state = PlayerState.Idle;
    PlayerState State
    {
        get { return state; }
        set
        {
            state = value;

            Animator anim = GetComponent<Animator>();
            switch (State)
            {
                case PlayerState.Idle:
                    anim.CrossFade("Wait", .1f);
                    break;
                case PlayerState.Moving:
                    anim.CrossFade("Run", .1f);
                    break;
                case PlayerState.Skill:
                    anim.CrossFade("Attack", .1f, -1, 0);
                    break;
                case PlayerState.Die:
                    break;
            }
        }
    }

    private void Update()
    {
        switch (State)
        {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Skill:
                UpdateSkill();
                break;
            case PlayerState.Die:
                UpdateDie();
                break;
        }
    }

    private int _mask = (1 << (int)Define.Layer.Ground | (1 << (int)Define.Layer.Monster));
    private GameObject _lockTarget;
    private bool _stopSkill = false;
    private void OnMouseEvent(Define.MouseEvent evt)
    {
        if (State == PlayerState.Die)
            return;

        switch (State)
        {
            case PlayerState.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Skill:
                if (evt == Define.MouseEvent.PointerUp)
                    _stopSkill = true;
                break;
        }
    }

    private void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100, _mask);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                if (raycastHit)
                {
                    _destPos = hit.point;
                    State = PlayerState.Moving;

                    if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                        _lockTarget = hit.collider.gameObject;
                    else
                        _lockTarget = null;
                }
                break;
            case Define.MouseEvent.Press:
                if (_lockTarget == null && raycastHit)
                    _destPos = hit.point;
                break;
        }
    }

    private void UpdateIdle()
    {

    }
    private void UpdateMoving()
    {
        if (_lockTarget != null)
        {
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= 1)
            {
                State = PlayerState.Skill;
            }
        }

        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = PlayerState.Idle;
            return;
        }

        float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
        NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
        nma.Move(dir.normalized * moveDist);

        Debug.DrawRay(transform.position + Vector3.up * .5f, dir.normalized, Color.green);
        if (Physics.Raycast(transform.position + Vector3.up * .5f, dir, 1.0f, LayerMask.GetMask("Blcok")))
        {
            if (Input.GetMouseButton(0) == false)
                State = PlayerState.Idle;
            return;
        }

        if (dir.magnitude > .01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 30 * Time.deltaTime);
        }

        //Play Animation
        //wait_n_run_ratio = Mathf.Lerp(wait_n_run_ratio, 1, 5 * Time.deltaTime);
    }
    private void UpdateSkill()
    {

    }
    private void OnHitEvent()
    {
        if (_stopSkill)
            State = PlayerState.Idle;
        else
            State = PlayerState.Skill;
    }
    private void UpdateDie()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("Attack_b", true);
    }
}
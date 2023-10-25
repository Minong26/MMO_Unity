using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    private PlayerStat _stat;

    void Start()
    {
        WorldObjectType = Define.WorldObject.Player;
        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
        Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
        Managers.UI.MakeWorldSpaceUI<UI_Level>(transform);
    }

    private int _mask = (1 << (int)Define.Layer.Ground | (1 << (int)Define.Layer.Monster));
    private bool _stopSkill = false;
    private void OnMouseEvent(Define.MouseEvent evt)
    {
        if (State == Define.State.Die)
            return;

        switch (State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Skill:
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
                    State = Define.State.Moving;

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

    protected override void UpdateMoving()
    {
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= 1)
            {
                State = Define.State.Skill;
            }
        }

        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
            return;
        }

        Debug.DrawRay(transform.position + Vector3.up * .5f, dir.normalized, Color.green);
        if (Physics.Raycast(transform.position + Vector3.up * .5f, dir, 1.0f, LayerMask.GetMask("Blcok")))
        {
            if (Input.GetMouseButton(0) == false)
                State = Define.State.Idle;
            return;
        }

        float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
        transform.position += dir.normalized * moveDist;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 50 * Time.deltaTime);
    }
    protected override void UpdateSkill()
    {
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }
    private void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat);
        }

        if (_stopSkill)
            State = Define.State.Idle;
        else
        {
            State = Define.State.Skill;
            Managers.Sound.Play("Sounds/Unity-Chan Voice/univ0001");
        }
    }
}
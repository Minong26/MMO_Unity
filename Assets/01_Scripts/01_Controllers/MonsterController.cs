using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    private Stat _stat;
    private float _scanRange = 5.0f;
    private float _attackRange = 2.0f;

    private Vector3 originPos;

    void Start()
    {
        WorldObjectType = Define.WorldObject.Monster;
        _stat = gameObject.GetComponent<Stat>();
        originPos = gameObject.transform.position;

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
        if (gameObject.GetComponentInChildren<UI_Level>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_Level>(transform);
    }

    protected override void UpdateIdle()
    {
        GameObject player = Managers.Game.GetPlayer();
        if (player == null)
            return;

        float distance = (player.transform.position - transform.position).magnitude;
        if (distance <= _scanRange)
        {
            _lockTarget = player;
            State = Define.State.Moving;
            return;
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
                NavMeshAgent nma2 = gameObject.GetOrAddComponent<UnityEngine.AI.NavMeshAgent>();
                nma2.SetDestination(transform.position);
                State = Define.State.Skill;
                return;
            }
        }

        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f || dir.magnitude > _scanRange)
        {
            State = Define.State.Idle;
            return;
        }

        float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
        NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
        nma.Move(dir.normalized * moveDist);

        Debug.DrawRay(transform.position + Vector3.up * .5f, dir.normalized, Color.green);
        if (Physics.Raycast(transform.position + Vector3.up * .5f, dir, 1.0f, LayerMask.GetMask("Blcok")))
        {
            if (Input.GetMouseButton(0) == false)
                State = Define.State.Idle;
            return;
        }

        if (dir.magnitude > .01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 30 * Time.deltaTime);
        }
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

    protected override void UpdateDie()
    {
        Debug.Log($"{gameObject.name} Die");
    }

    void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat);

            if (targetStat.HP > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _attackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving;
            }
            else
                State = Define.State.Idle;
        }
        else
            State = Define.State.Idle;
    }
}

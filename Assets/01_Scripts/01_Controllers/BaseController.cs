using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] protected Vector3 _destPos;
    [SerializeField] protected GameObject _lockTarget;

    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

    [SerializeField] protected Define.State state = Define.State.Idle;
    public virtual Define.State State
    {
        get { return state; }
        set
        {
            state = value;

            Animator anim = GetComponent<Animator>();
            switch (State)
            {
                case Define.State.Idle:
                    anim.CrossFade("Wait", .1f);
                    break;
                case Define.State.Moving:
                    anim.CrossFade("Run", .1f);
                    break;
                case Define.State.Skill:
                    anim.CrossFade("Attack", .1f, -1, 0);
                    break;
                case Define.State.Die:
                    break;
            }
        }
    }
    private void Update()
    {
        switch (State)
        {
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Moving:
                UpdateMoving();
                break;
            case Define.State.Skill:
                UpdateSkill();
                break;
            case Define.State.Die:
                UpdateDie();
                break;
        }
    }


    protected virtual void UpdateIdle() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateSkill() { }
    protected virtual void UpdateDie() { }
}
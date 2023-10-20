using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Define.CameraMode _mode = Define.CameraMode.QuaterView;
    [SerializeField] private Vector3 _delta;
    [SerializeField] private GameObject _player;

    private void LateUpdate()
    {
        if (_mode == Define.CameraMode.QuaterView)
        {
            if (_player.isValid() == false)
                return;

            RaycastHit hit;
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Block")))
            {
                float dist = (hit.point - _player.transform.position).magnitude * .8f;
                transform.position = Vector3.Slerp(transform.position, _player.transform.position + _delta.normalized * dist, .08f);
            }else
            {
                transform.position = _player.transform.position + _delta;
                //transform.position = Vector3.Slerp(transform.position, _player.transform.position + _delta, .1f);
                transform.LookAt(_player.transform.position + new Vector3(0, 2f, 0));
            }
        }
    }

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuaterView;
        _delta = delta;
    }
}

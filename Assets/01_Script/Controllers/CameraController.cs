using System.Collections;
using System.Collections.Generic;
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
            RaycastHit hit;
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
            {
                float dist = (hit.point - _player.transform.position).magnitude * .8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }else
            {
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform.position + new Vector3(0, 2f, 0));
            }
        }
    }

    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuaterView;
        _delta = delta;
    }
}

using UnityEngine;

public class TestSound : MonoBehaviour
{
    private int i = 0;

    private void OnTriggerEnter(Collider other)
    {
        i++;

        if (i % 2 == 0)
            Managers.Sound.Play("univ0001", Define.Sound.Bgm);
        else
            Managers.Sound.Play("univ0002", Define.Sound.Bgm);
    }
}

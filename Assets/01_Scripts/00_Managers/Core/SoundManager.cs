using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private AudioSource[] _audioSource = new AudioSource[(int)Define.Sound.MaxCount];
    private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");

        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundName = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundName.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundName[i] };
                _audioSource[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSource[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect,  float pitch = 1.0f)
    {
        if (audioClip == null)
        {
            return;
        }

        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSource[(int)Define.Sound.Effect];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSource[(int)(Define.Sound.Effect)];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    AudioClip GetOrAddAudioClip(string path, Define.Sound type)
    {
        AudioClip audioClip = null;
        if (type == Define.Sound.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path , audioClip);
            }
        }
        if (audioClip == null)
            Debug.Log($"AudioClip Missing from {path}");

        return audioClip;
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSource)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }
}

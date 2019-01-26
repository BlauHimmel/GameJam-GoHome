using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private Dictionary<string, int> AudioDictionary = new Dictionary<string, int>();

    private const int MaxAudioCount = 10;
    private const string ResourcePath = "Audio/";
    private const string StreamingAssetsPath = "";
    private AudioSource BGMAudioSource;
    private AudioSource LastAudioSource;

    public void Init()
    {

    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioName">音效名</param>
    /// <param name="volume">音量</param>
    public void SoundPlay(string audioName, float volume = 1)
    {
        if (AudioDictionary.ContainsKey(audioName))
        {
            if (AudioDictionary[audioName] <= MaxAudioCount)
            {
                AudioClip sound = this.GetAudioClip(audioName);
                if(sound != null)
                {
                    StartCoroutine(this.PlayClipEnd(sound, audioName));
                    this.PlayClip(sound, volume);
                    AudioDictionary[audioName]++;
                }
            }
        }
        else
        {
            AudioDictionary.Add(audioName, 1);
            AudioClip sound = this.GetAudioClip(audioName);
            if(sound != null)
            {
                StartCoroutine(this.PlayClipEnd(sound, audioName));
                this.PlayClip(sound, volume);
                AudioDictionary[audioName]++;
            }
        }
    }
    /// <summary>
    /// 暂停音效
    /// </summary>
    /// <param name="audioName">音效名</param>
    public void SoundPause(string audioName)
    {
        if (this.LastAudioSource != null)
        {
            this.LastAudioSource.Pause();
        }
    }
    /// <summary>
    ///  暂停所有音乐音效
    /// </summary>
    public void SoundAllPause()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        if (audioSources != null && audioSources.Length > 0)
        {
            foreach(AudioSource audio in audioSources)
            {
                audio.Pause();
            }
        }
    }
    /// <summary>
    /// 停止指定音效
    /// </summary>
    /// <param name="audioName">音效名</param>
    public void SoundStop(string audioName)
    {
        GameObject go = this.transform.Find(audioName).gameObject;
        if (go != null)
        {
            Destroy(go);
        }
    }
    /// <summary>
    /// 设置背景音乐音量
    /// </summary>
    /// <param name="volume"></param>
    public void BGMSetVolume(float volume)
    {
        if (this.BGMAudioSource != null)
        {
            this.BGMAudioSource.volume = volume;
        }
    }
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="bgmName"></param>
    /// <param name="volume"></param>
    public void BGMPlay(string bgmName, float volume = 1f)
    {
        BGMStop();

        if (bgmName != null)
        {
            AudioClip bgmSound = this.GetAudioClip(bgmName);
            if (bgmSound != null)
            {
                this.PlayLoopBGMAudioClip(bgmSound, volume);
            }
        }
    }
    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void BGMPause()
    {
        if (this.BGMAudioSource != null)
        {
            this.BGMAudioSource.Pause();
        }
    }
    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void BGMStop()
    {
        if (this.BGMAudioSource != null && this.BGMAudioSource.gameObject)
        {
            Destroy(this.BGMAudioSource.gameObject);
            this.BGMAudioSource = null;
        }
    }
    /// <summary>
    /// 重新播放背景音乐
    /// </summary>
    public void BGMReplay()
    {
        if (this.BGMAudioSource != null)
        {
            this.BGMAudioSource.Play();
        }
    }

    #region 背景音乐
    /// <summary>
    /// 背景音乐控制器
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="volume"></param>
    /// <param name="isLoop"></param>
    /// <param name="name"></param>
    private void PlayBGMAudioClip(AudioClip audioClip, float volume = 1f, bool isLoop = false, string name = null)
    {
        if (audioClip==null)
        {
            return;
        }
        else
        {
            GameObject go = new GameObject(name);
            go.transform.parent = this.transform;
            AudioSource loopClip = go.AddComponent<AudioSource>();
            loopClip.clip = audioClip;
            loopClip.volume = volume;
            loopClip.loop = true;
            loopClip.pitch = 1f;
            loopClip.Play();
            this.BGMAudioSource = loopClip;
        }
    }
    /// <summary>
    /// 播放一次的背景音乐
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="volume"></param>
    /// <param name="name"></param>
    private void PlayOnceBGMAudioClip(AudioClip audioClip, float volume = 1f, string name = null)
    {
        PlayBGMAudioClip(audioClip, volume, false, name == null ? "BGMSound" : name);
    }
    /// <summary>
    /// 循环播放的背景音乐
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="volume"></param>
    /// <param name="name"></param>
    private void PlayLoopBGMAudioClip(AudioClip audioClip, float volume, string name = null)
    {
        PlayBGMAudioClip(audioClip, volume, true, name == null ? "LoopSound" : name);
    }

    #endregion

    #region  音效
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="volume"></param>
    /// <param name="name"></param>
    private void PlayClip(AudioClip audioClip, float volume = 1f, string name = null)
    {

        if (audioClip == null)
        {
            return;
        }
        else
        {
            GameObject go = new GameObject(name == null ? "SoundClip" : name);
            go.transform.parent = this.transform;
            AudioSource source = go.AddComponent<AudioSource>();
            StartCoroutine(this.PlayClipEndDestroy(audioClip, go));
            source.pitch = 1f;
            source.volume = volume;
            source.clip = audioClip;
            source.Play();
            this.LastAudioSource = source;
        }

    }
    /// <summary>
    /// 播放完音效删除物体
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="go"></param>
    /// <returns></returns>
    private IEnumerator PlayClipEndDestroy(AudioClip audioClip, GameObject go)
    {
        if (go == null||audioClip == null)
        {
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(audioClip.length * Time.timeScale);
            Destroy(go);
        }
    }
    /// <summary>
    /// 音效播放完后处理函数
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="audioName"></param>
    /// <returns></returns>
    private IEnumerator PlayClipEnd(AudioClip audioClip, string audioName)
    {
        if (audioClip != null)
        {
            yield return new WaitForSeconds(audioClip.length * Time.timeScale);
            AudioDictionary[audioName]--;
            if (AudioDictionary[audioName] <= 0)
            {
                AudioDictionary.Remove(audioName);
            }
        }
        yield break;
    }

    #endregion

    #region 音效资源路径

    enum eResType
    {
        AB = 0,
        CLIP = 1
    }

    private AudioClip GetAudioClip(string audioName, eResType type = eResType.CLIP)
    {
        AudioClip audioClip = null;
        switch (type)
        {
            case eResType.AB:
                break;
            case eResType.CLIP:
                audioClip = GetResAudioClip(audioName);
                break;
            default:
                break;
        }
        return audioClip;
    }

    private IEnumerator GetAbAudioClip(string audioName)
    {
        yield return null;
    }

    private AudioClip GetResAudioClip(string audioName)
    {
        return Resources.Load(ResourcePath + audioName) as AudioClip;
    }

    #endregion 
}

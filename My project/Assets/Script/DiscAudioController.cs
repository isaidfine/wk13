using UnityEngine;

public class DiscAudioController : MonoBehaviour
{
    public AudioSource normalAudioSource; // 正常播放的AudioSource
    public AudioSource[] scratchAudioSources; // 搓碟音效的AudioSource数组
    public AudioClip[] scratchClips; // 搓碟音效的数组
    public DiscController discController; // 对DiscController的引用

    private float lastRotationAmount;
    private int currentScratchIndex;
    private int currentAudioSourceIndex;

    void Start()
    {
        normalAudioSource.Play(); // 开始播放正常音频
    }

    void Update()
    {
        if (discController.IsDragging)
        {
            if (!normalAudioSource.isPlaying)
            {
                normalAudioSource.Stop(); // 立即停止正常音频
            }
            
            
            float rotationAmount = Mathf.Abs(discController.currentRotationSpeed * Time.deltaTime);
            lastRotationAmount += rotationAmount;

            if (lastRotationAmount >= 135f)
            {
                PlayScratchSound();
                lastRotationAmount -= 135f; // 重置累积旋转量
            }

            if (!normalAudioSource.isPlaying)
            {
                normalAudioSource.Pause(); // 暂停正常音频
            }
        }
        else
        {
            if (!normalAudioSource.isPlaying)
            {
                normalAudioSource.Play(); // 重新播放正常音频
            }
            lastRotationAmount = 0; // 重置累积旋转量
        }
    }

    void PlayScratchSound()
    {
        scratchAudioSources[currentAudioSourceIndex].clip = scratchClips[currentScratchIndex];
        scratchAudioSources[currentAudioSourceIndex].Play();

        currentScratchIndex = (currentScratchIndex + 1) % scratchClips.Length;
        currentAudioSourceIndex = (currentAudioSourceIndex + 1) % scratchAudioSources.Length;
    }
}
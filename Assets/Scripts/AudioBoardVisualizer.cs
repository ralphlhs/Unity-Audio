using UnityEngine;
using UnityEngine.Audio;

public class AudioBoardVisualizer : MonoBehaviour
{
    //보드 증가량
    public float minBoard = 50.0f;
    public float maxBoard = 500.0f;

    public AudioClip audioClip;
    public AudioSource audioSource;
    public Board[] boards;
    public AudioMixer audioMixer;

    //스펙트럼용 범위
    public int samples = 64;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boards = GetComponentsInChildren<Board>();
        //오브젝트에 연결된 자식들 객체.
        if (audioSource == null)
        {// "AudioSource" 게임오브젝트를 생성하고, 해당 오브젝트에 AudioSource 컴포넌트 추가.
            audioSource = new GameObject("AudioSource").AddComponent<AudioSource>();
        }
        else { //존재하면 씬에서 찾아서 등록
            audioSource = GameObject.Find("AudioSource").AddComponent<AudioSource>();
        }
        audioSource.clip = audioClip;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
        //오디오믹서 그룹 중에서 "Master" 그룹을 찾아 적용합니다. 
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float[] data = audioSource.GetSpectrumData(samples, 0, FFTWindow.Rectangular);
        //GetSpectrumData(samples, channel, FFTWindow);

        //samples = FFT(신호에 대한 주파수 영역)을 지정할 배열, 이 배열 값은 2의 제곱 수로 표현.
        //채널은 최대 8개의 채널을 지원해주고 있음. 일반 적으로는 0을 사용합니다.
        //FFT Window는 샘플링 진행할 때 쓰는 값.

        for (int i = 0; i < boards.Length; i++) { 

            var size = boards[i].GetComponent<RectTransform>().rect.size;

            size.y = minBoard + (data[i] * (maxBoard - minBoard)*5.0f);
            boards[i].GetComponent<RectTransform>().sizeDelta = size;
            //
        }
    }
}

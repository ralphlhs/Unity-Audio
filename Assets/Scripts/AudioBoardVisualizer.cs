using UnityEngine;
using UnityEngine.Audio;

public class AudioBoardVisualizer : MonoBehaviour
{
    //���� ������
    public float minBoard = 50.0f;
    public float maxBoard = 500.0f;

    public AudioClip audioClip;
    public AudioSource audioSource;
    public Board[] boards;
    public AudioMixer audioMixer;

    //����Ʈ���� ����
    public int samples = 64;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boards = GetComponentsInChildren<Board>();
        //������Ʈ�� ����� �ڽĵ� ��ü.
        if (audioSource == null)
        {// "AudioSource" ���ӿ�����Ʈ�� �����ϰ�, �ش� ������Ʈ�� AudioSource ������Ʈ �߰�.
            audioSource = new GameObject("AudioSource").AddComponent<AudioSource>();
        }
        else { //�����ϸ� ������ ã�Ƽ� ���
            audioSource = GameObject.Find("AudioSource").AddComponent<AudioSource>();
        }
        audioSource.clip = audioClip;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
        //������ͼ� �׷� �߿��� "Master" �׷��� ã�� �����մϴ�. 
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float[] data = audioSource.GetSpectrumData(samples, 0, FFTWindow.Rectangular);
        //GetSpectrumData(samples, channel, FFTWindow);

        //samples = FFT(��ȣ�� ���� ���ļ� ����)�� ������ �迭, �� �迭 ���� 2�� ���� ���� ǥ��.
        //ä���� �ִ� 8���� ä���� �������ְ� ����. �Ϲ� �����δ� 0�� ����մϴ�.
        //FFT Window�� ���ø� ������ �� ���� ��.

        for (int i = 0; i < boards.Length; i++) { 

            var size = boards[i].GetComponent<RectTransform>().rect.size;

            size.y = minBoard + (data[i] * (maxBoard - minBoard)*5.0f);
            boards[i].GetComponent<RectTransform>().sizeDelta = size;
            //
        }
    }
}

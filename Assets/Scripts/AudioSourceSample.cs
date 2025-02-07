using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class AudioSourceSample : MonoBehaviour
{
    public AudioSource audioSourceBGM; //0)�ν����Ϳ�
    public AudioSource audioSourceSFX; //3)������ ã�Ƽ� �����ϴ� ���.
    public AudioClip bgm;
    public AudioSource audioSourceResources; //4) Resources Load()����� �̿��� ���ҽ� ������ �ִ� ����� �ҽ��� Ŭ���� 

    //private AudioSource own_audioSource; //1)��ü�� ��ü������ ����� �ҽ��� ������ �ִ� ���.
    void Start()
    {
        //own_audioSource = GetComponent<AudioSource>(); //1)
        //GameObject.Find().GetComponent<AudioSource>().Play(); //)3
        audioSourceSFX.clip = Resources.Load("Explosion") as AudioClip;
        audioSourceSFX = GameObject.Find("SFX").GetComponent<AudioSource>();
        audioSourceBGM.clip = bgm;
        audioSourceSFX.clip = Resources.Load("Audio/BombCharge") as AudioClip;
        //���ҽ� �ε�� ��ΰ� �������ִٸ� ������/ ���ϸ����� �ۼ�.
        //���ҽ� �ε�� �ۼ��ϴ� �̸����� Ȯ���ڸ�(.jpg .avi)�� �ۼ����� �ʴ´�.
        //UnityWebRequest�� GetAudioClip��� Ȱ��.
        StartCoroutine("GetAudioClip");
            
        //����� �ҽ��� Ŭ���� bgm���� �����մϴ�.
        audioSourceBGM.Play();
        //audioSourceBGM.Pause();
        //audioSourceSFX.PlayOneShot(bgm);
        //audioSourceBGM.Stop();
        //audioSourceBGM.UnPause();
        //audioSourceBGM.PlayDelayed(1.0f);
    }
    IEnumerator GetAudioClip()
    {
        UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip("file:///" + Application.dataPath + "/Audio/" + "Explosion" + ".mp4", AudioType.WAV);
        yield return uwr.SendWebRequest();
        var new_clip = DownloadHandlerAudioClip.GetContent(uwr);
        audioSourceBGM.clip = new_clip;
        audioSourceBGM.Play();
    }
    //�̰ɷ� ȣ���� ��� �۾� ������ �� �����.
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class AudioSourceSample : MonoBehaviour
{
    public AudioSource audioSourceBGM; //0)인스펙터엣
    public AudioSource audioSourceSFX; //3)씬에서 찾아서 연결하는 경우.
    public AudioClip bgm;
    public AudioSource audioSourceResources; //4) Resources Load()기능을 이용해 리소스 폴더에 있는 오디오 소스의 클립을 

    //private AudioSource own_audioSource; //1)객체가 자체적으로 오디오 소스를 가지고 있는 경우.
    void Start()
    {
        //own_audioSource = GetComponent<AudioSource>(); //1)
        //GameObject.Find().GetComponent<AudioSource>().Play(); //)3
        audioSourceSFX.clip = Resources.Load("Explosion") as AudioClip;
        audioSourceSFX = GameObject.Find("SFX").GetComponent<AudioSource>();
        audioSourceBGM.clip = bgm;
        audioSourceSFX.clip = Resources.Load("Audio/BombCharge") as AudioClip;
        //리소스 로드시 경로가 정해져있다면 폴더명/ 파일명으로 작성.
        //리소스 로드시 작성하는 이름에는 확장자명(.jpg .avi)을 작성하지 않는다.
        //UnityWebRequest의 GetAudioClip기능 활용.
        StartCoroutine("GetAudioClip");
            
        //오디오 소스의 클립을 bgm으로 설정합니다.
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
    //이걸로 호출할 경우 작업 끝나면 값 사라짐.
    // Update is called once per frame
    void Update()
    {
        
    }
}

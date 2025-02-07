# 2월 6일 수업(H1)


```cs
public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider MasterVolumeSlider;
    [SerializeField] private Slider BGMVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;
    [SerializeField] private AudioSource m_AudioSource;
    private void Awake()
    {
        MasterVolumeSlider.onValueChanged.AddListener(SetMaster);
        BGMVolumeSlider.onValueChanged.AddListener(SetBGM);
        SFXVolumeSlider.onValueChanged.AddListener(SetSFX);
    }

    private void SetSFX(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    private void SetBGM(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    private void SetMaster(float volume)
    {   //오디오 믹서가 가지고 있는 파라미터(Expose)의 수치를 설정하는 기능.
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);

        //자주사용되는 Mathf함수
        //1. Mathf.Deg2Rad 
        // degree(60분법)을 통해 radian(호도법)을 계산하는 코드 -> 각도 계산 코드 PI / 180
        // 2. Mathf.Rad2Deg
        // 라디안 값을 디그리 값으로 바꿔줍니다.
        // 360 / (pi * 2) 1라디안은 약 57도
        // 3. Mathf.Abs() -> 절대값 계산. 
        // 4. Mathf.Atan 아크 탄젠트 값을 계산합니다. 
        // 5. Mathf.Ceil 소숫점 올림 계산.
        // 6. Mathf.Clamp(value, min, max) value를 min과 max 사이의 값으로 고정하는 기능.
        // 7. Mathf.Log10 베이스가 10으로 지정되어 있는 수의 로그를 반환해주는 기능.
        // ex) Debug.Log(Mathf.Log10(100))
        // 이번 예제에서는 오디오 믹서의 최소 - 최대 볼륨 값 때문에 로그 함수가 사용되었습니다. 
        // 최소가 -80, 최대가 0. 
        // 그래서 수치 변경시 Mathf.Log10(슬라이더 수치) * 20); 을 통해 범위를 설정하고 
        // 슬라이더의 최소 값이 0.01일 경우 -80이 1일 경우 0이 계산됩니다.
    }
```
+ 오디오 소스(Audio Source)
 + 씬에서 오디오 클립(Audio Clip)을 재생하는 도구

  + 재생을 하기 위해서는 오디오 리스너나 오디오 믹서를 통해 재생가능

  + 오디오 믹서는 만들어야 함. 리스너는 카메라에 있음.
+ 오디오컴포넌트 프로퍼티 :
 + Audio Resource : 오디오 클립등록
	+ Output :오디오리스너에 직접출력 됩니다. (None)만든 오디오 믹서가 있다면 그 믹서를 통해 출력합니다.	
	+ Mute : 
	+ Bypass Effects :오디오 소스에 적용되어 있는 필터효과를 분리
	+ Bypass Listener Effect : 리스너 효과를 키거나 끄는 기능
	+ Bypass Reverb Zones : 리버브 존을 키거나 끄는 효과
	+ 리버브 존 : 오디오 리스너의 위치에 따라 잔향 효과를 설정하는 도구.
  + play On Awake : 씬이 실행되는 시점에 사운드 재생. 비활성화시 Play() 명령을 통해 재생.

+ Loop : 무한히 도는거.

+ Priority : 오디오 소스의 우선순위 
	+ 0 = 최우선
	+ 128 = 기본
   + 256 = 최하위

+ Volume : 리스너 기준으로 거리 기준 소리에 대한 수치. 
	+ 컴퓨터 내의 소리를 재생하는 여러가지 요소 중 하나를 제어.

+ Pitch : 재생 속도가 빨라지거나 느려질때의 피치 변화량 1은 일반 속도.
	+ 1은 일반 속도
	+ 최대 수치 3

+ Stereo Pan : 소리 재생시 좌우 스피커 간의 소리를 분포르 조절 기능
 + 	-1 : 왼쪽 스피커
 + 	0 : 양쪽 균등
 + 	1 : 오른쪽 스피커

+ Stereo Blend : 0 : 사운드가 거리와 상관없이 일정하게 들어갑니다.
	+ 1 : 사운드가 사운드 트는 도구의 거리에 따라 변화.

+ Reverb Zone mix : 리버브 존에 대한 출력 신호양을 조절.
	+ 0: 영향을 안받는다.
	+ 1 : 오디오 소스와 리버브 존 사이의 신호를 최대치
	+ 1.1 : 10dB 증폭
	+ 리버브 존을 따로 설계해야 하는 상황은? : 동굴에서 소리가 울리는 효과 연출. 건물 등에서 다른 부분을 반해서 울리는 소리에 대한 설정

+ Doppler Level : 거리에 따른 사운드 높낮이표현. 0 : 효과가 없음.
+ Spread : 사운드가 퍼지는 각도 (0 ~360) 
 + 0 : 한점에서 사운드가 나오는 방식
 + 360 : 모든 방향으로 균일하게 퍼지는 방식

+ Rollfoff Mode : 그래프 설정
	+ 1. 로그 그래프 : 가까우면 사운드가 크고, 멀수록 점점 빠르게 사운드가 작아짐.
	+ 2. 선형 그래프 : 거리에 따라 일정하게 사운드가 변화하는 구조
	+ 3. 커스텅 그래프 : 직접 조절하는 영역

+ 오디오 믹서(Audio Mixer)
 + 오디오 소스에 대한 제어, 균형, 조정을 제공하는 도구.

+ 만드는법
 + Create -> Audio -> AudioMixer를 통해 Audio Group를 생성합니다. 

+ 최초 생성시
 + exit play mode :

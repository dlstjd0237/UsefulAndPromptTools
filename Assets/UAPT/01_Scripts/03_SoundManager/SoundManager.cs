using UnityEngine;
using UnityEngine.Audio;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(SoundManager))]
public class SoundManagerEditor : Editor
{
    const string INFO = "\n�����̴� ���� ��\n" +
       "--------------------\n" +
       "Slider Min Value : 0.001\n " +
       "Slider Max Value : 1\n\n" +
        "MixerPath : AudioMixer�� ����ִ� ���� �ּ�\n" +
        "����) Resources/AudioMixer ���� �ȿ��ִ� \n" +
        "      _Mixer �� �������� ������\n" +
        "MixerPath �ȿ��� AudioMixer/_Mixer �� ���ָ� �ȴ�.";

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox(INFO, MessageType.Info);
        base.OnInspectorGUI();
        SoundManager soundManager = (SoundManager)target;
        // [CustomEditor(typeof(SoundManager))] �� �����ִ� target�� ������
        if (GUILayout.Button("���� ������ ����"))
        {
            soundManager.DeleteSoundData();
        }
    }


}

#endif
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private string _mixerPath;
    private AudioMixer _mixer;
    public AudioMixer Mixer
    {
        get
        {
            if (_mixer == null)
            {
                _mixer = Resources.Load<AudioMixer>(_mixerPath);
            }
            return _mixer;
        }
    }
    private void OnEnable()
    {
        if (_mixer == null)
            _mixer = Resources.Load<AudioMixer>(_mixerPath);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            VolumeSetMaster(0.3f);
        }
    }
    public void DeleteSoundData()
    {
        PlayerPrefs.DeleteKey("MasterVolume");
        PlayerPrefs.DeleteKey("MusicVolume");
        PlayerPrefs.DeleteKey("SFXVoluem");
#if UNITY_EDITOR
        Debug.Log("���� ���� ������ ���� ��");
#endif
    }

    public void VolumeSetMaster(float volume)
    {
        _mixer.SetFloat("Master", Mathf.Log10(volume) * 20); PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void VolumeSetMusic(float volume)
    {
        _mixer.SetFloat("Music", Mathf.Log10(volume) * 20); PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void VolumeSetSFX(float volume)
    {
        _mixer.SetFloat("SFX", Mathf.Log10(volume) * 20); PlayerPrefs.SetFloat("SFXVoluem", volume);
    }
}

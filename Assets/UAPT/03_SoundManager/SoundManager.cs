using UnityEngine;
using UnityEngine.Audio;

#region ���� ������ ����
#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(SoundManager))]
public class SoundManagerEditor : Editor
{
    const string INFO = "�����̴� ���� ��\n" +
       "--------------------\n" +
       "Slider Min Value : 0.001\n" +
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
#endregion
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private string _mixerPath;
    private AudioMixer _mixer;
    public AudioMixer Mixer
    {
        get
        {
            MixerNullChake();
            return _mixer;
        }
    }
    private void OnEnable()
    {
        MixerNullChake();
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
#region ���� �׽�Ʈ
#if UNITY_EDITOR

        Debug.Log("���� ���� ������ ���� ��");
#endif
#endregion
    }

    public void VolumeSetMaster(float volume)
    {
        MixerNullChake();
        _mixer.SetFloat("Master", Mathf.Log10(volume) * 20); PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void VolumeSetMusic(float volume)
    {
        MixerNullChake();
        _mixer.SetFloat("Music", Mathf.Log10(volume) * 20); PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void VolumeSetSFX(float volume)
    {
        MixerNullChake();
        _mixer.SetFloat("SFX", Mathf.Log10(volume) * 20); PlayerPrefs.SetFloat("SFXVoluem", volume);
    }

    /// <summary>
    /// Mixer�� Null�ΰ�� ������
    /// </summary>
    private void MixerNullChake()
    {
        if (_mixer != null)
            return;
        _mixer = Resources.Load<AudioMixer>(_mixerPath);
    }
}

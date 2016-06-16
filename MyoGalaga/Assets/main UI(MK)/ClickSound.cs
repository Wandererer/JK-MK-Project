using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

 [RequireComponent(typeof(Button))]

public class ClickSound : MonoBehaviour,IPointerEnterHandler
{
    public AudioClip sound_onMouseOver;
    public AudioClip sound_onMouseClick;

    private Button button { get { return GetComponent<Button>(); } }

    private AudioSource source_onMouseOver { get { return GetComponent<AudioSource>(); } }
    private AudioSource source_onMouseClick { get { return GetComponent<AudioSource>(); } }

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<AudioSource>();

        source_onMouseOver.clip = sound_onMouseOver;
        source_onMouseOver.playOnAwake = false;

        source_onMouseClick.clip = sound_onMouseClick;
        source_onMouseClick.playOnAwake = false;
        
        // 버튼 클릭 시 사운드 재생
        button.onClick.AddListener(() => PlaySound_onClick());
	}

    // 마우스 포인터가 올려져 있으면 사운드 재생
    public void OnPointerEnter(PointerEventData ped)
    {
        PlaySound_onMounseOver();
    }


    void PlaySound_onMounseOver()
    {
        source_onMouseOver.PlayOneShot(sound_onMouseOver);
    }

    void PlaySound_onClick()
    {
        source_onMouseClick.PlayOneShot(sound_onMouseClick);
    }
}

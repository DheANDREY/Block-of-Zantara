using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimateDescButton : MonoBehaviour
{
    public GameObject button;
    public GameObject Desc;

    private SoundEffect sfx;

    private void Start()
    {
        sfx = GameObject.FindWithTag("SFX").GetComponent<SoundEffect>();
    }
    public void playAnimButton()
    {
        sfx.PlaySFX_OpDesc();
        button.transform.DORotate(new Vector3(180, 0, -90),0.5f);
        Desc.SetActive(true);
        StartCoroutine(disableDesc(4f));
    }
    private IEnumerator disableDesc(float delay)
    {
        yield return new WaitForSeconds(delay);
        Desc.SetActive(false);
        button.transform.DORotate(new Vector3(0, 0, -90), 0.5f);
    }
}

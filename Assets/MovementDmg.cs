using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovementDmg : MonoBehaviour
{
    public Transform[] animDmg;
    public GameObject prefabObj;
    // Start is called before the first frame update
    void Start()
    {
        //animDmg.DOMoveX(6.5f, .75f).SetEase(Ease.InBack).OnComplete(() => Destroy(prefabObj,0.25f));
        ActivateSelectedEffect(_effectSkill);
    }

    private void VFXDirectD()
    {
        animDmg[0].DOMoveX(6.5f, .75f).SetEase(Ease.InBack).OnComplete(() => Destroy(prefabObj, 0.25f));
    }
    private void VFXLowestD()
    {
        animDmg[0].DOMoveX(9f, 1f).SetEase(Ease.InBack).OnComplete(() => Destroy(prefabObj, 0.25f));
        animDmg[1].DOMoveX(0f, 1f).SetEase(Ease.InBack).OnComplete(() => Destroy(prefabObj, 0.25f));
    }

    [SerializeField]private EffectSkill _effectSkill;
    public enum EffectSkill
    {
        DirectDmg,
        LowestDmg
    }
    public void ActivateSelectedEffect(EffectSkill skillName)
    {
        switch (skillName)
        {
            case EffectSkill.DirectDmg:
                VFXDirectD();
                break;
            case EffectSkill.LowestDmg:
                VFXLowestD();
                break;
        }
    }
}

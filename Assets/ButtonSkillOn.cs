using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSkillOn : MonoBehaviour
{
    public GameObject buttonSkill;
    public ManaCharManager MCM;

    private void Update()
    {
        if (MCM.isButtonSkillOn)
        {
            ButtonSkillStatus(true);
        }
        else
        {
            ButtonSkillStatus(false);
        }
    }
    public void ButtonSkillStatus(bool isOn)
    {
        if (isOn)
        {
            buttonSkill.SetActive(true);
        }
        else
        {
            buttonSkill.SetActive(false);
        }
    }
}

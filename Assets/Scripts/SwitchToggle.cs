using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    private static readonly int Normal = Animator.StringToHash("Normal");
    private static readonly int Toggled = Animator.StringToHash("Toggled");
    private static readonly int IsPressed = Animator.StringToHash("IsPressed");

    public void OnValueChanged()
    {
        var toggle = gameObject.GetComponent<Toggle>();
        var anim = gameObject.GetComponent<Animator>();
        anim.SetBool(IsPressed, toggle.isOn);
        anim.SetTrigger(toggle.isOn ? Toggled : Normal);
    }
}
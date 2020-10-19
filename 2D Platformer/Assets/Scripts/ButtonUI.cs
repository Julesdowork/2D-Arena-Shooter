using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] string buttonHoverSfx = "ButtonHover";
    [SerializeField] string buttonPressSfx = "ButtonPress";

    public void OnMouseOver()
    {
        AudioManager.instance.PlaySound(buttonHoverSfx);
    }

    public void OnMouseDown()
    {
        AudioManager.instance.PlaySound(buttonPressSfx);
    }
}

using UnityEngine;
using FMODUnity;
public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance;
    [Header("Menu SFX")]
    public EventReference menuHover;
    public EventReference menuClick;
    [Header("Player SFX")]
    public EventReference playerFootstep;
    public EventReference playerJump;
    public EventReference playerFall;


    private void Awake()
    {
        Instance = this;
    }
}
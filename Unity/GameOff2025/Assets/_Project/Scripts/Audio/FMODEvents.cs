using UnityEngine;
using FMODUnity;
public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance;

    public EventReference menuHover;
    public EventReference menuClick;
    public EventReference playerFootstep;


    private void Awake()
    {
        Instance = this;
    }
}
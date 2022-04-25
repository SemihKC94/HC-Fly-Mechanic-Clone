/*//////////////////////////////////////////////////////////////////////////////////////////
//      █─▄▄▄▄█▄─█─▄█─▄▄▄─█                                                               //
//      █▄▄▄▄─██─▄▀██─███▀█             Scripts created by Semih Kubilay Çetin            //
//      ▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▄▄▀                                                               //
//////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Runner Mechanic", menuName ="SKC/Runner Mechanic Data")]
public class SKC_RunnerMechanic : ScriptableObject
{
    [Header("Movement Config")]
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float sideSpeed = 5f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private Vector2 xClamp = Vector2.one;

    [Space, Header("Animator Config")]
    [SerializeField] private string idleAnimName = "Idle";
    [SerializeField] private string runAnimName = "Run";
    [SerializeField] private string jumpAnimName = "Jump";
    [SerializeField] private string flyAnimName = "Fly";
    [SerializeField] private string fallAnimName = "Fall";
    [SerializeField] private string victoryAnimName = "Victory";
    [SerializeField] private string lossAnimName = "Loss";

    // Accessibles
    public float ForwardSpeed { get { return this.forwardSpeed; } }
    public float SideSpeed { get { return this.sideSpeed; } }
    public float JumpForce { get { return this.jumpForce; } }
    public Vector2 XClamp { get { return this.xClamp; } }
    public string IdleAnim { get { return this.idleAnimName; } }
    public string RunAnim { get { return this.runAnimName; } }
    public string JumpAnim { get { return this.jumpAnimName; } }
    public string FlyAnim { get { return this.flyAnimName; } }
    public string FallAnim { get { return this.fallAnimName; } }
    public string VictoryAnim { get { return this.victoryAnimName; } }
    public string LossAnim { get { return this.lossAnimName; } }
}
/* Tip    #if UNITY_EDITOR
          Debug.Log("Unity Editor");
          #endif                          Tip End */
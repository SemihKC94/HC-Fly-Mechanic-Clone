/*//////////////////////////////////////////////////////////////////////////////////////////
//      █─▄▄▄▄█▄─█─▄█─▄▄▄─█                                                               //
//      █▄▄▄▄─██─▄▀██─███▀█             Scripts created by Semih Kubilay Çetin            //
//      ▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▄▄▀                                                               //
//////////////////////////////////////////////////////////////////////////////////////////*/
using UnityEngine;

public class CamController : MonoBehaviour
{
    [Header("Camera Config")]
    [SerializeField] private Vector3 camOffset = Vector3.zero;
    [SerializeField] private float smoothSpeed = .2f;
    [SerializeField] private bool lerpOrDamp = false;
    [SerializeField] private bool LookAtTarget = false;

    // Privates
    private Transform target;

    // Temp
    private Vector3 desiredPos, smoothedPos, velocity;
    private void LateUpdate()
    {
        if (GameManager.GameState == GameState.Play)
        {
            if (target != null)
            {
                desiredPos = target.position + camOffset;

                if (lerpOrDamp)
                {
                    smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
                    transform.position = smoothedPos;
                }
                else
                {
                    smoothedPos = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothSpeed);
                    transform.position = smoothedPos;
                }

                if (LookAtTarget) transform.LookAt(target);
            }
        }
    }
    
    public void Assign(Transform newTarget)
    {
        target = newTarget;
    }

    public void ResetCamera()
    {
        transform.position = camOffset;
    }
}
/* Tip    #if UNITY_EDITOR
          Debug.Log("Unity Editor");
          #endif                          Tip End */
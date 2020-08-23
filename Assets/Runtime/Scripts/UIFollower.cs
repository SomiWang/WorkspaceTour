using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollower : MonoBehaviour
{
    public Vector3 OriginPos;
    private float showUpDist = 5.0f;
    private const float repeatRate = 0.1f;
    UIBehavior ui
    {
        get
        {
            if (_ui == null) _ui = GetComponent<UIBehavior>();
            return _ui;
        }
    }
    UIBehavior _ui;


    private void Awake()
    {
        if (!IsInvoking("LookAtCamera"))
        {
            InvokeRepeating("LookAtCamera", 0, repeatRate);
        }

        if (!IsInvoking("CheckCameraDistance"))
        {
            InvokeRepeating("CheckCameraDistance", 0, repeatRate);
        }
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }

    private void LookAtCamera()
    {
        //transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    private void CheckCameraDistance()
    {
        //if (Vector3.SqrMagnitude(transform.position - Camera.main.transform.position) > Mathf.Sqrt(showUpDist))
        float dist = Vector3.Distance(OriginPos, Camera.main.transform.position);
        //Debug.Log($"{gameObject.name} Distance : {dist}");
        if (dist > showUpDist)
        {
            ui?.Hide();
        }
        else
        {
            ui?.Show();
        }
    }
}

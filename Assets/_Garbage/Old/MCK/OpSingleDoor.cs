using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpSingleDoor : MonoBehaviour
{
    public OpSwitch swich;

    [Range(1.0f, 90.0f)]
    public float OpSpeed = 1.0f;
    private float speed;
    public bool isPush;

    public Vector3 CloseRotation;
    public Vector3 OpenRotation;

    void Start()
    {
        CloseRotation = transform.localRotation.eulerAngles;

        if (isPush)
        {
            speed = OpSpeed;
            OpenRotation = CloseRotation - new Vector3(0.0f, 90.0f, 0.0f);
        }
        else
        {
            speed = -OpSpeed;
            OpenRotation = CloseRotation + new Vector3(0.0f, 90.0f, 0.0f);
        }
    }
    IEnumerator Open(float speed)
    {
        while (true)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(OpenRotation), OpSpeed * Time.deltaTime);
            if (transform.localRotation == Quaternion.Euler(OpenRotation))
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void DoorOpen()
    {
        StartCoroutine(Open(speed));
    }

    IEnumerator Close(float speed)
    {
        while (true)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(CloseRotation), OpSpeed * Time.deltaTime);
            if (transform.localRotation == Quaternion.Euler(CloseRotation))
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void DoorClose()
    {
        StartCoroutine(Close(speed));
    }
}

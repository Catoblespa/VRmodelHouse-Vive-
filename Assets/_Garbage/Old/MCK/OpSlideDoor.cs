using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpSlideDoor : MonoBehaviour
{
    public OpSwitch swich;
    public Vector3 OpenPosition;
    public Vector3 ClosePosition;
    public float OpenRange;
    public bool isLeft;
    public bool isUse;

    [Range(1.0f, 10.0f)]
    public float OpSpeed;
    private float speed;
    private float range;

    void Start()
    {
        speed = OpSpeed;
        range = OpenRange;
        if (isLeft)
        {
            range = -range;
        }

        ClosePosition = transform.localPosition;
        OpenPosition = ClosePosition + new Vector3(range, 0.0f, 0.0f);

        isUse = true;
    }


    public void DoorOpen()
    {
        bool tmp = swich.isOn;
        if(isUse)
        {
            isUse = false;
            StartCoroutine(Open(speed));
        }
        else
        {
            swich.isOn = tmp;
        }
    }

    public void DoorClose()
    {
        bool tmp = swich.isOn;
        if (isUse)
        {
            isUse = false;
            StartCoroutine(Close(speed));
        }
        else
        {
            swich.isOn = tmp;
        }
    }

    IEnumerator Open(float speed)
    {
        while (!isUse)
        {
            if (isLeft)
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0.0f, 0.0f));
                if (transform.localPosition.x <= OpenPosition.x)
                {
                    transform.localPosition = OpenPosition;
                    break;
                }
            }
            else
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0.0f, 0.0f));
                if (transform.localPosition.x >= OpenPosition.x)
                {
                    transform.localPosition = OpenPosition;
                    break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
        isUse = true;
    }


    IEnumerator Close(float speed)
    {
        while (!isUse)
        {
            if (isLeft)
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0.0f, 0.0f));
                if (transform.localPosition.x >= ClosePosition.x)
                {
                    transform.localPosition = ClosePosition;
                    break;
                }
            }
            else
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0.0f, 0.0f));
                if (transform.localPosition.x <= ClosePosition.x)
                {
                    transform.localPosition = ClosePosition;
                    break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
        isUse = true;
    }
}

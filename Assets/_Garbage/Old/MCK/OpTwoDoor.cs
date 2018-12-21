using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpTwoDoor : MonoBehaviour
{
    public OpSwitch swich;
    public Transform Door_L;
    public Transform Door_R;

    [Range(1.0f, 90.0f)]
    public float OpSpeed = 1.0f;


    IEnumerator Open(float speed)
    {
        while (true)
        {
            //왼쪽 문
            Door_L.Rotate(new Vector3(0.0f, speed, 0.0f));
            Door_R.Rotate(new Vector3(0.0f, -speed, 0.0f));

            if (Door_L.localEulerAngles.y >= 100.0f
                && Door_R.localEulerAngles.y <= 250.0f)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void DoorOpen()
    {
        StartCoroutine(Open(OpSpeed));
    }

    IEnumerator Close(float speed)
    {
        while (true)
        {
            //왼쪽 문
            Door_L.rotation = Quaternion.Slerp(Door_L.rotation, Quaternion.Euler(Vector3.zero), OpSpeed * Time.deltaTime);
            Door_R.rotation = Quaternion.Slerp(Door_R.rotation, Quaternion.Euler(Vector3.zero), OpSpeed * Time.deltaTime);

            if (Door_L.rotation == Quaternion.Euler(Vector3.zero)
                && Door_R.rotation == Quaternion.Euler(Vector3.zero))
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void DoorClose()
    {
        StartCoroutine(Close(OpSpeed));
    }
}

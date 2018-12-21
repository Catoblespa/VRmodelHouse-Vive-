using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRH_Base;
using TMPro;  


[CustomEditor(typeof(VRH_SingleDoor))]
public class VRH_SingleDoorInspector : Editor
{
    VRH_SingleDoor vrh_SingleDoor = null;
    Transform _transform;

    StartTransform _startTransform = new StartTransform();
    float _OpenSpeed = 0.0f;
    bool _isOpened = false;

    HINGE _hinge;
    Vector3 _hingePos = new Vector3();

    AXIS _Asix;
    bool _isLeftDoor = true;
    int _AngleDegree = 0;

    string _subscriptText = null;
    Vector3 _subscriptPosition = new Vector3();
    Vector3 _subscriptRotation = new Vector3();

    float time = 0.0f;

    bool _isTest = false;
    bool _OnEditButton = false;
    bool _OnUIEditButton = false;
    Tool _current;


    private void OnEnable()
    {
        vrh_SingleDoor = (VRH_SingleDoor)target;

        _isOpened = vrh_SingleDoor.isOpened;
        _isLeftDoor = vrh_SingleDoor.isLeftDoor;
        _transform = vrh_SingleDoor.transform;
        _Asix = vrh_SingleDoor.axis;
        _startTransform.position = _transform.position;
        _startTransform.rotation = _transform.rotation;
        _OpenSpeed = vrh_SingleDoor.OpenSpeed;
        _subscriptText = vrh_SingleDoor.SubscriptText;
        _current = Tools.current;

        _subscriptPosition = vrh_SingleDoor.SubscriptPosition;
    }

    private void OnDisable()
    {
        _transform.position = _startTransform.position;
        _transform.rotation = _startTransform.rotation;
        Tools.current = _current;

        if (vrh_SingleDoor.subscript != null && !Application.isPlaying)
        {
            DestroyImmediate(vrh_SingleDoor.subscript);

            Debug.Log("Disable");
        }
    }

    private void OnDestroy()
    {
        if (!_transform.GetComponent<VRH_SingleDoor>())
        {
            MeshRenderer[] ChildObject = _transform.GetComponentsInChildren<MeshRenderer>();

            for (int i = 0; i < ChildObject.Length; ++i)
            {
                if (ChildObject[i].gameObject.layer == LayerMask.NameToLayer("Door"))
                    ChildObject[i].gameObject.layer = 0;
            }
            VRH_ObjectBase x = _transform.GetComponent<VRH_ObjectBase>();
            DestroyImmediate(x);
            Debug.Log(_transform.name + ": Destroy VRH_ObjectBase");
        }
    }

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        //
        EditorGUILayout.LabelField("General Setting", TitleLabelStyle.style);
        //
        EditorGUILayout.BeginVertical(AttributesStyle.style);
        {
            _isOpened = EditorGUILayout.Toggle("Is Opened", vrh_SingleDoor.isOpened);
            vrh_SingleDoor.isOpened = _isOpened;

            _OpenSpeed = EditorGUILayout.Slider("Open Speed", vrh_SingleDoor.OpenSpeed, 1.0f, 100.0f);
            vrh_SingleDoor.OpenSpeed = _OpenSpeed;
        }
        EditorGUILayout.EndVertical();
        //        
        EditorGUILayout.LabelField("Hinge Setting", TitleLabelStyle.style);
        //
        EditorGUILayout.BeginVertical(AttributesStyle.style);
        {
            _hinge = (HINGE)EditorGUILayout.EnumPopup("Hinge", vrh_SingleDoor.hinge);
            vrh_SingleDoor.hinge = _hinge;

            switch (_hinge)
            {
                case HINGE.Auto:
                    _OnEditButton = false;

                    EditorGUILayout.TextField("Not yet Conding");
                    break;

                case HINGE.Manual:
                    //
                    EditorGUI.BeginDisabledGroup(!_OnEditButton);
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            _hingePos = vrh_SingleDoor.HingePostion - _startTransform.position;
                            _hingePos = EditorGUILayout.Vector3Field("Hinge Position", _hingePos);

                            if (GUILayout.Button("Reset", GUILayout.Width(EditButtonStyle.width), GUILayout.Height(EditButtonStyle.height)))
                            {
                                vrh_SingleDoor.HingePostion = _startTransform.position;
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                        //
                    }
                    EditorGUI.EndDisabledGroup();
                    //
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.FlexibleSpace();

                        bool _Button = GUILayout.Toggle(_OnEditButton, "Edit", "Button", GUILayout.Width(EditButtonStyle.width), GUILayout.Height(EditButtonStyle.height));

                        if (_Button)
                        {
                            Tools.current = Tool.None;
                        }
                        else
                        {
                            if (_OnEditButton)
                            {
                                Tools.current = _current;
                            }
                            else
                            {
                                _current = Tools.current;
                            }
                        }
                        _OnEditButton = _Button;
                    }
                    EditorGUILayout.EndHorizontal();
                    //
                    break;
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.LabelField("Angle Setting", TitleLabelStyle.style);
        //
        EditorGUILayout.BeginVertical(AttributesStyle.style);
        {
            _Asix = (AXIS)EditorGUILayout.EnumPopup("Axis", vrh_SingleDoor.axis);
            vrh_SingleDoor.axis = _Asix;

            _AngleDegree = EditorGUILayout.IntSlider("Angle Degree", (int)vrh_SingleDoor.AngleDegree, 1, 180);
            vrh_SingleDoor.AngleDegree = _AngleDegree;

            _isLeftDoor = EditorGUILayout.Toggle("Is Left Door", vrh_SingleDoor.isLeftDoor);
            vrh_SingleDoor.isLeftDoor = _isLeftDoor;

            if (_isTest)
            {
                if (time < _AngleDegree)
                {
                    time += Time.deltaTime * _OpenSpeed;
                    if (_isOpened)
                    {
                        _transform.RotateAround(vrh_SingleDoor.HingePostion, Door.GetAsix(_Asix, !_isLeftDoor), Time.deltaTime * _OpenSpeed);
                    }
                    else
                    {
                        _transform.RotateAround(vrh_SingleDoor.HingePostion, Door.GetAsix(_Asix, _isLeftDoor), Time.deltaTime * _OpenSpeed);
                    }
                }
                else
                {
                    time = 0;
                    _isOpened = !_isOpened;
                    vrh_SingleDoor.isOpened = _isOpened;
                }
            }
            else
            {
                if (!Application.isPlaying)
                {
                    _startTransform.position = _transform.position;
                    _startTransform.rotation = _transform.rotation;

                    _isOpened = false;
                    vrh_SingleDoor.isOpened = _isOpened;
                }
            }


            EditorGUILayout.BeginHorizontal();
            {
                EditorGUI.BeginDisabledGroup(Application.isPlaying);
                {
                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button(_isTest ? "Stop" : "Rotate Test", GUILayout.Width(TestButtonStyle.width), GUILayout.Height(TestButtonStyle.height)))
                    {
                        if (!_isTest)
                        {
                            time = 0;
                            _isTest = !_isTest;
                            return;
                        }
                        else
                        {
                            _transform.position = _startTransform.position;
                            _transform.rotation = _startTransform.rotation;
                            _isTest = !_isTest;
                        }
                    }

                }
                EditorGUI.EndDisabledGroup();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        //

        EditorGUILayout.LabelField("UI Setting", TitleLabelStyle.style);
        //
        EditorGUILayout.BeginVertical(AttributesStyle.style);
        {
            EditorGUI.BeginDisabledGroup(!_OnUIEditButton);
            {
                EditorGUILayout.BeginHorizontal();
                {
                    _subscriptPosition = vrh_SingleDoor.SubscriptPosition - _startTransform.position;
                    _subscriptPosition = EditorGUILayout.Vector3Field("UI Position", _subscriptPosition);

                    if (GUILayout.Button("Reset", GUILayout.Width(EditButtonStyle.width), GUILayout.Height(EditButtonStyle.height)))
                    {
                        vrh_SingleDoor.SubscriptPosition = _startTransform.position;
                    }
                }
                EditorGUILayout.EndHorizontal();
                //
                EditorGUILayout.BeginHorizontal();
                {
          
                    _subscriptRotation = EditorGUILayout.Vector3Field("UI Rotation", vrh_SingleDoor.SubscriptRotation);
                    vrh_SingleDoor.SubscriptRotation = _subscriptRotation;

                    if (GUILayout.Button("Reset", GUILayout.Width(EditButtonStyle.width), GUILayout.Height(EditButtonStyle.height)))
                    {
                        vrh_SingleDoor.SubscriptRotation = Vector3.zero;
                    }
                }
                EditorGUILayout.EndHorizontal();
                //
            }
            EditorGUI.EndDisabledGroup();
            //
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();

                bool _Button = GUILayout.Toggle(_OnUIEditButton, "Edit", "Button", GUILayout.Width(EditButtonStyle.width), GUILayout.Height(EditButtonStyle.height));
                {
                    if (_Button)
                    {
                        Tools.current = Tool.None;

                        if (vrh_SingleDoor.subscript == null)
                        {
                            vrh_SingleDoor.subscript = Instantiate(Resources.Load("UI/SubscriptUI")) as GameObject;
                            vrh_SingleDoor.subscript.transform.parent = _transform;

                            TextMeshProUGUI[] textMeshs = new TextMeshProUGUI[2];

                            if (vrh_SingleDoor.subscript.GetComponentsInChildren<TextMeshProUGUI>() != null)
                            {
                                textMeshs = vrh_SingleDoor.subscript.GetComponentsInChildren<TextMeshProUGUI>();
                                for (int i = 0; i < 2; ++i)
                                {
                                    textMeshs[i].text = vrh_SingleDoor.SubscriptText;
                                }
                            }
                        }
                        vrh_SingleDoor.subscript.transform.position = vrh_SingleDoor.SubscriptPosition;
                        vrh_SingleDoor.subscript.transform.rotation = Quaternion.Euler(vrh_SingleDoor.SubscriptRotation);
                    }
                    else
                    {
                        if (_OnUIEditButton)
                        {
                            Tools.current = _current;
                        }
                        else
                        {
                            _current = Tools.current;
                        }

                        if (vrh_SingleDoor.subscript != null && !Application.isPlaying)
                        {
                            DestroyImmediate(vrh_SingleDoor.subscript);
                        }
                    }
                }
                _OnUIEditButton = _Button;
            }
            EditorGUILayout.EndHorizontal();
            //

            EditorGUILayout.BeginHorizontal();
            {
                _subscriptText = EditorGUILayout.TextField("UI Text", vrh_SingleDoor.SubscriptText);
                vrh_SingleDoor.SubscriptText = _subscriptText;
            }
            EditorGUILayout.EndHorizontal();
            //
        }
        EditorGUILayout.EndVertical();



    }

    private void OnSceneGUI()
    {
        if (_hinge == HINGE.Manual && _OnEditButton)
        {
            vrh_SingleDoor.HingePostion = Handles.PositionHandle(vrh_SingleDoor.HingePostion, Quaternion.Euler(Vector3.zero));
            Handles.color = Handles.xAxisColor;
            vrh_SingleDoor.HingePostion = Handles.Slider(vrh_SingleDoor.HingePostion, Vector3.left);
            Handles.color = Handles.yAxisColor;
            vrh_SingleDoor.HingePostion = Handles.Slider(vrh_SingleDoor.HingePostion, Vector3.down);
            Handles.color = Handles.zAxisColor;
            vrh_SingleDoor.HingePostion = Handles.Slider(vrh_SingleDoor.HingePostion, Vector3.back);
            _hingePos = vrh_SingleDoor.HingePostion - _transform.position;
        }

        if (_hinge == HINGE.Manual && _OnUIEditButton)
        {
            vrh_SingleDoor.SubscriptPosition = Handles.PositionHandle(vrh_SingleDoor.SubscriptPosition, Quaternion.Euler(Vector3.zero));
            Handles.color = Handles.xAxisColor;
            vrh_SingleDoor.SubscriptPosition = Handles.Slider(vrh_SingleDoor.SubscriptPosition, Vector3.left);
            Handles.color = Handles.yAxisColor;
            vrh_SingleDoor.SubscriptPosition = Handles.Slider(vrh_SingleDoor.SubscriptPosition, Vector3.down);
            Handles.color = Handles.zAxisColor;
            vrh_SingleDoor.SubscriptPosition = Handles.Slider(vrh_SingleDoor.SubscriptPosition, Vector3.back);
            _subscriptPosition = vrh_SingleDoor.SubscriptPosition - _transform.position;
        }
    }
}





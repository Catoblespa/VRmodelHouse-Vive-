using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRH_Base;
using TMPro;


[CustomEditor(typeof(VRH_SlideDoor))]
public class VRH_SlideDoorInspector : Editor
{
    VRH_SlideDoor vrh_SlideDoor = null;
    Transform _transform;

    StartTransform _startTransform = new StartTransform();
    float _OpenSpeed = 0.0f;
    bool _isOpened = false;

    HINGE _hinge;
    Vector3 _hingePos = new Vector3();

    AXIS _Asix;
    bool _isLeftDoor = true;
    Vector3 _OpenPosition = new Vector3();
    float _OpenDistance = 0;
    bool _OpenPositionSetting = false;

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
        vrh_SlideDoor = (VRH_SlideDoor)target;

        _isOpened = vrh_SlideDoor.isOpened;
        _isLeftDoor = vrh_SlideDoor.isLeftDoor;
        _transform = vrh_SlideDoor.transform;
        _Asix = vrh_SlideDoor.axis;
        _startTransform.position = _transform.position;
        _startTransform.rotation = _transform.rotation;
        _OpenSpeed = vrh_SlideDoor.OpenSpeed;
        _subscriptText = vrh_SlideDoor.SubscriptText;
        _current = Tools.current;

        _OpenPosition = vrh_SlideDoor.OpenPosition;
        _OpenDistance = vrh_SlideDoor.OpenDistance;

        _subscriptPosition = vrh_SlideDoor.SubscriptPosition;
    }

    private void OnDisable()
    {
        _transform.position = _startTransform.position;
        _transform.rotation = _startTransform.rotation;
        Tools.current = _current;

        if (vrh_SlideDoor.subscript != null && !Application.isPlaying)
        {
            DestroyImmediate(vrh_SlideDoor.subscript);

            Debug.Log("Disable");
        }
    }

    private void OnDestroy()
    {
        if (!_transform.GetComponent<VRH_SlideDoor>())
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
            _isOpened = EditorGUILayout.Toggle("Is Opened", vrh_SlideDoor.isOpened);
            vrh_SlideDoor.isOpened = _isOpened;

            _OpenSpeed = EditorGUILayout.Slider("Open Speed", vrh_SlideDoor.OpenSpeed, 1.0f, 20.0f);
            vrh_SlideDoor.OpenSpeed = _OpenSpeed;
        }
        EditorGUILayout.EndVertical();
        //        
        EditorGUILayout.LabelField("Hinge Setting", TitleLabelStyle.style);
        //
        EditorGUILayout.BeginVertical(AttributesStyle.style);
        {
            _hinge = (HINGE)EditorGUILayout.EnumPopup("Hinge", vrh_SlideDoor.hinge);
            vrh_SlideDoor.hinge = _hinge;

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
                            _hingePos = vrh_SlideDoor.HingePostion - _startTransform.position;
                            _hingePos = EditorGUILayout.Vector3Field("Hinge Position", _hingePos);

                            if (GUILayout.Button("Reset", GUILayout.Width(EditButtonStyle.width), GUILayout.Height(EditButtonStyle.height)))
                            {
                                vrh_SlideDoor.HingePostion = _startTransform.position;
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
            _Asix = (AXIS)EditorGUILayout.EnumPopup("Axis", vrh_SlideDoor.axis);
            vrh_SlideDoor.axis = _Asix;

            EditorGUILayout.BeginHorizontal(AttributesStyle.style);
            {
                _OpenPosition = EditorGUILayout.Vector3Field("Open Position", vrh_SlideDoor.OpenPosition);

                if (GUILayout.Button("Set", GUILayout.Width(EditButtonStyle.width), GUILayout.Height(EditButtonStyle.height)))
                {
                    _OpenPosition = _transform.position;
                }
                vrh_SlideDoor.OpenPosition = _OpenPosition;
            }
            EditorGUILayout.EndHorizontal();

            _OpenDistance = Vector3.Distance(_startTransform.position, _OpenPosition);
            vrh_SlideDoor.OpenDistance = _OpenDistance;

            _isLeftDoor = EditorGUILayout.Toggle("Is Left Door", vrh_SlideDoor.isLeftDoor);
            vrh_SlideDoor.isLeftDoor = _isLeftDoor;

            if (_isTest)
            {
                if (time < _OpenDistance)
                {
                    time += Time.deltaTime * _OpenSpeed;
                    if (_isOpened)
                    {
                        _transform.position = Vector3.MoveTowards(_transform.position, _startTransform.position, Time.deltaTime * _OpenSpeed);
                    }
                    else
                    {
                        _transform.position = Vector3.MoveTowards(_transform.position, _OpenPosition, Time.deltaTime * _OpenSpeed);
                    }
                }
                else
                {
                    time = 0;
                    _isOpened = !_isOpened;
                    vrh_SlideDoor.isOpened = _isOpened;
                }
            }
            else
            {
                if (!Application.isPlaying)
                {
                    _startTransform.position = _transform.position;
                    _startTransform.rotation = _transform.rotation;

                    _isOpened = false;
                    vrh_SlideDoor.isOpened = _isOpened;
                }
            }


            EditorGUILayout.BeginHorizontal();
            {
                EditorGUI.BeginDisabledGroup(Application.isPlaying);
                {
                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button(_isTest ? "Stop" : "Slide Test", GUILayout.Width(TestButtonStyle.width), GUILayout.Height(TestButtonStyle.height)))
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
                    _subscriptPosition = vrh_SlideDoor.SubscriptPosition - _startTransform.position;
                    _subscriptPosition = EditorGUILayout.Vector3Field("UI Position", _subscriptPosition);

                    if (GUILayout.Button("Reset", GUILayout.Width(EditButtonStyle.width), GUILayout.Height(EditButtonStyle.height)))
                    {
                        vrh_SlideDoor.SubscriptPosition = _startTransform.position;
                    }
                }
                EditorGUILayout.EndHorizontal();
                //
                EditorGUILayout.BeginHorizontal();
                {

                    _subscriptRotation = EditorGUILayout.Vector3Field("UI Rotation", vrh_SlideDoor.SubscriptRotation);
                    vrh_SlideDoor.SubscriptRotation = _subscriptRotation;

                    if (GUILayout.Button("Reset", GUILayout.Width(EditButtonStyle.width), GUILayout.Height(EditButtonStyle.height)))
                    {
                        vrh_SlideDoor.SubscriptRotation = Vector3.zero;
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

                        if (vrh_SlideDoor.subscript == null)
                        {
                            vrh_SlideDoor.subscript = Instantiate(Resources.Load("UI/SubscriptUI")) as GameObject;
                            vrh_SlideDoor.subscript.transform.parent = _transform;

                            TextMeshProUGUI[] textMeshs = new TextMeshProUGUI[2];

                            if (vrh_SlideDoor.subscript.GetComponentsInChildren<TextMeshProUGUI>() != null)
                            {
                                textMeshs = vrh_SlideDoor.subscript.GetComponentsInChildren<TextMeshProUGUI>();
                                for (int i = 0; i < 2; ++i)
                                {
                                    textMeshs[i].text = vrh_SlideDoor.SubscriptText;
                                }
                            }
                        }
                        vrh_SlideDoor.subscript.transform.position = vrh_SlideDoor.SubscriptPosition;
                        vrh_SlideDoor.subscript.transform.rotation = Quaternion.Euler(vrh_SlideDoor.SubscriptRotation);
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

                        if (vrh_SlideDoor.subscript != null && !Application.isPlaying)
                        {
                            DestroyImmediate(vrh_SlideDoor.subscript);
                        }
                    }
                }
                _OnUIEditButton = _Button;
            }
            EditorGUILayout.EndHorizontal();
            //

            EditorGUILayout.BeginHorizontal();
            {
                _subscriptText = EditorGUILayout.TextField("UI Text", vrh_SlideDoor.SubscriptText);
                vrh_SlideDoor.SubscriptText = _subscriptText;
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
            vrh_SlideDoor.HingePostion = Handles.PositionHandle(vrh_SlideDoor.HingePostion, Quaternion.Euler(Vector3.zero));
            Handles.color = Handles.xAxisColor;
            vrh_SlideDoor.HingePostion = Handles.Slider(vrh_SlideDoor.HingePostion, Vector3.left);
            Handles.color = Handles.yAxisColor;
            vrh_SlideDoor.HingePostion = Handles.Slider(vrh_SlideDoor.HingePostion, Vector3.down);
            Handles.color = Handles.zAxisColor;
            vrh_SlideDoor.HingePostion = Handles.Slider(vrh_SlideDoor.HingePostion, Vector3.back);
            _hingePos = vrh_SlideDoor.HingePostion - _transform.position;
        }

        if (_hinge == HINGE.Manual && _OnUIEditButton)
        {
            vrh_SlideDoor.SubscriptPosition = Handles.PositionHandle(vrh_SlideDoor.SubscriptPosition, Quaternion.Euler(Vector3.zero));
            Handles.color = Handles.xAxisColor;
            vrh_SlideDoor.SubscriptPosition = Handles.Slider(vrh_SlideDoor.SubscriptPosition, Vector3.left);
            Handles.color = Handles.yAxisColor;
            vrh_SlideDoor.SubscriptPosition = Handles.Slider(vrh_SlideDoor.SubscriptPosition, Vector3.down);
            Handles.color = Handles.zAxisColor;
            vrh_SlideDoor.SubscriptPosition = Handles.Slider(vrh_SlideDoor.SubscriptPosition, Vector3.back);
            _subscriptPosition = vrh_SlideDoor.SubscriptPosition - _transform.position;
        }
    }
}





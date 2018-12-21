using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRH_Base;

public class VRH_Menuitem : MenuItemToolBox
{

    private static VRH_Menuitem _window;
    public static VRH_Menuitem window
    {
        get
        {
            if (_window = null)
                _window = FindObjectOfType(typeof(VRH_Menuitem)) as VRH_Menuitem;

            return _window;
        }
        set
        {
            _window = value;
        }
    }

    public VRH_Menuitem()
    {
        if (_window = null)
        {
            _window = FindObjectOfType(typeof(VRH_Menuitem)) as VRH_Menuitem;
            _window = GetWindow<VRH_Menuitem>(false, "ToolBox", true);
        }

    }

    int iSelectMenu;
    static Vector2 scrollPosition = Vector2.zero;

    void Init()
    {
        if (_window == null)
            _window = GetWindow<VRH_Menuitem>(false, "ToolBox", true);

        _window.minSize = new Vector2(300, 100);
        _window.maxSize = new Vector2(500, 500);
    }
    [MenuItem("VRhouse/ToolBox")]
    static void Open()
    {
        _window = GetWindow<VRH_Menuitem>(false, "ToolBox", true);
    }

    public void OnGUI()
    {
        Init();

        Target = Selection.activeObject as GameObject;

        EditorGUILayout.BeginHorizontal(GUI.skin.box);
        {
            iSelectMenu = GUILayout.Toolbar(iSelectMenu, new string[] { "General", "Door", "Light", "Furniture" });
        }
        EditorGUILayout.EndHorizontal();



        EditorGUILayout.BeginHorizontal(GUI.skin.box);
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);


            int selGridInt = 0;
            switch (iSelectMenu)
            {
                // Genaral
                case 0:
                    GUIContent[] Contents =
                    {
                        //버튼 추가
                        new GUIContent("Add Collider",LoadIcon("Add_Collider.png")),
                        new GUIContent("Set Ground",LoadIcon("Set_Ground.png")),
                        new GUIContent("Delete Ground",LoadIcon("Delete_Ground.png")),
                        new GUIContent("Create Player for HTC Vive",LoadIcon("Create_Character_for_VIVE.png")),
                        new GUIContent("Create Player for GoogleVR",LoadIcon("Create_Character_for_GVR.png"))
                    };

                    float width = _window.position.width - 45;
                    float height = (width / 2.0f) * (Contents.Length / 2 + 1);

                    GUIStyle style = new GUIStyle(GUI.skin.button)
                    {
                        fontStyle = FontStyle.Bold,
                        imagePosition = ImagePosition.ImageAbove,
                        fontSize = (int)(width / 18),
                        wordWrap = true
                    };

                    selGridInt = GUILayout.SelectionGrid(-1, Contents, 2, style, GUILayout.Width(width), GUILayout.Height((width / 5.0f) * Contents.Length));


                    switch (selGridInt)
                    {
                        //버튼 기능 추가
                        case 0: //"Add Collider"
                            if (Target != null)
                            {
                                MeshRenderer[] targets = Target.GetComponentsInChildren<MeshRenderer>();

                                for (int i = 0; i < targets.Length; ++i)
                                {
                                    targets[i].lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
                                    targets[i].reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
                                    targets[i].receiveShadows = true;
                                    if (targets[i].GetComponent<MeshCollider>() == null)
                                        targets[i].gameObject.AddComponent<MeshCollider>();
                                }
                            }
                            break;

                        case 1: //"Set Ground"
                            if (Target != null)
                            {
                                Target.layer = LayerMask.NameToLayer("Ground");
                                Target.AddComponent<VRH_GroundSetting>();
                            }
                            break;

                        case 2: //"Delete Ground"
                            if (Target != null)
                            {
                                if (Target.gameObject.layer == LayerMask.NameToLayer("Ground"))
                                    Target.layer = 0;
                                VRH_GroundSetting x = null;
                                if (x = Target.GetComponent<VRH_GroundSetting>())
                                    DestroyImmediate(x);
                            }
                            break;

                        case 3://"Create Player for Vive"
                            GameObject player = Resources.Load("Prefabs/PlayerSteamVR") as GameObject;
                            Instantiate(player, new Vector3(15.0f,0.4f,15.0f),Quaternion.identity);
                            break;

                        case 4: //"Create Player for GoogleVR"

                            break;
                    }

                    break;
                // Door
                case 1:
                    {
                        Component[] Componets =
                        {
                            //여기에 추가하면 됨
                            AddComponentToList<VRH_SingleDoor>("Hinged Door", "Hinged_Door.png"),
                            AddComponentToList<VRH_SlideDoor>("Sliding Door", "Sliding_Door.png"),
                            AddComponentToList<VRH_SlideDoor>("Revolving Door", "Revolving_Door.png")
                        };

                        selGridInt = PaintSelectionGrid(_window, Componets);

                        if (selGridInt >= 0)
                            AddComponentFromButton(Componets[selGridInt], true);
                    }
                    break;
                // Light
                case 2:
                    {
                        Component[] Componets =
                        {
                            //여기에 추가하면 됨
                            AddComponentToList<VRH_Sunlight>("Sun Light","Sunlight.png"),
                            AddComponentToList<Incandescent_Light>("Incandescent","Incandescent.png"),
                            AddComponentToList<Mood_Lighting>("Mood Lighting","Mood_Lighting.png"),
                            AddComponentToList<Spot_Light>("Spotlight","Spotlight.png"),
                            AddComponentToList<_Switch>("Switch",null),
                        };

                        selGridInt = PaintSelectionGrid(_window, Componets);

                        if (selGridInt >= 0)
                            AddComponentFromButton(Componets[selGridInt], false);
                    }
                    break;

                // Furniture
                case 3:

                    break;
            }
            GUILayout.EndScrollView();
        }
        EditorGUILayout.EndHorizontal();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;


namespace VRH_Base
{
    public enum HINGE { Manual = 0, Auto }
    public enum ANGLE { Manual = 0, Auto }
    public enum AXIS { X_AXIS = 0, Y_AXIS, Z_AXIS }

    public class MenuItemToolBox : EditorWindow
    {

        public GameObject Target;

        public struct Component
        {
            public GUIContent content;
            public System.Type InnerType;

            public Component(GUIContent _content, System.Type _InnerType)
            {
                content = _content;
                InnerType = _InnerType;
            }
        }


        protected Component AddComponentToList<T>(string _Name, string _ImagePath)
        {
            Texture texture = null;
            if (_ImagePath != null)
            {
                texture = EditorGUIUtility.Load("Assets/Editor/Image/" + _ImagePath) as Texture;
            }
        
            Component result = new Component(new GUIContent(_Name, texture), InneredType<T>());

            return result;
        }

        protected Texture LoadIcon(string _ImagePath)
        {
            Texture texture = null;
            if (_ImagePath != null)
            {
                texture = EditorGUIUtility.Load("Assets/Editor/Image/" + _ImagePath) as Texture;
            }
            return texture;
        }

        protected void AddComponentFromButton(Component _component, bool _option)
        {
            /* 2018년 11월 25일 오후 11시
               1. 라이트를 넣을 오브젝트를 클릭한다.
               2. 라이트버튼을 누른다.
               3. 1에서 클릭한 오브젝트에 우리가 구현한 스크립트가 추가된다.
               4. 1의 오브젝트에 _component.content.text안에 저장된 이름의 오브젝트가 생성되며 1에서 클릭한 오브젝트의 자식으로 들어간다.
               5. _component.content.text 오브젝트는 라이트가 들어있다. 이 오브젝트는 자식 오브젝트가 없다.
               실패. clone에 Light컴포넌트를 넣어야하는데 기술력 부족
               따라서 5번을 하지 않고 태양과 같이 라이트 오브젝트를 또 추가하여 자식오브젝트에 넣음.
            */

            if (_option)
            {
                if (Target != null && Target.gameObject.layer == 0 && Target.GetComponent(_component.InnerType) == null)
                    Target.AddComponent(_component.InnerType);
            }
            else
            {
   
                GameObject clone = new GameObject(_component.content.text);
                clone.AddComponent(_component.InnerType);

                /* 2018년 11월 25일 -명재
                if (_component.content.text == "Sun Light") // 태양이면 태양오브젝트 생성
                {                   
                    clone.AddComponent(_component.InnerType);
                }
                else // 태양이 아니면 클릭한 오브젝트에 스크립트추가, 따로 오브젝트 생성 없음
                {
                    //Target.AddComponent(_component.InnerType);                   
                    clone.AddComponent(_component.InnerType);
                }
                */

                if (Target != null) 
                {
                    clone.transform.parent = Target.transform;
                    clone.transform.localPosition = Vector3.zero;
                }
            }

        }

        protected int PaintSelectionGrid(EditorWindow _window, Component[] _component)
        {
            int selGridInt = 0;
            GUIContent[] selContents = new GUIContent[_component.Length];

            for (int i = 0; i < _component.Length; ++i)
            {
                selContents[i] = new GUIContent(_component[i].content);
            }

            GUILayout.BeginVertical(GUI.skin.label);
            {
                float width = _window.position.width - 45;
                float height = (width / 2.0f) * (selContents.Length / 2 + 1);

                GUIStyle style = new GUIStyle(GUI.skin.button)
                {
                    fontStyle = FontStyle.Bold,
                    imagePosition = ImagePosition.ImageAbove,
                    fontSize = (int)(width / 20)
                };

                selGridInt = GUILayout.SelectionGrid(-1, selContents, 2, style, GUILayout.Width(width), GUILayout.Height((width / 5.0f) * selContents.Length));

            }
            GUILayout.EndVertical();

            return selGridInt;
        }


        protected System.Type InneredType<T>()
        {
            return typeof(T);
        }

    }



    public class StartTransform
    {
        public Vector3 position = new Vector3();
        public Quaternion rotation = new Quaternion();

        public StartTransform()
        {
            position = Vector3.zero;
            rotation = Quaternion.identity;
        }

        public StartTransform(Vector3 _position, Quaternion _rotation)
        {
            position = _position;
            rotation = _rotation;
        }

    }
    public class TitleLabelStyle
    {
        public static GUIStyle style = new GUIStyle(GUI.skin.label)
        {
            fontStyle = FontStyle.Bold
        };
    }

    public class AttributesStyle
    {
        public static GUIStyle style = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleRight
        };
    }

    public class ToolboxButtonStyle
    {
        public const float width = 100.0f;
        public const float height = 60.0f;
    }

    public class EditButtonStyle
    {
        public const float width = 60.0f;
        public const float height = 17.0f;
    }

    public class TestButtonStyle
    {
        public const float width = 100.0f;
        public const float height = 17.0f;
    }

    public static class Door
    {
        public static Vector3 GetAsix(AXIS _asix, bool _isLeft)
        {
            Vector3 result = new Vector3();
            switch (_asix)
            {
                case AXIS.X_AXIS:
                    if (_isLeft)
                        result = Vector3.forward;
                    else
                        result = Vector3.back;
                    break;
                case AXIS.Y_AXIS:
                    if (_isLeft)
                        result = Vector3.up;
                    else
                        result = Vector3.down;
                    break;
                case AXIS.Z_AXIS:
                    if (_isLeft)
                        result = Vector3.right;
                    else
                        result = Vector3.left;
                    break;
            }
            return result;
        }
    }

    public abstract class VRH_DoorBase : EventTrigger
    {
        protected Transform Transform_UITexture;

        protected abstract void GetSubscriptTextureTransform(string _Subscript);
    }
}

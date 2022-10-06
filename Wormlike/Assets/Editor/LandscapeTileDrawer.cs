using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(LandscapeTile))]

    public class LandscapeTileDrawer : UnityEditor.Editor
    {
        private LandscapeTile _target;
        
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Grow North"))
            {
                _target.GrowOnEdge(Vector2.up);
            }

            if (GUILayout.Button("Grow South"))
            {
                _target.GrowOnEdge(Vector2.down);

            }

            if (GUILayout.Button("Grow East"))
            {
                _target.GrowOnEdge(Vector2.left);

            }

            if (GUILayout.Button("Grow West"))
            {
                _target.GrowOnEdge(Vector2.right);

            }
        }

        void OnEnable()
        {
            _target = (LandscapeTile)target;
        }
       
    }
}

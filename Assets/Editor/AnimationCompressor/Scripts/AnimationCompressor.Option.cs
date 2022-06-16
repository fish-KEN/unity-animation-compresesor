using UnityEditor;

namespace AnimationCompressor
{
    public class Option
    {
        /// <summary>
        /// Allowable range position error in curve fitting
        /// </summary>
        public float PositionAllowError { get; set; } = 0.02f;

        /// <summary>
        /// Allowable range rotation error in curve fitting
        /// </summary>
        public float RotationAllowError { get; set; } = 0.01f;

        /// <summary>
        /// Allowable range scale error in curve fitting
        /// </summary>
        public float ScaleAllowError { get; set; } = 0.05f;

        /// <summary>
        /// Improved accuracy for endpoints such as feet and hands
        /// </summary>
        public bool EnableAccurateEndPointNodes { get; set; } = true;

        /// <summary>
        /// Endpoint computation tolerance
        /// if 3, Allow up to 3 layers from the end
        /// </summary>
        public int EndPointNodesRange { get; set; } = 4;

        public void OnGUI()
        {
            EditorGUILayout.TextArea("Allow Error Range");
            {
                EditorGUI.indentLevel++;

                PositionAllowError = EditorGUILayout.FloatField(nameof(PositionAllowError), PositionAllowError);
                RotationAllowError = EditorGUILayout.FloatField(nameof(RotationAllowError), RotationAllowError);
                ScaleAllowError = EditorGUILayout.FloatField(nameof(ScaleAllowError), ScaleAllowError);

                EditorGUI.indentLevel--;
            }

            EnableAccurateEndPointNodes = EditorGUILayout.Toggle(nameof(EnableAccurateEndPointNodes), EnableAccurateEndPointNodes);
            if(EnableAccurateEndPointNodes)
            {
                EditorGUI.indentLevel++;

                EndPointNodesRange = EditorGUILayout.IntField(nameof(EndPointNodesRange), EndPointNodesRange);

                EditorGUI.indentLevel--;
            }
        }
    }
}
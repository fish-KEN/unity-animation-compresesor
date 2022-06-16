using System.IO;
using UnityEditor;
using UnityEngine;

namespace AnimationCompressor
{
    public partial class Core
    {
        private Option option = null;
        private AnimationClip originClip = null;
        private AnimationClip compressClip = null;

        private readonly int TotalStep = 4;

        private void UpdateProgressBar(string desc, int step)
        {
            EditorUtility.DisplayProgressBar(nameof(AnimationCompressor), desc, (float)step / TotalStep);
        }

        private void ClearProgressBar()
        {
            EditorUtility.ClearProgressBar();
        }


        public void Compress(AnimationClip originClip, Option option)
        {
            if (originClip == null)
            {
                Debug.Log($"{nameof(AnimationCompressor)} AnimationClip is null");
                return;
            }

            this.option = option;
            this.originClip = originClip;

            ProcessCompress();
        }

        private void ProcessCompress()
        {
            var outputPath = Util.GetOutputPath(originClip);
            compressClip = Object.Instantiate(originClip);

            if (File.Exists(outputPath))
            {
                var exist = AssetDatabase.LoadAssetAtPath<AnimationClip>(outputPath);
                EditorUtility.CopySerialized(exist, compressClip);
            }

            compressClip.ClearCurves();

            PreCompress();
            Compress();

            AssetDatabase.CreateAsset(compressClip, outputPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            ClearProgressBar();
        }

        private void PreCompress()
        {
            UpdateProgressBar(nameof(GenerateBoneMapPass), 1);
            GenerateBoneMapPass();
        }

        private void Compress()
        {
            UpdateProgressBar(nameof(GenerateKeyFrameByCurveFittingPass), 2);
            GenerateKeyFrameByCurveFittingPass();

            UpdateProgressBar(nameof(KeyFrameReductionPass), 3);
            KeyFrameReductionPass();

            if (option.EnableAccurateEndPointNodes)
            {
                UpdateProgressBar(nameof(CalculateEndPointNode), 4);
                CalculateEndPointNode();
            }
        }
    }
}
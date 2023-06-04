using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.DotweenPrinter
{
    [ExecuteAlways]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextAnimationPrinter : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _tmpText;
        [SerializeField]
        private TextMeshData _textMeshData;
        private void Start()
        {
            _tmpText = GetComponent<TMP_Text>();
            _textMeshData = new TextMeshData();
            foreach (var info in _tmpText.textInfo.characterInfo)
            {
                _textMeshData.characterData.Add(new CharacterData());
            }
        }

        private void Update()
        {
            int index = 0;
            foreach (var data in _textMeshData.characterData)
            {
                int materialIndex = _tmpText.textInfo.characterInfo[index].materialReferenceIndex;
                Vector3[] vertices = _tmpText.textInfo.meshInfo[materialIndex].vertices;
                int vertexIndex = _tmpText.textInfo.characterInfo[index].vertexIndex;
                Vector3 offset = (vertices[vertexIndex + 0] + vertices[vertexIndex + 2]) / 2;
                Vector3[] destinationVertices = _tmpText.textInfo.meshInfo[materialIndex].vertices;
                Vector3 rotation = Vector3.zero, position = Vector3.zero, scale = Vector3.one;

                var matrix = Matrix4x4.TRS(data.position, Quaternion.Euler(data.rotation), data.scale);
                for (int i = 0; i < 4; i++)
                {
                    destinationVertices[vertexIndex + i] = matrix.MultiplyPoint(vertices[vertexIndex + i] - offset) + offset;
                }

                for (int i = 0; i < _tmpText.textInfo.meshInfo.Length; i++)
                {
                    _tmpText.textInfo.meshInfo[i].mesh.vertices = _tmpText.textInfo.meshInfo[i].vertices;
                    _tmpText.textInfo.meshInfo[i].mesh.colors32 = _tmpText.textInfo.meshInfo[i].colors32;
                    _tmpText.textInfo.textComponent.UpdateGeometry(_tmpText.textInfo.meshInfo[i].mesh, i);
                }

                index++;
            }

            
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

[Serializable]
public class CharacterData
{
    public Vector3 rotation = Vector3.zero;
    public Vector3 position = Vector3.zero;
    public Vector3 scale = Vector3.one;
}

[Serializable]
public class TextMeshData
{
    public List<CharacterData> characterData;

    public TextMeshData()
    {
        characterData = new List<CharacterData>();
    }
}

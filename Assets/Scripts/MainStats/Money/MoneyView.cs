using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class MoneyView : MonoBehaviour
{
    private TMP_Text _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TMP_Text>();

        if (_textMesh == null)
        {
            throw new NullReferenceException(nameof(_textMesh));
        }
    }

    public void Show(uint value)
    {
        _textMesh.text = value.ToString();
    }
}

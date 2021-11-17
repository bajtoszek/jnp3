using UnityEngine;

public class FloatingText : APooledObject
{
    [SerializeField]
    private TextMesh m_TextMesh = null;

    public void SetText(string text)
    {
        m_TextMesh.text = text;
    }
}

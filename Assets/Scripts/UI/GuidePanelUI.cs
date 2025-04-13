using UnityEngine;
using UnityEngine.UIElements;

public class GuidePanelUI : MonoBehaviour
{
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var btnClose = root.Q<Button>("btnCloseGuide");

        btnClose.clicked += () =>
        {
            gameObject.SetActive(false);
        };
    }
}

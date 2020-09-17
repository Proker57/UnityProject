using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LinkHandler : MonoBehaviour, IPointerClickHandler {
    TextMeshProUGUI tmpro;
    void Awake() {
        tmpro = GetComponent<TextMeshProUGUI>();
    }
    public void OnPointerClick(PointerEventData eventData) {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(tmpro, Input.mousePosition, Camera.main);
        if (linkIndex != -1) { // was a link clicked?
            TMP_LinkInfo linkInfo = tmpro.textInfo.linkInfo[linkIndex];

            // open the link id as a url, which is the metadata we added in the text field
            Application.OpenURL(linkInfo.GetLinkID());
        }
    }
}

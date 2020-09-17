using TMPro;
using UnityEngine;

namespace NoSuchStudio.Localization.Localizers {
    /// <summary>
    /// Localizes <see cref="TextMeshProUGUI"/> by setting its <see cref="TextMeshProUGUI.text"/> property
    /// based on the <see cref="PhrasedComponentLocalizer{LT, CT}.phrase"/> assigned to it and
    /// <see cref="LocalizationService.CurrentLocale"/>.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    [AddComponentMenu(LocalizationService.ComponentMenuPath + "/Text Mesh Pro Text Localizer (Phrased)")]
    public class TMProTextLocalizer : PhrasedComponentLocalizer<TMProTextLocalizer, TextMeshProUGUI> {
        public override void UpdatePhrasedComponent() {
            _component.text = _translation;
        }
    }
}

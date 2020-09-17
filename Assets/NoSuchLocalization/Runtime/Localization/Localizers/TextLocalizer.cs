using UnityEngine;
using UnityEngine.UI;

namespace NoSuchStudio.Localization.Localizers {
    /// <summary>
    /// Localizes <see cref="Text"/> by setting its <see cref="Text.text"/> property
    /// based on the <see cref="PhrasedComponentLocalizer{LT, CT}.phrase"/> assigned to it and
    /// <see cref="LocalizationService.CurrentLanguage"/>.
    /// </summary>
    [RequireComponent(typeof(Text))]
    [AddComponentMenu(LocalizationService.ComponentMenuPath + "/Text Localizer (Phrased)")]
    public class TextLocalizer : PhrasedComponentLocalizer<TextLocalizer, Text> {
        public override void UpdatePhrasedComponent() {
            _component.text = _translation;
        }
    }
}

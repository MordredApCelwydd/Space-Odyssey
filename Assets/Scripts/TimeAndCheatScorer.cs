using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class TimeAndCheatScorer : MonoBehaviour
{
    private TextMeshProUGUI _displayLabel;
    private string _data;

    private void Start()
    {
        _displayLabel = GetComponent<TextMeshProUGUI>();
        _data = TimeAndStatusManager.timeElapsed.ToString(CultureInfo.InvariantCulture) + " secs" +
                (TimeAndStatusManager.areCheatsUsed ? " w/ cheats :)" : "");
        _displayLabel.text = _data;
    }
}

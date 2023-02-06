using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    public JSONData jsonData;
    public TMP_Text[] dataText;

    private void OnEnable()
    {
        dataText[0].text = jsonData.gameData.TotalGamesPlayed.ToString();
        dataText[1].text = jsonData.gameData.XWinCount.ToString();
        dataText[2].text = jsonData.gameData.OWinCount.ToString();
        dataText[3].text = jsonData.gameData.TotalGamesPlayedWithAI.ToString();
        dataText[4].text = jsonData.gameData.WinCount.ToString();
        dataText[5].text = jsonData.gameData.LooseCount.ToString();
    }
}

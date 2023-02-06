using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Board : MonoBehaviour
{
    public GameObject chooseMark, board, winner;

    public Sprite spriteX;
    public Sprite spriteO;

    public Color colorX;
    public Color colorO;

    public TMP_Text wonText;

    public Mark currentMark;

    public Mark[] marks;

    [SerializeField] private int _markCount;

    private bool _canPlay = true;

    public bool playWithAI;

    public Box[] boxes;

    private bool _won = false;

    public JSONData json;

    private Mark _startMark;

    private void Start()
    {
        marks = new Mark[9];
    }

    public void ChooseStartigMark(string mark)
    {
        currentMark = (mark == "X") ? Mark.X : Mark.O;
        board.SetActive(true);
        chooseMark.SetActive(false);
        _startMark = currentMark;
    }

    public void HitBox()
    {
        GameObject buttonGameObject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        var box = buttonGameObject.GetComponent<Box>();

        if (!box.isMarked && _canPlay)
        {
            marks[box.index] = currentMark;

            GetSprite();
            box.SetAsMarked(GetSprite(), currentMark, SetColor());
            _markCount++;

            CheckIfWon();

            if (_markCount == 9 && !_won)
            {
                winner.SetActive(true);
                wonText.text = "IT'S A TIE!";
                json.gameData.TotalGamesPlayed += 1;
                SFX.instance.PlaySound(4);

                if (playWithAI)
                {
                    json.gameData.TotalGamesPlayedWithAI += 1;
                }
            }

            if (!playWithAI)
            {
                SwitchPlayer();
            }
            else if (playWithAI && _markCount <= 8 && !_won)
            {
                SwitchPlayer();
                StartCoroutine(AITurn());
            }
        }
    }

    private void CheckIfWon()
    {
        _won = CheckCombinations();

        if (_won)
        {
            _canPlay = false;
            SetWonText();
        }

        PutDataIntoJson();
    }

    private bool CheckCombinations()
    {
        return
            AreBoxesMatched(0, 1, 2) ||
            AreBoxesMatched(3, 4, 5) ||
            AreBoxesMatched(6, 7, 8) ||
            AreBoxesMatched(0, 3, 6) ||
            AreBoxesMatched(1, 4, 7) ||
            AreBoxesMatched(2, 5, 8) ||
            AreBoxesMatched(0, 4, 8) ||
            AreBoxesMatched(2, 4, 6);
    }

    private bool AreBoxesMatched(int a, int b, int c)
    {
        Mark mark = currentMark;
        bool isMatched = (marks[a] == mark && marks[b] == mark && marks[c] == mark);
        return isMatched;
    }

    private Color SetColor()
    {
        return (currentMark == Mark.X) ? colorX : colorO;
    }

    private void SwitchPlayer()
    {
        currentMark = (currentMark == Mark.X) ? Mark.O : Mark.X;
    }

    private Sprite GetSprite()
    {
        return (currentMark == Mark.X) ? spriteX : spriteO;
    }

    private void SetWonText()
    {
        winner.SetActive(true);
        wonText.text = (currentMark == Mark.X) ? "X WON" : "O WON";
        SFX.instance.PlaySound(3);
    }

    private IEnumerator AITurn()
    {
        _canPlay = false;
        yield return new WaitForSeconds(0.3f);
        var randomIndex = 0;

        while (marks[randomIndex] != Mark.None)
        {
            randomIndex = RandomIndex();
        }

        marks[randomIndex] = currentMark;
        boxes[randomIndex].SetAsMarked(GetSprite(), currentMark, SetColor());
        yield return new WaitForSeconds(0.3f);
        _canPlay = true;
        CheckIfWon();

        if (_won)
        {
            json.gameData.LooseCount += 1;
        }

        SwitchPlayer();
        _markCount++;
    }

    private int RandomIndex()
    {
        int randomIndex = Random.Range(0, marks.Length);
        return randomIndex;
    }

    public void PlayWithAI()
    {
        playWithAI = true;
    }

    public void PlayAgain()
    {
        for (int i = 0; i < marks.Length; i++)
        {
            marks[i] = Mark.None;
        }

        currentMark = Mark.None;
        _startMark = Mark.None;
        _markCount = 0;
        _canPlay = true;

        chooseMark.SetActive(true);
        board.SetActive(false);
        winner.SetActive(false);
    }

    private void PutDataIntoJson()
    {
        if (_won && !playWithAI)
        {
            json.gameData.TotalGamesPlayed += 1;
        }

        if (_won && !playWithAI && currentMark == Mark.X)
        {
            json.gameData.XWinCount += 1;
        }

        if (_won && !playWithAI && currentMark == Mark.O)
        {
            json.gameData.OWinCount += 1;
        }

        if (_won && playWithAI)
        {
            json.gameData.TotalGamesPlayedWithAI += 1;
        }

        if (_won && playWithAI && _startMark == currentMark)
        {
            json.gameData.WinCount += 1;
        }

        json.SaveJSON();
    }

    private void OnDisable()
    {
        PlayAgain();
        playWithAI = false;
    }
}

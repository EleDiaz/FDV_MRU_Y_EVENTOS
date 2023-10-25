using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    private GameplaySystem _gameplay;

    private TMPro.TextMeshProUGUI _text;

    // Start is called before the first frame update
    void Start()
    {
        _gameplay = GameSystem.Instance.GetComponent<GameplaySystem>();

        _text = GetComponent<TMPro.TextMeshProUGUI>();
        if (_text == null)
        {
            Debug.LogError("No TextMeshProUGUI component found");
            return;
        }

        if (_gameplay == null)
        {
            Debug.LogError("We wont be able to update the score at the UI, no Gameplay reference");
        }
        _gameplay.scoreChanged += UpdateScores;
        _gameplay.maxScoreChanged += UpdateScores;
    }


    // We dont really need _score, in this case because we keep simple, but in other cases a better isolation around the score properties could be done.
    void UpdateScores(int _score)
    {
        _text.text = _gameplay.Score.ToString(); // + " / " + _gameplay.MaxScore.ToString();
    }

}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MessageScript : MonoBehaviour
{
    private const int maxLength = 6;
    private TMPro.TextMeshProUGUI messagePanel;
    void Start()
    {
        messagePanel = GameObject
            .Find("MessagesPanal")
            .GetComponent<TMPro.TextMeshProUGUI>();
        messagePanel.text = "Welcome!\r\n\r\nGet ready for thrilling adventures, epic battles, and endless excitement. Whether you're a seasoned pro or new to gaming, there's something for everyone here.\r\n\r\nJoin our global community, conquer challenges, and write your own legend.\r\n\r\nLet the journey begin!";
        GameState.Subscribe(OnGameStateChange);
    }


    void Update()
    {

    }
    private void OnDestroy()
    {
        GameState.Unsubscribe(OnGameStateChange);
    }

    private void OnGameStateChange(string propName)
    {
        if (propName == nameof(GameState.GameMessages))
        {
            StringBuilder sb = new();
            foreach (var message in GameState.GameMessages.TakeLast(maxLength))
            {
                sb.Append($"{message.Moment.ToShortDateString()} {message.Text}\n");
            }
            messagePanel.text = sb.ToString();

        }
    }
}

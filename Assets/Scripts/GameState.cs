using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class GameState
{
    private static float _chacterStamina;
    public static float ChacterStamina
    {
        get => _chacterStamina;
        set
        {
            if (_chacterStamina != value)
            {
                _chacterStamina = value;
                NotifySubscribers(nameof(ChacterStamina));
            }
        }
    }
    private static bool _isCompassVisible;
    public static bool isCompassVisible
    {
        get => _isCompassVisible;
        set
        {
            if (value != _isCompassVisible)
            {
                _isCompassVisible = value;
                NotifySubscribers(nameof(isCompassVisible));
            }
        }
    }

    private static bool _isrRadarVisible;
    public static bool isrRadarVisible
    {
        get => _isrRadarVisible;
        set
        {
            if (value != _isrRadarVisible)
            {
                _isrRadarVisible = value;
                NotifySubscribers(nameof(isrRadarVisible));
            }
        }
    }

    private static float _score;
    public static float Score
    {
        get => Mathf.RoundToInt(_score);
        set
        {
            if (value != _score)
            {
                _score = value;
                NotifySubscribers(nameof(Score));
            }
        }
    }

    private static List<GameMessage> gameMessages = new();

    public static ReadOnlyCollection<GameMessage> GameMessages => new(gameMessages);

    public static void AddGameMessage(GameMessage message)
    {
        gameMessages.Add(message);
        NotifySubscribers(nameof(GameMessages));
    }
    public static void RemoveGameMessage(GameMessage message)
    {
        gameMessages.Remove(message);
        NotifySubscribers(nameof(GameMessages));
    }

    private static float _coinCost = 1f;
    public static float CoinCost => _coinCost;
    public static float UpdateCoinCost() => _coinCost = 1f;
        //* (isCompassVisible ? 1f : 1.5f)
        //* (isrRadarVisible ? 1f : 1.5f)
        //* (isCompassVisible || isrRadarVisible ? 1f : 1.5f);

    private static void OnCoincostChange(string propName)
    {
        if(propName == nameof(isCompassVisible) || propName == nameof(isrRadarVisible))
        {
            UpdateCoinCost();
            NotifySubscribers(nameof(CoinCost));
        }
    }

    public static List<Action<String>> Subscribers { get; } = new() { OnCoincostChange };
    public static void Subscribe (Action<String> action) => Subscribers.Add(action);
    public static void Unsubscribe(Action<String> action) => Subscribers.Remove(action);
    private static void NotifySubscribers(String propertyName)
    {
        Subscribers.ForEach(action => action(propertyName));
    }
}

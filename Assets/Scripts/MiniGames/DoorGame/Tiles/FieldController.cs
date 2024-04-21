using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace MiniGames.DoorGame.Tiles
{
    public class FieldController: MonoBehaviour
    {
        [SerializeField] private FieldState fieldState = FieldState.Inactive;
        [SerializeField] private FieldTile fieldTilePrefab;
        [SerializeField] private int spacing = 2;
        
        private readonly Dictionary<Guid, FieldTile> _tiles = new();
        private GridLayoutGroup _gridLayoutGroup;
        private HashSet<Guid> _currentPattern;
        private GameEvents _gameEvents;
        private int _openTilesCount;

        private readonly Random _random = new();

        private void Start()
        {
            _gameEvents = FindObjectOfType<GameEvents>().GetComponent<GameEvents>();
            _gameEvents.onTileClick.AddListener(OnTileClickHandler);
        }

        public void Init(int gridSize)
        {
            _gridLayoutGroup ??= GetComponent<GridLayoutGroup>();
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = gridSize;
            _gridLayoutGroup.spacing = new Vector2(spacing, spacing);
            var rect = GetComponent<RectTransform>().rect;
            _gridLayoutGroup.cellSize = new Vector2(rect.width / gridSize - spacing, rect.height / gridSize - spacing);
            for (var i = 0; i < gridSize * gridSize; i++)
            {
                var tile = Instantiate(fieldTilePrefab, transform);
                _tiles[tile.Id] = tile;
            }
        }

        public void StartRound(int patternSize, int showPatternTimeInSeconds)
        {
            if(fieldState != FieldState.Inactive) return;
            ClearField();
            _currentPattern = GeneratePattern(patternSize);
            _openTilesCount = 0;
            StartCoroutine(ShowPatternCoroutine(showPatternTimeInSeconds));
        }
        
        private IEnumerator ShowPatternCoroutine(int showPatternTimeInSeconds)
        {
            fieldState = FieldState.ShowingPattern;
            ClearField();
            Debug.Log("Show pattern start");
            ShowPattern(_currentPattern);
            yield return new WaitForSeconds(showPatternTimeInSeconds);
            ClearField();
            Debug.Log("Show pattern finish");
            fieldState = FieldState.RoundStarted;
        }

        private IEnumerator ShowErrorCoroutine()
        {
            fieldState = FieldState.ShowingError;
            foreach (var tile in _tiles.Values)
            {
                tile.Open();
            }
            yield return new WaitForSeconds(0.3f);
            ClearField();
            fieldState = FieldState.Inactive;
            _gameEvents.onLooseRound.Invoke();
        }

        private void ShowPattern(HashSet<Guid> pattern)
        {
            foreach (var guid in pattern)
            {
                Debug.Log(guid);
                _tiles[guid].Open();
            }
        }

        private void ClearField()
        {
            foreach (var tile in _tiles.Values)
            {
                tile.Hide();
            }
        }

        private HashSet<Guid> GeneratePattern(int patternSize)
        {
            Debug.Log($"tiles count: {_tiles.Keys.Count}");
            var pattern = _tiles.Keys.OrderBy(_ => _random.Next()).Take(patternSize).ToHashSet();
            Debug.Log($"pattern size: {pattern.Count}");
            return pattern;
        }
        
        private void OnTileClickHandler(Guid tileId)
        {
            if(fieldState != FieldState.RoundStarted) return;
            if (_currentPattern.Contains(tileId))
            {
                if (!_tiles[tileId].IsOpened)
                {
                    _tiles[tileId].Open();
                    _openTilesCount++;
                }

                if (_openTilesCount != _currentPattern.Count) return;
                
                fieldState = FieldState.Inactive;
                ClearField();
                _gameEvents.onWinRound.Invoke();
            }
            else
            {
                StartCoroutine(ShowErrorCoroutine());
            }
        }
    }
}
using System;
using UniRx;
using UnityEngine;
namespace BlockBreaker
{
    public class BoardBreakManager : MonoBehaviour
    {
        public PlayerBoardMover PlayerBoardMover;
        public AxisMoveController ArrowMoveController;

        private readonly BoardBreakEvent boardBreakEvent = new BoardBreakEvent();
        private readonly TimeCountManager timeCountManager = new TimeCountManager();

        private void Awake()
        {
            PlayerBoardMover?.Init(boardBreakEvent.GameStart, ArrowMoveController);
            PlayerBoardMover?.SetSpeed(3);

            boardBreakEvent.GameStart.Subscribe(_ => Debug.Log("GameStart"));

            timeCountManager
                ?.CreateCountDownObservable(5)
                .Subscribe(t => Debug.Log(t), () => boardBreakEvent.GameStart.OnNext(Unit.Default));
        }

        public class BoardBreakEvent
        {
            public Subject<Unit> GameStart { get; } = new Subject<Unit>();
        }
    }
}
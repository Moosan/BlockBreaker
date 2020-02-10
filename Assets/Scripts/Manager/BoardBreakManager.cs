using BlockBreaker.Board;
using UniRx;
using UnityEngine;
using BlockBreaker.General;
using BlockBreaker.Ball;
namespace BlockBreaker.Manager
{
    public class BoardBreakManager : MonoBehaviour
    {
        public PlayerBoardMover PlayerBoardMover;
        public BallProvider BallProvider; 
        public AxisMoveController ArrowMoveController;
        public GameRule GameRule = new GameRule(3,2);
        private readonly BoardBreakEvent boardBreakEvent = new BoardBreakEvent();
        private readonly TimeCountFactory timeCountManager = new TimeCountFactory();
        
        private void Awake()
        {
            PlayerBoardMover.Init(boardBreakEvent.GameStart,ArrowMoveController,GameRule);
            BallProvider.BallMover.Init(boardBreakEvent.GameStart, ArrowMoveController, GameRule);
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
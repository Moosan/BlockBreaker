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
        public int GameStartInterval;
        public GameRule GameRule;
        private GameRuleHandler GameRuleHandler;
        private readonly BoardBreakEvent boardBreakEvent = new BoardBreakEvent();
        private readonly TimeCountFactory timeCountManager = new TimeCountFactory();
        
        private void Awake()
        {
            GameRuleHandler = new GameRuleHandler(GameRule);
            PlayerBoardMover.Init(boardBreakEvent.GameStart,ArrowMoveController,GameRuleHandler);
            BallProvider.BallMover.Init(boardBreakEvent.GameStart, ArrowMoveController, GameRuleHandler);
            boardBreakEvent.GameStart.Subscribe(_ => Debug.Log("GameStart"));

            timeCountManager
                ?.CreateCountDownObservable(GameStartInterval)
                .Subscribe(t => Debug.Log(t), () => boardBreakEvent.GameStart.OnNext(Unit.Default));
        }

        public class BoardBreakEvent
        {
            public Subject<Unit> GameStart { get; } = new Subject<Unit>();
        }
    }
}
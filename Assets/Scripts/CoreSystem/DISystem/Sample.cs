using UnityEngine;
namespace CoreSystem.DISystem.Sample
{
    public class Sample : MonoBehaviour
    {
        private void Awake()
        {
            var container = new Container();
            container.Bind(c => new TestLog("test"));
            container.Bind(c => new TestA(c.Get<TestLog>()));
            container.Bind(c => new TestB(c.Get<TestA>(), c.Get<TestLog>()));

            var testB = container.Get<TestB>();
        }
    }

    public class TestLog
    {
        public string Log { get; }
        public TestLog(string log)
        {
            Log = log;
        }
    }

    public class TestA
    {
        public TestA(TestLog testLog)
        {

        }
    }

    public class TestB
    {
        public TestB(TestA testA,TestLog testLog)
        {

        }
    }
}
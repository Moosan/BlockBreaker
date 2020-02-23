using UnityEngine;
namespace CoreSystem.DISystem.Sample
{
    public class Sample : MonoBehaviour
    {
        private void Awake()
        {
            var container = new Container();
            container.Bind<TestLog>("test");
            container.Bind<ILogger>("test", c => c.Get<TestLog>("test"));
            container.Bind("test", c => new TestLog());
            container.Bind("test", c => new TestA(c.Get<TestLog>("test")));
            container.Bind("test", c => new TestB(c.Get<TestA>("test"), c.Get<TestLog>("test")));

            var testB = container.Get<TestB>("test");
        }
    }

    public interface ILogger
    {

    }

    public class TestLog : ILogger
    {

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
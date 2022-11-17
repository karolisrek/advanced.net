namespace Test
{
    public interface ITestClass
    {
        public string Hello();
    }

    public class TestClass : ITestClass
    {
        private ITestClass2 _testClass2;
        public TestClass(ITestClass2 tc)
        {
            _testClass2 = tc;
        }
        public string Hello() => $"Hello world! {_testClass2.Hello2()}";
    }


    public interface ITestClass2
    {
        public string Hello2();
    }

    public class TestClass2 : ITestClass2
    {
        public string Hello2() => "Helloooooooo world!";
    }
}

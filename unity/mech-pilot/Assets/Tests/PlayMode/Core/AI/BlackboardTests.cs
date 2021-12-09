using Core.AI;
using NUnit.Framework;

namespace Tests.PlayMode.Core.AI
{
    public class BlackboardTests
    {
        [Test]
        public void Blackboard_UsersCanWriteData()
        {
            var sut = new Blackboard();

            var result = sut.Write("test-key", new object());

            Assert.AreEqual(BlackboardOperationStatus.Success, result.Status);
        }

        [Test]
        public void Blackboard_UsersCanReadWrittenData()
        {
            var sut = new Blackboard();
            var value = new object();
            var key = "test-key";
            sut.Write(key, value);

            var result = sut.Read(key);

            Assert.AreEqual(BlackboardOperationStatus.Success, result.Status);
            Assert.AreEqual(value, result.Data );
        }
    }
}
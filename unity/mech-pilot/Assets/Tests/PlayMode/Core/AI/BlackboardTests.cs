using System.Collections.Generic;
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

            var result = sut.Read(new BlackboardQueryRequest(key));

            Assert.AreEqual(BlackboardOperationStatus.Success, result.Status);
            Assert.AreEqual(value, result.Data);
        }

        [Test]
        public void Blackboard_UserCanViewListOfAvailableKeys()
        {
            var sut = new Blackboard();
            var emptyList = new HashSet<string>();

            Assert.AreEqual(emptyList, sut.AvailableKeys);
        }

        [Test]
        public void Blackboard_NewDataEntryIsAddedToListOfKeys()
        {
            var sut = new Blackboard();
            var key = "test-key";
            var set = new HashSet<string>();
            set.Add(key);

            sut.Write(key, new object());

            Assert.AreEqual(set, sut.AvailableKeys);
        }

        [Test]
        public void Blackboard_DataCanBeRemoved()
        {
            var sut = new Blackboard();
            var key = "test-key";
            var emptySet = new HashSet<string>();
            sut.Write(key, new object());
            Assert.NotNull(sut.Read(new BlackboardQueryRequest(key)).Data);

            sut.Remove(key);

            Assert.AreEqual(BlackboardOperationStatus.FailureKeyNotFound, sut.Read(new BlackboardQueryRequest(key)).Status);
            Assert.AreEqual(emptySet, sut.AvailableKeys);
        }

        [Test]
        public void Blackboard_TypeSafeRead()
        {
            var sut = new Blackboard();
            var value = 1f;
            sut.Write("test-key", value);

            var readOperation = sut.Read<float>(new BlackboardQueryRequest("test-key"));

            Assert.NotNull(readOperation.Data);
            Assert.AreEqual(value, readOperation.Data);
        }
    }
}
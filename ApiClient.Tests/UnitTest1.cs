using System;
using ApiClient.Models;
using NUnit.Framework;

namespace ApiClient.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            var client = new BetEventClient("http://localhost:27254");
            var result = client.Create(new BetEventMeta(){Name = "cdnsonc", BetDeadline = DateTime.Now});
            Console.WriteLine(result.Name);
            return;
        }
    }
}

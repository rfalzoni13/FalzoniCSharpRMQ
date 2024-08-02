using FalzoniCSharpRMQ.Common.Config;
using FalzoniCSharpRMQ.Consumer.Workers;
using FalzoniCSharpRMQ.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq.Expressions;

namespace FalzoniCSharpRMQ.Tests
{
    [TestClass]
    public class ConsumerTest
    {

        private Mock<ConsumerWorker> _mock;
        private Expression<Func<ConsumerWorker, object>> _predicate;

        [TestInitialize]
        public void TestInitialize()
        {
            _mock = new Mock<ConsumerWorker>();

            _predicate = m => m.Consume(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
        }

        [TestMethod]
        public void TestRunConsumer_Direct_Success()
        {
            ConsumerWorker obj = TestUtils<ConsumerWorker>.SetupReturnMock(_mock, _predicate, "Teste de mensagem Direct");

            var message = obj.Consume(RabbitMQAttributes.EXG_DIRECT_NAME, "direct", RabbitMQAttributes.QUEUE_PRODUCT_DATA, RabbitMQAttributes.RK_PRODUCT_DATA);

            Assert.AreEqual("Teste de mensagem Direct", message);
        }

        [TestMethod]
        public void TestRunConsumer_Fanout_Success()
        {
            ConsumerWorker obj = TestUtils<ConsumerWorker>.SetupReturnMock(_mock, _predicate, "Teste de mensagem Fanout");

            string message = obj.Consume(RabbitMQAttributes.EXG_FANOUT_NAME, "fanout", RabbitMQAttributes.QUEUE_PRODUCT_LOG, string.Empty);

            Assert.AreEqual("Teste de mensagem Fanout", message);
        }

        [TestMethod]
        public void TestRunConsumer_Topic_Success()
        {
            ConsumerWorker obj = TestUtils<ConsumerWorker>.SetupReturnMock(_mock, _predicate, "Teste de mensagem Topic");

            string message = obj.Consume(RabbitMQAttributes.EXG_TOPIC_NAME, "topic", RabbitMQAttributes.QUEUE_PRODUCT_DATA, RabbitMQAttributes.RK_PRODUCT_DATA);

            Assert.AreEqual("Teste de mensagem Topic", message);
        }

        [TestMethod]
        public void TestRunConsumer_Direct_Error()
        { 
            ConsumerWorker obj = TestUtils<ConsumerWorker>.SetupReturnMock(_mock, _predicate, "Teste");

            var message = obj.Consume(RabbitMQAttributes.EXG_DIRECT_NAME, "direct", RabbitMQAttributes.QUEUE_PRODUCT_DATA, RabbitMQAttributes.RK_PRODUCT_DATA);

            Assert.AreNotEqual("Teste de mensagem Direct", message);
        }

        [TestMethod]
        public void TestRunConsumer_Fanout_Error()
        {
            ConsumerWorker obj = TestUtils<ConsumerWorker>.SetupReturnMock(_mock, _predicate, "Teste");

            string message = obj.Consume(RabbitMQAttributes.EXG_FANOUT_NAME, "fanout", RabbitMQAttributes.QUEUE_PRODUCT_LOG, string.Empty);

            Assert.AreNotEqual("Teste de mensagem Fanout", message);
        }

        [TestMethod]
        public void TestRunConsumer_Topic_Error()
        {
            ConsumerWorker obj = TestUtils<ConsumerWorker>.SetupReturnMock(_mock, _predicate, "Teste");

            string message = obj.Consume(RabbitMQAttributes.EXG_TOPIC_NAME, "topic", RabbitMQAttributes.QUEUE_PRODUCT_DATA, null);

            Assert.AreNotEqual("Teste de mensagem Topic", message);
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void TestRunProducer_Direct_Fail()
        {
            try
            {
                ConsumerWorker obj = TestUtils<ConsumerWorker>.SetupExceptionReturnMock(_mock, _predicate);
                obj.Consume(RabbitMQAttributes.EXG_DIRECT_NAME, "direct", RabbitMQAttributes.QUEUE_PRODUCT_DATA, RabbitMQAttributes.RK_PRODUCT_DATA);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void TestRunProducer_Fanout_Fail()
        {
            try
            {
                ConsumerWorker obj = TestUtils<ConsumerWorker>.SetupExceptionReturnMock(_mock, _predicate);
                obj.Consume(RabbitMQAttributes.EXG_FANOUT_NAME, "fanout", RabbitMQAttributes.QUEUE_PRODUCT_DATA, string.Empty);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void TestRunProducer_Topic_Fail()
        {
            try
            {
                ConsumerWorker obj = TestUtils<ConsumerWorker>.SetupExceptionReturnMock(_mock, _predicate);
                obj.Consume(RabbitMQAttributes.EXG_TOPIC_NAME, "topic", RabbitMQAttributes.QUEUE_PRODUCT_LOG, RabbitMQAttributes.RK_PRODUCT_LOG);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }
    }
}

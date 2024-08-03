using FalzoniCSharpRMQ.Common.Config;
using FalzoniCSharpRMQ.Producer.Workers;
using FalzoniCSharpRMQ.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq.Expressions;

namespace FalzoniCSharpRMQ.Tests
{
    [TestClass]
    public class ProducerTest
    {
        private Mock<ProducerWorker> _mock;
        private Expression<Action<ProducerWorker>> _predicate;

        [TestInitialize]
        public void TestInitialize()
        {
            _mock = new Mock<ProducerWorker>();

            _predicate = m => m.Produce(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
        }

        [TestMethod]
        public void TestRunProducer_Direct_Success()
        {
            ProducerWorker obj = TestUtils<ProducerWorker, string>.SetupMock(_mock, _predicate);

            obj.Produce("Teste de mensagem Direct", RabbitMQAttributes.EXG_DIRECT_NAME, RabbitMQAttributes.QUEUE_PRODUCT_DATA, "direct", RabbitMQAttributes.RK_PRODUCT_DATA);

            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void TestRunProducer_Direct_Fail()
        {
            try
            {
                ProducerWorker obj = TestUtils<ProducerWorker, string>.SetupExceptionMock(_mock, _predicate);
                obj.Produce("Teste de mensagem Direct", RabbitMQAttributes.EXG_DIRECT_NAME, RabbitMQAttributes.QUEUE_PRODUCT_DATA, "direct", RabbitMQAttributes.RK_PRODUCT_DATA);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void TestRunProducer_Fanout_Success()
        {
            ProducerWorker obj = TestUtils<ProducerWorker, string>.SetupMock(_mock, _predicate);

            obj.Produce("Teste de mensagem Fanout", RabbitMQAttributes.EXG_FANOUT_NAME, RabbitMQAttributes.QUEUE_PRODUCT_LOG, "fanout", string.Empty);

            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void TestRunProducer_Fanout_Fail()
        {
            try
            {
                ProducerWorker obj = TestUtils<ProducerWorker, string>.SetupExceptionMock(_mock, _predicate);
                obj.Produce("Teste de mensagem Fanout", RabbitMQAttributes.EXG_FANOUT_NAME, RabbitMQAttributes.QUEUE_PRODUCT_LOG, "fanout", string.Empty);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public void TestRunProducer_Topic_Success()
        {
            ProducerWorker obj = TestUtils<ProducerWorker, string>.SetupMock(_mock, _predicate);

            obj.Produce("Teste de mensagem Topic", RabbitMQAttributes.EXG_TOPIC_NAME, RabbitMQAttributes.QUEUE_PRODUCT_LOG, "topic", RabbitMQAttributes.RK_PRODUCT_LOG);

            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void TestRunProducer_Topic_Fail()
        {
            try
            {
                ProducerWorker obj = TestUtils<ProducerWorker, string>.SetupExceptionMock(_mock, _predicate);
                obj.Produce("Teste de mensagem Topic", RabbitMQAttributes.EXG_TOPIC_NAME, RabbitMQAttributes.QUEUE_PRODUCT_LOG, "topic", RabbitMQAttributes.RK_PRODUCT_LOG);
            }
            catch(Exception)
            {
                Assert.Fail();
            }

        }
    }
}

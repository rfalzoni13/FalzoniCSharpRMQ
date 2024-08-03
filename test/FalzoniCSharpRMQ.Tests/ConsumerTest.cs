using FalzoniCSharpRMQ.Common.Config;
using FalzoniCSharpRMQ.Consumer.Workers;
using FalzoniCSharpRMQ.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FalzoniCSharpRMQ.Tests
{
    [TestClass]
    public class ConsumerTest
    {
        private List<string> _list;
        private Mock<ConsumerWorker> _mock;
        private Expression<Func<ConsumerWorker, object>> _predicateSingle;
        private Expression<Func<ConsumerWorker, object>> _predicateAll;
        private Expression<Func<ConsumerWorker, Task>> _predicateAllAsync;

        [TestInitialize]
        public void TestInitialize()
        {
            _mock = new Mock<ConsumerWorker>();

            _predicateSingle = m => m.Consume(It.IsAny<string>());
            _predicateAll = m => m.ConsumeAll(It.IsAny<string>());
            _predicateAllAsync = m => m.ConsumeAllAsync(It.IsAny<string>());
        }

        [TestMethod]
        public void TestRunConsumerAll_Success()
        {
            _list = new List<string>() 
            { 
                "Teste de mensagem Direct", 
                "Teste de mensagem Fanout",
                "Teste de mensagem Topic"
            };

            ConsumerWorker obj = TestUtils<ConsumerWorker, List<string>>.SetupReturnMock(_mock, _predicateAll, _list);

            var messages = obj.ConsumeAll(RabbitMQAttributes.QUEUE_PRODUCT_DATA);

            Assert.AreEqual(messages, _list);
        }

        [TestMethod]
        public void TestRunConsumerAllAsync_Success()
        {
            _list = new List<string>()
            {
                "Teste de mensagem Direct",
                "Teste de mensagem Fanout",
                "Teste de mensagem Topic"
            };

            ConsumerWorker obj = TestUtils<ConsumerWorker, List<string>>.SetupReturnMockAsync(_mock, _predicateAllAsync, _list);

            var messages = obj.ConsumeAllAsync(RabbitMQAttributes.QUEUE_PRODUCT_DATA).Result;

            Assert.AreEqual(messages, _list);
        }

        [TestMethod]
        public void TestRunConsumerAll_Error()
        {
            _list = new List<string>()
            {
                "Teste de mensagem Direct",
                "Teste de mensagem Fanout",
                "Teste de mensagem Topic"
            };

            ConsumerWorker obj = TestUtils<ConsumerWorker, List<string>>.SetupReturnMock(_mock, _predicateAll, _list);

            var messages = obj.ConsumeAll(RabbitMQAttributes.QUEUE_PRODUCT_DATA);

            Assert.AreNotEqual(messages, new List<string>() 
            { 
                "Teste de mensagem Direct",
                "Teste de mensagem Fanout",
                "Teste de mensagem Topic" 
            });
        }

        [TestMethod]
        public void TestRunConsumerAllAsync_Error()
        {
            _list = new List<string>()
            {
                "Teste de mensagem Direct",
                "Teste de mensagem Fanout",
                "Teste de mensagem Topic"
            };

            ConsumerWorker obj = TestUtils<ConsumerWorker, List<string>>.SetupReturnMockAsync(_mock, _predicateAllAsync, _list);

            var messages = obj.ConsumeAllAsync(RabbitMQAttributes.QUEUE_PRODUCT_DATA).Result;

            Assert.AreNotEqual(messages, new List<string>()
            {
                "Teste de mensagem Direct",
                "Teste de mensagem Fanout",
                "Teste de mensagem Topic"
            });
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void TestRunConsumerAll_Fail()
        {
            try
            {
                ConsumerWorker obj = TestUtils<ConsumerWorker, List<string>>.SetupExceptionReturnMock(_mock, _predicateAll);
                obj.ConsumeAll(RabbitMQAttributes.QUEUE_PRODUCT_LOG);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void TestRunConsumerAllAsync_Fail()
        {
            try
            {
                ConsumerWorker obj = TestUtils<ConsumerWorker, List<string>>.SetupExceptionReturnMock(_mock, _predicateSingle);
                obj.ConsumeAllAsync(RabbitMQAttributes.QUEUE_PRODUCT_LOG).RunSynchronously();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestRunConsumerSingle_Direct_Success()
        {
            _list = new List<string>() { "Teste de mensagem Direct" };

            ConsumerWorker obj = TestUtils<ConsumerWorker, List<string>>.SetupReturnMock(_mock, _predicateSingle, _list);

            var messages = obj.Consume(RabbitMQAttributes.QUEUE_PRODUCT_DATA);

            Assert.AreEqual(messages, _list);
        }

        [TestMethod]
        public void TestRunConsumerSingle_Fanout_Success()
        {
            _list = new List<string>() { "Teste de mensagem Fanout" };

            ConsumerWorker obj = TestUtils<ConsumerWorker, List<string>>.SetupReturnMock(_mock, _predicateSingle, _list);

            var messages = obj.Consume(RabbitMQAttributes.QUEUE_PRODUCT_DATA);

            Assert.AreEqual(messages, _list);
        }

        [TestMethod]
        public void TestRunConsumerSingle_Topic_Success()
        {
            _list = new List<string>() { "Teste de mensagem Topic" };

            ConsumerWorker obj = TestUtils<ConsumerWorker, List<string>>.SetupReturnMock(_mock, _predicateSingle, _list);

            var messages = obj.Consume(RabbitMQAttributes.QUEUE_PRODUCT_LOG);

            Assert.AreEqual(messages, _list);
        }

        [TestMethod]
        public void TestRunConsumerSingle_Direct_Error()
        {
            _list = new List<string>() { "Teste de mensagem Direct" };

            ConsumerWorker obj = TestUtils<ConsumerWorker, List<string>>.SetupReturnMock(_mock, _predicateSingle, _list);

            var messages = obj.Consume(RabbitMQAttributes.QUEUE_PRODUCT_LOG);

            Assert.AreNotEqual(messages, new List<string>() { "Teste de mensagem Direct" });
        }

        [TestMethod]
        public void TestRunConsumerSingle_Fanout_Error()
        {
            _list = new List<string>() { "Teste de mensagem Fanout" };

            ConsumerWorker obj = TestUtils<ConsumerWorker, List<string>>.SetupReturnMock(_mock, _predicateSingle, _list);

            var messages = obj.Consume(RabbitMQAttributes.QUEUE_PRODUCT_DATA);

            Assert.AreNotEqual(messages, new List<string>() { "Teste de mensagem Fanout" });
        }

        [TestMethod]
        public void TestRunConsumerSingle_Topic_Error()
        {
            _list = new List<string>() { "Teste de mensagem Topic" };

            ConsumerWorker obj = TestUtils<ConsumerWorker, List<string>>.SetupReturnMock(_mock, _predicateSingle, _list);

            var messages = obj.Consume(RabbitMQAttributes.QUEUE_PRODUCT_LOG);

            Assert.AreNotEqual(messages, new List<string>() { "Teste de mensagem Topic" });
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void TestRunProducer_Direct_Fail()
        {
            try
            {
                ConsumerWorker obj = TestUtils<ConsumerWorker, List<string>>.SetupExceptionReturnMock(_mock, _predicateSingle);
                obj.Consume(RabbitMQAttributes.QUEUE_PRODUCT_DATA);
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
                ConsumerWorker obj = TestUtils<ConsumerWorker, List<string>>.SetupExceptionReturnMock(_mock, _predicateSingle);
                obj.Consume(RabbitMQAttributes.QUEUE_PRODUCT_LOG);
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
                ConsumerWorker obj = TestUtils<ConsumerWorker, List<string>>.SetupExceptionReturnMock(_mock, _predicateSingle);
                obj.Consume(RabbitMQAttributes.QUEUE_PRODUCT_DATA);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }
    }
}

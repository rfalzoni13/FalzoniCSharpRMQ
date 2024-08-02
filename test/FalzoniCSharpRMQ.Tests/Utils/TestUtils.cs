using Moq;
using System.Linq.Expressions;
using System;

namespace FalzoniCSharpRMQ.Tests.Utils
{
    public static class TestUtils<T>
        where T : class
    {
        public static T SetupMock(Mock<T> mock, Expression<Action<T>> predicate)
        {
            mock.Setup(predicate);

            return mock.Object;
        }

        public static T SetupReturnMock(Mock<T> mock, Expression<Func<T, object>> predicate, object ret)
        {
            mock.Setup(predicate).Returns(ret);

            return mock.Object;
        }

        public static T SetupExceptionMock(Mock<T> mock, Expression<Action<T>> predicate)
        {
            mock.Setup(predicate).Throws(new Exception());

            return mock.Object;
        }

        public static T SetupExceptionReturnMock(Mock<T> mock, Expression<Func<T, object>> predicate)
        {
            mock.Setup(predicate).Throws(new Exception());

            return mock.Object;
        }
    }
}

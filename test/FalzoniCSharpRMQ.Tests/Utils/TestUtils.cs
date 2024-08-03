using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FalzoniCSharpRMQ.Tests.Utils
{
    public static class TestUtils<T, TResult>
        where T : class
        where TResult : class
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

        public static T SetupReturnMockAsync(Mock<T> mock, Expression<Func<T, Task>> predicate, TResult ret)
        {
            mock.Setup(predicate).Returns(Task.FromResult<TResult>(ret));

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

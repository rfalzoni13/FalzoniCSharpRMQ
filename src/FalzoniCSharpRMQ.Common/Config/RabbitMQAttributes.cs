namespace FalzoniCSharpRMQ.Common.Config
{
    public static class RabbitMQAttributes
    {
        // Exchanges
        public const string EXG_DIRECT_NAME = "falzoniexg.direct";
        public const string EXG_FANOUT_NAME = "falzoniexg.fanout";
        public const string EXG_TOPIC_NAME = "falzoniexg.topic";

        //Product
        public const string QUEUE_PRODUCT_DATA = "product.data";
        public const string RK_PRODUCT_DATA = "product.data";

        public const string QUEUE_PRODUCT_LOG = "product.log";
        public const string RK_PRODUCT_LOG = "product.log";
        
        
        public const string RK_PRODUCT_ALL = "product.*";        
    }
}

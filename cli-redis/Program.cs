using System;
using StackExchange.Redis; 

namespace cli_redis
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

            IDatabase db = redis.GetDatabase();
            db.StringSet("key1", "Hello world from redis"); 
            Console.WriteLine(db.StringGet("key1")); 

            redis.Close(); 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace AppCSRedis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IDistributedCache _redis;

        public ValuesController(IDistributedCache distributedCache)
        {
            _redis = distributedCache;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            //使用以来注入读取Redis数据
            //IDistributedCache 存储数据默认使用的Hash
            _redis.SetString("message","少时诵诗书所所所所所所所所所所所所所所所");
            var message = _redis.GetString("message");


            //使用CSRedis提供的静态方法
            var bus23 = RedisHelper.HGetAll("23");

            
            var message2 = RedisHelper.HGet("message","data");


            return new string[] { message, Newtonsoft.Json.JsonConvert.SerializeObject(bus23), message2 };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

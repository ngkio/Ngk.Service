using System;
using Thor.Framework.Data.Model.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ngk.DataAccess.Entities.Mongo
{
    /// <summary>
    /// 监控日志对象
    /// </summary>
    public class MonitorLog : IMongoEntityModel
    {
        public Guid Id { get; set; }
        
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string Path { get; set; }

        public double Elapsed { get; set; }

        /// <summary>
        /// 请求执行开始时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ExecuteStartTime { get; set; }

        /// <summary>
        /// 请求执行结束时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ExecuteEndTime { get; set; }
        
        /// <summary>
        /// 请求的IP地址
        /// </summary>
        public string IP { get; set; }

        public string ClientAgent { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string HttpMethod { get; set; }
        
        /// <summary>
        /// 请求的Action 参数
        /// </summary>
        public string ActionParams { get; set; }

        /// <summary>
        /// 输出参数
        /// </summary>
        public string Response { get; set; }
        
        /// <summary>
        /// Http请求头
        /// </summary>
        public string HttpRequestHeaders { get; set; }

        /// <summary>
        /// 耗时
        /// </summary>
        public double CostTime
        {
            get { return (ExecuteEndTime - ExecuteStartTime).TotalSeconds; }
        }

        /// <summary>
        /// 冗余映射其它字段
        /// </summary>
        [BsonExtraElements]
        public BsonDocument NewElements { get; set; }

        /// <summary>
        /// 获取监控指标日志
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Action执行时间监控：\r\n{0}：{1}\r\n开始时间：{2}\r\n结束时间：{3}\r\n总 时 间：{4}秒\r\n请求参数：{5}\r\n输出参数：{6}\r\nHttp请求头:{7}\r\n客户端IP：{8}",
                HttpMethod,
                Path,
                ExecuteStartTime,
                ExecuteEndTime,
                CostTime,
                ActionParams,
                Response,
                HttpRequestHeaders,
                IP
                );
        }
    }
}

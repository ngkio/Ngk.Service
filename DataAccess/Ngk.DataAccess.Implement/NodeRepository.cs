using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Thor.Framework.Common.Cache;
using Thor.Framework.Common.Pager;
using Thor.Framework.Data;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Repository.Relational;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Ngk.Common.Enum;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class NodeRepository : BaseRepository<Node>, INodeRepository
    {
        private readonly IMemoryCache _cache;
        private Node _node;

        private static int _curIndex;
        private static readonly ConcurrentDictionary<string, DateTime> DelayMap = new ConcurrentDictionary<string, DateTime>();
        private const string CacheKeyFormat = "ngk-chain-node-{0}-{1}";

        public NodeRepository(IDbContextCore dbContext, IMemoryCache memoryCache, IConfiguration configuration) :
            base(dbContext)
        {
            _cache = memoryCache;
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="type">节点类型</param>
        /// <returns>节点</returns>
        public Node GetNode(string chainCode, EnumNodeType type)
        {
            var endpointList = GetNodeList(chainCode, type);
            var endpointCount = endpointList.Count;

            var index = _curIndex >= endpointCount ? 0 : _curIndex;
            for (var i = 0; i < endpointCount; i++)
            {
                _node = endpointList[index];

                if (index + 1 >= endpointCount)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }

                if (DelayMap.ContainsKey(_node.HttpAddress))
                {
                    if (DelayMap[_node.HttpAddress] <= DateTime.UtcNow)
                    {
                        DelayMap.TryRemove(_node.HttpAddress, out _);
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            _curIndex = index;
            Console.WriteLine(_node.HttpAddress);

            return _node;
        }

        /// <summary>
        /// 节点Api错误
        /// </summary>
        public void ApiError()
        {
            _node.ErrorCount++;

            try
            {
                Update(_node);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 节点Api异常
        /// </summary>
        public void ApiException()
        {
            if (!DelayMap.ContainsKey(_node.HttpAddress))
            {
                DelayMap.TryAdd(_node.HttpAddress, DateTime.UtcNow.AddMinutes(1));
            }

            ApiError();
        }

        public override int Add(Node entity, bool withTrigger = false)
        {
            return base.Add(entity, withTrigger);
        }

        protected override IQueryable<Node> GetAdvQuery<TQueryParam>(TQueryParam queryParam)
        {
            var result = base.GetAdvQuery(queryParam).Where(p => p.State == (int)EnumState.Normal);
            if (queryParam is NodeParam)
            {
                var param = queryParam as NodeParam;

                if (!string.IsNullOrEmpty(param.Name))
                {
                    result = result.Where(p => p.Name.Contains(param.Name));
                }
                if (!string.IsNullOrEmpty(param.ChainCode))
                {
                    result = result.Where(p => p.ChainCode == param.ChainCode);
                }
                if (!string.IsNullOrEmpty(param.HttpAddress))
                {
                    result = result.Where(p => p.HttpAddress.Contains(param.HttpAddress));
                }
                if (!string.IsNullOrEmpty(param.Address))
                {
                    result = result.Where(p => p.Address.Contains(param.Address));
                }
                if (param.IsSuper.HasValue)
                {
                    result = result.Where(p => p.IsSuper == param.IsSuper.Value);
                }
                if (param.PlayerAlternative.HasValue)
                {
                    result = result.Where(p => p.PlayerAlternative == param.PlayerAlternative.Value);
                }
                if (param.QueryAlternative.HasValue)
                {
                    result = result.Where(p => p.QueryAlternative == param.QueryAlternative.Value);
                }
                if (param.ServerAlternative.HasValue)
                {
                    result = result.Where(p => p.ServerAlternative == param.ServerAlternative.Value);
                }
            }
            return result;
        }



        /// <summary>
        /// 获取节点合集
        /// </summary>
        /// <param name="chainCode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Node> GetNodeList(string chainCode, EnumNodeType type)
        {
            var key = string.Format(CacheKeyFormat, chainCode, type);
            if (CacheManager.TryGet<List<Node>>(key, out var result) && result.Any())
            {
                return result;
            }
            return RefreshNodeList(chainCode, type);
        }


        /// <summary>
        /// 刷新节点列表缓存
        /// </summary>
        /// <param name="chainCode"></param>
        /// <param name="type"></param>
        public List<Node> RefreshNodeList(string chainCode, EnumNodeType type)
        {
            var key = string.Format(CacheKeyFormat, chainCode, type);

            var query = DbSet.Where(p => p.ChainCode == chainCode && p.State == (int)EnumState.Normal)
                .AsQueryable();
            switch (type)
            {
                case EnumNodeType.Player:
                    query = query.Where(p => p.PlayerAlternative);
                    break;
                case EnumNodeType.Query:
                    query = query.Where(p => p.QueryAlternative);
                    break;
                case EnumNodeType.Server:
                default:
                    query = query.Where(p => p.ServerAlternative);
                    break;
            }

            var result = query.OrderByDescending(p => p.Priority).ToList();

            try
            {
                if (result.Any())
                {
                    CacheManager.Set(key, result, 30);
                    CacheManager.Refresh(key);
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return result;
        }


        /// <summary>
        /// 分页查询节点管理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedResults<Node> QueryNode(NodeParam model)
        {
            var iQueryable = GetAdvQuery(model);
            return iQueryable.ToPagedResults<Node, Node>(model);
        }
    }
}



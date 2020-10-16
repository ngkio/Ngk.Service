using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.Interface;
using Thor.Framework.Business.Relational;
using Thor.Framework.Common.IoC;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using Newtonsoft.Json;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.Common.Enum;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.DataAccess.Interface;
using Ngk.DataAccess.Interface.Mongo;

namespace Ngk.Business.Implement
{
    public class NodeLogic : BaseBusinessLogic<Node, INodeRepository>, INodeLogic
    {
        private readonly IOperateLogRepository _operateLogRepository;
        private readonly ContractClientFactory _contractFactory;
        #region ctor
        public NodeLogic(INodeRepository repository, IOperateLogRepository operateLogRepository, ContractClientFactory contractFactory) : base(repository)
        {
            _operateLogRepository = operateLogRepository;
            _contractFactory = _contractFactory = (ContractClientFactory)AspectCoreContainer.CreateScope().Resolve(typeof(ContractClientFactory)); ;
        }
        #endregion

        public override void Create(Node entity, out ExcutedResult result)
        {
            entity.State = (int)EnumState.Normal;
            //entity.DeleteIp = "";
            //entity.Deleter = "";
            base.Create(entity, out result);
            result.Data = entity.Id;
        }

        public override void Update(Node entity, out ExcutedResult result)
        {
            var model = Repository.GetSingle(entity.Id);
            if (model == null)
            {
                result = ExcutedResult.FailedResult(BusinessResultCode.ArgumentError, "实体未找到");
                return;
            }
            model.Name = entity.Name;
            model.ChainCode = entity.ChainCode;
            model.HttpAddress = entity.HttpAddress;
            model.IsSuper = entity.IsSuper;
            model.Address = entity.Address;
            model.ErrorCount = entity.ErrorCount;
            model.TimeOut = entity.TimeOut;
            model.Priority = entity.Priority;
            model.QueryAlternative = entity.QueryAlternative;
            model.PlayerAlternative = entity.PlayerAlternative;
            model.ServerAlternative = entity.ServerAlternative;
            Repository.Update(model);
            result = ExcutedResult.SuccessResult();
        }

        public override void Delete(Guid id, out ExcutedResult result)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                result = ExcutedResult.FailedResult(BusinessResultCode.ArgumentError, "节点不存在");
                return;
            }
            entity.State = (int)EnumState.Deleted;
            Repository.Update(entity);
            result = ExcutedResult.SuccessResult();
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="chainCode"></param>
        /// <param name="type">节点类型</param>
        /// <returns>节点</returns>
        public Node GetNode(string chainCode, EnumNodeType type)
        {
            var list = Repository.GetNode(chainCode, type);
            return list;
        }

        /// <summary>
        /// 获取节点合集
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Node> GetNodeList(string chainCode, EnumNodeType type)
        {
            var list = Repository.GetNodeList(chainCode, type);
            return list;
        }

        /// <summary>
        /// 更新错误数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExcutedResult AddErrorCount(Guid id)
        {
            var point = GetById(id);
            if (point == null)
                return ExcutedResult.FailedResult(BusinessResultCode.DataNotExist, "数据不存在，请刷新!");
            point.ErrorCount += 1;
            Update(point, out var excuted);
            return excuted;
        }

        public ExcutedResult QueryNodeInfos(NodeParam model)
        {
            try
            {
                if (model == null) return ExcutedResult.FailedResult(BusinessResultCode.ArgumentError, "参数错误或无效");
                if (String.IsNullOrEmpty(model.SortName)) model.SortName = "CreateTime";
                var dataInfo = Repository.QueryNode(model);
                if (dataInfo != null)
                {
                    return ExcutedResult.SuccessResult(dataInfo);
                }
                return ExcutedResult.FailedResult(BusinessResultCode.DataNotExist, "数据不存在，请刷新!");
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 插入新的节点
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExcutedResult InsertNode(InsertNodeRequestModel model)
        {
            try
            {
                var user = CurrentUser;
                if (model == null) throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
                model.Vefity();
                Node node = new Node()
                {
                    Id = Guid.NewGuid(),
                    ChainCode = "EOS",
                    Name = model.Name,
                    HttpAddress = model.HttpAddress,
                    IsSuper = model.IsSuper,
                    Address = model.Address,
                    ErrorCount = model.ErrorCount,
                    TimeOut = model.TimeOut,
                    Priority = model.Priority,
                    QueryAlternative = model.QueryAlternative,
                    PlayerAlternative = model.PlayerAlternative,
                    ServerAlternative = model.ServerAlternative,
                    Deleter = string.Empty,
                    CreateTime = DateTime.UtcNow,
                    DeleteTime = default(DateTime),
                    State = (int)EnumState.Normal,
                    DeleteIp = String.Empty,
                };
                Repository.Add(node);

                OperateLog log = new OperateLog();
                log.Id = Guid.NewGuid();
                log.ClientIp = user.ClientIP;
                log.CreateTime = DateTime.UtcNow;
                log.ManagerAccount = user.Mobile;
                log.ManagerId = user.Id;
                log.OriginalValue = String.Empty;
                log.NewValue = JsonConvert.SerializeObject(node);
                log.Operate = "Insert";
                log.Function = "插入新的节点";
                _operateLogRepository.Insert(log);

                return ExcutedResult.SuccessResult();
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 更新节点信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExcutedResult UpdateNode(UpdateNodeRequestModel model)
        {
            try
            {
                var user = CurrentUser;
                if (model == null) throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
                model.Vefity();
                var node = Repository.GetSingle(model.Id);
                var oldNode = node;
                if (!String.IsNullOrEmpty(model.Name)) node.Name = model.Name;
                if (!String.IsNullOrEmpty(model.HttpAddress)) node.HttpAddress = model.HttpAddress;
                node.IsSuper = model.IsSuper;
                if (model.TimeOut == default(int)) node.TimeOut = model.TimeOut;
                if (model.Priority == default(int)) node.Priority = model.Priority;
                node.QueryAlternative = model.QueryAlternative;
                node.PlayerAlternative = model.PlayerAlternative;
                node.ServerAlternative = model.ServerAlternative;
                if (!String.IsNullOrEmpty(model.Address)) node.Address = model.Address;

                Repository.Update(node);

                OperateLog log = new OperateLog();
                log.Id = Guid.NewGuid();
                log.ClientIp = user.ClientIP;
                log.CreateTime = DateTime.UtcNow;
                log.ManagerAccount = user.Mobile;
                log.ManagerId = user.Id;
                log.OriginalValue = JsonConvert.SerializeObject(oldNode);
                log.NewValue = JsonConvert.SerializeObject(node);
                log.Operate = "Update";
                log.Function = "更新节点信息";
                _operateLogRepository.Insert(log);

                return ExcutedResult.SuccessResult();
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 删除节点信息
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public ExcutedResult DeleteNode(string nodeId)
        {
            try
            {
                var user = CurrentUser;
                var guid = Guid.Empty;
                if (String.IsNullOrEmpty(nodeId) || !Guid.TryParse(nodeId, out guid)) throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
                var node = Repository.GetSingle(guid);
                if (node == null) throw new BusinessException(BusinessResultCode.DataNotExist, "数据不存在，请刷新!");
                node.DeleteIp = CurrentUser.ClientIP;
                node.DeleteTime = DateTime.UtcNow;
                node.Deleter = CurrentUser.NickName;
                node.State = (int)EnumState.Deleted;
                Repository.Update(node);

                OperateLog log = new OperateLog();
                log.Id = Guid.NewGuid();
                log.ClientIp = user.ClientIP;
                log.CreateTime = DateTime.UtcNow;
                log.ManagerAccount = user.Mobile;
                log.ManagerId = user.Id;
                log.OriginalValue = JsonConvert.SerializeObject(node);
                log.NewValue = String.Empty;
                log.Operate = "Delete";
                log.Function = "删除节点信息";
                _operateLogRepository.Insert(log);

                return ExcutedResult.SuccessResult();
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ExcutedResult> GetSpeed(Guid id)
        {
            var point = Repository.GetSingle(id);
            if (point == null)
                return ExcutedResult.FailedResult("", "");
            try
            {
                var contract = _contractFactory.GetService<IInfoClient>(point.ChainCode);
                var asynResult = await contract.TestNetSpeed(point.TimeOut, point.HttpAddress);
                var result = asynResult;
                if (result.Status != EnumStatus.Success)
                {
                    point.ErrorCount += 1;
                    Repository.Update(point);
                }
                return result;
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }
    }
}



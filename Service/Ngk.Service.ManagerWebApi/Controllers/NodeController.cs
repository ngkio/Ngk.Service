using System;
using System.Threading.Tasks;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Ngk.Business.Interface;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.DTO.Param;
using Ngk.Service.ManagerWebApi.Attribute;

namespace Ngk.Service.ManagerWebApi.Controllers
{
    /// <summary>
    /// 后端节点控制器
    /// </summary>
    [Language]
    public class NodeController : Controller
    {
        private readonly INodeLogic _nodeLogic;

        public NodeController(INodeLogic nodeLogic)
        {
            _nodeLogic = nodeLogic;
        }

        public IActionResult Index()
        {
            return null;
        }

        /// <summary>
        /// 查询节点信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult QueryNodeInfos([FromBody] NodeParam model)
        {
            return _nodeLogic.QueryNodeInfos(model);
        }

        /// <summary>
        /// 查润新的节点
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult InsertNode([FromBody] InsertNodeRequestModel model)
        {
            return _nodeLogic.InsertNode(model);
        }


        [HttpPost]
        public ExcutedResult UpdateNode([FromBody] UpdateNodeRequestModel model)
        {
            return _nodeLogic.UpdateNode(model);
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        [HttpGet]
        public ExcutedResult DeleteNode(String nodeId)
        {
            return _nodeLogic.DeleteNode(nodeId);
        }


        [HttpGet]
        public Task<ExcutedResult> GetSpeed(Guid id)
        {
            return _nodeLogic.GetSpeed(id);
        }
    }
}
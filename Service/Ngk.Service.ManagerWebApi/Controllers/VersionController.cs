using System;
using Thor.Framework.Common.Helper;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.DataAccess.DTO.Manager;
using Ngk.DataAccess.DTO.Param;
using Ngk.Service.ManagerWebApi.Attribute;
using Version = Ngk.DataAccess.Entities.Version;

namespace Ngk.Service.ManagerWebApi.Controllers
{
    [Language]
    public class VersionController : Controller
    {
        private readonly IVersionLogic _versionLogic;

        public VersionController(IVersionLogic versionLogic)
        {
            _versionLogic = versionLogic;

        }

        /// <summary>
        /// 版本控制列表（分页）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="number"></param>
        /// <param name="clientType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortName"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpGet]
        public ExcutedResult GetQuery(string name, int? number, int? clientType, int pageIndex = 1, int pageSize = 10, string sortName = "", bool? order = null)
        {

            try
            {
                VersionParam param = new VersionParam()
                {
                    Name = name,
                    Number = number,
                    ClientType = clientType,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    SortName = "date",
                    IsSortOrderDesc = true
                };
                if (!string.IsNullOrEmpty(sortName))
                {
                    param.SortName = sortName;
                }
                if (order.HasValue)
                {
                    param.IsSortOrderDesc = order.Value;
                }
                var result = _versionLogic.GetQuery(param);
                return ExcutedResult.SuccessResult(result);
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }


        /// <summary>
        /// 添加版本控制
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult Create([FromBody] VersionCreateRequest model)
        {
            try
            {
                var versionModle = _versionLogic.GetCurrentVersion(model.ClientType);
                if (versionModle != null)
                {

                    if (model.Number <= versionModle.Number)
                        return ExcutedResult.FailedResult(BusinessResultCode.VersionIsSmallError, "当前版本号小于历史版本号");
                }
                //VersionCreateRequest two = JsonConvert.DeserializeObject<VersionCreateRequest>(obj.ToString());

                //  return null;
                //  var model = new VersionEditRequest();
                var entity = MapperHelper<VersionCreateRequest, Version>.Map(model);
                _versionLogic.Create(entity, out var result);
                return result;
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }
        /// <summary>
        /// 修改版本控制
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult Update([FromBody] VersionEditRequest model)
        {
            try
            {
                var versionModle = _versionLogic.GetCurrentVersion(model.ClientType);
                if (versionModle != null)
                {

                    if (model.Number < versionModle.Number)
                        return ExcutedResult.FailedResult(BusinessResultCode.VersionIsSmallError, "当前版本号小于历史版本号");
                }
                //VersionEditRequest two = JsonConvert.DeserializeObject<VersionEditRequest>(obj.ToString());

                //var model = new VersionEditRequest();
                _versionLogic.Update(model, out var result);
                return result;
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }
        /// <summary>
        /// 删除版本控制
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult Delete(Guid id)
        {
            try
            {
                _versionLogic.DeleteLogic(id, out var result);
                return result;
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }
    }
}
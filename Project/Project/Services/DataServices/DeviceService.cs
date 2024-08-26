using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectViewModels;
using Project.Common;
using System.Collections;

namespace Project.Services.DataServices
{
    public class DeviceService
    {
        private readonly IRestClient restClient;
        public DeviceService(IRestClient client)
        {
            restClient = client;
        }

        public async Task<List<DeviceTypeVm>> GetDeviceTypeList()
        {
            return await RestClientHelper.ExecuteRequestAsync<List<DeviceTypeVm>>(restClient, Method.Get, ApiSettings.DeviceTypeList);
        }

        public async Task<List<DeviceVm>> GetProjectDeviceList(Guid projectId)
        {
            return await RestClientHelper.ExecuteRequestAsync<List<DeviceVm>>(restClient, Method.Get, ApiSettings.ProjectDeviceByProjectId, 
                queryParameters: new Dictionary<string,string>
                {
                    { "projectId",projectId.ToString() }
                });
        }
        public async Task<List<DeviceVm>> GetDeviceList(DeviceReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<List<DeviceVm>>(restClient, Method.Post, ApiSettings.DeviceList, body: req);
        }
        public async Task<PaginatedList<DeviceStockVm>> DevicePaginatedList(DeviceReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<DeviceStockVm>>(restClient, Method.Post, ApiSettings.DevicePaginatedList, body: req);
        }

        public async Task<List<DeviceVm>> GetDeviceListLocal(DeviceReqs req, List<DeviceVm> list)
        {
            return await Task.Run(() =>
            {
                if (req.DeviceTypeId != null)
                    list = list.Where(m => m.DeviceTypeId == req.DeviceTypeId).ToList();

                if (!string.IsNullOrWhiteSpace(req.Content))
                    list = list.Where(m => m.DeviceType.DeviceTypeName.Contains(req.Content) || (m.DeviceType.DeviceModel != null && m.DeviceType.DeviceModel.Contains(req.Content)) || m.DeviceNumber.Contains(req.Content)).ToList();
                if (req.Status != null)
                    list = list.Where(m => m.DeviceStatus == req.Status).ToList();

                list = list.OrderBy(o => o.DeviceNumber).ToList();
                return list;
            });            
        }

        public async Task<bool> SaveProjectDevice(ProjectVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveProjectDevice, body: vm);
        }

        public async Task<bool> SaveDevice(DeviceVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveDevice, body: vm);
        }

        public async Task<DeviceVm> GetDeviceById(Guid deviceId)
        {
            return await RestClientHelper.ExecuteRequestAsync<DeviceVm>(restClient, Method.Get, ApiSettings.GetDeviceById, queryParameters: new Dictionary<string, string>
            {
                { nameof(deviceId), deviceId.ToString() }
            });
        }

        public async Task<bool> RemoveDevice(Guid deviceId)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Get, ApiSettings.RemoveDevice, queryParameters: new Dictionary<string, string>
            {
                { nameof(deviceId), deviceId.ToString() }
            });
        }

        public async Task<PaginatedList<ProjectDeviceVm>> PaginatedProjectDeviceHistory(DeviceReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<ProjectDeviceVm>>(restClient, Method.Post, ApiSettings.PaginatedProjectDeviceHistory, body: req);
        }

        public async Task<PaginatedList<DeviceTypeVm>> PaginatedDeviceType(CommonReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<DeviceTypeVm>>(restClient, Method.Post, ApiSettings.PaginatedDeviceType, body: req);
        }

        public async Task<DeviceTypeVm?> GetDeviceTypeById(Guid deviceTypeId)
        {
            return await RestClientHelper.ExecuteRequestAsync<DeviceTypeVm?>(restClient, Method.Get, ApiSettings.GetDeviceTypeById, queryParameters: new Dictionary<string, string>
            {
                { nameof(deviceTypeId), deviceTypeId.ToString() }
            });
        }

        public async Task<bool> SaveDeviceType(DeviceTypeVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveDeviceType, body: vm);
        }
        public async Task<bool> BatchSaveDevice(BatchSaveDeviceReq req)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.BatchSaveDevice, body: req);
        }
        public async Task<bool> SetHandleBy(Guid deviceId, Guid staffId)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Get, ApiSettings.SetHandleBy, queryParameters:
                new Dictionary<string, string>
                {
                    { nameof(deviceId), deviceId.ToString() },
                    { nameof(staffId), staffId.ToString() } 
                });
        }
    }
}

using Microsoft.Win32;
using Project.Common;
using ProjectViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.DataServices
{
    public class AttachmentsService
    {
        private readonly IRestClient restClient;
        public AttachmentsService(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<bool> UploadMultipleFilesAsync(IEnumerable<string> filePaths, Guid projectId)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.UploadMultiple, files: filePaths,
                queryParameters: new Dictionary<string, string>
                {
                    { nameof(projectId), projectId.ToString() }
                });
        }

        public async Task<List<ProjectAttachmentVm>> GetProjectAttachments(Guid projectId)
        {
            return await RestClientHelper.ExecuteRequestAsync<List<ProjectAttachmentVm>>(restClient, Method.Get, ApiSettings.GetProjectAttachments,
                queryParameters: new Dictionary<string, string>
                {
                    { nameof(projectId), projectId.ToString() }
                });
        }

        public async Task<bool> RemoveProjectAttachment(ProjectAttachmentVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient,Method.Post,ApiSettings.RemoveProjectAttachment,body:vm);
        }

        public async Task<PaginatedList<ProjectAttachmentVm>> PaginatedProjectAttachments(ProjectReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<ProjectAttachmentVm>>(restClient, Method.Post, ApiSettings.PaginatedProjectAttachments, body: req);
        }

        public async Task<bool> DownloadAttachment(ProjectAttachmentVm vm) 
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "所有文件 (*.*)|*.*";
            saveFileDialog.FileName = vm.FileName;
            saveFileDialog.Title = "保存文件";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                var baseUrl = App.Current.Properties[MessageToken.RestClientBaseUrl] as string;               
                //vm.FileAddress = $"{baseUrl}{vm.FileAddress}";                
                string url = $"{baseUrl}{vm.FileAddress}";
                string filePath = saveFileDialog.FileName;

                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                    {
                        response.EnsureSuccessStatusCode();

                        using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
                        {
                            using (Stream streamToWriteTo = File.Open(filePath, FileMode.Create))
                            {
                                await streamToReadFrom.CopyToAsync(streamToWriteTo);
                            }
                        }
                    }
                }
                return true;                
            }
            return false;
        }

        public async Task<List<AttachmentRequirementVm>> GetAttachmentRequirementList()
        {
            return await RestClientHelper.ExecuteRequestAsync<List<AttachmentRequirementVm>>(restClient, Method.Get, ApiSettings.GetAttachmentRequirementList);
        }

        public async Task<bool> SavePlaceOnFile(PlaceOnFileVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SavePlaceOnFile, body: vm);
        }

        public async Task<PaginatedList<PlaceOnFileVm>> PaginatedApplyPlaceOnFileProject(ProjectReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<PlaceOnFileVm>>(restClient, Method.Post, ApiSettings.PaginatedApplyPlaceOnFileProject, body: req);
        }
    }
}

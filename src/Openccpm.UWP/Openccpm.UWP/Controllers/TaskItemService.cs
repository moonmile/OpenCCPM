using Openccpm.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Openccpm.UWP.Controllers
{
    public class TaskItemService
    {
        protected string _url;
        public TaskItemService(string url)
        {
            _url = url;
        }

        public async Task<List<TaskItem>> GetItems()
        {
            var hc = new HttpClient();
            var res = await hc.GetAsync(_url + $"/api/Task");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var items = js.Deserialize<List<TaskItem>>(jr);
            return items;
        }
        public async Task<TaskItem> GetItem(string id)
        {
            var hc = new HttpClient();
            var res = await hc.GetAsync(_url + $"/api/Task/{id}");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var item = js.Deserialize<TaskItem>(jr);
            return item;
        }

        public async Task<TaskItem> AddItem(TaskItem item)
        {
            var hc = new HttpClient();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await hc.PostAsync(_url + $"/api/Task", cont);

            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var newItem = js.Deserialize<TaskItem>(jr);
            return newItem;
        }
        public async Task UpdateItem(TaskItem item)
        {
            var hc = new HttpClient();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await hc.PutAsync(_url + $"/api/Task/{item.Id}", cont);
        }
        public async Task DeleteItem(string id)
        {
            var hc = new HttpClient();
            var res = await hc.DeleteAsync(_url + $"/api/Task/{id}");
        }


        /// <summary>
        /// 指定のタスクIDの開始/終了時間を取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<StartEndTime>> GetStartEnd(string id)
        {
            var hc = new HttpClient();
            var res = await hc.GetAsync(_url + $"/api/StartEnd/Task/{id}");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var items = js.Deserialize<List<StartEndTime>>(jr);
            return items;
        }

        /// <summary>
        /// 開始/終了日時を追加する
        /// </summary>
        /// <param name="id"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="isplan"></param>
        /// <returns></returns>
        public async Task<StartEndTime> AddStartEnd(string id, DateTime? start, DateTime? end, bool isplan)
        {
            var item = new StartEndTime()
            {
                TaskId = id,
                StartAt = start,
                EndAt = end,
                IsPlan = isplan
            };
            var hc = new HttpClient();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await hc.PostAsync(_url + $"/api/StartEnd", cont);
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var newItem = js.Deserialize<StartEndTime>(jr);
            return newItem;
        }

        /// <summary>
        /// 開始/終了日時を更新する
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task UpdateStartEnd(StartEndTime item)
        {
            var hc = new HttpClient();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await hc.PutAsync(_url + $"/api/StartEnd/{item.Id}", cont);
        }
        /// <summary>
        /// 最初の開始/終了日時を更新する
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task UpdateStartEnd(string id, DateTime? start, DateTime? end, bool isplan)
        {
            var items = await GetStartEnd(id);
            items = items.Where(x => x.IsPlan).ToList();

            if (items.Count == 0)
            {
                // 新規に作成する
                await AddStartEnd(id, start, end, isplan);
            }
            else
            {
                var item = items[0];
                item.StartAt = start;
                item.EndAt = end;
                await UpdateStartEnd(item);
            }
        }
    }


    /// <summary>
    /// チケット駆動サービス
    /// </summary>
    public class TicketService : TaskItemService
    {
        public TicketService(string url) : base(url)
        {
        }
        public async Task<List<Ticket>> GetTickets()
        {
            var hc = new HttpClient();
            var res = await hc.GetAsync(_url + $"/api/Ticket");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var items = js.Deserialize<List<Ticket>>(jr);
            return items;
        }
        public async Task<Ticket> GetTicket(string id)
        {
            var hc = new HttpClient();
            var res = await hc.GetAsync(_url + $"/api/Ticket/{id}");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var item = js.Deserialize<Ticket>(jr);
            return item;
        }
        public async Task<Ticket> AddTicket(Ticket item)
        {
            var hc = new HttpClient();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await hc.PostAsync(_url + $"/api/Ticket", cont);

            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var newItem = js.Deserialize<Ticket>(jr);
            return newItem;
        }
        public async Task UpdateTicket(Ticket item)
        {
            var hc = new HttpClient();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await hc.PutAsync(_url + $"/api/Ticket/{item.Id}", cont);
        }
        public async Task DeleteTicket(string id)
        {
            var hc = new HttpClient();
            var res = await hc.DeleteAsync(_url + $"/api/Ticket/{id}");
        }
    }
    /// <summary>
    /// WBS作成サービス
    /// </summary>
    public class WbsService : TaskItemService
    {
        public WbsService(string url) : base(url)
        {
        }
        public async Task<List<WbsView>> GetWbses()
        {
            var hc = new HttpClient();
            var res = await hc.GetAsync(_url + $"/api/WBS");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var items = js.Deserialize<List<WbsView>>(jr);
            return items;
        }
        public async Task<WbsView> GetWbs(string id)
        {
            var hc = new HttpClient();
            var res = await hc.GetAsync(_url + $"/api/WBS/{id}");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var item = js.Deserialize<WbsView>(jr);
            return item;
        }
    }


    /// <summary>
    /// ProjectItem/Viewテスト用サービス
    /// </summary>
    public class ProjectService : TaskItemService
    {
        public ProjectService(string url) : base(url)
        {
        }
        public async Task<List<ProjectItem>> GetProjectItem()
        {
            var hc = new HttpClient();
            var res = await hc.GetAsync(_url + $"/api/ProjectItem");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var items = js.Deserialize<List<ProjectItem>>(jr);
            return items;
        }
        public async Task<ProjectItem> GetProjectItem(int id)
        {
            var hc = new HttpClient();
            var res = await hc.GetAsync(_url + $"/api/ProjectItem/{id}");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var item = js.Deserialize<ProjectItem>(jr);
            return item;
        }
        public async Task<List<ProjectView>> GetProjectView()
        {
            var hc = new HttpClient();
            var res = await hc.GetAsync(_url + $"/api/ProjectView");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var items = js.Deserialize<List<ProjectView>>(jr);
            return items;
        }
        public async Task<ProjectView> GetProjectView(int id)
        {
            var hc = new HttpClient();
            var res = await hc.GetAsync(_url + $"/api/ProjectView/{id}");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var item = js.Deserialize<ProjectView>(jr);
            return item;
        }
    }

}

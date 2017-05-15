﻿using Openccpm.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Openccpm.UWP.Controllers
{

    /// <summary>
    /// JSONを扱うRESTfulな拡張クラス
    /// </summary>
    public static class HttpClientJsonExtensions
    {
        public static async Task<T> GetAsync<T>( this HttpClient client, string requestUri)
        {
            var res = await client.GetAsync(requestUri);
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var items = js.Deserialize<T>(jr);
            return items;
        }
        public static async Task<T> PostAsync<T>(this HttpClient client, string requestUri, T value)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PostAsync(requestUri, cont);
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var newItem = js.Deserialize<T>(jr);
            return newItem;
        }
        public static async Task PutAsync<T>(this HttpClient client, string requestUri, T value)
        {
            var hc = new HttpClient();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await hc.PutAsync(requestUri, cont);
            return;
        }
        public static async Task DeleteAsync(this HttpClient client, string requestUri)
        {
            var res = await client.DeleteAsync(requestUri);
            return;
        }
    }

    /// <summary>
    /// テーブルアクセスサービス
    /// </summary>
    /// <typeparam name="T">EntityDataを継承するエンティティ</typeparam>
    public abstract class TableWebApiService<T> where T : EntityData
    {
        /// <summary>
        /// Web API の URL
        /// </summary>
        protected string _url;
        /// <summary>
        /// アクセス先のテーブル名
        /// api/_tableName で呼び出す
        /// </summary>
        protected string _tableName;

        public TableWebApiService( string url, string tableName )
        {
            _url = url;
            _tableName = tableName;
        }

        /// <summary>
        /// 一覧リストを取得
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetItemsAsync()
        {
            var hc = new HttpClient();
            var res = await hc.GetAsync($"{_url}/api/{_tableName}");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var items = js.Deserialize<List<T>>(jr);
            return items;
        }
        /// <summary>
        /// 指定IDのアイテムを取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(string id)
        {
            var hc = new HttpClient();
            var res = await hc.GetAsync($"{_url}/api/{_tableName}/{id}");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var item = js.Deserialize<T>(jr);
            return item;
        }
        /// <summary>
        /// アイテムを追加する
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<T> AddAsync(T item)
        {
            var hc = new HttpClient();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await hc.PostAsync($"{_url}/api/{_tableName}", cont);
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var newItem = js.Deserialize<T>(jr);
            return newItem;
        }
        /// <summary>
        /// 指定アイテムを更新する
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T item)
        {
            var hc = new HttpClient();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await hc.PutAsync($"{_url}/api/{_tableName}/{item.Id}", cont);
        }
        /// <summary>
        /// 指定IDを削除する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string id)
        {
            var hc = new HttpClient();
            var res = await hc.DeleteAsync($"{_url}/api/{_tableName}/{id}");
        }
    }


    /// <summary>
    /// 読み取り専用テーブルアクセスサービス
    /// </summary>
    /// <typeparam name="T">EntityDataを継承するエンティティ</typeparam>
    public abstract class ReadOnlyTableWebApiService<T> 
    {
        /// <summary>
        /// Web API の URL
        /// </summary>
        protected string _url;
        /// <summary>
        /// アクセス先のテーブル名
        /// api/_tableName で呼び出す
        /// </summary>
        protected string _tableName;

        public ReadOnlyTableWebApiService(string url, string tableName)
        {
            _url = url;
            _tableName = tableName;
        }

        /// <summary>
        /// 一覧リストを取得
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetItemsAsync()
        {
            var hc = new HttpClient();
            var res = await hc.GetAsync($"{_url}/api/{_tableName}");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var items = js.Deserialize<List<T>>(jr);
            return items;
        }
        /// <summary>
        /// 指定IDのアイテムを取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(string id)
        {
            var hc = new HttpClient();
            var res = await hc.GetAsync($"{_url}/api/{_tableName}/{id}");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var item = js.Deserialize<T>(jr);
            return item;
        }
    }
    /// <summary>
    /// プロジェクト情報へのアクセス
    /// </summary>
    public class ProjectService : TableWebApiService<Project>
    {
        public ProjectService(string url) : base(url, "Project")
        {
        }
    }
    /// <summary>
    /// チケット情報へのアクセス
    /// </summary>
    public class TicketService : TableWebApiService<TicketView>
    {
        public TicketService(string url) : base(url, "Ticket")
        {
        }
        public new List<TicketView> GetItemsAsync()
        {
            // 全てのチケットを取得することはできない
            throw new Exception("cannot get all tickets");
        }
        /// <summary>
        /// プロジェクトIDを指定してチケットを取得する
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<List<TicketView>> GetItemsAsync( string projectId )
        {
            var hc = new HttpClient();
            var items = await hc.GetAsync<List<TicketView>>($"{_url}/api/Ticket/Project/{projectId}");
            return items;
        }
    }

    // 以下はマスターデータ用
    /// <summary>
    /// トラッカー情報アクセス
    /// </summary>
    public class TrackerService : TableWebApiService<Tracker>
    {
        public TrackerService(string url) : base(url, "Tracker")
        {
        }
        public async Task<List<Tracker>> GetItemsAsync(string projectId)
        {
            return await GetItemsAsync();
        }
    }
    /// <summary>
    /// ステータス情報アクセス
    /// </summary>
    public class StatusService : TableWebApiService<Status>
    {
        public StatusService(string url) : base(url, "Status")
        {
        }
        public async Task<List<Status>> GetItemsAsync(string projectId)
        {
            return await GetItemsAsync();
        }
    }
    /// <summary>
    /// 優先度情報アクセス
    /// </summary>
    public class PriorityService : TableWebApiService<Priority>
    {
        public PriorityService(string url) : base(url, "Priority")
        {
        }
        public async Task<List<Priority>> GetItemsAsync(string projectId)
        {
            return await GetItemsAsync();
        }
    }
    /// <summary>
    /// ユーザー情報アクセス
    /// </summary>
    public class UserService   : ReadOnlyTableWebApiService<User>
    {
        public UserService(string url) : base(url, "User")
        {
            _url = url;
        }
        public async Task<List<User>> GetItemsAsync(string projectId)
        {
            return await GetItemsAsync();
        }
    }


    /// <summary>
    /// チケット駆動用のサービス
    /// </summary>
    public class TicketDrivenService
    {
        private string _url;

        public ProjectService Project { get; set; }
        public TicketService Ticket { get; set; }

        public List<Status>     ListStatus { get; private set; }
        public List<Tracker>    ListTracker { get; private set; }
        public List<Priority>   ListPriority { get; private set; }
        public List<User>       ListAssignTo { get; private set; }

        /// <summary>
        /// 操作対象のプロジェクト
        /// </summary>
        public Project TargetProject { get; set; }

        public TicketDrivenService( string url )
        {
            this._url = url;
            this.Project = new ProjectService(url);
            this.Ticket = new TicketService(url);
            this.TargetProject = null;              // 未定義状態
        }

        /// <summary>
        /// プロジェクト内で利用するトラッカー、ステータス、優先度、担当者のリストを取得
        /// </summary>
        /// <returns></returns>
        public async Task Initalize(string projectId)
        {
            this.ListTracker = await new TrackerService(this._url).GetItemsAsync(projectId);
            this.ListStatus = await new StatusService(this._url).GetItemsAsync(projectId);
            this.ListPriority = await new PriorityService(this._url).GetItemsAsync(projectId);
            this.ListAssignTo = await new UserService(this._url).GetItemsAsync(projectId);
        }


        /// <summary>
        /// プロジェクト番号を指定してチケット一覧を取得
        /// </summary>
        /// <param name="projectNo"></param>
        /// <returns></returns>
        public async Task<List<TicketView>> GetTicketsAsync(string projectNo = null)
        {
            TargetProject = await this.Project.GetAsync(projectNo);
            return await Ticket.GetItemsAsync(TargetProject.Id);
        }
        /// <summary>
        /// チケットIDを指定して詳細情報を取得
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TicketView> GetTicketAsync(string Id )
        {
            return await Ticket.GetAsync(Id);
        }
        /// <summary>
        /// 新規チケットを追加
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<TicketView> AddTicketAsync( TicketView item )
        {
            return await Ticket.AddAsync(item);
        }
        /// <summary>
        /// 既存チケットを更新
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<TicketView> UpdateTicketAsync(TicketView item)
        {
            await Ticket.UpdateAsync(item);
            return item;
        }
        /// <summary>
        /// 既存チケットを削除
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task DeleteTicketAsync(string Id)
        {
            await Ticket.DeleteAsync(Id);
        }
    }





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
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Oqtane.Services;
using Oqtane.Module.Blogs.Models;
using Oqtane.Shared;

namespace Oqtane.Module.Blogs.Services
{
    public class BlogService : ServiceBase, IBlogService
    {
        private readonly HttpClient http;
        private readonly SiteState sitestate;
        private readonly NavigationManager NavigationManager;

        public BlogService(HttpClient http, SiteState sitestate, NavigationManager NavigationManager)
        {
            this.http = http;
            this.sitestate = sitestate;
            this.NavigationManager = NavigationManager;
        }

        private string apiurl
        {
            get { return CreateApiUrl(sitestate.Alias, NavigationManager.Uri, "Blog"); }
        }

        public async Task<List<Blog>> GetBlogsAsync(int ModuleId)
        {
            return await http.GetJsonAsync<List<Blog>>(apiurl + "?moduleid=" + ModuleId.ToString() + "&entityid=" + ModuleId.ToString());
        }

        public async Task<Blog> GetBlogAsync(int BlogId, int ModuleId)
        {
            return await http.GetJsonAsync<Blog>(apiurl + "/" + BlogId.ToString() + "?moduleid=" + ModuleId.ToString() + "&entityid=" + ModuleId.ToString());
        }

        public async Task AddBlogAsync(Blog Blog)
        {
            await http.PostJsonAsync(apiurl + "?entityid=" + Blog.ModuleId.ToString(), Blog);
        }

        public async Task UpdateBlogAsync(Blog Blog)
        {
            await http.PutJsonAsync(apiurl + "/" + Blog.BlogId.ToString() + "?entityid=" + Blog.ModuleId.ToString(), Blog);
        }

        public async Task DeleteBlogAsync(int BlogId, int ModuleId)
        {
            await http.DeleteAsync(apiurl + "/" + BlogId.ToString() + "?moduleid=" + ModuleId.ToString() + "&entityid=" + ModuleId.ToString());
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PactumWebService.Data;
using sabatex.PactumContragent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PactumWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PactumController : ControllerBase
    {
        private readonly ILogger<PactumController> _logger;
        private readonly IConfiguration _configuration;
        private readonly PactumDbContext _dbContext;
        public PactumController(ILogger<PactumController> logger, IConfiguration configuration, PactumDbContext dbContext)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = dbContext;
        }
        async Task<string> GetToken()
        {
            var http = new HttpClient();
            var pin = _configuration.GetSection("Packtum")["login"];
            var content = new StringContent($"UserName={pin}&Password={pin}", Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var result = await http.PostAsync("https://pactumsys.com/api/v1/cba2911c-01f9-4ca7-833a-46fb0cc079f2/accounts/authentication", content);
            if (result.IsSuccessStatusCode)
            {
                var rj = await result.Content.ReadFromJsonAsync<object>();
                JsonElement je = (JsonElement)rj;
                if (je.TryGetProperty("access_token", out var a_token))
                {
                    return a_token.GetString();
                }
                else
                {
                    throw new Exception($"Error get property access_token");
                }
            }
            else
            {
                throw new Exception($"Error load token  {result.StatusCode.ToString()}");
            }
        }
        async Task<IEnumerable<ServiceState>> GetState(string token)
        {
            var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent("", Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var result = await http.PostAsync("https://pactumsys.com/api/v1/cba2911c-01f9-4ca7-833a-46fb0cc079f2/accounts/profile", content);
            if (result.IsSuccessStatusCode)
            {
                var rj = await result.Content.ReadFromJsonAsync<object>();
                JsonElement je = (JsonElement)rj;
                if (je.TryGetProperty("Subscriptions", out var Subscriptions))
                {
                    var resultAr = new List<ServiceState>();

                    var sResult = Subscriptions.GetRawText();
                    return System.Text.Json.JsonSerializer.Deserialize<ServiceState[]>(sResult);

                }
                else
                {
                    throw new Exception($"Error get property access_token");
                }
            }
            else
            {
                throw new Exception($"Error load status  {result.StatusCode.ToString()}");
            }
        }
        async Task<string> GetCode(string token, string code)
        {
            var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string queryString = "https://pactumsys.com/api/v1/cba2911c-01f9-4ca7-833a-46fb0cc079f2/contractors/" + code + "?source=1c";
            var result = await http.GetAsync(queryString);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsStringAsync();
                //try 
                //{
                //    var rj = await result.Content.ReadFromJsonAsync<Contragent>();
                //    return rj;
                //}
                //catch(Exception e)
                //{
                //    throw new Exception(e.Message);
                //}

                //JsonElement je = (JsonElement)rj;
                //if (je.TryGetProperty("Subscriptions", out var Subscriptions))
                //{
                //    var resultAr = new List<ServiceState>();

                //    var sResult = Subscriptions.GetRawText();
                //    return "";
                //    //return System.Text.Json.JsonSerializer.Deserialize<ServiceState[]>(sResult);

                //}
                //else
                //{
                //    throw new Exception($"Error get property access_token");
                //}
            }
            else
            {
                throw new Exception($"Error load status  {result.StatusCode.ToString()}");
            }
        }

        [HttpGet]
        public async Task<IEnumerable<ServiceState>> Get()
        {
            string token = await GetToken();
            return await GetState(token);
        }


        [HttpGet]
        [Route("GetContragent")]
        public async Task<ResultQuery<Contragent>> GetContragent(string code, bool isFresh = false)
        {
            var result = new ResultQuery<Contragent> { IsSucces = true };
            string qr = "";

            // search in local database
            var rq = _dbContext.ServiceAnswers.Where(w => w.Code == code.Trim()).OrderByDescending(o => o.Date).FirstOrDefault();
            if (rq != null && !isFresh)
            {
                qr = rq.Answer;
                result.IsCashed = true;
            }
            else
            {
                try
                {
                    string token = await GetToken();
                    int countTry = 20;
                    bool isSucces = false;
                    do
                    {
                        qr = await GetCode(token, code.Trim());
                        var temp = System.Text.Json.JsonSerializer.Deserialize<Contragent>(qr);
                        isSucces = temp.RegisterStates.SingleOrDefault(s => s.Status != "Done") == null;
                        if (isSucces) break;
                        await Task.Delay(500);
                    } while (countTry-- > 0);
                    rq = new ServiceAnswer
                    {
                        Code = code.Trim(),
                        Answer = qr,
                        Date = DateTime.Now,
                        Attempt = countTry
                    };
                    _dbContext.ServiceAnswers.Add(rq);
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    result.Error = e.Message;
                    result.IsSucces = false;
                }
            }
            result.Attempt = rq.Attempt;
            result.Date = rq.Date;
            try
            {
                result.Result = System.Text.Json.JsonSerializer.Deserialize<Contragent>(qr);
            }
            catch (Exception e)
            {
                result.Error = e.Message;
                result.IsSucces = false;
            }
            return result;
        }
        [HttpGet]
        [Route("GetLocalCashe")]
        public async Task<ServiceAnswer[]> GetLocalCashe()
        {
            return await _dbContext.ServiceAnswers.ToArrayAsync();
        }


    }
}

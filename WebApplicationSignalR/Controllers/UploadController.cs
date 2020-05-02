using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationSignalR.Hubs;
using WebApplicationSignalR.Models;

namespace WebApplicationSignalR.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHubContext<BroadCastHub> hubContext;
        public UploadController(IHubContext<BroadCastHub> _hubContext)
        {
            hubContext = _hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> UploadFile()
        {
            try
            {
                // FormDataからSignalRのconnectionIdを取り出す
                if (!Request.Form.TryGetValue("connectionId", out var connectionIdOfSignalR))
                {
                    throw new HubException("SignalRのconnectionIdが渡されていません。"); 
                }
                var myConnectionId = connectionIdOfSignalR[0];
                SendFeedBack(myConnectionId);

                /*
                var counter = 0;
                var currentCount = 0;
                */

                /*
                var data = new List<RecordItem>();
                var postedFile = Request.Form.Files;
                */

                /*
                await Task.Delay(500);
                currentCount++;
                SendFeedBack(currentCount, counter);
                */

                /*
                if (postedFile.Count <= 0 || postedFile == null)
                    return Json(new { error = true, message = "Empty File was uploaded" });

                if (postedFile[0] == null || postedFile[0].Length <= 0)
                {
                    return Json(new { error = true, message = "Empty File was uploaded" });
                }
                */

                /*
                await Task.Delay(500);
                currentCount++;
                SendFeedBack(currentCount, counter);
                */

                /*
                var fileInfo = new FileInfo(postedFile[0].FileName);
                var extention = fileInfo.Extension;
                if (extention.ToLower() != ".csv")
                {
                    return Json(new { error = true, message = "invalid file format" });
                }

                using (StreamReader sr = new StreamReader(postedFile[0].OpenReadStream()))
                {
                    await Task.Delay(500);
                    currentCount++;
                    SendFeedBack(currentCount, counter);

                    while (!sr.EndOfStream)
                    {
                        String Info = sr.ReadLine();
                        String[] Records;
                        if (Info.Contains('\"'))
                        {
                            var row = string.Empty;
                            var model = Info.Replace("\"", "#*").Split('#');
                            foreach (var item in model)
                            {
                                var d = item.Replace("*,", ",");
                                if (d.Contains("*"))
                                {
                                    row += d.Replace("*", "").Replace(",", "");
                                }
                                else
                                {
                                    row += d;
                                }
                            }
                            Records = new String[row.Split(new char[] { ',' }).Length];
                            row.Split(new char[] { ',' }).CopyTo(Records, 0);

                        }
                        else
                        {
                            Records = new String[Info.Split(new char[] { ',' }).Length];
                            Info.Split(new char[] { ',' }).CopyTo(Records, 0);
                        }
                        var strAmount = Records[7].ToString().Trim();
                        decimal output;
                        if (string.IsNullOrEmpty(strAmount) || !decimal.TryParse(strAmount, out output)) continue;



                        var datafile = new RecordItem()
                        {
                            Company = Records[1].ToString().Trim(),
                            Category = Records[3].ToString().Trim(),
                            City = Records[4].ToString().Trim(),
                            Date = Records[6].ToString().Trim(),
                            Currency = Records[8].ToString().Trim(),
                            Amount = decimal.Parse(Records[7].ToString().Trim()),

                        };

                        data.Add(datafile);

                        counter++;
                        SendFeedBack(currentCount, counter);
                    }

                    sr.Close();
                    sr.Dispose();

                    await Task.Delay(500);
                    currentCount++;
                    SendFeedBack(currentCount, counter);

                }
                */

                /*
                await Task.Delay(500);
                currentCount++;
                SendFeedBack(myConnectionId, currentCount, counter);
                */

                //return Json(new { error = false, data = data });
                return Json(new { error = false, data = new { } });

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    error = true,
                    message = ex.InnerException != null ?
                    ex.InnerException.Message : ex.Message
                });
            }
        }

        private async void SendFeedBack(string myConnectionId)
        {
            /*
            var totalCount = 4;
            var feedBackModel = new FeedbackModel()
            {
                currentCount = currentCount,
                currentPercent = (currentCount * 100 / totalCount).ToString(),
                UploadCount = UploadCount,
            };
            */
            //await hubContext.Clients.All.SendAsync("feedBack", feedBackModel);
            await hubContext.Clients.Client(myConnectionId).SendAsync("feedBack", new { connectionId = myConnectionId });
        }

        private async void SendFeedBackAll(string myConnectionId)
        {
            /*
            var totalCount = 4;
            var feedBackModel = new FeedbackModel()
            {
                currentCount = currentCount,
                currentPercent = (currentCount * 100 / totalCount).ToString(),
                UploadCount = UploadCount,
            };
            await hubContext.Clients.All.SendAsync("feedBack", feedBackModel);
            */
            await hubContext.Clients.All.SendAsync("feedBack", new { connectionId = myConnectionId });
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

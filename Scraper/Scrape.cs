using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using CsvHelper;
using System.Globalization;
using System.Reflection;

namespace Scraper
{
    public struct ListData
    {
        public List<string> name { get; set; }
        public List<string> value { get; set; }

        public string Make { get; set; }
    }

    public class FakeList<T> : IEnumerable<T>
    {
        private List<T> listData;

        public FakeList(List<T> _data)
        {
            listData = _data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return listData.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void switchEnumerable(List<T> _enum)
        {
            listData = _enum;
        }

        public void Iterate(Action<T, int> action)
        {
            var counter = 0;
            var enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                action(enumerator.Current, counter);
                counter++;
            }
        }
    }

    public partial class Scrape
    {
        private HtmlDocument doc;
        public ConcurrentBag<DetailDataForGrid> details = new ConcurrentBag<DetailDataForGrid>();
        private string _Html;
        private string _SessionId;
        private List<string> pagesDocuments = new List<string>();
        private Action populateDetailGrid;
        private Action disableUI;
        private Action setStatusUi;
        private string url = "https://www17.asnet2.com/asnet_en/auction/detail";
        public int maxRow = 0;
        public int maxRowNum = 0;
        public int currentRow = 0;
        public Scrape(string sessionId, string html, Action populateGrid, Action DisableUI, Action SetStatusUi)
        {
            this._Html = html;
            var strSessionId = ExtractSession(sessionId);
            this._SessionId = strSessionId;
            populateDetailGrid = populateGrid;
            disableUI = DisableUI;
            setStatusUi = SetStatusUi;
        }
        DetailDataForGrid scrapeDetails(HtmlDocument document)
        {
            DetailDataForGrid detail = new DetailDataForGrid();
            //try
            //{
            var node = doc.GetElementbyId("no_box");
            //detail.LOT_NUM = node.ChildNodes[3].InnerText.Trim().Replace("&nbsp;", "").Replace("\n", "").Replace("b>","").Replace(">", "").Trim();
            detail.LOT_NUM = node.ChildNodes[3]?.ChildNodes[1]?.InnerText?.Trim()?.Replace("&nbsp;", "")?.Replace("\n", "")?.Replace("b>", "")?.Replace(">", "")?.Trim();
            //if (node.ChildNodes[3].ChildNodes[2].InnerText.Contains("Time(s)"))
            //{
            //    detail.LOT_NUM  += " " + node.ChildNodes[3].ChildNodes[2].InnerText.Split(new string[] { "Time(s)" }, StringSplitOptions.None)[0]?.Replace("(", "").Replace("&nbsp;", "").Trim() + "Times(s)";
            //}
            detail.LOT_NUM.Trim();
            node = doc.GetElementbyId("kaijo_box");
            if (node.ChildNodes.Where(w => w.Name == "span").ToList()[0].InnerText.Trim().Contains("AA Site"))
            {
                detail.AUCTION_NUM = node.ChildNodes.Where(w => w.Name == "span").ToList()[1].InnerText.Trim();
            }
            node = doc.GetElementbyId("dt01");

            node.ChildNodes[1]?.ChildNodes.ToList().ForEach((table) =>
            {
                if (table.Name == "td")
                {
                    if (table.Attributes[0].Name == "class")
                    {
                        switch (table.Attributes[0].Value)
                        {
                            case "shamei":
                                {

                                    detail.MODEL_NAME = table.InnerText.Trim();
                                }
                                break;

                            case "grade":
                                {
                                    if (table.InnerText.Contains("★"))
                                        detail.GRADE = table.InnerText.Trim().Split('★')[0];
                                    else if (table.InnerText.Contains("☆"))
                                        detail.GRADE = table.InnerText.Trim().Split('☆')[0];
                                    else
                                        detail.GRADE = table.InnerText.Trim();
                                }
                                break;

                            case "year ct":
                                {
                                    detail.YEAR = table.ChildNodes[0].InnerText.Trim();
                                }
                                break;
                        }
                    }
                }
            });
            node = document.GetElementbyId("dt02");
            node.ChildNodes[1].ChildNodes.ToList().ForEach((table) =>
            {
                if (table.Attributes.Count > 0)
                {
                    if (table.Attributes[0].Name == "class")
                    {
                        switch (table.Attributes[0].Value)
                        {
                            case "cc ct":
                                {
                                    detail.CC = table.InnerText.Trim();
                                }
                                break;
                            case "type":
                                {
                                    //detail.Model = table.NextSibling.NextSibling.InnerText.Trim();
                                    var x = table.InnerHtml.Split('/');
                                    if (x.Length == 2)
                                    {
                                        detail.MODEL = table.InnerHtml.Split('/')[0];
                                        detail.VIN = table.InnerHtml.Split('/')[1];
                                    }
                                    if (x.Length == 1)
                                    {
                                        detail.MODEL = table.InnerHtml.Split('/')[0];
                                    }
                                }
                                break;
                            case "shift ct":
                                {
                                    detail.TRANSMISSION = table.InnerText.Trim();
                                }
                                break;
                            case "ac ct":
                                {
                                    detail.CONDITIONER = table.InnerText.Trim();
                                }
                                break;
                        }
                    }
                }
            });
            node = document.GetElementbyId("dt03");
            node.ChildNodes[1].ChildNodes.ToList().ForEach((table) =>
            {
                if (table.Attributes.Count > 0)
                {
                    if (table.Attributes[0].Name == "class")
                    {
                        switch (table.Attributes[0].Value)
                        {
                            case "shaken ct":
                                {
                                    detail.REGISTRATION_TIME = table.InnerText.Trim();
                                }
                                break;
                            case "kyori ct":
                                {
                                    detail.KM = table.InnerText.Trim().Replace("km", "");
                                }
                                break;
                            case "color":
                                {
                                    detail.COLOR = table.InnerText.Trim();
                                }
                                break;
                        }
                    }
                }
            });
            node = document.GetElementbyId("dt04");
            node.ChildNodes[1].ChildNodes.ToList().ForEach((table) =>
            {
                if (table.Attributes.Count > 0)
                {
                    if (table.Attributes[0].Name == "class")
                    {
                        switch (table.Attributes[0].Value)
                        {
                            case "biko":
                                {
                                    detail.EQUIPMENTS = table.InnerText.Trim();
                                }
                                break;
                                //case "color_no ct":
                                //    {
                                //        detail.ColorNumber = table.InnerText.Trim();
                                //    }
                                //    break;
                        }
                    }
                }
            });

            #region picture fetch
            node = doc.GetElementbyId("photo1");

            detail.PICTURE2_LINK = node != null ? node.Attributes[3].Value : "";
            if (detail.PICTURE2_LINK.Length > 0)
            {
                detail.ACTUAL_PICTURE2_LINK = detail.PICTURE2_LINK;
                var picLink2 = detail.PICTURE2_LINK.Split('/');
                var picName2 = getPicName(picLink2[picLink2.Length - 1]);
                detail.ACTUAL_PICTURE2_NAME = picName2;
                detail.PICTURE2_LINK = $"/auctiondata/AsnetImg/{picName2}";
            }

            node = doc.GetElementbyId("photo2");

            detail.PICTURE3_LINK = node != null ? node.Attributes[3].Value : "";
            if (detail.PICTURE3_LINK.Length > 0)
            {
                detail.ACTUAL_PICTURE3_LINK = detail.PICTURE3_LINK;
                var picLink3 = detail.PICTURE3_LINK.Split('/');
                var picName3 = getPicName(picLink3[picLink3.Length - 1]);
                detail.ACTUAL_PICTURE3_NAME = picName3;
                detail.PICTURE3_LINK = $"/auctiondata/AsnetImg/{ picName3 }";
            }
            node = doc.GetElementbyId("photo3");

            detail.PICTURE4_LINK = node != null ? node.Attributes[3].Value : "";
            if (detail.PICTURE4_LINK.Length > 0)
            {
                detail.ACTUAL_PICTURE4_LINK = detail.PICTURE4_LINK;
                var picLink4 = detail.PICTURE4_LINK.Split('/');
                var picName4 = getPicName(picLink4[picLink4.Length - 1]);
                detail.ACTUAL_PICTURE4_NAME = picName4;
                detail.PICTURE4_LINK = $"/auctiondata/AsnetImg/{ picName4 }";

            }

            node = doc.GetElementbyId("doc");
            if (node.Attributes.Count > 2)
            {
                detail.PICTURE1_LINK = node != null ? node.Attributes[2].Value : "";
                if (detail.PICTURE1_LINK.Length > 0)
                {
                    detail.ACTUAL_PICTURE1_LINK = detail.PICTURE1_LINK;
                    var picLink1 = detail.PICTURE1_LINK.Split('/');
                    var picName1 = getPicName(picLink1[picLink1.Length - 1]);
                    detail.ACTUAL_PICTURE1_NAME = picName1;
                    detail.PICTURE1_LINK = $"/auctiondata/AsnetImg/{picName1}";
                }
            }
            #endregion

            var audion = doc.GetElementbyId("seri_fr")?.ChildNodes;
            if (audion.Count > 0)
            {
                var y = audion.Where(w => w.HasClass("upprRow")).ToList();
                if (y.Count == 1)
                {
                    var z = y[0].ChildNodes.Where(w => w.HasClass("hyoka")).ToList();
                    if (z.Count == 1)
                    {
                        detail.AUDION_GRADE = z[0].InnerText.Trim();
                    }
                }
            }
            detail.START_PRICE = doc.GetElementbyId("st_price")?.ChildNodes[1]?.InnerText;
            detail.START_PRICE = detail.START_PRICE?.Replace("&yen;", "")?.Replace("Price", "")?.Trim();

            detail.FINAL_PRICE = doc.GetElementbyId("last_price")?.InnerText;
            detail.FINAL_PRICE = detail.FINAL_PRICE?.Replace("&yen;", "")?.Replace("Price", "")?.Replace("Final", "")?.Trim();
            if (doc.GetElementbyId("entry")?.ChildNodes?.Count > 0)
            {
                var p = doc.GetElementbyId("entry")?.ChildNodes[1]?.InnerHtml?.Split(' ');
                detail.AUCTION_DATE = p[0].Trim();
                if (p.Length > 1)
                    detail.AUCTION_TIME = p[1].Trim();
            }

            detail.LOT_STATUS = doc.GetElementbyId("aa_seiyaku")?.ChildNodes[1]?.InnerHtml?.Trim();
            if (detail.LOT_STATUS == null)
                detail.LOT_STATUS = doc.GetElementbyId("nagare")?.ChildNodes[1]?.InnerHtml?.Trim();
            if (detail.AUCTION_DATE == null && detail.AUCTION_TIME == null)
            {
                var act_details = doc.GetElementbyId("bidlimit")?.ChildNodes[1]?.InnerText;
                if (act_details?.Length > 0)
                {

                    detail.AUCTION_DATE = act_details?.Split(' ')[0]?.Trim();
                    if (act_details?.Split(' ').Length > 1)
                        detail.AUCTION_TIME = act_details?.Split(' ')[1]?.Trim();
                }
            }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            return detail;
        }
        private string ExtractSession(string sessionId)
        {
            return sessionId.Split(';').Where(w => w.Contains("PHPSESSID")).ToList()[0].Split('=')[1];
        }
        public void Start(bool initial, int i, string html_new = "")
        {

            var currentHtml = this._Html;
            if (html_new != "")
            {
                this._Html = html_new;
                currentHtml = html_new;
            }
            currentRow = i;

            if (this._SessionId.Contains("PHPSESSID"))
            {
                this._SessionId = ExtractSession(this._SessionId);
            }

            var cookieData = $"PHPSESSID={this._SessionId}";
            doc = new HtmlDocument();
            doc.LoadHtml(currentHtml);

            var node = doc.GetElementbyId("form1");


            var tableList = initial == true ? node.ChildNodes[1].ChildNodes[2].ChildNodes.ToList() : node.ChildNodes[1].ChildNodes.ToList();

            var counter = 0;

            var listData = new ListData
            {
                value = new List<string>(),
                name = new List<string>()
            };

            var tableData = new List<ListData>();
            var faketableList = new FakeList<HtmlNode>(tableList.Where(l => l.Name == "input").ToList());
            #region Preparing listData
            faketableList.Iterate((n, index) =>
            {
                n.Attributes.ToList().ForEach((attr) =>
                {
                    if (attr.Name == "name")
                    {
                        listData.name.Add(attr.Value);
                    }
                    else if (attr.Name == "value")
                    {
                        listData.value.Add(attr.Value);
                    }
                });

                if ((index + 1) % 3 == 0)
                {
                    tableData.Add(listData);
                    listData.value = new List<string>();
                    listData.name = new List<string>();
                }
            });
            //try
            //{
            if (initial == true)
            {
                faketableList.switchEnumerable(tableList);
                ListData table;
                faketableList.Iterate((n, index) =>
                {
                    if (n.Name != "#text")
                    {
                        if (n.Name == "tr")
                        {
                            var t = n.ChildNodes.Where(td => td.Name == "td" && td.Attributes[0].Value == "5").ToList();
                            if (t.Count > 0)
                            {
                                table = tableData[counter];
                                table.Make = t[0].InnerText.Split(' ')[0];
                                tableData[counter] = table;
                                counter++;
                            }
                        }
                    }
                });
            }
            else
            {
                ListData x;
                var makeDetails = doc.GetElementbyId("list").ChildNodes.Where(w => w.Name == "tr").Select(s => s.ChildNodes).Skip(1).Where(w => w.Count > 11).ToList().Select(s => s[11].InnerText.Split(' ')[0]).ToList();
                makeDetails.ForEach(f =>
                {
                    x = tableData[counter];
                    x.Make = f;
                    tableData[counter] = x;
                    counter++;
                });
            }
            //}
            //catch
            //{

            //}

            #endregion
            var c = 0;
            Parallel.ForEach(tableData, async (data) =>
            {
                await Task.Run(() => getDetails(url, data, cookieData)).ContinueWith(async (t) =>
                {
                    AddToGrid(await t, c, tableData.Count() - 1, i, initial, data.Make);
                    c++;
                }
                );
            });

            if (initial == true)
            {
                GetAllPages();
            }

        }
        public void AddToGrid(string detailHtml, int c, int length, int c2, bool intial, string make)
        {
            doc.LoadHtml(detailHtml);
            var detailDataForGrid = scrapeDetails(doc);
            detailDataForGrid.MAKE = make;
            details.Add(detailDataForGrid);
            setStatusUi();
            if (c2 == maxRow)
            {
                if (c == length)
                {
                    populateDetailGrid();
                    disableUI();
                }
            }

            if (intial == true)
            {
                if (c == length && maxRow < 2)
                {
                    populateDetailGrid();
                    disableUI();
                }
            }
        }

        #region GetPagination Data
        async Task<string> getDetails(string url, ListData data, string cookie)
        {
            NameValueCollection formData = HttpUtility.ParseQueryString(String.Empty);
            if (data.name != null)
            {
                for (var i = 0; i < data.name.Count; i++)
                {
                    //formData += System.Web.HttpUtility.UrlEncode(data.name[i]) + ":"
                    //    + System.Web.HttpUtility.UrlEncode(data.value[i]) + "\n";
                    formData.Add(data.name[i], data.value[i]);
                }
            }
            string postData = formData.ToString();

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Headers["Cookie"] = cookie;
            //httpRequest.CookieContainer = new CookieContainer();
            //httpRequest.CookieContainer.Add(new Uri("https://www17.asnet2.com"),new Cookie("PHPSESSID",cookie));
            byte[] byteData = Encoding.ASCII.GetBytes(postData);

            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.ContentLength = byteData.Length;
            httpRequest.Timeout = 1 * 60 * 60 * 1000;
            //
            Stream requestStream = await httpRequest.GetRequestStreamAsync();
            requestStream.Write(byteData, 0, byteData.Length);
            requestStream.Close();
            //
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)await httpRequest.GetResponseAsync();

            Stream responseStream = myHttpWebResponse.GetResponseStream();

            StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8);

            string pageContent = await myStreamReader.ReadToEndAsync();

            myStreamReader.Close();
            responseStream.Close();

            myHttpWebResponse.Close();

            return pageContent;
        }
        public async Task GetAllPages()
        {
            doc = new HtmlDocument();
            doc.LoadHtml(this._Html);
            doc.GetElementbyId("srchItems").ChildNodes.ToList().Where(w => w.Name == "input");
            var x = doc.GetElementbyId("srchItems").ChildNodes.ToList().Where(w => w.Name == "input").Select(s => s.Attributes).ToList();
            maxRowNum = Convert.ToInt32(x.Single(w => w.Count > 2 && w[2].Value == "rowNums")[1].Value);
            string currentResponseHtml = "";
            List<Task<string>> liTask = new List<Task<string>>();
            maxRow = (maxRowNum / 50) + 1;
            for (int i = 2; i <= maxRow;)
            //for (int i = 2; i < maxRow; i++)
            {
                if (i == 2)
                {
                    currentResponseHtml = await GetListPages(i, this._Html, false);
                    this._Html = currentResponseHtml;
                    //setStatusUi();
                    Start(false, i, currentResponseHtml);
                }
                else
                {

                    await Task.Run(async () => await GetListPages(i, this._Html, true)).ContinueWith(async (t) =>
                     {
                         var response = await t;
                         //setStatusUi();
                         this._Html = response;
                         Start(false, i, response);
                     });

                }
                i++;
            }
        }
        public async Task<string> GetListPages(int pageNo, string currentHtml, bool isCorrectPage2)
        {
            var doc1 = new HtmlDocument();
            doc1.LoadHtml(currentHtml);
            doc1.GetElementbyId("srchItems").ChildNodes.ToList().Where(w => w.Name == "input");
            var x = doc1.GetElementbyId("srchItems").ChildNodes.ToList().Where(w => w.Name == "input").Select(s => s.Attributes).ToList();
            string ss = $"page={pageNo}";
            NameValueCollection formData = HttpUtility.ParseQueryString(String.Empty);
            if (ss != "page=2" && isCorrectPage2)
            {
                x.Where(w => w.Count > 2).ToList().ForEach(f => { ss += "&" + f[1].Value.ToString() + "=" + f[2].Value.ToString(); });
                x.Where(w => w.Count == 2).ToList().ForEach(f => { ss += "&" + f[1].Value.ToString() + "="; });
            }
            else
            {
                x.Where(w => w.Count > 2).ToList().ForEach(f => { ss += "&" + f[2].Value.ToString() + "=" + f[1].Value.ToString(); });
                x.Where(w => w.Count == 2).ToList().ForEach(f => { ss += "&" + f[1].Value.ToString() + "="; });
            }
            ss = ss.Replace("page=1&", "");
            ss = ss.Replace($"page={pageNo - 1}&", "");
            if (this._SessionId.Contains("PHPSESSID"))
            {
                _SessionId = ExtractSession(_SessionId);
            }
            var cookieData = $"PHPSESSID={this._SessionId}";
            var detailHtml = await getList("https://www17.asnet2.com/asnet_en/auction/list?" + ss, cookieData);
            return detailHtml;
        }
        async Task<string> getList(string url, string cookie)
        {

            // string postData = data;

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Headers["Cookie"] = cookie;
            // byte[] byteData = Encoding.ASCII.GetBytes(postData);

            httpRequest.ContentType = "application/x-www-form-urlencoded";
            //  httpRequest.ContentLength = byteData.Length;
            httpRequest.Timeout = 1 * 60 * 60 * 1000;

            //Stream requestStream = await httpRequest.GetRequestStreamAsync();
            //requestStream.Write(byteData, 0, byteData.Length);
            //requestStream.Close();

            var x = await httpRequest.GetResponseAsync();
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)x;

            Stream responseStream = myHttpWebResponse.GetResponseStream();

            StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8);

            string pageContent = await myStreamReader.ReadToEndAsync();

            myStreamReader.Close();
            responseStream.Close();

            myHttpWebResponse.Close();

            return pageContent;
        }
        #endregion



    }
}

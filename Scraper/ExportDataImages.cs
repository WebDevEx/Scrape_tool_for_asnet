using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scraper
{
    public partial class Scrape
    {
        #region Image Download
        public void ExportToImage(string path)
        {
            var ImagePath = Path.GetFullPath(path);
            if (!Directory.Exists(Path.GetDirectoryName(ImagePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(ImagePath));
            var pictureDetails = details.Select(s => new
            {
                pic1 = s.ACTUAL_PICTURE1_LINK,
                pic1_name = s.ACTUAL_PICTURE1_NAME,
                pic2 = s.ACTUAL_PICTURE2_LINK,
                pic2_name = s.ACTUAL_PICTURE2_NAME,
                pic3 = s.ACTUAL_PICTURE3_LINK,
                pic3_name = s.ACTUAL_PICTURE3_NAME,
                pic4 = s.ACTUAL_PICTURE4_LINK,
                pic4_name = s.ACTUAL_PICTURE4_NAME,

            }).ToList();
            Parallel.ForEach(pictureDetails, (data) =>
           {
               try
               {
                   if (data.pic1?.Trim().Length > 0)
                       DownloadImage(data.pic1, $"{path}\\{ data.pic1_name}");
                   if (data.pic2?.Trim().Length > 0)
                       DownloadImage(data.pic2, $"{path}\\{ data.pic2_name}");
                   if (data.pic3?.Trim().Length > 0)
                       DownloadImage(data.pic3, $"{path}\\{data.pic3_name}");
                   if (data.pic4?.Trim().Length > 0)
                       DownloadImage(data.pic4, $"{path}\\{ data.pic4_name}");
               }
               catch { }
           });
            System.Threading.Thread.Sleep(5000);
            //Parallel.ForEach(pictureDetails,  (data) =>
            //{
            //    if (data.pic1?.Trim().Length > 0)
            //        DownloadImage(data.pic1, $"{path}\\{ data.pic1_name}");
            //    if (data.pic2?.Trim().Length > 0)
            //         DownloadImage(data.pic2, $"{path}\\{ data.pic2_name}");
            //    if (data.pic3?.Trim().Length > 0)
            //         DownloadImage(data.pic3, $"{path}\\{data.pic3_name}");
            //    if (data.pic4?.Trim().Length > 0)
            //         DownloadImage(data.pic4, $"{path}\\{ data.pic4_name}");
            //});
        }
        public void DownloadImage(string imgUrl, string path)
        {
            //using(HttpClient client = new HttpClient())
            //{
            //    var response = await client.GetAsync(imgUrl);
            //    if (response.StatusCode == HttpStatusCode.OK)
            //    {
            //        using (var fs = new FileStream(path, FileMode.CreateNew))
            //        {
            //            await response.Content.CopyToAsync(fs);
            //        }
            //    }
            //}
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(imgUrl, path);
            }
        }

        string getPicName(string picCompleteName)
        {
            string imgName = "";
            if (picCompleteName.Contains("ImgGet?"))
            {
                imgName = Guid.NewGuid().ToString("N") + ".jpg";
            }
            else
            {
                var picLink1 = picCompleteName.Split('/');
                imgName = picLink1[picLink1.Length - 1];
            }
            return imgName;
        }
        #endregion

        #region CSV Download
        public void ExportToCSV(string path)
        {
            path = Path.GetFullPath(path);
            var data = details.Select(s => new DetailDataForCSV
            {
                AUCTION_NUM = s.AUCTION_NUM,
                LOT_NUM = s.LOT_NUM,
                YEAR = s.YEAR,
                MAKE = s.MAKE,
                MODEL_NAME = s.MODEL_NAME,
                GRADE = s.GRADE,
                MODEL = s.MODEL,
                CC = s.CC,
                REGISTRATION_TIME = s.REGISTRATION_TIME,
                KM = s.KM,
                COLOR = s.COLOR,
                TRANSMISSION = s.TRANSMISSION,
                CONDITIONER = s.CONDITIONER,
                AUDION_GRADE = s.AUDION_GRADE,
                EXTERIOR_GRADE = s.EXTERIOR_GRADE,
                INTERIOR_GRADE = s.INTERIOR_GRADE,
                START_PRICE = s.START_PRICE,
                LOT_STATUS = s.LOT_STATUS,
                AUCTION_DATE = s.AUCTION_DATE,
                AUCTION_TIME = s.AUCTION_TIME,
                FINAL_PRICE = s.FINAL_PRICE,
                VIN = s.VIN,
                EQUIPMENTS = s.EQUIPMENTS,
                PICTURE1_LINK = s.PICTURE1_LINK,
                PICTURE2_LINK = s.PICTURE2_LINK,
                PICTURE3_LINK = s.PICTURE3_LINK,
                PICTURE4_LINK = s.PICTURE4_LINK,
            }).ToList();
            CreateCSVFromGenericList(data, path);
        }
        public static void CreateCSVFromGenericList<T>(List<T> list, string csvCompletePath)
        {
            if (list == null || list.Count == 0) return;

            //get type from 0th member
            Type t = list[0].GetType();
            string newLine = Environment.NewLine;

            if (!Directory.Exists(Path.GetDirectoryName(csvCompletePath))) Directory.CreateDirectory(Path.GetDirectoryName(csvCompletePath));

            using (var sw = new StreamWriter(csvCompletePath))
            {
                //make a new instance of the class name we figured out to get its props
                object o = Activator.CreateInstance(t);
                //gets all properties
                PropertyInfo[] props = o.GetType().GetProperties();

                //foreach of the properties in class above, write out properties
                //this is the header row
                sw.Write(string.Join(",", props.Select(d => d.Name).ToArray()) + newLine);

                //this acts as datarow
                foreach (T item in list)
                {
                    //this acts as datacolumn
                    var row = string.Join(",", props.Select(d => $"\"{item.GetType().GetProperty(d.Name).GetValue(item, null)?.ToString()}\"")
                                                            .ToArray());
                    sw.Write(row + newLine);

                }
            }
        }
        #endregion
    }
}

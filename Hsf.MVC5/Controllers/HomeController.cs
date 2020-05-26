using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hsf.MVC5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UploadFile(string folderId)
        {

            try
            {
                var file = Request.Files[0]; //获取选中文件  
                var filecombin = file.FileName.Split('.');
                if (file == null || string.IsNullOrEmpty(file.FileName) || file.ContentLength == 0 || filecombin.Length < 2)
                {
                    return Json(new
                    {
                        fileid = 0,
                        src = "",
                        name = "",
                        msg = "上传出错 请检查文件名 或 文件内容"
                    });
                }


                //获取文件完整文件名(包含绝对路径)
                //文件存放路径格式：/Resource/ResourceFile/{userId}{data}/{guid}.{后缀名}
                string account = "";
                string fileGuid = file.FileName;//Guid.NewGuid().ToString()
                long filesize = file.ContentLength;
                string FileEextension = Path.GetExtension(file.FileName);
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                string virtualPath = string.Format("~/Resource/DocumentFile/{0}/{1}/{2}{3}", account, uploadDate, fileName, FileEextension);
                string fullFileName = this.Server.MapPath(virtualPath);

                var tmpIndex = 0;
                var addFileName = fileName;
                //判断是否存在相同文件名的文件 相同累加1继续判断
                while (System.IO.File.Exists(fullFileName))
                {
                    addFileName = fileName + ++tmpIndex;
                    virtualPath = string.Format("~/Resource/DocumentFile/{0}/{1}/{2}{3}", account, uploadDate, addFileName, FileEextension);
                    fullFileName = this.Server.MapPath(virtualPath);
                }

                //创建文件夹
                string path = Path.GetDirectoryName(fullFileName);
                Directory.CreateDirectory(path);
                if (!System.IO.File.Exists(fullFileName))
                {
                    //保存文件
                    file.SaveAs(fullFileName);
                    ////文件信息写入数据库
                    //FileInfoEntity fileInfoEntity = new FileInfoEntity();
                    //fileInfoEntity.Create();
                    ////fileInfoEntity.FileId = fileGuid;
                    //if (!string.IsNullOrEmpty(folderId))
                    //{
                    //    fileInfoEntity.FolderId = folderId;
                    //}
                    //else
                    //{
                    //    fileInfoEntity.FolderId = "0";
                    //}
                    //fileInfoEntity.FileName = addFileName + FileEextension;
                    //fileInfoEntity.FilePath = virtualPath;
                    //fileInfoEntity.FileSize = filesize.ToString();
                    //fileInfoEntity.FileExtensions = FileEextension;
                    //fileInfoEntity.FileType = FileEextension.Replace(".", "");
                    //fileInfoBLL.SaveForm("", fileInfoEntity);
                }
                return Json(new
                {
                    src = virtualPath.Trim().Replace("~", ""),
                    name = addFileName + FileEextension,   // 获取文件名不含后缀名
                    msg = "上传成功"
                });
            }
            catch { }
            return Json(new
            {
                src = "",
                name = "",   // 获取文件名不含后缀名
                msg = "上传出错"
            });
        }
    }
}
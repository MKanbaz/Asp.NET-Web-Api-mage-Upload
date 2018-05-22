using ImageUpload.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI.WebControls;

namespace ImageUpload.Controllers
{
    [RoutePrefix("api/imgUpload")]
    public class UploadController : ApiController
    {
        ApiDb db = new ApiDb();

        [HttpPost]
        [Route("Upload")]
        [AllowAnonymous]
        [ResponseType(typeof(FileUpload))]
        public async Task<string> Imgmodel()
        {

            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                var httpRequest = HttpContext.Current.Request;

                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];

                        var fileName = postedFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();

                        var filePath = HttpContext.Current.Server.MapPath("~/Content/Upload/" + fileName);


                        postedFile.SaveAs(filePath);


                        Galery glr = new Galery();
                        
                        glr.Url = "/Content/Upload/" + fileName;                       
                        db.Galerys.Add(glr);
                        db.SaveChanges();

                        return "/Content/Upload/" + fileName;
                    }
                }
            }
            catch (Exception exception)
            {
                return exception.Message;
            }

            return "no files";
        }
    }
}

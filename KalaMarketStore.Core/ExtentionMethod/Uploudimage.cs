using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.ExtentionMethod
{
    public static class Uploudimage
    {


        #region CreateImage
        public static string CreateImage(IFormFile file)
        {
            try
            {
                string imgName = GeneratCode.GuidCode() + Path.GetExtension(file.FileName);

                string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Site/assets/images", imgName);

                using (var streem = new FileStream(imgPath, FileMode.Create))
                {
                    file.CopyTo(streem);
                }
                return imgName;

            }
            catch (Exception)
            {

                return "false";
            }

        }

        #endregion



        #region DeleteImage
        public static bool DeleteImage(string path,string imageName)
        {
            try
            {
                string FullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Site/assets/" + path + "/" + imageName);
                if (File.Exists(FullPath))
                {
                    File.Delete(FullPath);
                    return true;
                }
                return false;   
            
            }
            catch (Exception)
            {

                return false;
            }
        }

        #endregion

    }
}

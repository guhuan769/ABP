using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.BlobStoring;

namespace MicroClassroom.BackendAdminApp.Host.Controllers;

public class ImageController : AbpController
{
    private readonly Dictionary<string, string> ContentTypDict = new Dictionary<string, string> {
        {"jpg","image/jpeg"},
        {"jpeg","image/jpeg"},
        {"jpe","image/jpeg"},
        {"png","image/png"},
        {"gif","image/gif"},
        {"ico","image/x-ico"},
        {"tif","image/tiff"},
        {"tiff","image/tiff"},
        {"fax","image/fax"},
        {"wbmp","image//vnd.wap.wbmp"},
        {"rp","image/vnd.rn-realpix"}
    };

    private readonly IBlobContainer<CourseBlobContainer> _blobCourseContainer;
    private readonly IBlobContainer<TeacherBlobContainer> _blobTeacherContainer;
    private readonly IBlobContainer<BannerBlobContainer> _blobBannerContainer;
    private readonly IBlobContainer<MechanismBlobContainer> _blobMechanismContainer;

    public ImageController(IBlobContainer<CourseBlobContainer> blobCourseContainer,
        IBlobContainer<TeacherBlobContainer> blobTeacherContainer,
        IBlobContainer<BannerBlobContainer> blobBannerContainer,
        IBlobContainer<MechanismBlobContainer> blobMechanismContainer)
    {
        _blobMechanismContainer = blobMechanismContainer;
        _blobCourseContainer = blobCourseContainer;
        _blobTeacherContainer = blobTeacherContainer;
        _blobBannerContainer = blobBannerContainer;
    }


    [HttpGet]
    [Route("image/course/{width}/{name}")]
    public async Task<IActionResult> GetCourseImage(int width, string name)
    {
        return await GetImage(_blobCourseContainer, width, name);
    }

    [HttpGet]
    [Route("image/banner/{width}/{name}")]
    public async Task<IActionResult> GetBannerImage(int width, string name)
    {
        return await GetImage(_blobBannerContainer, width, name);
    }

    [HttpGet]
    [Route("image/teacher/{width}/{name}")]
    public async Task<IActionResult> GetTeacherImage(int width, string name)
    {
        return await GetImage(_blobTeacherContainer, width, name);
    }

    [HttpGet]
    [Route("image/mechanism/{width}/{name}")]
    public async Task<IActionResult> GetMechanismImage(int width, string name)
    {
        return await GetImage(_blobMechanismContainer, width, name);
    }

    private async Task<IActionResult> GetImage<TContainer>(IBlobContainer<TContainer> blobContainer,
        int width, string name) where TContainer : class
    {
        if (!CurrentTenant.Id.HasValue)
        {
            throw new UserFriendlyException($"invalid current tenant");
        }

        if (!name.Contains('.'))
        {
            throw new UserFriendlyException($"`{name}` invalid format");
        }

        var extendName = name.Substring(name.LastIndexOf('.') + 1).ToLower();
        if (!ContentTypDict.ContainsKey(extendName))
        {
            throw new UserFriendlyException($"`{extendName}` invalid Content-Type");
        }

        var contentType = ContentTypDict[extendName];
        // original image
        if (width <= 0)
        {
            var bytes = await blobContainer.GetAllBytesAsync(name);
            return new FileContentResult(bytes, contentType);
        }
        else
        {
            // narrow image
            using var ws = await blobContainer.GetAsync(name);
            using var imgBmp = new Bitmap(ws);

            // find new size
            var oWidth = imgBmp.Width;
            var oHeight = imgBmp.Height;
            var height = oHeight;
            if (width > oWidth)
            {
                width = oWidth;
            }
            else
            {
                height = width * oHeight / oWidth;
            }

            var newImg = new Bitmap(imgBmp, width, height);
            newImg.SetResolution(72, 72);

            var ms = new MemoryStream();
            newImg.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            var bytes = ms.GetBuffer();
            ms.Close();
            ws.Close();

            return new FileContentResult(bytes, contentType);
        }
    }
}

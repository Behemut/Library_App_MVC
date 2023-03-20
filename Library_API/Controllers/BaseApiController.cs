using Library_API.Extensions;
using Library_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult HandleResult<T>(ResponseAPI<T> result)
        {
            if (result == null)
            {
                return NotFound();
            }
            if (result.IsSuccess && result.Value != null)
            {
                return Ok(result.Value);
            }
            if (result.IsSuccess && result.Value == null)
            {
                return NotFound();
            }
            else
            {
                return BadRequest(result.Error);

            }
        }


        //protected ActionResult HandlePageResult<T>(ResponseAPI<PagedList<T>> result)
        //{
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    if (result.IsSuccess && result.Value != null)
        //    {
        //        Response.AddPaginationHeader(result.Value.CurrentPage, result.Value.PageSize,
        //                                     result.Value.TotalCount, result.Value.TotalPages);
        //        return Ok(result.Value);

        //    }
        //    if (result.IsSuccess && result.Value == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return BadRequest(result.Error);

        //    }
        //}

    }
}

﻿using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess && result.Value is not null)
                return Ok(result.Value);

            if (result.IsSuccess && result.Value is null)
                return NotFound();

            return BadRequest(result.Error);
        }

        protected ActionResult HandlePagedResult<T>(Result<PagedList<T>> result)
        {
            if (result.IsSuccess && result.Value is not null)
            {
                Response.AddPaginationHeader(result.Value.CurrentPage, result.Value.PageSize, result.Value.TotalCount, result.Value.TotalPages);
                return Ok(result.Value);
            }

            if (result.IsSuccess && result.Value is null)
                return NotFound();

            return BadRequest(result.Error);
        }


        protected ActionResult HandleResult(Result? result)
        {
            if (result is null)
                return NotFound();

            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.Error);
        }
    }
}

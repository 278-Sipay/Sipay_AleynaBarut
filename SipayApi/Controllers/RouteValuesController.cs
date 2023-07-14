using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SipayApi.Controllers;


[ApiController]
[Route("sipy/api/[controller]")]
public class RouteValuesController : ControllerBase
{

    public RouteValuesController()
    {

    }


    [HttpGet("GetByIdQuery")]
    public string GetByIdQuery([FromQuery] int id)
    {
        return "GetByIdQuery";
    }

    [HttpGet("GetByIdRoute/{id}")]
    public string GetByIdRoute(int id)
    {
        return "GetByIdRoute";
    }

    [HttpGet("GetByParamenterQuery")]
    public string GetByParamenterQuery([FromQuery] int? id, [FromQuery] string? name, [FromQuery] string? lastname)
    {
        return $"id={id}||name={name}||lastname={lastname}";
    }

    [HttpGet("GetByParamenterRoute/{id}/{name}/{lastname}")]
    public string GetByParamenterRoute(int? id,string? name,string? lastname)
    {
        return $"id={id}||name={name}||lastname={lastname}";
    }
}

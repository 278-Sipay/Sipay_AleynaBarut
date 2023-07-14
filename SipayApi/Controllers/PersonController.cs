using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SipayApi.Models;
using SipayApi.Validator;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SipayApi.Controllers;


[ApiController]
[Route("sipy/api/[controller]")]

public class PersonController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] Person person)
    {
        var validator = new PersonValidator();
        var result = validator.Validate(person);

        if (result.IsValid)
        {
            return Ok(person);
        }

        var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
        return BadRequest(errorMessages);
    }

}

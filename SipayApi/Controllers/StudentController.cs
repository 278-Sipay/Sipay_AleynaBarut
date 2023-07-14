using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SipayApi.Models;

namespace SipayApi.Controllers;


public class ApiResponse<T>
{
    public T Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }

    public ApiResponse(T Data)
    {
        this.Data = Data;
        this.Success = true;
        this.Message = string.Empty;
    }
}



[ApiController]
[Route("sipy/api/[controller]")]
public class StudentController : ControllerBase
{
    private List<Student> list;
    public StudentController()
    {
        list = new();
        list.Add(new Student { Id = 1, Age = 23, Email = "aleyna@gmail.com", Lastname = "Barut", Name = "Aleyna" });
        list.Add(new Student { Id = 2, Age = 29, Email = "ebru@gmail.com", Lastname = "Kaya", Name = "Ebru" });
        list.Add(new Student { Id = 3, Age = 18, Email = "kubra@gmail.com", Lastname = "Yılmaz", Name = "Kübra" });
    }


    [HttpGet]
    public IActionResult Get()

    {
        try
        {
            return Ok(list);
        }
        catch (Exception ex)
        {
            // Hata durumunda 500 (Internal Server Error) hatası döndürür
            return StatusCode(500, "An error occurred: " + ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var student = list.FirstOrDefault(x => x.Id == id);
        if (student != null)
        {
            return Ok(student);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("ByParameters")]
    public IActionResult Get([FromQuery] string name, [FromQuery] string lastname, [FromQuery] int age)
    {
        List<Student> filteredList = list;

        if (!string.IsNullOrWhiteSpace(name))
        {
            filteredList = filteredList.Where(x => x.Name.ToUpper().Contains(name.ToUpper())).ToList();
        }
        if (!string.IsNullOrWhiteSpace(lastname))
        {
            filteredList = filteredList.Where(x => x.Lastname.ToUpper().Contains(lastname.ToUpper())).ToList();
        }

        return Ok(filteredList);
    }

   

    [HttpPost]
    public IActionResult Post([FromBody] Student student)
    {
        student.Id = list.Count() + 1;
        list.Add(student);
        // Yeni kaynağın URI'si
        var resourceUri = Url.Action("Get", new { id = student.Id });

        // 201 (Created) durum kodunu ve yeni kaynağın URI'sini döndürür
        return Created(resourceUri, list);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Student student)
    {
        var existingStudent = list.FirstOrDefault(x => x.Id == id);
        if (existingStudent != null)
        {
            list.Remove(existingStudent);
            student.Id = id;
            list.Add(student);
        }

        return Ok(list);
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existingStudent = list.FirstOrDefault(x => x.Id == id);
        if (existingStudent != null)
        {
            list.Remove(existingStudent);
            return Ok(list);
        }
        else
        {
            return BadRequest("Student Not Found.");
        }
    }

    // Microsoft.AspNetCore.Mvc.NewtonsoftJson nuget paketi eklendi.Startup.cs ekleme yapıldı.
    // Bir JSON kaynağının belirli bir kısmını değiştirmek (ya da eklemek) istiyorsak, HttpPatch operasyonunu kullanıyoruz.

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, JsonPatchDocument<Student> patchDocument)
    {
        if (patchDocument == null)
        {
            return BadRequest(ModelState);
        }
        var exist = list.FirstOrDefault(x => x.Id == id);
        if (exist == null)
        {
            return NotFound();
        }

        patchDocument.ApplyTo(exist, ModelState);

        if (ModelState.IsValid)
        {
            return Ok(exist);
        }

        return BadRequest(ModelState);
    }
}

using Microsoft.AspNetCore.Mvc;
using N11ProductService;

namespace SipayApi.Controllers;


[ApiController]
[Route("sipy/api/[controller]")]
public class SoapTestController : ControllerBase
{
    public SoapTestController()
    {

    }


    [HttpDelete("{id}")]
    public async void DeleteProductById(int id)
    {
        ProductServicePortClient productService = new ProductServicePortClient();
        var request = new DeleteProductByIdRequest();
        request.auth = new Authentication()
        {
            appKey = "appkey",
            appSecret = "appsecret"
        };
        request.productId = id;
        var response = await productService.DeleteProductByIdAsync(request);
        if (response.DeleteProductByIdResponse.result.errorCode != "0")
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace eCommerceApp.BLL
{
   public class ApiResponse
    {
       
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public object Result { get; set; }
            public ApiResponse(int statusCode, string message, object result)
            {
                StatusCode = statusCode;
                Message = message;
                Result = result;

            }
            //public string SerializeApiResponse(ApiResponse response)
            //{
            //    var options = new JsonSerializerOptions
            //    {
            //        ReferenceHandler = ReferenceHandler.Preserve, // Use ReferenceHandler.Preserve to support cycles
            //                                                      // Other JsonSerializerOptions settings if needed
            //    };

            //    return JsonSerializer.Serialize(response, options);
            //}
        }
   }


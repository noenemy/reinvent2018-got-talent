using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using GotTalent_API.Data;
using GotTalent_API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Amazon.S3;
using Amazon.S3.Transfer;
using GotTalent_API.Utils;
using Amazon.Rekognition;

namespace GotTalent_API.Controllers
{
    // TODO : [Route("api/gameplay/{gameId}/{actionType})]
    [Route("api/[controller]")]
    [ApiController]
    public class GameplayController : ControllerBase
    {
        IAmazonS3 S3Client { get; set; }
        IAmazonRekognition RekognitionClient { get; set; }

        public GameplayController(IAmazonS3 s3Client, IAmazonRekognition rekognitionClient)
        {
            this.S3Client = s3Client;
            this.RekognitionClient = rekognitionClient;
        }

        // Post api/gameplay
        [HttpPost]
        public async Task<IActionResult> PostImage([FromBody] GameplayPostImageDTO dto)
        {
            Console.WriteLine("PostImage entered.");

            string bucketName = "reinvent-gottalent";

            // Retrieving image data
            // ex: game/10/Happiness.jpg
            string keyName = string.Format("game/{0}/{1}.jpg", dto.gameId, dto.actionType);
            byte[] imageByteArray = Convert.FromBase64String(dto.base64Image);
            if (imageByteArray.Length == 0)
                return BadRequest("Image length is 0.");

            // Image manipulation (TODO: resize)


            // Evaluate rekognition metrics
            using (MemoryStream ms = new MemoryStream(imageByteArray))
            {
                // call Rekonition API
                RekognitionUtil.EvaluateEmotionScore(this.RekognitionClient, ms);   

                // Upload image to S3 bucket
                await Task.Run(() => S3Util.UploadToS3(this.S3Client, bucketName, keyName, ms));

                //RekognitionUtil.EvaluateEmotionScore(this.RekognitionClient, bucketName, keyName);
            }

            // Database update


            // Send response

            return Ok(new { filename = keyName });
        }        
    }
}
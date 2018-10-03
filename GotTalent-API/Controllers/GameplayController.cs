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

namespace GotTalent_API.Controllers
{
    // TODO : [Route("api/gameplay/{gameId}/{actionType})]
    [Route("api/[controller]")]
    [ApiController]
    public class GameplayController : ControllerBase
    {
        IAmazonS3 S3Client { get; set; }

        public GameplayController(IAmazonS3 s3Client)
        {
            this.S3Client = s3Client;
        }

        // Post api/gameplay
        [HttpPost]
        public async Task<IActionResult> PostImage([FromBody] GameplayPostImageDTO dto)
        {
            Console.WriteLine("PostImage entered.");

            string bucketName = "reinvent-gottalent";

            // 1. Retrieving image data
            // game/10/Happiness.jpg
            string keyName = string.Format("game/{0}/{1}.jpg", dto.gameId, dto.actionType);
            byte[] imageByteArray = Convert.FromBase64String(dto.base64Image);
            if (imageByteArray.Length == 0)
                return BadRequest("Image length is 0.");

            // 2. Image manipulation


            // 3. Evaluate emotion score


            // 4. Upload image to S3 bucket
            using (MemoryStream ms = new MemoryStream(imageByteArray))
            {
                await Task.Run(() => UploadToS3(bucketName, keyName, ms));
            }

            // 5. Database update

            // 6. Send response

            return Ok(new { filename = keyName });
        }        

        public void UploadToS3(string bucketName, string key, Stream stream)
        {
            Console.WriteLine("UploadToS3 entered." + stream.Length);
            
            try
            {
                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = stream,
                    BucketName = bucketName,
                    CannedACL = S3CannedACL.AuthenticatedRead,
                    Key = key
                };

                // File upload to S3
                TransferUtility fileTransferUtility = new TransferUtility(this.S3Client);
                fileTransferUtility.Upload(uploadRequest);
                Console.WriteLine("Upload completed");
            }
            catch (AmazonS3Exception s3Exception)
            {
                Console.WriteLine(s3Exception.Message, s3Exception.InnerException);
            }
        }

        public Image CreateSampleImage(string message)
        {
            Image image = new Bitmap(2000, 1024);

            Graphics graph = Graphics.FromImage(image);
            graph.Clear(Color.Azure);
            Pen pen = new Pen(Brushes.Black);
            graph.DrawLines(pen, new Point[] { new Point(10,10), new Point(800, 900) });
            graph.DrawString(message, 
                new Font(new FontFamily("DecoType Thuluth"), 20,  FontStyle.Bold), 
                Brushes.Blue, new PointF(150, 90));
            
            return image;
        }
    }
}
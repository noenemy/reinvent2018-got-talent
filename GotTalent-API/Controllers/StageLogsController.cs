using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.S3;
using GotTalent_API.Data;
using GotTalent_API.DTOs;
using GotTalent_API.Models;
using GotTalent_API.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GotTalent_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StageLogsController : ControllerBase
    {
        private DataContext _context;
        IAmazonS3 S3Client { get; set; }
        IAmazonRekognition RekognitionClient { get; set; }        

        public StageLogsController(DataContext context, IAmazonS3 s3Client, IAmazonRekognition rekognitionClient)
        {
            _context = context;
            this.S3Client = s3Client;
            this.RekognitionClient = rekognitionClient;            
        }

        // GET api/stagelogs
        [HttpGet]
        public async Task<IActionResult> GetStageLogs()
        {
            var values = await _context.StageLog.ToListAsync();
            return Ok(values);
        }

        // GET api/stagelogs/5
        [HttpGet("{game_id}")]
        public async Task<IActionResult> GetStageLog(int game_id)
        {
            var values = await _context.StageLog.Where(x => x.game_id == game_id).ToListAsync();
            return Ok(values);
        }

        // POST api/stagelogs
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GameStagePostImageDTO dto)
        {
            Console.WriteLine("PostImage entered.");

            string bucketName = "reinvent-gottalent";

            // Retrieving image data
            // ex: game/10/Happiness.jpg
            string keyName = string.Format("game/{0}/{1}.jpg", dto.gameId, dto.actionType);
            string croppedKeyName = string.Format("game/{0}/{1}_cropped.jpg", dto.gameId, dto.actionType);
            byte[] imageByteArray = Convert.FromBase64String(dto.base64Image);
            if (imageByteArray.Length == 0)
                return BadRequest("Image length is 0.");

            StageLog newStageLog = null;

            using (MemoryStream ms = new MemoryStream(imageByteArray))
            {
                // call Rekonition API
                FaceDetail faceDetail = await RekognitionUtil.GetFaceDetailFromStream(this.RekognitionClient, ms);   

                // Crop image to get face only
                System.Drawing.Image originalImage = System.Drawing.Image.FromStream(ms);
                System.Drawing.Image croppedImage = GetCroppedFaceImage(originalImage, faceDetail.BoundingBox);
                MemoryStream croppedms = new MemoryStream();
                croppedImage.Save(croppedms, ImageFormat.Jpeg);

                // Upload image to S3 bucket
                //await Task.Run(() => S3Util.UploadToS3(this.S3Client, bucketName, keyName, ms));
                await Task.Run(() => S3Util.UploadToS3(this.S3Client, bucketName, keyName, croppedms));

                // Get a specific emotion score
                double emotionScore = 0.0f;
                if (dto.actionType != "Profile")
                {
                    emotionScore = RekognitionUtil.GetEmotionScore(faceDetail.Emotions, dto.actionType);
                }

                int evaluatedAge = (faceDetail.AgeRange.High + faceDetail.AgeRange.Low) / 2;
                string evaluatedGender = faceDetail.Gender.Value;

                // Database update
                newStageLog = new StageLog{
                    game_id = dto.gameId,
                    action_type = dto.actionType,
                    score = emotionScore,
                    file_loc = keyName,
                    age = evaluatedAge,
                    gender = evaluatedGender,
                    log_date = DateTime.Now 
                };

                var value = _context.StageLog.Add(newStageLog);
                await _context.SaveChangesAsync();  
            }

            // Send response
            string signedURL = S3Util.GetPresignedURL(this.S3Client, bucketName, keyName);
            newStageLog.file_loc = signedURL;

            return Ok(newStageLog);            
        }

        public static System.Drawing.Image GetCroppedFaceImage(System.Drawing.Image originalImage, BoundingBox box)
        {
            int left = Convert.ToInt32(originalImage.Width * box.Left);
            int top = Convert.ToInt32(originalImage.Height * box.Top);
            int width = Convert.ToInt32(originalImage.Width * box.Width);
            int height = Convert.ToInt32(originalImage.Height * box.Height);

            Rectangle rect = new Rectangle(left - (width*1/3), top - (height*2/5), width+(width*2/3), height+(height*2/3));
            Bitmap bmp = originalImage as Bitmap;
            Bitmap croppedImage = bmp.Clone(rect, bmp.PixelFormat);

            return croppedImage;
        }

        // PUT api/stagelogs/5
        [HttpPut("{game_id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/stagelogs/5
        [HttpDelete("{game_id}")]
        public void Delete(int id)
        {
        }       

        // POST api/newtest
        [HttpPost("/newtest")]
        public async Task<IActionResult> PostNewTest([FromBody] GameStagePostImageDTO dto)
        {
            Console.WriteLine("PostNewTest entered.");

            string bucketName = "reinvent-gottalent";

            // Retrieving image data
            // ex: game/10/Happiness.jpg
            // string keyName = string.Format("game/{0}/{1}.jpg", dto.gameId, dto.actionType);
            string keyName = string.Format("game/0/drawing.jpg");
            string croppedKeyName = string.Format("game/{0}/{1}_cropped.jpg", dto.gameId, dto.actionType);
            byte[] imageByteArray = Convert.FromBase64String(dto.base64Image);
            if (imageByteArray.Length == 0)
                return BadRequest("Image length is 0.");

            StageLog newStageLog = null;

            using (MemoryStream ms = new MemoryStream(imageByteArray))
            {
                // call Rekonition API
                FaceDetail faceDetail = await RekognitionUtil.GetObjectDetailFromStream(this.RekognitionClient, ms);   

                // Crop image to get face only
                // System.Drawing.Image originalImage = System.Drawing.Image.FromStream(ms);
                // System.Drawing.Image croppedImage = GetCroppedFaceImage(originalImage, faceDetail.BoundingBox);
                // MemoryStream croppedms = new MemoryStream();
                // croppedImage.Save(croppedms, ImageFormat.Jpeg);

                // Upload image to S3 bucket
            //     await Task.Run(() => S3Util.UploadToS3(this.S3Client, bucketName, keyName, croppedms));

            //     // Get a specific emotion score
            //     double emotionScore = 0.0f;
            //     if (dto.actionType != "Profile")
            //     {
            //         emotionScore = RekognitionUtil.GetEmotionScore(faceDetail.Emotions, dto.actionType);
            //     }

            //     int evaluatedAge = (faceDetail.AgeRange.High + faceDetail.AgeRange.Low) / 2;
            //     string evaluatedGender = faceDetail.Gender.Value;

            //     // Database update
            //     newStageLog = new StageLog{
            //         game_id = dto.gameId,
            //         action_type = dto.actionType,
            //         score = emotionScore,
            //         file_loc = keyName,
            //         age = evaluatedAge,
            //         gender = evaluatedGender,
            //         log_date = DateTime.Now 
            //     };

            //     var value = _context.StageLog.Add(newStageLog);
            //     await _context.SaveChangesAsync();  
            }

            // // Send response
            // string signedURL = S3Util.GetPresignedURL(this.S3Client, bucketName, keyName);
            // newStageLog.file_loc = signedURL;

            return Ok(newStageLog);            
        }
 
    }
}